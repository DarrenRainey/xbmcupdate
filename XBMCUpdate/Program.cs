using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NLog;
using System.Diagnostics;
using System.Threading;
using System.Configuration;

namespace XbmcUpdate.Runtime
{
    static class Program
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>


        [STAThread]
        static void Main( string[] args )
        {
            bool tray = false;
            bool silentUpdate = false;

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler( Program_UnhandledException );

            foreach( string arg in args )
            {
                if( arg.Trim( '/', '-', '-' ).ToLower() == "update" )
                {
                    silentUpdate = true;
                }
                if( arg.Trim( '/', '-', '-' ).ToLower() == "tray" )
                {
                    tray = true;
                }
            }

            try
            {
                logger.Info( "{0} v{1} Starting up.", Process.GetCurrentProcess().ProcessName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() );

                if( IsAnotherInstanceRunning() )
                {
                    MessageBox.Show( "Another instance of XBMCUpdate is already running.", "XBMC Update", MessageBoxButtons.OK, MessageBoxIcon.Information );
                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault( false );

                    UpdateGui frmUpdate = new UpdateGui();

                    frmUpdate.SiletUpdate = silentUpdate;
                    frmUpdate.StartInTray = tray;

                    if( tray )
                    {
                        frmUpdate.ShowInTaskbar = false;
                    }

                    //frmUpdate.StartUpdate();

                    Application.Run( frmUpdate );
                }
            }
            catch( System.Exception ex )
            {
                logger.Fatal( "Application Exception on Main.{0}", ex );
                Application.Exit();
            }
        }



        public static bool IsAnotherInstanceRunning()
        {
            bool result = false;

            logger.Info( "Checking for another instance of XBMCUpdate" );

            Process cureentInstance = Process.GetCurrentProcess();
            Process[] allInstances = Process.GetProcessesByName( cureentInstance.ProcessName );

            logger.Info( "Instances of XBMCUpdate Detected: {0}", allInstances.Length );

            if( allInstances.Length > 1 )
            {
                result = true;
            }

            return result;
        }

        static void Program_UnhandledException( object sender, UnhandledExceptionEventArgs e )
        {
            logger.Fatal( "AppDomain Exception. Sender:{0}. Error:{1}", sender, e.ExceptionObject.ToString() );
            Application.Exit();
        }
    }
}
