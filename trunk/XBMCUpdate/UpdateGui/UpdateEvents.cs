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
                UpdateEvenMessage( message );

                switch( Settings.XbmcAutostart )
                {
                    //Always
                    case 1:
                        {
                            logger.Info( "XBMC is set to always autostart" );
                            XbmcManager.StartXbmc();
                            ShutDown();
                            break;
                        }

                    //Only if closed
                    case 2:
                        {
                            logger.Info( "XBMC is set to auto start only if it was closed." );
                            if( XbmcManager.XbmcTerminated )
                            {
                                logger.Info( "XBMC was terminated during this session" );
                                XbmcManager.StartXbmc();
                                ShutDown();
                            }
                            break;
                        }
                }

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
                UpdateEvenMessage( message );
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
