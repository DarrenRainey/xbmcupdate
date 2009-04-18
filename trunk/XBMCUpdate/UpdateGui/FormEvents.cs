using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace XbmcUpdate.Runtime
{
    internal partial class UpdateGui : Form
    {
        void mNotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.ShowInTaskbar = true;
        }

        void mUpdate_Click(object sender, EventArgs e)
        {
            StartUpdate();
        }

        void mDisplayForm_Click(object sender, EventArgs e)
        {
            Show();
            this.ShowInTaskbar = true;
        }

        void mExitApplication_Click(object sender, EventArgs e)
        {
            //Call our overridden exit thread core method!
            this.Close();
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

        private void rtxtLog_TextChanged(object sender, EventArgs e)
        {
            rtxtLog.SelectionStart = rtxtLog.Text.Length;
            rtxtLog.ScrollToCaret();
        }


        private void downloadRefreshTimer_Tick(object sender, EventArgs e)
        {
            double mbDownloaded = update.Download.BytesRead / 1048576d;
            double mbSize = update.Download.FileSize / 1048576d;
            lblStatus.Text = string.Format("{0} MB / {1} MB", mbDownloaded.ToString("0.00"), mbSize.ToString("0.00"));
        }


        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateBindedUi();
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            Settings.ReleaseUrl = txtReleaseUrl.Text;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtReleaseUrl.Text = Settings.ReleaseUrl;
        }

        private void UpdateGui_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (UpdateInProgress)
            {
                var response = MessageBox.Show("An update is in progress are you sure you want to close XBMCUpdate?", "Cancel Update", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (response != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }

        }

        private void UpdateGui_FormClosed(object sender, FormClosedEventArgs e)
        {
            update.Abort();
        }

        private void UpdateGui_Load(object sender, EventArgs e)
        {
            InitNlog();

            if (StartInTray)
            {
                InitTray();
            }

        }

        private void UpdateGui_Shown(object sender, EventArgs e)
        {
            if (StartInTray)
            {
                this.Hide();
            }

            InitiateSelfupdate();


            if (SilentUpdate)
            {
                StartUpdate();
            }
        }


        int _countDown = 5;
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
                logger.Info("Shutdown timer is closing the application");
                this.Close();
            }
        }
    }
}
