/*
 *   XBMCUpdate: Automatic Update Client for XBMC. (www.xbmc.org)
 * 
 *   Copyright (C) 2009  Keivan Beigi
 * 
 *   This program is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either xbmcVersion 3 of the License, or
 *   (at your option) any later xbmcVersion.
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
using System.Diagnostics;
using System.IO;
using NLog;
using XbmcUpdate.Tools;

namespace XbmcUpdate.UpdateEngine
{
    internal static class XbmcManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private const string VersionFile = "update.xml";
        internal static bool XbmcTerminated;

        //Return path to program files folder.
        private static string ProgramFilesPath
        {
            get
            {
                if (8 == IntPtr.Size
                    || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
                {
                    return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
                }
                return Environment.GetEnvironmentVariable("ProgramFiles");
            }
        }

        internal static void SaveVersion(XbmcVersionInfo xbmcVersion)
        {
            Logger.Info("Updating your installation status");
            string fileContent = Serilizer.SerializeObject(xbmcVersion);
            Serilizer.WriteToFile(string.Format(@"{0}\{1}", Settings.XbmcPath, VersionFile), fileContent, false);
        }

        internal static XbmcVersionInfo GerVersion()
        {
            var installedVersion = new XbmcVersionInfo();

            try
            {
                string versionFilePath = string.Format(@"{0}\{1}", Settings.XbmcPath, VersionFile);

                if (File.Exists(versionFilePath))
                {
                    string fileContent = Serilizer.ReadFile(versionFilePath);
                    installedVersion = Serilizer.DeserializeObject<XbmcVersionInfo>(fileContent);

                    if (installedVersion != null)
                    {
                        Logger.Info("Rev:{0}, Installation Date:{1}, Supplier:{2}", installedVersion.BuildNumber,
                                    installedVersion.InstallationDate, installedVersion.Suplier);
                    }
                    // if deserilization failes installedVersion will be set to null,
                    // this will reset the installed version to version 0.0
                    else
                    {
                        installedVersion = new XbmcVersionInfo();
                    }
                }
                else
                {
                    Logger.Info("No xbmcVersion files were found. The latest revision will be installed on next update");
                }
            }
            catch (Exception e)
            {
                Logger.Fatal("An error has occurred while getting installed xbmcVersion info. {0}", e.ToString());
                installedVersion = new XbmcVersionInfo();
            }

            return installedVersion;
        }


        internal static void StopXbmc()
        {
            Logger.Info("Closing all instances of XBMC");
            Process[] xbmcProcesses = Process.GetProcessesByName("xbmc");

            foreach (Process item in xbmcProcesses)
            {
                //logger.Info("An instance of xbmc was found. processId:{0}", item.)
                item.Kill();
                XbmcTerminated = true;
            }
        }


        internal static bool IsXbmcRunning()
        {
            bool result;
            Logger.Info("Verifying if xbmc is running");
            Process[] xbmcProcesses = Process.GetProcessesByName("xbmc");

            if (xbmcProcesses.Length != 0)
            {
                result = true;
                Logger.Info("An Instance of XBMC was detected.");
            }
            else
            {
                result = false;
                Logger.Info("No Instances of XBMC were detected.");
            }

            return result;
        }

        internal static void StartXbmc()
        {
            string xbmcStartFileName = string.Format("{0}\\{1}", Settings.XbmcPath, Settings.XbmcExe);

            Logger.Info("Attempting to start XBMC '{0} {1}'", xbmcStartFileName, Settings.XbmcStartupArgs);

            try
            {
                var xbmcStartInfo = new ProcessStartInfo
                                        {
                                            FileName = xbmcStartFileName,
                                            Arguments = Settings.XbmcStartupArgs
                                        };

                var xbmcProcess = new Process {StartInfo = xbmcStartInfo};

                xbmcProcess.Start();
                Logger.Info("XBMC Started Successfully");
            }
            catch (Exception e)
            {
                Logger.Error("An error has occurred while trying to start XBMC. {0}", e.Message);
            }
        }
    }

}