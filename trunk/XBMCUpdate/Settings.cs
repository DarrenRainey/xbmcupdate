﻿using System;
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

        private static void UpdateValue( string key, string value )
        {
            logger.Trace( "Writing Setting to file. Key:'{0}' Value:'{1}'", key, value );
            config.AppSettings.Settings.Remove( key );
            config.AppSettings.Settings.Add( key, value );
            config.Save();
        }

        private static string GetConfigValue( string Key, string Default, bool makePermanent )
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
                    UpdateValue( Key, Default );
                }
                value = Default;
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


        internal static Version ApplicationVersion
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

    }
}
