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
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using NLog;

namespace XbmcUpdate
{
    internal static class Settings
    {
        private static readonly Configuration Config =
            ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();


        static Settings()
        {
            CleanUp();
        }

        internal static string XbmcPath
        {
            get { return GetConfigValue("XbmcPath", "", true); }
            set { UpdateValue("XbmcPath", value); }
        }

        internal static string TempFolder
        {
            get { return String.Concat(Application.StartupPath, @"\temp\"); }
        }

        internal static string SourceName
        {
            get { return GetConfigValue("SourceName", @"", true); }
            set { UpdateValue("SourceName", value); }
        }

        internal static string SelfUpdateUrl
        {
            get { return GetConfigValue("SelfUpdateUrl", @"http://code.google.com/p/xbmcupdate/downloads", false); }
        }

        internal static string SourceManifest
        {
            get { return GetConfigValue("Manifest", Path.Combine(Application.StartupPath, @"sources.xml"), false); }
        }

        internal static int ShutdownCountdown
        {
            get { return 5; }
        }

        internal static string XbmcExe
        {
            get { return "xbmc.exe"; }
        }

        internal static Version ApplicationVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version; }
        }

        internal static Int32 XbmcAutostart
        {
            get { return Convert.ToInt32(GetConfigValue("XbmcAutostart", 0, true)); }
            set { UpdateValue("XbmcAutostart", value); }
        }

        internal static bool XbmcAutoShutdown
        {
            get { return Convert.ToBoolean(GetConfigValue("XbmcAutoShutdown", true, true)); }
            set { UpdateValue("XbmcAutoShutdown", value); }
        }

        internal static bool PreventStandBy
        {
            get { return Convert.ToBoolean(GetConfigValue("PreventStandBy", true, true)); }
            set { UpdateValue("PreventStandBy", value); }
        }

        internal static string XbmcStartupArgs
        {
            get { return GetConfigValue("XbmcStartupArgs", "", true); }
            set { UpdateValue("XbmcStartupArgs", value); }
        }

        private static void UpdateValue(string key, object value)
        {
            Logger.Trace("Writing Setting to file. Key:'{0}' Value:'{1}'", key, value);
            Config.AppSettings.Settings.Remove(key);
            Config.AppSettings.Settings.Add(key, value.ToString());
            Config.Save();
        }

        private static string GetConfigValue(string key, object defaultValue, bool makePermanent)
        {
            string value;

            if (Config.AppSettings.Settings[key] != null)
            {
                value = Config.AppSettings.Settings[key].Value;
            }
            else
            {
                Logger.Warn("Unable to find config key '{0}' defaultValue:'{1}'", key, defaultValue);
                if (makePermanent)
                {
                    UpdateValue(key, defaultValue.ToString());
                }
                value = defaultValue.ToString();
            }

            return value;
        }


        private static void CleanUp()
        {
            Config.AppSettings.Settings.Remove("ReleaseUrl");
        }
    }
}