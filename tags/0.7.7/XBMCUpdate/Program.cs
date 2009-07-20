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
using System.Windows.Forms;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace XbmcUpdate
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static string _arguments = "";

        private static Logger _logger;
        private static bool _silentUpdate;
        private static bool _tray;

        internal static string Arguments
        {
            get { return _arguments.Trim(); }
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
                catch (Exception)
                {
                }

                var config = new LoggingConfiguration();

                var fileTargt = new FileTarget();
                fileTargt.Layout = "${longdate}-${callsite}|${level}|${message} ${exception:format=ToString}";
                fileTargt.FileName = "${basedir}\\logs\\${processname:lowerCase=true}.${date:format=yyyy-MM-dd}.log";

                var rule1 = new LoggingRule("*", LogLevel.Trace, fileTargt);

                config.AddTarget("guiTarget", fileTargt);
                config.LoggingRules.Add(rule1);

                LogManager.Configuration = config;
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("Fatal Logger Error.{0}{1}", Environment.NewLine, e), "XBMCUpdate Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        [STAThread]
        private static void Main(string[] args)
        {
            SetupNlog();
            _logger = LogManager.GetCurrentClassLogger();

            AppDomain.CurrentDomain.UnhandledException += ProgramUnhandledException;

            foreach (string arg in args)
            {
                if (arg.Trim('/', '-', '-').ToLower() == "update")
                {
                    _silentUpdate = true;

                    _arguments += "//update ";
                }
                if (arg.Trim('/', '-', '-').ToLower() == "tray")
                {
                    _tray = true;
                    _arguments += "//tray ";
                }
            }

            try
            {
                _logger.Info("************************************************************************");
                _logger.Info("{0} v{1} Starting up", Process.GetCurrentProcess().ProcessName,
                            Settings.ApplicationVersion.ToString());
                _logger.Info("Framework version installed: {0}", Campari.Software.FrameworkVersionDetection.LatestVersion);
                _logger.Info("************************************************************************");

                if (IsAnotherInstanceRunning())
                {
                    MessageBox.Show("Another instance of XBMCUpdate is already running", "XBMCUpdate",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    var frmUpdate = new UpdateGui();

                    frmUpdate.SilentUpdate = _silentUpdate;
                    frmUpdate.StartInTray = _tray;

                    if (_tray)
                    {
                        frmUpdate.ShowInTaskbar = false;
                    }

                    //frmUpdate.StartUpdate();

                    Application.Run(frmUpdate);
                }
            }
            catch (Exception ex)
            {
                _logger.Fatal("Application Exception on Main.{0}", ex);
                if (!_silentUpdate)
                {
                    MessageBox.Show(
                        String.Format("{0}{1}Please notify the developer.", ex.Message, Environment.NewLine),
                        "XBMCUpdate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Application.Exit();
            }
        }


        static bool IsAnotherInstanceRunning()
        {
            bool result = false;

            _logger.Info("Checking for another instance of XBMCUpdate");

            Process cureentInstance = Process.GetCurrentProcess();
            Process[] allInstances = Process.GetProcessesByName(cureentInstance.ProcessName);

            _logger.Info("Instances of XBMCUpdate Detected: {0}", allInstances.Length);

            if (allInstances.Length > 1)
            {
                result = true;
            }

            return result;
        }

        private static void ProgramUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _logger.Fatal("AppDomain Exception. Sender:{0}. Error:{1}", sender, e.ExceptionObject.ToString());
            if (!_silentUpdate)
            {
                MessageBox.Show(
                    String.Format("{0}{1}Please notify the developer.", e.ExceptionObject, Environment.NewLine),
                    "XBMCUpdate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Application.Exit();
        }
    }
}