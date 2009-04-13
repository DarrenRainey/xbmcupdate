using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;

namespace XbmcUpdate.SelfUpdate
{
    class Update
    {

        static Logger logger = LogManager.GetCurrentClassLogger();
        List<string> updateFiles;
        List<FileInfo> updateFileInfo;

        internal Update()
        {
            updateFiles = new List<string>();
            updateFileInfo = new List<FileInfo>();

            foreach( string currentFile in Directory.GetFiles( Program.UpdatePath ) )
            {
                var currentFileInfo = new FileInfo( currentFile );
                updateFiles.Add( currentFileInfo.Name.ToLower() );
                updateFileInfo.Add( currentFileInfo );
            }
        }


        internal void ShutDownApp()
        {
            logger.Info( "Verifying if any of the files that need to be updated are being used by other processes" );
            Process[] allProcesses = Process.GetProcesses();

            foreach( Process currentProcess in allProcesses )
            {
                try
                {
                    if( updateFiles.Contains( currentProcess.MainModule.ModuleName.ToLower() ) )
                    {
                        logger.Info( "Process:{0} is associated with file:{1}. Terminating process.", currentProcess.ProcessName, currentProcess.MainModule.FileName );
                        currentProcess.CloseMainWindow();
                        currentProcess.WaitForExit( 2000 );
                        if( !currentProcess.HasExited )
                        {
                            logger.Warn( "Process {0} failed to exit gracefully. Forcing it to terminate", currentProcess.ProcessName );
                            currentProcess.Kill();
                            currentProcess.WaitForExit( 2000 );
                        }

                        if( !currentProcess.HasExited )
                        {
                            logger.Warn( "Process STILL Running! WHAT THE HELL! - Kill it with FIRE!. {0}", currentProcess.ProcessName );
                        }
                        else
                        {
                            logger.Info( "Process {0} has been terminated successfully", currentProcess.ProcessName );
                        }
                    }
                }
                catch( Win32Exception e )
                {
                    if( e.Message.ToLower() != "access is denied" && e.Message.ToLower() != "unable to enumerate the process modules." )
                    {
                        logger.Error( currentProcess + " " + e.Message );
                    }
                }
                catch( Exception e )
                {
                    logger.Error( currentProcess + " " + e.Message );
                }
            }
        }


        internal void CopyUpdate()
        {
            logger.Info( "Copying update to target directory" );


            foreach( var currentFile in updateFileInfo )
            {
                try
                {
                    File.Copy( currentFile.ToString(), Application.StartupPath + "\\" + currentFile.Name.ToString(), true );
                    logger.Info( "File '{0}' Updated successfully", currentFile.Name );
                }
                catch( Exception e )
                {
                    logger.Fatal( "An error has occurred while copying file {0}. {1}", currentFile, e.ToString() );
                    throw;
                }
            }

            logger.Info( "Total of {0} file(s) have been updated successfully", updateFiles.Count );

        }


        internal void CleanUp()
        {
            logger.Info( "Cleaning Up {0}", Program.UpdatePath );

            try
            {
                Directory.Delete( Program.UpdatePath, true );
            }
            catch( Exception e )
            {
                logger.Error( "An error has occurred while preforming cleanup.{0}", e.ToString() );
            }

        }


    }
}
