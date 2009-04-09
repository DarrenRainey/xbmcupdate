using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using XbmcUpdate.Tools;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Threading;
using XbmcUpdate.Runtime;

namespace XbmcUpdate.Managers
{
    internal delegate void UpdateEventHandler( UpdateManager sender, string message );

    internal class UpdateManager
    {
        static Logger logger = LogManager.GetCurrentClassLogger();
        FastZip zipClient = new FastZip();
        string compressedBuildPath;
        string uncompressedBuildPath;

        public event UpdateEventHandler OnCheckUpdateStart;
        public event UpdateEventHandler OnCheckUpdateStop;

        public event UpdateEventHandler OnDownloadStart;
        public event UpdateEventHandler OnDownloadStop;

        public event UpdateEventHandler OnUnZipStart;
        public event UpdateEventHandler OnUnZipStop;

        public event UpdateEventHandler OnInstallStart;
        public event UpdateEventHandler OnInstallStop;


        public event UpdateEventHandler OnUpdateError;

        private Thread updateThread;


        public UpdateManager()
        {
            Directory.CreateDirectory( Settings.TempFolder );
            logger.Info( "Creating Temporary folder at: {0}", Settings.TempFolder );
        }


        private DownloadManager _downloadManager;
        public DownloadManager Download
        {
            get
            {
                return _downloadManager;
            }
        }

        int onlineBuildNumber;
        public int OnlineBuildNumber
        {
            get
            {
                return onlineBuildNumber;
            }
        }

        int currentBuildNumber;
        public int CurrentBuildNumber
        {
            get
            {
                return currentBuildNumber;
            }
        }


        public bool CheckUpdate()
        {

            bool updateAvilable = false;

            if( OnCheckUpdateStart != null )
            {
                OnCheckUpdateStart( this, "Looking for updates." );
            }

            try
            {
                //Detecting local Build
                currentBuildNumber = XbmcManager.GerVersion().BuildNumber;
                //Getting the latest revision number.
                var revlist = ReleaseManager.GetBuildList();

                if( revlist != null && revlist.Count != 0 )
                {
                    revlist.Sort();
                    onlineBuildNumber = revlist[revlist.Count - 1];

                    logger.Info( "Latest available build:{0}. Currently installed:{1}", onlineBuildNumber, currentBuildNumber );

                    if( onlineBuildNumber <= currentBuildNumber )
                    {
                        logger.Info( "No updates is necessary." );
                    }

                    updateAvilable = currentBuildNumber < onlineBuildNumber;
                }


                if( OnCheckUpdateStop != null )
                {
                    if( updateAvilable )
                    {
                        OnCheckUpdateStop( this, "Latest Available Build : " + onlineBuildNumber );
                    }
                    else
                    {
                        OnCheckUpdateStop( this, "No update is necessary. Build Installed: " + CurrentBuildNumber );
                    }
                }
            }
            catch( Exception e )
            {
                logger.FatalException( "An Error has occurred while checking for updates.", e );
                if( OnUpdateError != null )
                {
                    OnUpdateError( this, "An Error has occurred while checking for updates." );
                }
            }

            return updateAvilable;
        }

        public void ExctractBuild()
        {
            if( OnUnZipStart != null )
            {
                OnUnZipStart( this, "Extracting Build..." );
            }

            try
            {

                string unZipPath = compressedBuildPath.Replace( ".zip", @"\" );

                try
                {
                    if( Directory.Exists( unZipPath ) )
                    {
                        logger.Info( "Trying to delete previous extracted copy." );
                        Directory.Delete( unZipPath, true );
                    }
                }
                catch( Exception e )
                {
                    logger.Warn( "Unable to delete old extracted files. {0}", e.ToString() );
                }

                Directory.CreateDirectory( unZipPath );

                uncompressedBuildPath = String.Concat( unZipPath, @"\xbmc\" );
                logger.Info( "Extracting Build {0} to {1}", onlineBuildNumber, uncompressedBuildPath );

                zipClient.ExtractZip( compressedBuildPath, unZipPath, "" );
                logger.Info( "All files extracted successfully" );



                if( OnUnZipStop != null )
                {
                    OnUnZipStop( this, "All Files Extracted Successfully" );
                }
            }
            catch( Exception e )
            {
                logger.FatalException( "An error has occurred while extracting build", e );
                if( OnUpdateError != null )
                    OnUpdateError( this, "An error has occurred while extracting build" + e.Message );
            }
        }

        public void ApplyUpdate()
        {
            try
            {
                //Download or verify that we have the compressed version of the latest build
                GetBuild( false );

                ExctractBuild();

                InstallBuild();

                logger.Info( "Successfully updated to build {0}", OnlineBuildNumber );
            }
            catch( Exception e )
            {
                logger.FatalException( "An error has occurred during update", e );
                if( OnUpdateError != null )
                    OnUpdateError( this, "An error has occurred during update." + e.Message );
            }
        }

        private void InstallBuild()
        {
            if( OnInstallStart != null )
            {
                OnInstallStart( this, "Killing XBMC" );
            }

            try
            {
                XbmcManager.StopXbmc();

                //Sleeping for 1 seconds. just to make sure all file locks are released
                Thread.Sleep( 1000 );



                if( OnInstallStart != null )
                {
                    OnInstallStart( this, "Installing Build..." );
                }

                CopyFolder( uncompressedBuildPath, Settings.XbmcPath );

                //Register Build
                VersionInfo verInfo = new VersionInfo();
                verInfo.BuildNumber = onlineBuildNumber;
                verInfo.InstallationDate = DateTime.Now;

                XbmcManager.SaveVersion( verInfo );

                CleanTemp();

                if( OnInstallStop != null )
                {
                    OnInstallStop( this, "Successfully Installed Build " + onlineBuildNumber );
                }
            }
            catch( Exception e )
            {
                logger.FatalException( "An error has occurred while installing update", e );
                if( OnUpdateError != null )
                    OnUpdateError( this, "An error has occurred while installing update." + e.Message );

            }

        }

        private void CleanTemp()
        {
            logger.Info( "Cleaning Temp folder." );

            var tempSubfolders = Directory.GetDirectories( Settings.TempFolder );

            foreach( var folder in tempSubfolders )
            {
                try
                {
                    DeleteFolder( folder );
                }
                catch( Exception e )
                {
                    logger.Info( "Unable to delete '{0}'. {1}", folder, e.Message );
                }
            }
        }


        private void DeleteFolder( string path )
        {
            foreach( var file in Directory.GetFiles( path ) )
            {
                try
                {
                    File.Delete( file );
                }
                catch( Exception e )
                {
                    logger.Error( "Failed to delete file '{0}'. {1}", file, e.Message );
                }
            }
            string[] subDir = Directory.GetDirectories( path );

            foreach( var folder in subDir )
            {
                try
                {
                    DeleteFolder( folder );

                }
                catch( Exception e )
                {
                    logger.Error( "Failed to delete folder '{0}'. {1}", folder, e.Message );
                }
            }

            Directory.Delete( path, true );
        }

        public void InstallUpdatesAsync()
        {
            updateThread = new Thread( ApplyUpdate );
            updateThread.Start();
        }

        public void Abort()
        {
            if( updateThread != null )
            {
                if( updateThread.IsAlive )
                {
                    updateThread.Abort();
                }
            }
        }

        internal void GetBuild( bool forced )
        {
            if( OnDownloadStart != null )
            {
                OnDownloadStart( this, "Downloading build " + onlineBuildNumber + "..." );
            }

            try
            {

                string buildUrl = ReleaseManager.GetBuildUrl( onlineBuildNumber );
                compressedBuildPath = string.Concat( Settings.TempFolder, @"\XBMC-", onlineBuildNumber, ".zip" );

                //If not forced check to see if the file has already been downloaded
                if( !forced && File.Exists( compressedBuildPath ) )
                {
                    //Check the size of the file against server size
                    FileInfo localFileInfo = new FileInfo( compressedBuildPath );
                    if( DownloadManager.GetFileSize( buildUrl ) == localFileInfo.Length )
                    {
                        logger.Info( "File '{0}' with the matching file size exists. skipping download.", localFileInfo.Name );

                        if( OnDownloadStop != null )
                        {
                            OnDownloadStop( this, "Already Downloaded Skipping." );
                        }

                        return;
                    }

                    logger.Info( "Partial file detected. Re-Downloading file." );

                }

                logger.Info( "Downloading build {0} from the server.", onlineBuildNumber );

                _downloadManager = new DownloadManager();
                _downloadManager.Download( buildUrl, compressedBuildPath );

                if( OnDownloadStop != null )
                {
                    OnDownloadStop( this, String.Format( "Build {0} Installed", onlineBuildNumber ) );
                }
            }
            catch( Exception e )
            {
                logger.FatalException( "An error has occurred while downloading the latest build", e );
                if( OnUpdateError != null )
                    OnUpdateError( this, "An error has occurred while downloading the latest build" + e.Message );
            }
        }


        internal void CopyFolder( string source, string destination )
        {
            destination += @"\";

            logger.Info( "Copying folder '{0}'", source );

            if( !Directory.Exists( destination ) )
                Directory.CreateDirectory( destination );

            foreach( var subDirectory in Directory.GetDirectories( source ) )
            {
                if( !subDirectory.Contains( "userdata" ) )
                {
                    CopyFolder( subDirectory, subDirectory.Replace( source, destination ) );
                }
            }

            foreach( var file in Directory.GetFiles( source ) )
            {
                FileInfo currentFile = new FileInfo( file );

                string newFile = ( String.Concat( destination, currentFile.Name ) );
                File.Copy( currentFile.FullName, newFile, true );
            }
        }



    }
}
