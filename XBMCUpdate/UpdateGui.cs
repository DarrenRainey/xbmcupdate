using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using NLog;
using NLog.Config;
using NLog.Targets;
using XbmcUpdate.Managers;

namespace XbmcUpdate.Runtime
{


    internal partial class UpdateGui : Form
    {

        Logger logger = LogManager.GetCurrentClassLogger();
        UpdateManager update = new UpdateManager();

        private NotifyIcon mNotifyIcon;
        private ContextMenuStrip mContextMenu;
        private ToolStripMenuItem mUpdate;
        private ToolStripMenuItem mDisplayForm;
        private ToolStripMenuItem mExitApplication;

        internal bool SilentUpdate
        {
            get;
            set;
        }

        internal bool StartInTray
        {
            get;
            set;
        }

        private bool _updateInProgress;
        private bool UpdateInProgress
        {
            get
            {
                return _updateInProgress;
            }
            set
            {
                btnCheckUpdate.Enabled = !value;
                btnBrows.Enabled = !value;

                if( mUpdate != null )
                {
                    mUpdate.Enabled = !value;
                }

                _updateInProgress = value;
            }
        }

        internal UpdateGui()
        {
            InitializeComponent();


            update.OnCheckUpdateStart += new UpdateEventHandler( update_OnCheckUpdateStart );
            update.OnCheckUpdateStop += new UpdateEventHandler( update_OnCheckUpdateStop );

            update.OnDownloadStart += new UpdateEventHandler( update_OnDownloadStart );
            update.OnDownloadStop += new UpdateEventHandler( update_OnDownloadStop );

            update.OnUnZipStart += new UpdateEventHandler( update_OnUnZipStart );
            update.OnUnZipStop += new UpdateEventHandler( update_OnUnZipStop );

            update.OnInstallStart += new UpdateEventHandler( update_OnInstallStart );
            update.OnInstallStop += new UpdateEventHandler( update_OnInstallStop );

            update.OnUpdateProcessStart += new UpdateEventHandler( update_OnUpdateProcessStart );
            update.OnUpdateProcessStop += new UpdateEventHandler( update_OnUpdateProcessStop );

            update.OnUpdateError += new UpdateEventHandler( update_OnUpdateError );

            this.Text = String.Concat( "XBMCUpdate ", Settings.ApplicationVersion.ToString() );

            UpdateVersionStat();
            UpdateBindedUi();
        }


        private void InitTray()
        {
            //Instantiate the NotifyIcon attaching it to the components container and 
            //provide it an icon, note, you can embed this resource 
            mNotifyIcon = new NotifyIcon( this.components );
            mNotifyIcon.Icon = XbmcUpdate.Runtime.Properties.Resources.app;
            mNotifyIcon.Text = "XBMC Update";
            mNotifyIcon.Visible = true;

            //Instantiate the context menu and items
            mContextMenu = new ContextMenuStrip();
            mDisplayForm = new ToolStripMenuItem();
            mExitApplication = new ToolStripMenuItem();
            mUpdate = new ToolStripMenuItem();

            //Attach the menu to the notify icon
            mNotifyIcon.ContextMenuStrip = mContextMenu;


            //Setup the items and add them to the menu strip, adding handlers to be created later

            mUpdate.Text = "Update XBMC";
            mUpdate.Click += new EventHandler( mUpdate_Click );
            mContextMenu.Items.Add( mUpdate );

            mDisplayForm.Text = "Show XBMCUpdate";
            mDisplayForm.Click += new EventHandler( mDisplayForm_Click );
            mContextMenu.Items.Add( mDisplayForm );

            mExitApplication.Text = "Exit";
            mExitApplication.Click += new EventHandler( mExitApplication_Click );
            mContextMenu.Items.Add( mExitApplication );
            mNotifyIcon.DoubleClick += new EventHandler( mNotifyIcon_DoubleClick );


            this.components.Add( mNotifyIcon );
        }

        private void InitNlog()
        {
            RichTextBoxTarget txtTarget = new RichTextBoxTarget();

            txtTarget.Layout = "${message}";

            txtTarget.ControlName = rtxtLog.Name;
            txtTarget.FormName = this.Name;
            txtTarget.UseDefaultRowColoringRules = false;

            LoggingRule rule1 = new LoggingRule( "*", LogLevel.Trace, txtTarget );

            LogManager.Configuration.AddTarget( "guiTarget", txtTarget );
            LogManager.Configuration.LoggingRules.Add( rule1 );
        }

        private void UpdateBindedUi()
        {
            if( String.IsNullOrEmpty( txtReleaseUrl.Text.Trim() ) )
            {
                txtReleaseUrl.Text = Settings.ReleaseUrl;
            }

            txtXbmcPath.Text = Settings.XbmcPath;
        }

        private void UpdateVersionStat()
        {
            try
            {
                var currentBuild = XbmcManager.GerVersion();

                if( currentBuild.BuildNumber != 0 )
                {
                    TimeSpan installed = DateTime.Now - currentBuild.InstallationDate;

                    string Age;

                    if( installed.TotalDays > 1 )
                    {
                        Age = string.Format( "Installed {0} day(s) ago", installed.TotalDays.ToString( "0" ) );
                    }
                    else
                    {
                        if( installed.TotalHours < 1 )
                        {
                            Age = string.Format( "Installed {0} minute(s) ago", installed.TotalMinutes.ToString( "0" ) );
                        }
                        else
                        {
                            Age = string.Format( "Installed {0} hour(s) ago", installed.TotalHours.ToString( "0" ) );
                        }
                    }

                    lblStatus.Text = String.Format( "Current Build:{0}{1}{2}", currentBuild.BuildNumber, Environment.NewLine, Age );
                }
                else
                {
                    lblStatus.Text = ( "Unknown local build. Latest build will be installed" );

                }
            }
            catch( Exception e )
            {
                logger.Fatal( "An error has occurred while generating update stat string.{0}", e.Message );
            }
        }

        private bool ChangeXbmcFolder()
        {
            bool result = false;

            xbmcFolderDialog.ShowDialog( this );

            if( !String.IsNullOrEmpty( xbmcFolderDialog.SelectedPath ) )
            {
                txtXbmcPath.Text = xbmcFolderDialog.SelectedPath;

                logger.Info( "XBMC Location changed to '{0}'", txtXbmcPath.Text );

                Settings.XbmcPath = txtXbmcPath.Text;

                UpdateVersionStat();

                result = true;
            }

            return result;
        }

        private bool ValidateXbmcFolder()
        {
            bool result = false;

            if( String.IsNullOrEmpty( Settings.XbmcPath ) )
            {
                logger.Info( "XBMC Path has not been set" );
                ChangeXbmcFolder();
            }
            else if( !Directory.Exists( Settings.XbmcPath ) )
            {
                logger.Info( "{0} Doesn't exists. Creating directory", Settings.XbmcPath );

                try
                {
                    Directory.CreateDirectory( Settings.XbmcPath );
                }
                catch( Exception e )
                {
                    logger.Error( "An error has occurred while creating xbmc folder. {0}", e.Message );
                }
            }

            result = Directory.Exists( Settings.XbmcPath );

            return result;
        }

        private void InitiateSelfupdate()
        {
            UpdateEvenMessage( "Checking for application updates." );
            try
            {
                UpdateInProgress = true;

                if( SelfUpdate.SelfUpdate.DownloadUpdate() )
                {
                    UpdateEvenMessage( "Installing Update" );
                    logger.Info( "Stating selfupdate.exe" );
                    UpdateInProgress = false;
                    Process selfUpdateProcess = new Process();
                    selfUpdateProcess.StartInfo = new ProcessStartInfo( Application.StartupPath + "\\selfupdate.exe", Program.Arguments );
                    selfUpdateProcess.Start();
                    this.Close();
                }
                else
                {
                    UpdateVersionStat();
                }
            }
            catch( Exception ex )
            {
                logger.Error( "An error has occurred while checking for Application update.{0}", ex.ToString() );
                UpdateEvenMessage( "An error has occurred during selfupdate." );
            }

            UpdateInProgress = false;
        }

        internal void StartUpdate()
        {
            logger.Info( "Initiating Update" );

            try
            {
                picUpdateCheck.Image = XbmcUpdate.Runtime.Properties.Resources.feed_blue;
                picDownload.Image = XbmcUpdate.Runtime.Properties.Resources.download_blue;
                picUnzip.Image = XbmcUpdate.Runtime.Properties.Resources.unzip_blue;
                picInstall.Image = XbmcUpdate.Runtime.Properties.Resources.install_blue;

                if( ValidateXbmcFolder() )
                {
                    if( update.CheckUpdate() )
                    {

                        UpdateEvenMessage( "Update Available. Build:" + update.OnlineBuildNumber );

                        DialogResult dialogResult = DialogResult.Cancel;

                        if( !SilentUpdate )
                        {
                            dialogResult = MessageBox.Show( "A new release of XBMC is available! Would you like to processed with installation?", "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Question );
                        }

                        if( SilentUpdate || dialogResult == DialogResult.Yes )
                        {
                            update.InstallUpdatesAsync();
                        }
                    }
                    else
                    {
                        lblStatus.Text = String.Format( "No update is necessary. Build Installed:{0}", update.CurrentBuildNumber );
                        ShutDown();
                    }
                }
                else
                {
                    lblStatus.Text = String.Format( "You must select your xbmc location before you can update" );
                    logger.Warn( "You must select your xbmc location before you can update" );
                    UpdateInProgress = false;
                }
            }
            catch( Exception e )
            {
                logger.Fatal( "An error has occurred while attempting to update xbmc. {0}", e.ToString() );
                lblStatus.Text = String.Format( "An error has occurred while attempting to update xbmc" );
                UpdateInProgress = false;
                ShutDown();
            }
        }

        //private void DisableButtons()
        //{
        //    btnCheckUpdate.Enabled = false;
        //    btnBrows.Enabled = false;

        //    if( mUpdate != null )
        //    {
        //        mUpdate.Enabled = false;
        //    }
        //}

        //private void EnabledButtons()
        //{
        //    btnCheckUpdate.Enabled = true;
        //    btnBrows.Enabled = true;

        //    if( mUpdate != null )
        //    {
        //        mUpdate.Enabled = true;
        //    }

        //    if( SilentUpdate )
        //    {
        //        ShutDown( 5 );
        //    }
        //}

        private void ShutDown()
        {
            logger.Info( "Shutdown timer has been initiated. Due in {0} second(s)", Settings.ShutdownCountdown );
            _countDown = Settings.ShutdownCountdown;
            ShutdownTimer.Enabled = true;
        }

        private void tabPageUpdate_Click( object sender, EventArgs e )
        {

        }
    }
}
