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
using System.Text;
using System.Configuration;
using NLog;
using System.Windows.Forms;

namespace XbmcUpdate.Runtime
{
    class Settings
    {

        static Configuration config = ConfigurationManager.OpenExeConfiguration( ConfigurationUserLevel.None );
        static Logger logger = LogManager.GetCurrentClassLogger();

        private static void UpdateValue( string key, object value )
        {
            logger.Trace( "Writing Setting to file. Key:'{0}' Value:'{1}'", key, value );
            config.AppSettings.Settings.Remove( key );
            config.AppSettings.Settings.Add( key, value.ToString() );
            config.Save();
        }

        private static string GetConfigValue( string Key, object Default, bool makePermanent )
        {
            string value = null;

            if( config.AppSettings.Settings[Key] != null )
            {
                value = config.AppSettings.Settings[Key].Value;
            }
            else
            {
                logger.Warn( "Unable to find config key '{0}' default:'{1}'", Key, Default );
                if( makePermanent )
                {
                    UpdateValue( Key, Default.ToString() );
                }
                value = Default.ToString();
            }

            return value;
        }

        internal static string XbmcPath
        {
            get
            {
                return GetConfigValue( "XbmcPath", "", true );
            }
            set
            {
                UpdateValue( "XbmcPath", value );
            }
        }

        internal static string TempFolder
        {
            get
            {
                return String.Concat( Application.StartupPath, @"\temp\" );
            }
        }

        internal static string ReleaseUrl
        {
            get
            {
                return GetConfigValue( "ReleaseUrl", @"http://danielpatton.com/user-accounts/XBMC-updates/", true );
            }
            set
            {
                UpdateValue( "ReleaseUrl", value );
            }
        }

        internal static string SelfUpdateUrl
        {
            get
            {
                return GetConfigValue( "SelfUpdateUrl", @"http://code.google.com/p/xbmcupdate/downloads", false );
            }

        }

        internal static int ShutdownCountdown
        {
            get
            {
                return 5;
            }
        }

        internal static string XbmcExe
        {
            get
            {
                return "xbmc.exe";
            }
        }

        internal static Version ApplicationVersion
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        internal static Int32 XbmcAutostart
        {
            get
            {
                return Convert.ToInt32( GetConfigValue( "XbmcAutostart", 0, true ) );
            }
            set
            {
                UpdateValue( "XbmcAutostart", value );
            }
        }

        internal static bool XbmcAutoShutdown
        {
            get
            {
                return Convert.ToBoolean( GetConfigValue( "XbmcAutoShutdown", true, true ) );
            }
            set
            {
                UpdateValue( "XbmcAutoShutdown", value );
            }
        }

        internal static string XbmcStartupArgs
        {
            get
            {
                return GetConfigValue( "XbmcStartupArgs", "", true );
            }
            set
            {
                UpdateValue( "XbmcStartupArgs", value );
            }
        }

    }
}
