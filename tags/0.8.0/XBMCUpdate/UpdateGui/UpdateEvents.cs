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
using System.Windows.Forms;
using XbmcUpdate.Properties;
using XbmcUpdate.UpdateEngine;

namespace XbmcUpdate
{
    internal partial class UpdateGui : Form
    {
        private void update_OnInstallStop(UpdateManager sender, string message)
        {
            if (picInstall.InvokeRequired)
            {
                var d = new UpdateEventHandler(update_OnInstallStop);
                Invoke(d, new object[] { sender, message });
            }
            else
            {
                picInstall.Image = Resources.install_green;
                UpdateEventMessage(message);
            }
        }

        private void update_OnInstallStart(UpdateManager sender, string message)
        {
            if (picInstall.InvokeRequired)
            {
                var d = new UpdateEventHandler(update_OnInstallStart);
                Invoke(d, new object[] { sender, message });
            }
            else
            {
                picInstall.Image = Resources.install_orange;
                UpdateEventMessage(message);
            }
        }

        private void update_OnUnZipStop(UpdateManager sender, string message)
        {
            if (picUnzip.InvokeRequired)
            {
                var d = new UpdateEventHandler(update_OnUnZipStop);
                Invoke(d, new object[] { sender, message });
            }
            else
            {
                picUnzip.Image = Resources.unzip_green;
                UpdateEventMessage(message);
            }
        }

        private void update_OnUnZipStart(UpdateManager sender, string message)
        {
            if (picUnzip.InvokeRequired)
            {
                var d = new UpdateEventHandler(update_OnUnZipStart);
                Invoke(d, new object[] { sender, message });
            }
            else
            {
                picUnzip.Image = Resources.unzip_orange;
                UpdateEventMessage(message);
            }
        }

        private void update_OnDownloadStop(UpdateManager sender, string message)
        {
            if (picDownload.InvokeRequired)
            {
                var d = new UpdateEventHandler(update_OnDownloadStop);
                Invoke(d, new object[] { sender, message });
            }
            else
            {
                downloadRefreshTimer.Enabled = false;
                picDownload.Image = Resources.download_green;
                UpdateEventMessage(message);
            }
        }

        private void update_OnDownloadStart(UpdateManager sender, string message)
        {
            if (picDownload.InvokeRequired)
            {
                var d = new UpdateEventHandler(update_OnDownloadStart);
                Invoke(d, new object[] { sender, message });
            }
            else
            {
                downloadRefreshTimer.Enabled = true;
                picDownload.Image = Resources.download_orange;
                UpdateEventMessage(message);
            }
        }

        private void update_OnCheckUpdateStop(UpdateManager sender, string message)
        {
            if (picUpdateCheck.InvokeRequired)
            {
                var d = new UpdateEventHandler(update_OnCheckUpdateStop);
                Invoke(d, new object[] { sender, message });
            }
            else
            {
                picUpdateCheck.Image = Resources.feed_green;
                UpdateEventMessage(message);
            }
        }

        private void update_OnCheckUpdateStart(UpdateManager sender, string message)
        {
            if (picUpdateCheck.InvokeRequired)
            {
                var d = new UpdateEventHandler(update_OnCheckUpdateStart);
                Invoke(d, new object[] { sender, message });
            }
            else
            {
                picUpdateCheck.Image = Resources.feed_orange;
                UpdateEventMessage(message);
            }
        }

        private void update_OnUpdateError(UpdateManager sender, string message)
        {
            if (picUpdateCheck.InvokeRequired)
            {
                var d = new UpdateEventHandler(update_OnUpdateError);
                Invoke(d, new object[] { sender, message });
            }
            else
            {
                UpdateEventMessage(message);
                UpdateInProgress = false;

                if (SilentUpdate)
                {
                    ShutDown();
                }
            }
        }


        private void update_OnUpdateProcessStop(UpdateManager sender, string message)
        {
            if (btnCheckUpdate.InvokeRequired)
            {
                var d = new UpdateEventHandler(update_OnUpdateProcessStop);
                Invoke(d, new object[] { sender, message });
            }
            else
            {
                UpdateInProgress = false;
                UpdateEventMessage(message);

                switch (Settings.XbmcAutostart)
                {
                    //Always
                    case 1:
                        {
                            _logger.Info("XBMC is set to always autostart");
                            XbmcManager.StartXbmc();
                            ShutDown();
                            break;
                        }

                    //Only if closed
                    case 2:
                        {
                            _logger.Info("XBMC is set to auto start only if it was closed.");
                            if (XbmcManager._xbmcTerminated)
                            {
                                _logger.Info("XBMC was terminated during this session");
                                XbmcManager.StartXbmc();
                                ShutDown();
                            }
                            break;
                        }
                }

                if (SilentUpdate)
                {
                    ShutDown();
                }
            }
        }

        private void update_OnUpdateProcessStart(UpdateManager sender, string message)
        {
            if (btnCheckUpdate.InvokeRequired)
            {
                var d = new UpdateEventHandler(update_OnUpdateProcessStart);
                Invoke(d, new object[] { sender, message });
            }
            else
            {
                UpdateInProgress = true;
                UpdateEventMessage(message);
            }
        }

        private void UpdateEventMessage(string message)
        {
            lblStatus.Text = String.IsNullOrEmpty(message) ? "" : message.Trim();
            UpdateVersionStat();

            Refresh();
        }
    }
}