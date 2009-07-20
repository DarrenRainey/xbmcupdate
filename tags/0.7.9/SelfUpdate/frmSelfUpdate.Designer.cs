namespace XbmcUpdate.SelfUpdate
{
    partial class frmSelfUpdate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( frmSelfUpdate ) );
            this.lblStat = new System.Windows.Forms.Label();
            this.timerShutdown = new System.Windows.Forms.Timer( this.components );
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblStat
            // 
            this.lblStat.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStat.Font = new System.Drawing.Font( "Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
            this.lblStat.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblStat.Location = new System.Drawing.Point( 0, 0 );
            this.lblStat.Name = "lblStat";
            this.lblStat.Size = new System.Drawing.Size( 462, 54 );
            this.lblStat.TabIndex = 0;
            this.lblStat.Text = "Initiating Self Update";
            this.lblStat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerShutdown
            // 
            this.timerShutdown.Interval = 1000;
            this.timerShutdown.Tick += new System.EventHandler( this.timerShutdown_Tick );
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point( 148, 57 );
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size( 167, 23 );
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Start XBMCUpdate";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Visible = false;
            this.btnClose.Click += new System.EventHandler( this.btnClose_Click );
            // 
            // frmSelfUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 462, 86 );
            this.Controls.Add( this.btnClose );
            this.Controls.Add( this.lblStat );
            this.Font = new System.Drawing.Font( "Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelfUpdate";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XbmcUpdate";
            this.Load += new System.EventHandler( this.frmSelfUpdate_Load );
            this.Shown += new System.EventHandler( this.frmSelfUpdate_Shown );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.Label lblStat;
        private System.Windows.Forms.Timer timerShutdown;
        private System.Windows.Forms.Button btnClose;
    }
}

