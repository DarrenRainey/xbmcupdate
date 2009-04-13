using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using NLog;
using System.Xml.Serialization;
using XbmcUpdate.Tools;
using XbmcUpdate.Runtime;
using System.Diagnostics;


namespace XbmcUpdate.Managers
{
    class XbmcManager
    {
        private static readonly string VERSION_FILE = "update.xml";

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
            logger.Info( "Verifying if xbmc is running" );
            var xbmcProcesses = Process.GetProcessesByName( "xbmc" );

            foreach( Process item in xbmcProcesses )
            {
                //logger.Info("An instance of xbmc was found. processId:{0}", item.)
                item.Kill();
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
