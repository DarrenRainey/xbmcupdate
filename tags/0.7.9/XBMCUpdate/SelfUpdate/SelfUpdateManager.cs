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
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;
using NLog;
using XbmcUpdate.Tools;

namespace XbmcUpdate.SelfUpdate
{
    internal class SelfUpdateManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static readonly string SelfUpdatePath = Application.StartupPath + "\\selfupdate\\";
        private static readonly string SelfUpdateTemp = Settings.TempFolder + "\\selfupdate\\";

        private Version _latestBuild = new Version();
        private string _latestBuildUrl = String.Empty;



        internal bool DownloadUpdate()
        {
            try
            {
                var selfUpdateStopwatch = new Stopwatch();
                selfUpdateStopwatch.Start();

                Logger.Info("Initiating Self-update. Local version:{0}", Settings.ApplicationVersion.ToString());
                SelfUpdateCleanup();

                GetLatestBuildInfo();

                if (_latestBuild > Settings.ApplicationVersion)
                {
                    Logger.Info("New version of XBMCUpdate is available from the server. Ver: {0}", _latestBuild);
                    if (_latestBuild > new Version("0.9.0") && !Campari.Software.FrameworkVersionDetection.IsInstalled(Campari.Software.FrameworkVersion.Fx35))
                    {
                        Logger.Warn("Unable to update to the latest version of XBMC .NET Framework 3.5 is required. Please Update your .NET framework to 3.5 SP1");
                        return false;
                    }

                    PrepUpdate();
                    return true;
                }

                Logger.Info("You have the most recent build of XBMCUpdate. No Update is necessary");
                selfUpdateStopwatch.Stop();
                Logger.Info("Selfupdate preparation took {0}s", selfUpdateStopwatch.Elapsed.TotalSeconds);

                return false;
            }
            catch (Exception e)
            {
                Logger.Error("An error has occurred while checking for Application update.{0}", e.Message);
                throw;
            }
        }


        private void PrepUpdate()
        {
            Logger.Info("Initiating Download.");
            string zipDestination = string.Format("{0}\\xbmcupdate{1}.zip", SelfUpdateTemp, _latestBuild);
            var download = new DownloadManager();
            download.Download(_latestBuildUrl, zipDestination);

            Logger.Info("Extracting update");
            var zipClient = new FastZip();
            zipClient.ExtractZip(zipDestination, SelfUpdatePath, @"+\.exe$;+\.pdb$;+\.dll$;-^nlog\.dll$");
            Logger.Info("Update extracted to {0}", SelfUpdatePath);

            string selfUpdateExe = Path.Combine(SelfUpdatePath, "selfupdate.exe");
            string selfUpdatePdb = Path.Combine(SelfUpdatePath, "selfupdate.pdb");

            if (File.Exists(selfUpdateExe))
            {
                File.Copy(selfUpdateExe, Path.Combine(Application.StartupPath, "selfupdate.exe"), true);
                File.Delete(selfUpdateExe);
            }
            if (File.Exists(selfUpdatePdb))
            {
                File.Copy(selfUpdatePdb, Path.Combine(Application.StartupPath, "selfupdate.pdb"), true);
                File.Delete(selfUpdatePdb);
            }
        }


        private void GetLatestBuildInfo()
        {
            string page = HtmlClient.GetPage(Settings.SelfUpdateUrl);
            new List<Int32>();

            Logger.Info("Trying to parse out the builds list from HTML string");

            MatchCollection matches = Regex.Matches(page, @"http:\/\/.*xbmcupdate_\d\.\d\.\d.zip",
                                                    RegexOptions.IgnoreCase);

            foreach (object buildUrl in matches)
            {
                try
                {
                    string build = Regex.Match(buildUrl.ToString(), @"\d\.\d\.\d", RegexOptions.IgnoreCase).Value;

                    if (!String.IsNullOrEmpty(build))
                    {
                        var thisFile = new Version(build);

                        if (thisFile > _latestBuild)
                        {
                            _latestBuild = thisFile;
                            _latestBuildUrl = buildUrl.ToString();
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Error("An error has occurred while parsing out XBMCUpdate Version. {0}", e.ToString());
                }
            }

            Logger.Info("Latest build available from the server: {0}", _latestBuild.ToString());
        }

        private static void SelfUpdateCleanup()
        {
            Logger.Info("Preforming Selfupdate Cleanup");

            try
            {
                if (Directory.Exists(SelfUpdatePath))
                {
                    Directory.Delete(SelfUpdatePath, true);
                }
            }
            catch (Exception e)
            {
                Logger.Error("An error has occurred while preforming selfupdate cleanup.{0}", e.Message);
            }

            try
            {
                if (Directory.Exists(SelfUpdateTemp))
                {
                    Directory.Delete(SelfUpdateTemp, true);
                }
            }
            catch (Exception e)
            {
                Logger.Error("An error has occurred while preforming selfupdate temp cleanup.{0}", e.Message);
            }

            Directory.CreateDirectory(SelfUpdateTemp);
        }
    }
}