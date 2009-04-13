using System.Deployment;
using System.Windows.Forms;
namespace XbmcUpdate.Runtime
{
    partial class UpdateGui
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( UpdateGui ) );
            this.btnBrows = new System.Windows.Forms.Button();
            this.xbmcFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.grpXbmcPath = new System.Windows.Forms.GroupBox();
            this.txtXbmcPath = new System.Windows.Forms.TextBox();
            this.btnCheckUpdate = new System.Windows.Forms.Button();
            this.downloadRefreshTimer = new System.Windows.Forms.Timer( this.components );
            this.lblStatus = new System.Windows.Forms.Label();
            this.iconToolTip = new System.Windows.Forms.ToolTip( this.components );
            this.picInstall = new System.Windows.Forms.PictureBox();
            this.picDownload = new System.Windows.Forms.PictureBox();
            this.picUnzip = new System.Windows.Forms.PictureBox();
            this.picUpdateCheck = new System.Windows.Forms.PictureBox();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPageUpdate = new System.Windows.Forms.TabPage();
            this.grpStatIcons = new System.Windows.Forms.GroupBox();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblReleaseUrl = new System.Windows.Forms.Label();
            this.txtReleaseUrl = new System.Windows.Forms.TextBox();
            this.chkAutoUpdate = new System.Windows.Forms.CheckBox();
            this.grpSchedule = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.chkFriday = new System.Windows.Forms.CheckBox();
            this.ckhSunday = new System.Windows.Forms.CheckBox();
            this.chkSaturday = new System.Windows.Forms.CheckBox();
            this.chkThursday = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.chkTuesday = new System.Windows.Forms.CheckBox();
            this.chkMonday = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.rtxtLog = new System.Windows.Forms.RichTextBox();
            this.ShutdownTimer = new System.Windows.Forms.Timer( this.components );
            this.grpXbmcPath.SuspendLayout();
            ( (System.ComponentModel.ISupportInitialize)( this.picInstall ) ).BeginInit();
            ( (System.ComponentModel.ISupportInitialize)( this.picDownload ) ).BeginInit();
            ( (System.ComponentModel.ISupportInitialize)( this.picUnzip ) ).BeginInit();
            ( (System.ComponentModel.ISupportInitialize)( this.picUpdateCheck ) ).BeginInit();
            this.tabMain.SuspendLayout();
            this.tabPageUpdate.SuspendLayout();
            this.grpStatIcons.SuspendLayout();
            this.grpStatus.SuspendLayout();
            ( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).BeginInit();
            this.tabSettings.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpSchedule.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBrows
            // 
            this.btnBrows.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnBrows.Location = new System.Drawing.Point( 248, 20 );
            this.btnBrows.Name = "btnBrows";
            this.btnBrows.Size = new System.Drawing.Size( 26, 20 );
            this.btnBrows.TabIndex = 3;
            this.btnBrows.Text = "...";
            this.btnBrows.UseVisualStyleBackColor = true;
            this.btnBrows.Click += new System.EventHandler( this.btnBrows_Click );
            // 
            // xbmcFolderDialog
            // 
            this.xbmcFolderDialog.Description = "XBMC Installation Folder";
            this.xbmcFolderDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // grpXbmcPath
            // 
            this.grpXbmcPath.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.grpXbmcPath.Controls.Add( this.btnBrows );
            this.grpXbmcPath.Controls.Add( this.txtXbmcPath );
            this.grpXbmcPath.Location = new System.Drawing.Point( 16, 109 );
            this.grpXbmcPath.Name = "grpXbmcPath";
            this.grpXbmcPath.Size = new System.Drawing.Size( 280, 55 );
            this.grpXbmcPath.TabIndex = 8;
            this.grpXbmcPath.TabStop = false;
            this.grpXbmcPath.Text = "XBMC Location";
            // 
            // txtXbmcPath
            // 
            this.txtXbmcPath.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.txtXbmcPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtXbmcPath.Location = new System.Drawing.Point( 6, 20 );
            this.txtXbmcPath.Multiline = true;
            this.txtXbmcPath.Name = "txtXbmcPath";
            this.txtXbmcPath.ReadOnly = true;
            this.txtXbmcPath.Size = new System.Drawing.Size( 236, 20 );
            this.txtXbmcPath.TabIndex = 2;
            this.txtXbmcPath.TextChanged += new System.EventHandler( this.txtXbmcPath_TextChanged );
            // 
            // btnCheckUpdate
            // 
            this.btnCheckUpdate.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnCheckUpdate.Location = new System.Drawing.Point( 93, 299 );
            this.btnCheckUpdate.Name = "btnCheckUpdate";
            this.btnCheckUpdate.Size = new System.Drawing.Size( 124, 32 );
            this.btnCheckUpdate.TabIndex = 9;
            this.btnCheckUpdate.Text = "Check for updates";
            this.btnCheckUpdate.UseVisualStyleBackColor = true;
            this.btnCheckUpdate.Click += new System.EventHandler( this.btnCheckUpdate_Click );
            // 
            // downloadRefreshTimer
            // 
            this.downloadRefreshTimer.Tick += new System.EventHandler( this.downloadRefreshTimer_Tick );
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font( "Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
            this.lblStatus.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblStatus.Location = new System.Drawing.Point( 6, 16 );
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size( 267, 41 );
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "Latest Build: 12345\r\nInstalled 3 day(s) ago";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picInstall
            // 
            this.picInstall.Image = global::XbmcUpdate.Runtime.Properties.Resources.install_blue;
            this.picInstall.InitialImage = global::XbmcUpdate.Runtime.Properties.Resources.install_blue;
            this.picInstall.Location = new System.Drawing.Point( 195, 15 );
            this.picInstall.Name = "picInstall";
            this.picInstall.Size = new System.Drawing.Size( 32, 32 );
            this.picInstall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picInstall.TabIndex = 15;
            this.picInstall.TabStop = false;
            this.iconToolTip.SetToolTip( this.picInstall, "Install Status" );
            // 
            // picDownload
            // 
            this.picDownload.ErrorImage = global::XbmcUpdate.Runtime.Properties.Resources.download_blue;
            this.picDownload.Image = global::XbmcUpdate.Runtime.Properties.Resources.download_blue;
            this.picDownload.Location = new System.Drawing.Point( 101, 15 );
            this.picDownload.Name = "picDownload";
            this.picDownload.Size = new System.Drawing.Size( 32, 32 );
            this.picDownload.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picDownload.TabIndex = 14;
            this.picDownload.TabStop = false;
            this.iconToolTip.SetToolTip( this.picDownload, "Download Status" );
            // 
            // picUnzip
            // 
            this.picUnzip.Image = global::XbmcUpdate.Runtime.Properties.Resources.unzip_blue;
            this.picUnzip.Location = new System.Drawing.Point( 148, 15 );
            this.picUnzip.Name = "picUnzip";
            this.picUnzip.Size = new System.Drawing.Size( 32, 32 );
            this.picUnzip.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picUnzip.TabIndex = 13;
            this.picUnzip.TabStop = false;
            this.iconToolTip.SetToolTip( this.picUnzip, "Extract Status" );
            // 
            // picUpdateCheck
            // 
            this.picUpdateCheck.Image = global::XbmcUpdate.Runtime.Properties.Resources.feed_blue;
            this.picUpdateCheck.Location = new System.Drawing.Point( 52, 15 );
            this.picUpdateCheck.Name = "picUpdateCheck";
            this.picUpdateCheck.Size = new System.Drawing.Size( 32, 32 );
            this.picUpdateCheck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picUpdateCheck.TabIndex = 12;
            this.picUpdateCheck.TabStop = false;
            this.iconToolTip.SetToolTip( this.picUpdateCheck, "Check Updates Status" );
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add( this.tabPageUpdate );
            this.tabMain.Controls.Add( this.tabSettings );
            this.tabMain.Controls.Add( this.tabPageLog );
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point( 0, 0 );
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size( 320, 363 );
            this.tabMain.TabIndex = 14;
            this.tabMain.SelectedIndexChanged += new System.EventHandler( this.tabMain_SelectedIndexChanged );
            // 
            // tabPageUpdate
            // 
            this.tabPageUpdate.Controls.Add( this.grpStatIcons );
            this.tabPageUpdate.Controls.Add( this.btnCheckUpdate );
            this.tabPageUpdate.Controls.Add( this.grpStatus );
            this.tabPageUpdate.Controls.Add( this.pictureBox1 );
            this.tabPageUpdate.Controls.Add( this.grpXbmcPath );
            this.tabPageUpdate.Location = new System.Drawing.Point( 4, 22 );
            this.tabPageUpdate.Name = "tabPageUpdate";
            this.tabPageUpdate.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPageUpdate.Size = new System.Drawing.Size( 312, 337 );
            this.tabPageUpdate.TabIndex = 0;
            this.tabPageUpdate.Text = "Update";
            this.tabPageUpdate.UseVisualStyleBackColor = true;
            // 
            // grpStatIcons
            // 
            this.grpStatIcons.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.grpStatIcons.Controls.Add( this.picInstall );
            this.grpStatIcons.Controls.Add( this.picUpdateCheck );
            this.grpStatIcons.Controls.Add( this.picUnzip );
            this.grpStatIcons.Controls.Add( this.picDownload );
            this.grpStatIcons.Location = new System.Drawing.Point( 16, 164 );
            this.grpStatIcons.Name = "grpStatIcons";
            this.grpStatIcons.Size = new System.Drawing.Size( 280, 62 );
            this.grpStatIcons.TabIndex = 19;
            this.grpStatIcons.TabStop = false;
            // 
            // grpStatus
            // 
            this.grpStatus.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.grpStatus.Controls.Add( this.lblStatus );
            this.grpStatus.Location = new System.Drawing.Point( 16, 226 );
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size( 280, 67 );
            this.grpStatus.TabIndex = 18;
            this.grpStatus.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.pictureBox1.Image = ( (System.Drawing.Image)( resources.GetObject( "pictureBox1.Image" ) ) );
            this.pictureBox1.Location = new System.Drawing.Point( 36, 8 );
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size( 241, 101 );
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add( this.groupBox1 );
            this.tabSettings.Controls.Add( this.chkAutoUpdate );
            this.tabSettings.Controls.Add( this.grpSchedule );
            this.tabSettings.Controls.Add( this.btnSave );
            this.tabSettings.Controls.Add( this.btnCancel );
            this.tabSettings.Location = new System.Drawing.Point( 4, 22 );
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Size = new System.Drawing.Size( 312, 337 );
            this.tabSettings.TabIndex = 2;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add( this.lblReleaseUrl );
            this.groupBox1.Controls.Add( this.txtReleaseUrl );
            this.groupBox1.Location = new System.Drawing.Point( 16, 167 );
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size( 280, 88 );
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // lblReleaseUrl
            // 
            this.lblReleaseUrl.AutoSize = true;
            this.lblReleaseUrl.Location = new System.Drawing.Point( 6, 17 );
            this.lblReleaseUrl.Name = "lblReleaseUrl";
            this.lblReleaseUrl.Size = new System.Drawing.Size( 71, 13 );
            this.lblReleaseUrl.TabIndex = 1;
            this.lblReleaseUrl.Text = "Release URL:";
            // 
            // txtReleaseUrl
            // 
            this.txtReleaseUrl.Location = new System.Drawing.Point( 7, 33 );
            this.txtReleaseUrl.Multiline = true;
            this.txtReleaseUrl.Name = "txtReleaseUrl";
            this.txtReleaseUrl.Size = new System.Drawing.Size( 267, 44 );
            this.txtReleaseUrl.TabIndex = 0;
            // 
            // chkAutoUpdate
            // 
            this.chkAutoUpdate.AutoSize = true;
            this.chkAutoUpdate.Enabled = false;
            this.chkAutoUpdate.Location = new System.Drawing.Point( 16, 13 );
            this.chkAutoUpdate.Name = "chkAutoUpdate";
            this.chkAutoUpdate.Size = new System.Drawing.Size( 112, 17 );
            this.chkAutoUpdate.TabIndex = 3;
            this.chkAutoUpdate.Text = "Automatic Update";
            this.chkAutoUpdate.UseVisualStyleBackColor = true;
            this.chkAutoUpdate.Click += new System.EventHandler( this.chkAutoUpdate_Click );
            this.chkAutoUpdate.CheckedChanged += new System.EventHandler( this.chkAutoUpdate_CheckedChanged );
            // 
            // grpSchedule
            // 
            this.grpSchedule.Controls.Add( this.dateTimePicker1 );
            this.grpSchedule.Controls.Add( this.chkFriday );
            this.grpSchedule.Controls.Add( this.ckhSunday );
            this.grpSchedule.Controls.Add( this.chkSaturday );
            this.grpSchedule.Controls.Add( this.chkThursday );
            this.grpSchedule.Controls.Add( this.checkBox1 );
            this.grpSchedule.Controls.Add( this.chkTuesday );
            this.grpSchedule.Controls.Add( this.chkMonday );
            this.grpSchedule.Enabled = false;
            this.grpSchedule.Location = new System.Drawing.Point( 16, 36 );
            this.grpSchedule.Name = "grpSchedule";
            this.grpSchedule.Size = new System.Drawing.Size( 280, 125 );
            this.grpSchedule.TabIndex = 0;
            this.grpSchedule.TabStop = false;
            this.grpSchedule.Text = "Update Schedule";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Checked = false;
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker1.Location = new System.Drawing.Point( 17, 90 );
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size( 247, 21 );
            this.dateTimePicker1.TabIndex = 8;
            // 
            // chkFriday
            // 
            this.chkFriday.AutoSize = true;
            this.chkFriday.Location = new System.Drawing.Point( 101, 44 );
            this.chkFriday.Name = "chkFriday";
            this.chkFriday.Size = new System.Drawing.Size( 56, 17 );
            this.chkFriday.TabIndex = 4;
            this.chkFriday.Text = "Friday";
            this.chkFriday.UseVisualStyleBackColor = true;
            // 
            // ckhSunday
            // 
            this.ckhSunday.AutoSize = true;
            this.ckhSunday.Location = new System.Drawing.Point( 19, 67 );
            this.ckhSunday.Name = "ckhSunday";
            this.ckhSunday.Size = new System.Drawing.Size( 62, 17 );
            this.ckhSunday.TabIndex = 6;
            this.ckhSunday.Text = "Sunday";
            this.ckhSunday.UseVisualStyleBackColor = true;
            // 
            // chkSaturday
            // 
            this.chkSaturday.AutoSize = true;
            this.chkSaturday.Location = new System.Drawing.Point( 185, 44 );
            this.chkSaturday.Name = "chkSaturday";
            this.chkSaturday.Size = new System.Drawing.Size( 70, 17 );
            this.chkSaturday.TabIndex = 5;
            this.chkSaturday.Text = "Saturday";
            this.chkSaturday.UseVisualStyleBackColor = true;
            // 
            // chkThursday
            // 
            this.chkThursday.AutoSize = true;
            this.chkThursday.Location = new System.Drawing.Point( 19, 44 );
            this.chkThursday.Name = "chkThursday";
            this.chkThursday.Size = new System.Drawing.Size( 71, 17 );
            this.chkThursday.TabIndex = 3;
            this.chkThursday.Text = "Thursday";
            this.chkThursday.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point( 185, 20 );
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size( 77, 17 );
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Wendsday";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // chkTuesday
            // 
            this.chkTuesday.AutoSize = true;
            this.chkTuesday.Location = new System.Drawing.Point( 101, 20 );
            this.chkTuesday.Name = "chkTuesday";
            this.chkTuesday.Size = new System.Drawing.Size( 67, 17 );
            this.chkTuesday.TabIndex = 1;
            this.chkTuesday.Text = "Tuesday";
            this.chkTuesday.UseVisualStyleBackColor = true;
            // 
            // chkMonday
            // 
            this.chkMonday.AutoSize = true;
            this.chkMonday.Location = new System.Drawing.Point( 19, 20 );
            this.chkMonday.Name = "chkMonday";
            this.chkMonday.Size = new System.Drawing.Size( 64, 17 );
            this.chkMonday.TabIndex = 0;
            this.chkMonday.Text = "Monday";
            this.chkMonday.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point( 28, 289 );
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size( 124, 32 );
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler( this.btnSave_Click );
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point( 158, 289 );
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size( 124, 32 );
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler( this.btnCancel_Click );
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add( this.rtxtLog );
            this.tabPageLog.Location = new System.Drawing.Point( 4, 22 );
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Size = new System.Drawing.Size( 312, 337 );
            this.tabPageLog.TabIndex = 1;
            this.tabPageLog.Text = "Log";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // rtxtLog
            // 
            this.rtxtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtLog.Location = new System.Drawing.Point( 0, 0 );
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.ReadOnly = true;
            this.rtxtLog.Size = new System.Drawing.Size( 312, 337 );
            this.rtxtLog.TabIndex = 0;
            this.rtxtLog.Text = "";
            this.rtxtLog.TextChanged += new System.EventHandler( this.rtxtLog_TextChanged );
            // 
            // ShutdownTimer
            // 
            this.ShutdownTimer.Interval = 1000;
            this.ShutdownTimer.Tick += new System.EventHandler( this.ShutdownTimer_Tick );
            // 
            // UpdateGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 320, 363 );
            this.Controls.Add( this.tabMain );
            this.Font = new System.Drawing.Font( "Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
            this.MaximizeBox = false;
            this.Name = "UpdateGui";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XBMC Update";
            this.Load += new System.EventHandler( this.UpdateGui_Load );
            this.Shown += new System.EventHandler( this.UpdateGui_Shown );
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler( this.UpdateGui_FormClosed );
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.UpdateGui_FormClosing );
            this.grpXbmcPath.ResumeLayout( false );
            this.grpXbmcPath.PerformLayout();
            ( (System.ComponentModel.ISupportInitialize)( this.picInstall ) ).EndInit();
            ( (System.ComponentModel.ISupportInitialize)( this.picDownload ) ).EndInit();
            ( (System.ComponentModel.ISupportInitialize)( this.picUnzip ) ).EndInit();
            ( (System.ComponentModel.ISupportInitialize)( this.picUpdateCheck ) ).EndInit();
            this.tabMain.ResumeLayout( false );
            this.tabPageUpdate.ResumeLayout( false );
            this.grpStatIcons.ResumeLayout( false );
            this.grpStatIcons.PerformLayout();
            this.grpStatus.ResumeLayout( false );
            ( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).EndInit();
            this.tabSettings.ResumeLayout( false );
            this.tabSettings.PerformLayout();
            this.groupBox1.ResumeLayout( false );
            this.groupBox1.PerformLayout();
            this.grpSchedule.ResumeLayout( false );
            this.grpSchedule.PerformLayout();
            this.tabPageLog.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.TextBox txtXbmcPath;
        private System.Windows.Forms.Button btnBrows;
        private System.Windows.Forms.FolderBrowserDialog xbmcFolderDialog;
        private System.Windows.Forms.GroupBox grpXbmcPath;
        private System.Windows.Forms.Button btnCheckUpdate;
        private System.Windows.Forms.Timer downloadRefreshTimer;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ToolTip iconToolTip;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPageUpdate;
        private System.Windows.Forms.PictureBox picInstall;
        private System.Windows.Forms.PictureBox picDownload;
        private System.Windows.Forms.PictureBox picUnzip;
        private System.Windows.Forms.PictureBox picUpdateCheck;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.GroupBox grpStatIcons;
        private System.Windows.Forms.TabPage tabPageLog;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox grpSchedule;
        private System.Windows.Forms.CheckBox chkTuesday;
        private System.Windows.Forms.CheckBox chkMonday;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox chkFriday;
        private System.Windows.Forms.CheckBox ckhSunday;
        private System.Windows.Forms.CheckBox chkSaturday;
        private System.Windows.Forms.CheckBox chkThursday;
        private System.Windows.Forms.CheckBox chkAutoUpdate;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblReleaseUrl;
        private System.Windows.Forms.TextBox txtReleaseUrl;
        private System.Windows.Forms.Button btnCancel;
        private Timer ShutdownTimer;
        private RichTextBox rtxtLog;
    }
}

