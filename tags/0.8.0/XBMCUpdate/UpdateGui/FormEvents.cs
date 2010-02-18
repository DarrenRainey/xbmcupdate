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
using System.Windows.Forms;
using XbmcUpdate.UpdateEngine.Source;

namespace XbmcUpdate
{
    internal partial class UpdateGui : Form
    {
        private int _countDown = 5;

        private void mNotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            ShowInTaskbar = true;
        }

        private void mUpdate_Click(object sender, EventArgs e)
        {
            StartUpdate();
        }

        private void mDisplayForm_Click(object sender, EventArgs e)
        {
            Show();
            ShowInTaskbar = true;
        }

        private void mExitApplication_Click(object sender, EventArgs e)
        {
            //Call our overridden exit thread core method!
            Close();
        }

        private void btnBrows_Click(object sender, EventArgs e)
        {
            ChangeXbmcFolder();
        }

        private void txtXbmcPath_TextChanged(object sender, EventArgs e)
        {
            UpdateVersionStat();
        }


        private void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            StartUpdate();
        }



        private void downloadRefreshTimer_Tick(object sender, EventArgs e)
        {
            double mbDownloaded = _update.Download.BytesRead / 1048576d;
            double mbSize = _update.Download.FileSize / 1048576d;
            lblStatus.Text = string.Format("{0} MB / {1} MB", mbDownloaded.ToString("0.00"), mbSize.ToString("0.00"));
        }


        private void btnApply_Click(object sender, EventArgs e)
        {
            //Settings.ReleaseUrl = txtReleaseUrl.Text;
            Settings.XbmcStartupArgs = txtXbmcStartArgs.Text;
            Settings.XbmcAutostart = cmbXbmcStart.SelectedIndex;
            Settings.XbmcAutoShutdown = chkUpdateIfXbmcIsRunning.Checked;
            Settings.PreventStandBy = chkPreventStandby.Checked;
            Settings.SourceName = cmbSources.SelectedItem.ToString();
        }

        private void UpdateGui_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (UpdateInProgress)
            {
                DialogResult response =
                    MessageBox.Show("An update is in progress are you sure you want to close XBMCUpdate?",
                                    "Cancel Update", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (response != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        private void UpdateGui_FormClosed(object sender, FormClosedEventArgs e)
        {
            _update.Abort();
        }

        private void UpdateGui_Load(object sender, EventArgs e)
        {
            if (StartInTray)
            {
                InitTray();
            }
        }

        private void UpdateGui_Shown(object sender, EventArgs e)
        {
            if (StartInTray)
            {
                Hide();
            }

            InitiateSelfupdate();


            if (SilentUpdate)
            {
                StartUpdate();
            }
        }


        private void ShutdownTimer_Tick(object sender, EventArgs e)
        {
            if (_countDown >= 0)
            {
                btnCheckUpdate.Enabled = false;
                btnCheckUpdate.Text = "Closing in " + _countDown;
                _countDown--;
            }
            else
            {
                _logger.Info("Shutdown timer is closing the application");
                Close();
            }
        }


        private void cmbXbmcStart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbXbmcStart.SelectedIndex == 0)
            {
                txtXbmcStartArgs.Enabled = false;
            }
            else
            {
                txtXbmcStartArgs.Enabled = true;
            }
        }

        private void cmbSources_SelectedIndexChanged(object sender, EventArgs e)
        {
            var source = SourceManager.GetSource(cmbSources.SelectedIndex);

            lnkSource.Text = source.Url;
            txtSrcRegex.Text = source.RegEx;
        }

        private void lnkSource_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(lnkSource.Text);
        }
    }
}