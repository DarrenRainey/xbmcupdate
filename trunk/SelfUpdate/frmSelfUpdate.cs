using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace XbmcUpdate.SelfUpdate
{
    public partial class frmSelfUpdate : Form
    {
        public frmSelfUpdate()
        {
            InitializeComponent();
        }


        int countDown = 15;
        private void timerShutdown_Tick( object sender, EventArgs e )
        {
            if( countDown > 0 )
            {
                btnClose.Visible = true;
                btnClose.Text = String.Format( "Starting XBMCUpdate. ({0})", countDown );
                countDown--;
            }
            else
            {
                this.Close();
            }
        }

        private void btnClose_Click( object sender, EventArgs e )
        {
            this.Close();
        }



        private void frmSelfUpdate_Shown( object sender, EventArgs e )
        {
            Update selfUpdate = new Update();

            lblStat.Text = "Closing XBMCUpdate";
            this.Refresh();

            selfUpdate.ShutDownApp();

            lblStat.Text = "Installing Updates";
            this.Refresh();

            try
            {
                selfUpdate.CopyUpdate();
                countDown = 0;

                lblStat.Text = "Update Completed";
                this.Refresh();

                lblStat.Text = "Cleaning Up";
                this.Refresh();
                selfUpdate.CleanUp();

            }
            catch( Exception ex )
            {
                lblStat.ForeColor = Color.IndianRed;
                lblStat.Text = ex.Message + " Please review log file for more details.";
            }

            timerShutdown.Enabled = true;
        }

        private void frmSelfUpdate_Load( object sender, EventArgs e )
        {

        }
    }
}
