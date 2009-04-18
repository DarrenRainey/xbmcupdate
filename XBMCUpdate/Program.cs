using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace XbmcUpdate.Runtime
{
    static class Program
    {
        static Logger logger;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>


        private static string arguments = "";
        internal static string Arguments
        {
            get
            {
                return arguments.Trim();
            }
        }

        private static void SetupNlog()
        {
            try
            {
                try
                {

                    if (File.Exists("nlog.config"))
                    {
                        File.Delete("nlog.config");
                    }
                }
                catch
                {
                }

                LoggingConfiguration config = new LoggingConfiguration();

                FileTarget fileTargt = new FileTarget();
                fileTargt.Layout = "${longdate}-${callsite}|${level}|${message} ${exception:format=ToString}";
                fileTargt.FileName = "${basedir}\\logs\\${processname:lowerCase=true}.${date:format=yyyy-MM-dd}.log";

                LoggingRule rule1 = new LoggingRule("*", LogLevel.Trace, fileTargt);

                config.AddTarget("guiTarget", fileTargt);
                config.LoggingRules.Add(rule1);

                LogManager.Configuration = config;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("Fatal Logger Error.{0}{1}", Environment.NewLine, e.ToString()), "XBMCUpdate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        static bool tray = false;
        static bool silentUpdate = false;

        [STAThread]
        static void Main(string[] args)
        {
            SetupNlog();
            logger = LogManager.GetCurrentClassLogger();

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program_UnhandledException);

            foreach (string arg in args)
            {
                if (arg.Trim('/', '-', '-').ToLower() == "update")
                {
                    silentUpdate = true;

                    arguments += "//update ";
                }
                if (arg.Trim('/', '-', '-').ToLower() == "tray")
                {
                    tray = true;
                    arguments += "//tray ";
                }
            }

            try
            {
                logger.Info("************************************************************************");
                logger.Info("{0} v{1} Starting up", Process.GetCurrentProcess().ProcessName, Settings.ApplicationVersion.ToString());
                logger.Info("************************************************************************");

                if (IsAnotherInstanceRunning())
                {
                    MessageBox.Show("Another instance of XBMCUpdate is already running", "XBMCUpdate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    UpdateGui frmUpdate = new UpdateGui();

                    frmUpdate.SilentUpdate = silentUpdate;
                    frmUpdate.StartInTray = tray;

                    if (tray)
                    {
                        frmUpdate.ShowInTaskbar = false;
                    }

                    //frmUpdate.StartUpdate();

                    Application.Run(frmUpdate);
                }
            }
            catch (System.Exception ex)
            {
                logger.Fatal("Application Exception on Main.{0}", ex);
                if (!silentUpdate)
                {
                    System.Windows.Forms.MessageBox.Show(String.Format("{0}{1}Please notify the developer.", ex.Message, Environment.NewLine), "XBMCUpdate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Application.Exit();
            }
        }



        internal static bool IsAnotherInstanceRunning()
        {
            bool result = false;

            logger.Info("Checking for another instance of XBMCUpdate");

            Process cureentInstance = Process.GetCurrentProcess();
            Process[] allInstances = Process.GetProcessesByName(cureentInstance.ProcessName);

            logger.Info("Instances of XBMCUpdate Detected: {0}", allInstances.Length);

            if (allInstances.Length > 1)
            {
                result = true;
            }

            return result;
        }

        static void Program_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            logger.Fatal("AppDomain Exception. Sender:{0}. Error:{1}", sender, e.ExceptionObject.ToString());
            if (!silentUpdate)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("{0}{1}Please notify the developer.", e.ExceptionObject.ToString(), Environment.NewLine), "XBMCUpdate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Application.Exit();
        }
    }
}
