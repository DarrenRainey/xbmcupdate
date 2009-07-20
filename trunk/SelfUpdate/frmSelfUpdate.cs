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
using System.Drawing;
using System.Windows.Forms;

namespace XbmcUpdate.SelfUpdate
{
    public partial class frmSelfUpdate : Form
    {
        private int countDown = 15;

        public frmSelfUpdate()
        {
            InitializeComponent();
        }

        private void timerShutdown_Tick(object sender, EventArgs e)
        {
            if (countDown > 0)
            {
                btnClose.Visible = true;
                btnClose.Text = String.Format("Starting XBMCUpdate. ({0})", countDown);
                countDown--;
            }
            else
            {
                Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void frmSelfUpdate_Shown(object sender, EventArgs e)
        {
            var selfUpdate = new Update();

            lblStat.Text = "Closing XBMCUpdate";
            Refresh();

            selfUpdate.ShutDownApp();

            lblStat.Text = "Installing Updates";
            Refresh();

            try
            {
                selfUpdate.CopyUpdate();
                countDown = 0;

                lblStat.Text = "Update Completed";
                Refresh();

                lblStat.Text = "Cleaning Up";
                Refresh();
                selfUpdate.CleanUp();
            }
            catch (Exception ex)
            {
                lblStat.ForeColor = Color.IndianRed;
                lblStat.Text = ex.Message + " Please review log file for more details.";
            }

            timerShutdown.Enabled = true;
        }

        private void frmSelfUpdate_Load(object sender, EventArgs e)
        {
        }
    }
}