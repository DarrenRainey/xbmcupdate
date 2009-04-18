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
using XbmcUpdate.Runtime;
using XbmcUpdate.Tools;


namespace XbmcUpdate.Managers
{
    class XbmcManager
    {
        private static readonly string VERSION_FILE = "update.xml";
        internal static bool XbmcTerminated = false;

        static Logger logger = LogManager.GetCurrentClassLogger();
        //Return path to program files folder.
        private string ProgramFilesPath
        {
            get
            {
                if( 8 == IntPtr.Size
                    || ( !String.IsNullOrEmpty( Environment.GetEnvironmentVariable( "PROCESSOR_ARCHITEW6432" ) ) ) )
                {
                    return Environment.GetEnvironmentVariable( "ProgramFiles(x86)" );
                }
                return Environment.GetEnvironmentVariable( "ProgramFiles" );
            }
        }

        internal static void SaveVersion( VersionInfo version )
        {
            logger.Info( "Updating your installation status" );
            string fileContent = Serilizer.SerializeObject<VersionInfo>( version );
            Serilizer.WriteToFile( string.Format( @"{0}\{1}", Settings.XbmcPath, VERSION_FILE ), fileContent, false );
        }

        internal static VersionInfo GerVersion()
        {

            VersionInfo installedVersion = new VersionInfo();

            try
            {
                string versionFilePath = string.Format( @"{0}\{1}", Settings.XbmcPath, VERSION_FILE );

                if( File.Exists( versionFilePath ) )
                {
                    string fileContent = Serilizer.ReadFile( versionFilePath );
                    installedVersion = Serilizer.DeserializeObject( fileContent );
                    logger.Info( "Rev:{0}, Installation Date:{1}, Supplier:{2}", installedVersion.BuildNumber, installedVersion.InstallationDate, installedVersion.Suplier );
                }
                else
                {
                    logger.Info( "No version files were found. The latest revision will be installed on next update" );
                }
            }
            catch( Exception e )
            {
                logger.Fatal( "An error has occurred while getting installed version info. {0}", e.ToString() );
            }

            return installedVersion;
        }


        internal static void StopXbmc()
        {
            logger.Info( "Closing all instances of XBMC" );
            var xbmcProcesses = Process.GetProcessesByName( "xbmc" );

            foreach( Process item in xbmcProcesses )
            {
                //logger.Info("An instance of xbmc was found. processId:{0}", item.)
                item.Kill();
                XbmcTerminated = true;
            }
        }


        internal static bool IsXbmcRunning()
        {
            bool result = false;
            logger.Info( "Verifying if xbmc is running" );
            var xbmcProcesses = Process.GetProcessesByName( "xbmc" );

            if( xbmcProcesses.Length != 0 )
            {
                result = true;
                logger.Info( "An Instance of XBMC was detected." );
            }
            else
            {
                result = false;
                logger.Info( "No Instances of XBMC were detected." );
            }

            return result;
        }

        internal static void StartXbmc()
        {
            string xbmcStartFileName = string.Format( "{0}\\{1}", Settings.XbmcPath, Settings.XbmcExe );

            logger.Info( "Attempting to start XBMC '{0} {1}'", xbmcStartFileName, Settings.XbmcStartupArgs );

            try
            {
                ProcessStartInfo xbmcStartInfo = new ProcessStartInfo();
                xbmcStartInfo.FileName = xbmcStartFileName;
                xbmcStartInfo.Arguments = Settings.XbmcStartupArgs;

                Process xbmcProcess = new Process();
                xbmcProcess.StartInfo = xbmcStartInfo;

                xbmcProcess.Start();
                logger.Info( "XBMC Started Successfully" );
            }
            catch( Exception e )
            {
                logger.Error( "An error has occurred while trying to start XBMC. {0}", e.Message );
            }
        }
    }

    public class VersionInfo
    {

        public int BuildNumber
        {
            get;
            set;
        }
        public string Suplier
        {
            get;
            set;
        }
        public DateTime InstallationDate
        {
            get;
            set;
        }
    }
}
