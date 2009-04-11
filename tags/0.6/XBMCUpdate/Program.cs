using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NLog;
using System.Diagnostics;
using System.Threading;
using System.Configuration;
using NLog.Config;
using NLog.Targets;
using System.IO;

namespace XbmcUpdate.Runtime
{
    static class Program
    {
        static Logger logger;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>


        private static void SetupNlog()
        {
            try
            {
                try
                {

                    if( File.Exists( "nlog.config" ) )
                    {
                        File.Delete( "nlog.config" );
                    }

                }
                catch
                {

                }

                LoggingConfiguration config = new LoggingConfiguration();

                FileTarget fileTargt = new FileTarget();
                fileTargt.Layout = "${longdate}-${callsite}|${level}|${message} ${exception:format=ToString}";
                fileTargt.FileName = "${basedir}\\logs\\${date:format=m}.log";



                LoggingRule rule1 = new LoggingRule( "*", LogLevel.Trace, fileTargt );

                config.AddTarget( "guiTarget", fileTargt );
                config.LoggingRules.Add( rule1 );

                LogManager.Configuration = config;
            }
            catch( Exception e )
            {
                System.Windows.Forms.MessageBox.Show( String.Format( "Fatal Logger Error.{0}{1}", Environment.NewLine, e.ToString() ), "XBMCUpdate Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                Application.Exit();
            }
        }



        [STAThread]
        static void Main( string[] args )
        {
            SetupNlog();
            logger = LogManager.GetCurrentClassLogger();

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
