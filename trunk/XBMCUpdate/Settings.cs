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

        private static void UpdateValue( string key, string value )
        {
            config.AppSettings.Settings.Remove( key );
            config.AppSettings.Settings.Add( key, value );
            config.Save();
        }

        private static string GetConfigValue( string Key )
        {
            string value = null;

            if( config.AppSettings.Settings[Key] != null )
            {
                value = config.AppSettings.Settings[Key].Value;
            }
            else
            {
                logger.Warn( "Unable to find config key '{0}'", Key );
            }

            return value;
        }

        internal static string XbmcPath
        {
            get
            {
                return GetConfigValue( "XbmcPath" );
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
                return GetConfigValue( "ReleaseUrl" );
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
                return @"http://code.google.com/p/xbmcupdate/downloads";
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
