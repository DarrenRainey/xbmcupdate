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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using NLog;
using XbmcUpdate.Properties;
using XbmcUpdate.UpdateEngine;
using XbmcUpdate.UpdateEngine.Source;

namespace XbmcUpdate
{
    internal partial class UpdateGui
    {
        private bool _updateInProgress;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private ContextMenuStrip _mContextMenu;
        private ToolStripMenuItem _mDisplayForm;
        private ToolStripMenuItem _mExitApplication;
        private NotifyIcon _mNotifyIcon;
        private ToolStripMenuItem _mUpdate;
        private readonly UpdateManager _update = new UpdateManager();

        internal UpdateGui()
        {
            InitializeComponent();


            _update.OnCheckUpdateStart += update_OnCheckUpdateStart;
            _update.OnCheckUpdateStop += update_OnCheckUpdateStop;

            _update.OnDownloadStart += update_OnDownloadStart;
            _update.OnDownloadStop += update_OnDownloadStop;

            _update.OnUnZipStart += update_OnUnZipStart;
            _update.OnUnZipStop += update_OnUnZipStop;

            _update.OnInstallStart += update_OnInstallStart;
            _update.OnInstallStop += update_OnInstallStop;

            _update.OnUpdateProcessStart += update_OnUpdateProcessStart;
            _update.OnUpdateProcessStop += update_OnUpdateProcessStop;

            _update.OnUpdateError += update_OnUpdateError;

            UpdateVersionStat();
            UpdateBindedUi();

            Text = String.Concat("XBMCUpdate ", Settings.ApplicationVersion.ToString());
        }

        internal bool SilentUpdate { get; set; }

        internal bool StartInTray { get; set; }

        private bool UpdateInProgress
        {
            get { return _updateInProgress; }
            set
            {
                btnCheckUpdate.Enabled = !value;
                btnBrows.Enabled = !value;

                if (_mUpdate != null)
                {
                    _mUpdate.Enabled = !value;
                }

                _updateInProgress = value;
            }
        }


        private void InitTray()
        {
            //Instantiate the NotifyIcon attaching it to the components container and 
            //provide it an icon, note, you can embed this resource 
            _mNotifyIcon = new NotifyIcon(components);
            _mNotifyIcon.Icon = Resources.app;
            _mNotifyIcon.Text = "XBMCUpdate";
            _mNotifyIcon.Visible = true;

            //Instantiate the context menu and items
            _mContextMenu = new ContextMenuStrip();
            _mDisplayForm = new ToolStripMenuItem();
            _mExitApplication = new ToolStripMenuItem();
            _mUpdate = new ToolStripMenuItem();

            //Attach the menu to the notify icon
            _mNotifyIcon.ContextMenuStrip = _mContextMenu;


            //Setup the items and add them to the menu strip, adding handlers to be created later

            _mUpdate.Text = "Update XBMC";
            _mUpdate.Click += mUpdate_Click;
            _mContextMenu.Items.Add(_mUpdate);

            _mDisplayForm.Text = "Show XBMCUpdate";
            _mDisplayForm.Click += mDisplayForm_Click;
            _mContextMenu.Items.Add(_mDisplayForm);

            _mExitApplication.Text = "Exit";
            _mExitApplication.Click += mExitApplication_Click;
            _mContextMenu.Items.Add(_mExitApplication);
            _mNotifyIcon.DoubleClick += mNotifyIcon_DoubleClick;


            components.Add(_mNotifyIcon);
        }

        private void UpdateBindedUi()
        {
            //if (String.IsNullOrEmpty(txtReleaseUrl.Text.Trim()))
            {
                //  txtReleaseUrl.Text = Settings.ReleaseUrl;
            }

            txtXbmcPath.Text = Settings.XbmcPath;

            cmbXbmcStart.SelectedIndex = Convert.ToInt32(Settings.XbmcAutostart);
            chkUpdateIfXbmcIsRunning.Checked = Settings.XbmcAutoShutdown;
            txtXbmcStartArgs.Text = Settings.XbmcStartupArgs;
            chkPreventStandby.Checked = Settings.PreventStandBy;



            cmbSources.Items.Clear();

            foreach (var source in SourceManager.SourceManifest.Sources)
            {
                cmbSources.Items.Insert(source.Index, source.SourceName);
            }

            cmbSources.SelectedIndex = SourceManager.GetSource().Index;

        }

        private void UpdateVersionStat()
        {
            try
            {
                XbmcVersionInfo currentBuild = XbmcManager.GerVersion();

                if (currentBuild.BuildNumber != 0)
                {
                    lblXbmcVersion.Text = String.Format("Current Rev.{0}{1}Installed: {2}", currentBuild.BuildNumber,
                                                                  Environment.NewLine, currentBuild.Age());
                }
                else
                {
                    lblXbmcVersion.Text = ("Unknown local version. Rev." + _update.OnlineBuildNumber + " will be installed.");
                }
            }
            catch (Exception e)
            {
                _logger.Fatal("An error has occurred while generating update stat string.{0}", e.Message);
            }
        }

        private bool ChangeXbmcFolder()
        {
            bool result = false;

            xbmcFolderDialog.ShowDialog(this);

            if (!String.IsNullOrEmpty(xbmcFolderDialog.SelectedPath))
            {
                txtXbmcPath.Text = xbmcFolderDialog.SelectedPath;

                _logger.Info("XBMC Location changed to '{0}'", txtXbmcPath.Text);

                Settings.XbmcPath = txtXbmcPath.Text;

                result = true;
            }

            return result;
        }

        private bool ValidateXbmcFolder()
        {

            if (String.IsNullOrEmpty(Settings.XbmcPath))
            {
                _logger.Info("XBMC Path has not been set");
                ChangeXbmcFolder();
            }
            else if (!Directory.Exists(Settings.XbmcPath))
            {
                _logger.Info("{0} Doesn't exists. Creating directory", Settings.XbmcPath);

                try
                {
                    Directory.CreateDirectory(Settings.XbmcPath);
                }
                catch (Exception e)
                {
                    _logger.Error("An error has occurred while creating xbmc folder. {0}", e.Message);
                }
            }

            return Directory.Exists(Settings.XbmcPath);
        }

        private void InitiateSelfupdate()
        {
            UpdateEventMessage("Checking for application updates.");
            try
            {
                UpdateInProgress = true;

                var selfUpdate = new SelfUpdate.SelfUpdateManager();

                if (selfUpdate.DownloadUpdate())
                {
                    UpdateEventMessage("Installing Update");
                    _logger.Info("Stating selfupdate.exe");
                    UpdateInProgress = false;
                    var selfUpdateProcess = new Process();
                    selfUpdateProcess.StartInfo = new ProcessStartInfo(Application.StartupPath + "\\selfupdate.exe",
                                                                       Program.Arguments);
                    selfUpdateProcess.Start();
                    Close();
                }
                else
                {
                    _update.CheckUpdate();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("An error has occurred while checking for Application update.{0}", ex.Message);
                UpdateEventMessage("An error has occurred during selfupdate.");
            }

            UpdateInProgress = false;
        }


        private void StartUpdate()
        {
            _logger.Info("Initiating Update");

            try
            {
                picUpdateCheck.Image = Resources.feed_blue;
                picDownload.Image = Resources.download_blue;
                picUnzip.Image = Resources.unzip_blue;
                picInstall.Image = Resources.install_blue;

                if (ValidateXbmcFolder())
                {
                    if (_update.CheckUpdate())
                    {
                        UpdateEventMessage("Update Available. Rev." + _update.OnlineBuildNumber);

                        DialogResult dialogResult = DialogResult.Cancel;

                        if (!SilentUpdate)
                        {
                            dialogResult =
                                MessageBox.Show(
                                    "A new release of XBMC is available! Would you like to proceed with installation?",
                                    "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        }

                        if (SilentUpdate || dialogResult == DialogResult.Yes)
                        {
                            _update.InstallUpdatesAsync();
                        }
                    }
                    else
                    {
                        if (SilentUpdate)
                        {
                            ShutDown();
                        }
                    }
                }
                else
                {
                    lblStatus.Text = String.Format("You must select your xbmc location before you can update");
                    _logger.Warn("You must select your xbmc location before you can update");
                    UpdateInProgress = false;
                }
            }
            catch (Exception e)
            {
                _logger.Fatal("An error has occurred while attempting to update xbmc. {0}", e.ToString());
                lblStatus.Text = String.Format("An error has occurred while attempting to update xbmc");
                UpdateInProgress = false;

                if (SilentUpdate)
                {
                    ShutDown();
                }
            }
        }

        private void ShutDown()
        {
            if (!ShutdownTimer.Enabled)
            {
                _logger.Info("Shutdown timer has been initiated. Due in {0} second(s)", Settings.ShutdownCountdown);
                _countDown = Settings.ShutdownCountdown;
                ShutdownTimer.Enabled = true;
            }
            else
            {
                _logger.Warn("Shutdown Counter is already running. Ignoring request.");
            }
        }






    }
}