using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace XbmcUpdate.SelfUpdate
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public static readonly string UpdatePath = Application.StartupPath + "\\selfupdate\\";
        public static string[] Arguments = null;

        private static string arguments = "";

        public static Logger logger;


        [STAThread]
        static void Main( string[] arg )
        {
            Arguments = arg;

            try
            {
                foreach( var item in arg )
                {
                    arguments = string.Format( "{0} {1} ", arguments, item );
                }


                SetupNlog();

                logger.Info( "************************************************************************" );
                logger.Info( "{0} v{1} Starting up", Process.GetCurrentProcess().ProcessName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() );
                logger.Info( "************************************************************************" );

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault( false );

                logger.Info( "Checking '{0}' for pending updates", UpdatePath );

                if( Directory.Exists( UpdatePath ) && Directory.GetFiles( UpdatePath ).Length != 0 )
                {
                    logger.Info( "Pending Update found. Initiating update sequence" );
                    Application.Run( new frmSelfUpdate() );
                }
                else
                {
                    logger.Info( "No pending updates found." );

                }
            }
            catch( Exception e )
            {
                logger.Fatal( "Fatal Error on main. {0}", e.ToString() );
            }


            StartXbmcUpdate();

        }

        internal static void StartXbmcUpdate()
        {
            try
            {
                logger.Info( "Preparing to launch XBMCUpdate" );

                ProcessStartInfo pStart = new ProcessStartInfo();
                pStart.Arguments = arguments.Trim();
                pStart.FileName = "xbmcupdate.exe";

                var xbmcUpdate = Process.Start( pStart );

                logger.Info( "XBMCUpdate launched at: {0}", xbmcUpdate.StartTime );
            }
            catch( Exception e )
            {
                logger.Fatal( "An error has occurred while starting XBMCUpdate. {0}", e.ToString() );
            }
            finally
            {
                Application.Exit();
            }
        }

        private static void SetupNlog()
        {
            try
            {
                LoggingConfiguration config = new LoggingConfiguration();

                FileTarget fileTargt = new FileTarget();
                fileTargt.Layout = "${longdate}-${callsite}|${level}|${message} ${exception:format=ToString}";
                fileTargt.FileName = "${basedir}\\logs\\${processname:lowerCase=true}.${date:format=yyyy-MM-dd}.log";
                LoggingRule rule1 = new LoggingRule( "*", LogLevel.Trace, fileTargt );

                config.AddTarget( "guiTarget", fileTargt );
                config.LoggingRules.Add( rule1 );

                LogManager.Configuration = config;

                logger = LogManager.GetCurrentClassLogger();
            }
            catch( Exception e )
            {
                System.Windows.Forms.MessageBox.Show( String.Format( "Fatal Logger Error.{0}{1}", Environment.NewLine, e.ToString() ), "SelfUpdate Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                Application.Exit();
            }
        }
    }
}
