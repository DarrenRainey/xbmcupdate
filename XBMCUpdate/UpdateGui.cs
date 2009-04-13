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

        private static string log;

        Logger logger = LogManager.GetCurrentClassLogger();
        UpdateManager update = new UpdateManager();

        private NotifyIcon mNotifyIcon;
        private ContextMenuStrip mContextMenu;
        private ToolStripMenuItem mUpdate;
        private ToolStripMenuItem mDisplayForm;
        private ToolStripMenuItem mExitApplication;


        void mNotifyIcon_DoubleClick( object sender, EventArgs e )
        {
            this.Show();
            this.ShowInTaskbar = true;
        }

        void mUpdate_Click( object sender, EventArgs e )
        {
            StartUpdate();
        }

        void mDisplayForm_Click( object sender, EventArgs e )
        {
            Show();
            this.ShowInTaskbar = true;
        }

        void mExitApplication_Click( object sender, EventArgs e )
        {
            //Call our overridden exit thread core method!
            this.Close();
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

            update.OnUpdateError += new UpdateEventHandler( update_OnUpdateError );

            this.Text = String.Concat( "XBMCUpdate ", Settings.ApplicationVersion.ToString() );

            UpdateVersionStat();
            UpdateUiData();
        }


        private void initNlog()
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


        private void btnBrows_Click( object sender, EventArgs e )
        {
            ChangeXbmcFolder();
        }

        internal bool SiletUpdate
        {
            get;
            set;
        }

        internal bool StartInTray
        {
            get;
            set;
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

        private bool ValidateXbmcPath()
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



        private void txtXbmcPath_TextChanged( object sender, EventArgs e )
        {
            UpdateVersionStat();
        }



        private void btnCheckUpdate_Click( object sender, EventArgs e )
        {
            StartUpdate();
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

                if( ValidateXbmcPath() )
                {
                    if( update.CheckUpdate() )
                    {

                        UpdateEvenMessage( "Update Available. Build:" + update.OnlineBuildNumber );

                        DialogResult dialogResult = DialogResult.Cancel;

                        if( !SiletUpdate )
                        {
                            dialogResult = MessageBox.Show( "A new release of XBMC is available! Would you like to processed with installation?", "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Question );
                        }

                        if( SiletUpdate || dialogResult == DialogResult.Yes )
                        {
                            update.InstallUpdatesAsync();
                        }
                        else
                        {
                            EnabledButtons();
                        }
                    }
                    else
                    {
                        lblStatus.Text = String.Format( "No update is necessary. Build Installed:{0}", update.CurrentBuildNumber );
                        EnabledButtons();
                    }
                }
                else
                {
                    lblStatus.Text = String.Format( "You must select your xbmc location before you can update" );
                    logger.Warn( "You must select your xbmc location before you can update" );
                    EnabledButtons();
                }
            }
            catch( Exception e )
            {
                logger.Fatal( "An error has occurred while attempting to update xbmc. {0}", e.ToString() );
                lblStatus.Text = String.Format( "An error has occurred while attempting to update xbmc" );
            }
        }

        #region StatusEvents
        void update_OnInstallStop( UpdateManager sender, string message )
        {
            if( picInstall.InvokeRequired )
            {
                UpdateEventHandler d = new UpdateEventHandler( update_OnInstallStop );
                this.Invoke( d, new object[] { sender, message } );
            }
            else
            {
                picInstall.Image = XbmcUpdate.Runtime.Properties.Resources.install_green;
                UpdateEvenMessage( message );

                EnabledButtons();
            }
        }

        void update_OnInstallStart( UpdateManager sender, string message )
        {
            if( picInstall.InvokeRequired )
            {
                UpdateEventHandler d = new UpdateEventHandler( update_OnInstallStart );
                this.Invoke( d, new object[] { sender, message } );
            }
            else
            {
                picInstall.Image = XbmcUpdate.Runtime.Properties.Resources.install_orange;
                UpdateEvenMessage( message );


                DisableButtons();
            }


        }

        void update_OnUnZipStop( UpdateManager sender, string message )
        {
            if( picUnzip.InvokeRequired )
            {
                UpdateEventHandler d = new UpdateEventHandler( update_OnUnZipStop );
                this.Invoke( d, new object[] { sender, message } );
            }
            else
            {
                picUnzip.Image = XbmcUpdate.Runtime.Properties.Resources.unzip_green;
                UpdateEvenMessage( message );
                EnabledButtons();
            }
        }

        void update_OnUnZipStart( UpdateManager sender, string message )
        {
            if( picUnzip.InvokeRequired )
            {
                UpdateEventHandler d = new UpdateEventHandler( update_OnUnZipStart );
                this.Invoke( d, new object[] { sender, message } );
            }
            else
            {
                picUnzip.Image = XbmcUpdate.Runtime.Properties.Resources.unzip_orange;
                UpdateEvenMessage( message );
                DisableButtons();
            }
        }

        void update_OnDownloadStop( UpdateManager sender, string message )
        {
            if( picDownload.InvokeRequired )
            {
                UpdateEventHandler d = new UpdateEventHandler( update_OnDownloadStop );
                this.Invoke( d, new object[] { sender, message } );
            }
            else
            {
                downloadRefreshTimer.Enabled = false;
                picDownload.Image = XbmcUpdate.Runtime.Properties.Resources.download_green;
                UpdateEvenMessage( message );
                EnabledButtons();

            }

        }

        void update_OnDownloadStart( UpdateManager sender, string message )
        {
            if( picDownload.InvokeRequired )
            {
                UpdateEventHandler d = new UpdateEventHandler( update_OnDownloadStart );
                this.Invoke( d, new object[] { sender, message } );
            }
            else
            {
                downloadRefreshTimer.Enabled = true;
                picDownload.Image = XbmcUpdate.Runtime.Properties.Resources.download_orange;
                UpdateEvenMessage( message );
                DisableButtons();
            }
        }

        void update_OnCheckUpdateStop( UpdateManager sender, string message )
        {
            if( picUpdateCheck.InvokeRequired )
            {
                UpdateEventHandler d = new UpdateEventHandler( update_OnCheckUpdateStop );
                this.Invoke( d, new object[] { sender, message } );
            }
            else
            {
                picUpdateCheck.Image = XbmcUpdate.Runtime.Properties.Resources.feed_green;
                UpdateEvenMessage( message );
            }
        }

        void update_OnCheckUpdateStart( UpdateManager sender, string message )
        {
            if( picUpdateCheck.InvokeRequired )
            {
                UpdateEventHandler d = new UpdateEventHandler( update_OnCheckUpdateStart );
                this.Invoke( d, new object[] { sender, message } );
            }
            else
            {
                picUpdateCheck.Image = XbmcUpdate.Runtime.Properties.Resources.feed_orange;
                UpdateEvenMessage( message );

                DisableButtons();
            }
        }

        void update_OnUpdateError( UpdateManager sender, string message )
        {
            if( picUpdateCheck.InvokeRequired )
            {
                UpdateEventHandler d = new UpdateEventHandler( update_OnUpdateError );
                this.Invoke( d, new object[] { sender, message } );
            }
            else
            {
                UpdateEvenMessage( message );

                EnabledButtons();
            }
        }


        private void UpdateEvenMessage( string message )
        {
            if( String.IsNullOrEmpty( message ) )
            {
                lblStatus.Text = "";
            }
            else
            {
                lblStatus.Text = message.Trim();
                if( mNotifyIcon != null )
                {
                    //mNotifyIcon.Text = message;
                }
            }

            this.Refresh();
        }
        #endregion


        internal static void Log( string message )
        {
            log = String.Concat( log, message, Environment.NewLine );
        }

        private void downloadRefreshTimer_Tick( object sender, EventArgs e )
        {
            if( update.Download != null && update.Download.BytesRead != 0 )
            {
                double mbDownloaded = update.Download.BytesRead / 1048576d;
                double mbSize = update.Download.FileSize / 1048576d;
                lblStatus.Text = string.Format( "{0} MB / {1} MB", mbDownloaded.ToString( "0.00" ), mbSize.ToString( "0.00" ) );
            }
        }


        private void DisableButtons()
        {
            btnCheckUpdate.Enabled = false;
            btnBrows.Enabled = false;

            if( mUpdate != null )
            {
                mUpdate.Enabled = false;
            }
        }


        private void EnabledButtons()
        {
            btnCheckUpdate.Enabled = true;
            btnBrows.Enabled = true;

            if( mUpdate != null )
            {
                mUpdate.Enabled = true;
            }

            if( SiletUpdate )
            {
                ShutDown( 5 );
            }
        }

        private void logTimer_Tick( object sender, EventArgs e )
        {
            //             if( txtLog.Text != log )
            //             {
            //                 txtLog.Text = log;
            //                 txtLog.SelectionStart = txtLog.Text.Length;
            //                 txtLog.ScrollToCaret();
            //             }
        }

        private void tabMain_SelectedIndexChanged( object sender, EventArgs e )
        {
            grpSchedule.Enabled = chkAutoUpdate.Checked;

            UpdateUiData();
        }

        private void UpdateUiData()
        {
            if( String.IsNullOrEmpty( txtReleaseUrl.Text.Trim() ) )
            {
                txtReleaseUrl.Text = Settings.ReleaseUrl;
            }

            txtXbmcPath.Text = Settings.XbmcPath;
        }
        private void chkAutoUpdate_CheckedChanged( object sender, EventArgs e )
        {
            grpSchedule.Enabled = chkAutoUpdate.Checked;
        }

        private void chkAutoUpdate_Click( object sender, EventArgs e )
        {
            grpSchedule.Enabled = chkAutoUpdate.Checked;
        }

        private void btnSave_Click( object sender, EventArgs e )
        {
            Settings.ReleaseUrl = txtReleaseUrl.Text;
        }

        private void btnCancel_Click( object sender, EventArgs e )
        {
            txtReleaseUrl.Text = Settings.ReleaseUrl;
        }

        private void UpdateGui_FormClosing( object sender, FormClosingEventArgs e )
        {
            if( !btnBrows.Enabled )
            {
                var response = MessageBox.Show( "An update is in progress are you sure you want to close XBMC Update?", "Cancel Update", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning );

                if( response != DialogResult.Yes )
                {
                    e.Cancel = true;
                }
            }

        }

        private void UpdateGui_FormClosed( object sender, FormClosedEventArgs e )
        {
            update.Abort();
        }

        private void ShutDown( int countDown )
        {
            logger.Info( "Shutdown timer has been initiated. Due in {0} second(s)", countDown );
            _countDown = countDown;
            ShutdownTimer.Enabled = true;

        }

        private void UpdateGui_Load( object sender, EventArgs e )
        {
            initNlog();

            if( StartInTray )
            {
                InitTray();
            }

        }

        private void UpdateGui_Shown( object sender, EventArgs e )
        {
            if( StartInTray )
            {
                this.Hide();
            }

            InitiateSelfupdate();


            if( SiletUpdate )
            {
                StartUpdate();
            }
        }

        private void InitiateSelfupdate()
        {
            UpdateEvenMessage( "Checking for application updates." );
            try
            {
                DisableButtons();

                if( SelfUpdate.SelfUpdate.DownloadUpdate() )
                {
                    UpdateEvenMessage( "Installing Update" );
                    logger.Info( "Stating xbmcselfupdate.exe" );
                    Process selfUpdateProcess = new Process();
                    selfUpdateProcess.StartInfo = new ProcessStartInfo( "selfupdate.exe", Program.Arguments );
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

            EnabledButtons();
        }


        int _countDown = 5;
        private void ShutdownTimer_Tick( object sender, EventArgs e )
        {
            if( _countDown >= 0 )
            {
                btnCheckUpdate.Enabled = false;
                btnCheckUpdate.Text = "Closing in " + _countDown;
                _countDown--;
            }
            else
            {
                logger.Info( "Shutdown timer is closing the application" );
                this.Close();
            }
        }

        private void rtxtLog_TextChanged( object sender, EventArgs e )
        {
            rtxtLog.SelectionStart = rtxtLog.Text.Length;
            rtxtLog.ScrollToCaret();
        }

        private void tabPageUpdate_Click( object sender, EventArgs e )
        {

        }







        //         private void SwitchButtonToStart()
        //         {
        //             btnCheckUpdate.Enabled = true;
        //             btnCheckUpdate.Visible = true;
        // 
        //             btnCancel.Enabled = false;
        //             btnCancel.Visible = false;
        //         }
        // 
        //         private void SwitchButtonToStop()
        //         {
        //             btnCheckUpdate.Enabled = false;
        //             btnCheckUpdate.Visible = false;
        // 
        //             btnCancel.Enabled = true;
        //             btnCancel.Visible = true;
        // 
        //         }


    }
}
