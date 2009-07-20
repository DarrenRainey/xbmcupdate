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
        internal static bool _xbmcTerminated;

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

        internal static void SaveVersion(VersionInfo version)
        {
            Logger.Info("Updating your installation status");
            string fileContent = Serilizer.SerializeObject(version);
            Serilizer.WriteToFile(string.Format(@"{0}\{1}", Settings.XbmcPath, VersionFile), fileContent, false);
        }

        internal static VersionInfo GerVersion()
        {
            var installedVersion = new VersionInfo();

            try
            {
                string versionFilePath = string.Format(@"{0}\{1}", Settings.XbmcPath, VersionFile);

                if (File.Exists(versionFilePath))
                {
                    string fileContent = Serilizer.ReadFile(versionFilePath);
                    installedVersion = Serilizer.DeserializeObject(fileContent);
                    Logger.Info("Rev:{0}, Installation Date:{1}, Supplier:{2}", installedVersion.BuildNumber,
                                installedVersion.InstallationDate, installedVersion.Suplier);
                }
                else
                {
                    Logger.Info("No version files were found. The latest revision will be installed on next update");
                }
            }
            catch (Exception e)
            {
                Logger.Fatal("An error has occurred while getting installed version info. {0}", e.ToString());
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
                _xbmcTerminated = true;
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
                var xbmcStartInfo = new ProcessStartInfo();
                xbmcStartInfo.FileName = xbmcStartFileName;
                xbmcStartInfo.Arguments = Settings.XbmcStartupArgs;

                var xbmcProcess = new Process();
                xbmcProcess.StartInfo = xbmcStartInfo;

                xbmcProcess.Start();
                Logger.Info("XBMC Started Successfully");
            }
            catch (Exception e)
            {
                Logger.Error("An error has occurred while trying to start XBMC. {0}", e.Message);
            }
        }
    }

    public class VersionInfo
    {
        public int BuildNumber { get; set; }
        public string Suplier { get;  set; }
        public DateTime InstallationDate { get; set; }

        public string Age()
        {
            var ts = new TimeSpan(DateTime.UtcNow.Ticks - InstallationDate.Ticks);
            double delta = ts.TotalSeconds;

            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            if (delta < 1 * MINUTE)
            {
                return ts.Seconds == 1 ? "One Second ago" : ts.Seconds + " Seconds ago";
            }
            if (delta < 2 * MINUTE)
            {
                return "a Minute ago";
            }
            if (delta < 45 * MINUTE)
            {
                return ts.Minutes + " Minutes ago";
            }
            if (delta < 90 * MINUTE)
            {
                return "an Hour ago";
            }
            if (delta < 24 * HOUR)
            {
                return ts.Hours + " Hours ago";
            }
            if (delta < 48 * HOUR)
            {
                return "Yesterday";
            }
            if (delta < 30 * DAY)
            {
                return ts.Days + " Days ago";
            }
            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "One Month ago" : months + " Months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "One year ago" : years + " Years ago";
            }

        }
    }
}