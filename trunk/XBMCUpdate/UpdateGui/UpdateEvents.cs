using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using XbmcUpdate.Managers;


namespace XbmcUpdate.Runtime
{
    internal partial class UpdateGui : Form
    {
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
                UpdateInProgress = false;

                if( SilentUpdate )
                {
                    ShutDown();
                }
            }
        }


        void update_OnUpdateProcessStop( UpdateManager sender, string message )
        {
            if( btnCheckUpdate.InvokeRequired )
            {
                UpdateEventHandler d = new UpdateEventHandler( update_OnUpdateProcessStop );
                this.Invoke( d, new object[] { sender, message } );
            }
            else
            {
                UpdateInProgress = false;


                if( SilentUpdate )
                {
                    ShutDown();
                }
            }
        }

        void update_OnUpdateProcessStart( UpdateManager sender, string message )
        {
            if( btnCheckUpdate.InvokeRequired )
            {
                UpdateEventHandler d = new UpdateEventHandler( update_OnUpdateProcessStart );
                this.Invoke( d, new object[] { sender, message } );
            }
            else
            {
                UpdateInProgress = true;
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
    }
}
