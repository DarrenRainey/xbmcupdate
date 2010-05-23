/*
 *   XBMCUpdate: Automatic Update Client for XBMC. (www.xbmc.org)
 * 
 *   Copyright (C) 2009  Keivan Beigi
 * 
 *   This program is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.
 *
 *   This program is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.
 *
 *   You should have received a copy of the GNU General Public License
 *   along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ICSharpCode.SharpZipLib.Zip;
using NLog;
using XbmcUpdate;
using XbmcUpdate.Tools;

namespace XbmcUpdate.UpdateEngine
{
    internal delegate void UpdateEventHandler(UpdateManager sender, string message);

    internal class UpdateManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly DownloadManager _downloadManager;
        private readonly FastZip _zipClient = new FastZip();
        private string _compressedBuildPath;
        private int _currentBuildNumber;
        private string _uncompressedBuildPath;
        private Thread _updateThread;


        internal UpdateManager()
        {
            _downloadManager = new DownloadManager();

            Directory.CreateDirectory(Settings.TempFolder);
            Logger.Info("Creating temporary folder at: {0}", Settings.TempFolder);
        }

        internal DownloadManager Download
        {
            get { return _downloadManager; }
        }

        internal int OnlineBuildNumber { get; private set; }

        internal event UpdateEventHandler OnCheckUpdateStart;
        internal event UpdateEventHandler OnCheckUpdateStop;

        internal event UpdateEventHandler OnDownloadStart;
        internal event UpdateEventHandler OnDownloadStop;

        internal event UpdateEventHandler OnUnZipStart;
        internal event UpdateEventHandler OnUnZipStop;

        internal event UpdateEventHandler OnInstallStart;
        internal event UpdateEventHandler OnInstallStop;


        internal event UpdateEventHandler OnUpdateError;

        internal event UpdateEventHandler OnUpdateProcessStart;
        internal event UpdateEventHandler OnUpdateProcessStop;


        internal bool CheckUpdate()
        {
            bool updateAvilable = false;

            if (OnCheckUpdateStart != null)
            {
                OnCheckUpdateStart(this, "Looking for updates");
            }

            try
            {
                //Detecting local Build
                _currentBuildNumber = XbmcManager.GerVersion().BuildNumber;
                //Getting the latest revision number.
                List<int> revlist = ReleaseManager.GetBuildList();

                string message ="You have the most recent version. No update is necessary.";

                if (revlist != null && revlist.Count != 0)
                {
                    revlist.Sort();
                    OnlineBuildNumber = revlist[revlist.Count - 1];

                    Logger.Info("Latest available build:{0}. Currently installed:{1}", OnlineBuildNumber,
                                _currentBuildNumber);

                    if (OnlineBuildNumber <= _currentBuildNumber)
                    {
                        Logger.Info("No updates is necessary");
                    }

                    updateAvilable = _currentBuildNumber < OnlineBuildNumber;

                    if (updateAvilable)
                    {
                        message = "Latest Available Rev. " + OnlineBuildNumber;
                    }
                }
                else
                {
                    message = "Could not find any release. Try another update source.";
                }
                               

                if (OnCheckUpdateStop != null)
                {
                    OnCheckUpdateStop(this, message);
                }
            }
            catch (Exception e)
            {
                Logger.FatalException("An Error has occurred while checking for updates", e);
                if (OnUpdateError != null)
                {
                    OnUpdateError(this, "An Error has occurred while checking for updates");
                }
            }

            return updateAvilable;
        }

        internal void InstallUpdatesAsync()
        {
            _updateThread = new Thread(ApplyUpdate);
            _updateThread.Start();
        }

        internal void Abort()
        {
            if (_updateThread != null)
            {
                if (_updateThread.IsAlive)
                {
                    _updateThread.Abort();
                }
            }
        }

        private void ApplyUpdate()
        {
            try
            {
                if (OnUpdateProcessStart != null)
                    OnUpdateProcessStart(this, "Starting update process for Rev. " + OnlineBuildNumber);

                if (Settings.PreventStandBy)
                {
                    PowerManager.PreventStandBy();
                }

                if (Settings.XbmcAutoShutdown || !XbmcManager.IsXbmcRunning())
                {
                    //Download or verify that we have the compressed version of the latest build
                    DownloadBuild(false);

                    ExctractBuild();

                    InstallBuild();

                    Logger.Info("Successfully updated to build {0}", OnlineBuildNumber);

                    if (OnUpdateProcessStop != null)
                        OnUpdateProcessStop(this, "Update Successfully Completed");
                }
                else
                {
                    Logger.Info("An instance of XBMC is detected. Skipping update.");
                    if (OnUpdateProcessStop != null)
                        OnUpdateProcessStop(this, "XBMC is running. Unable to proceed with update.");
                }
            }
            catch (Exception e)
            {
                Logger.FatalException("An error has occurred during update", e);
                if (OnUpdateError != null)
                    OnUpdateError(this, "An error has occurred during update" + e.Message);

                if (OnUpdateProcessStop != null)
                    OnUpdateProcessStop(this, "An error has occurred during update" + e.Message);
            }
            finally
            {
                PowerManager.Resume();
            }
        }

        private void DownloadBuild(bool forced)
        {
            if (OnDownloadStart != null)
            {
                OnDownloadStart(this, "Downloading Rev. " + OnlineBuildNumber + "...");
            }

            try
            {
                string buildUrl = ReleaseManager.GetBuildUrl(OnlineBuildNumber);
                _compressedBuildPath = string.Concat(Settings.TempFolder, @"\XBMC-", OnlineBuildNumber, ".zip");

                //If not forced check to see if the file has already been downloaded
                if (!forced && File.Exists(_compressedBuildPath))
                {
                    //Check the size of the file against server size
                    var localFileInfo = new FileInfo(_compressedBuildPath);
                    if (DownloadManager.GetFileSize(buildUrl) == localFileInfo.Length)
                    {
                        Logger.Info("File '{0}' with the matching file size exists. skipping download",
                                    localFileInfo.Name);

                        if (OnDownloadStop != null)
                        {
                            OnDownloadStop(this, "Already Downloaded Skipping");
                        }

                        return;
                    }

                    Logger.Info("Partial file detected. Re-Downloading file");
                }

                Logger.Info("Downloading build {0} from the server", OnlineBuildNumber);


                _downloadManager.Download(buildUrl, _compressedBuildPath);

                if (OnDownloadStop != null)
                {
                    OnDownloadStop(this, String.Format("Rev. {0} Installed", OnlineBuildNumber));
                }
            }
            catch (Exception e)
            {
                Logger.FatalException("An error has occurred while downloading the latest build", e);
                throw;
            }
        }


        private void ExctractBuild()
        {
            if (OnUnZipStart != null)
            {
                OnUnZipStart(this, "Extracting Update..");
            }

            try
            {
                string unZipPath = _compressedBuildPath.Replace(".zip", @"\");

                try
                {
                    if (Directory.Exists(unZipPath))
                    {
                        Logger.Info("Trying to delete previous extracted copy");
                        Directory.Delete(unZipPath, true);
                    }
                }
                catch (Exception e)
                {
                    Logger.Warn("Unable to delete old extracted files. {0}", e.ToString());
                }

                Directory.CreateDirectory(unZipPath);

                _uncompressedBuildPath = String.Concat(unZipPath, @"\xbmc\");
                Logger.Info("Extracting Update {0} to {1}", OnlineBuildNumber, _uncompressedBuildPath);

                _zipClient.ExtractZip(_compressedBuildPath, unZipPath, "");
                Logger.Info("All files extracted successfully");


                if (OnUnZipStop != null)
                {
                    OnUnZipStop(this, "All Files Extracted Successfully");
                }
            }
            catch (Exception e)
            {
                Logger.FatalException("An error has occurred while extracting build", e);
                throw;
            }
        }

        private void InstallBuild()
        {
            if (OnInstallStart != null)
            {
                OnInstallStart(this, "Killing XBMC");
            }

            try
            {
                XbmcManager.StopXbmc();

                //Sleeping for 1 seconds. just to make sure all file locks are released
                Thread.Sleep(1000);


                if (OnInstallStart != null)
                {
                    OnInstallStart(this, "Installing Update...");
                }

                CopyFolder(_uncompressedBuildPath, Settings.XbmcPath);

                //Register Build
                var verInfo = new XbmcVersionInfo {BuildNumber = OnlineBuildNumber, InstallationDate = DateTime.Now};

                XbmcManager.SaveVersion(verInfo);

                CleanTemp();

                if (OnInstallStop != null)
                {
                    OnInstallStop(this, "Successfully Installed XBMC " + OnlineBuildNumber);
                }
            }
            catch (Exception e)
            {
                Logger.FatalException("An error has occurred while installing update", e);
                throw;
            }
        }

        private static void CleanTemp()
        {
            Logger.Info("Cleaning Temp folder");

            string[] tempSubfolders = Directory.GetDirectories(Settings.TempFolder);

            foreach (string folder in tempSubfolders)
            {
                try
                {
                    DeleteFolder(folder);
                }
                catch (Exception e)
                {
                    Logger.Info("Unable to delete '{0}'. {1}", folder, e.Message);
                }
            }
        }


        private static void CopyFolder(string source, string destination)
        {
            destination += @"\";

            Logger.Info("Copying folder '{0}'", source);

            if (!Directory.Exists(destination))
                Directory.CreateDirectory(destination);

            foreach (string subDirectory in Directory.GetDirectories(source))
            {
                if (!subDirectory.Contains("userdata"))
                {
                    CopyFolder(subDirectory, subDirectory.Replace(source, destination));
                }
            }

            foreach (var file in Directory.GetFiles(source))
            {
                if (!file.ToLower().Contains("keymap.xml"))
                {
                    var currentFile = new FileInfo(file);
                    string newFile = (String.Concat(destination, currentFile.Name));
                    File.Copy(currentFile.FullName, newFile, true);
                }
                else
                {
                    Logger.Warn("Skipping file {0}", file);
                }
            }
        }


        private static void DeleteFolder(string path)
        {
            foreach (string file in Directory.GetFiles(path))
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception e)
                {
                    Logger.Error("Failed to delete file '{0}'. {1}", file, e.Message);
                }
            }
            string[] subDir = Directory.GetDirectories(path);

            foreach (string folder in subDir)
            {
                try
                {
                    DeleteFolder(folder);
                }
                catch (Exception e)
                {
                    Logger.Error("Failed to delete folder '{0}'. {1}", folder, e.Message);
                }
            }

            Directory.Delete(path, true);
        }
    }
}