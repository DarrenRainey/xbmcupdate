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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateGui));
            this.btnBrows = new System.Windows.Forms.Button();
            this.xbmcFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.grpXbmcPath = new System.Windows.Forms.GroupBox();
            this.txtXbmcPath = new System.Windows.Forms.TextBox();
            this.btnCheckUpdate = new System.Windows.Forms.Button();
            this.downloadRefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.lblStatus = new System.Windows.Forms.Label();
            this.iconToolTip = new System.Windows.Forms.ToolTip(this.components);
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
            this.txtReleaseUrl = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.rtxtLog = new System.Windows.Forms.RichTextBox();
            this.ShutdownTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkUpdateIfXbmcIsRunning = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpXbmcPath.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInstall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDownload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUnzip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUpdateCheck)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tabPageUpdate.SuspendLayout();
            this.grpStatIcons.SuspendLayout();
            this.grpStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabSettings.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBrows
            // 
            this.btnBrows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrows.Location = new System.Drawing.Point(248, 20);
            this.btnBrows.Name = "btnBrows";
            this.btnBrows.Size = new System.Drawing.Size(26, 20);
            this.btnBrows.TabIndex = 3;
            this.btnBrows.Text = "...";
            this.btnBrows.UseVisualStyleBackColor = true;
            this.btnBrows.Click += new System.EventHandler(this.btnBrows_Click);
            // 
            // xbmcFolderDialog
            // 
            this.xbmcFolderDialog.Description = "XBMC Installation Folder";
            this.xbmcFolderDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // grpXbmcPath
            // 
            this.grpXbmcPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.grpXbmcPath.Controls.Add(this.btnBrows);
            this.grpXbmcPath.Controls.Add(this.txtXbmcPath);
            this.grpXbmcPath.Location = new System.Drawing.Point(16, 109);
            this.grpXbmcPath.Name = "grpXbmcPath";
            this.grpXbmcPath.Size = new System.Drawing.Size(280, 55);
            this.grpXbmcPath.TabIndex = 8;
            this.grpXbmcPath.TabStop = false;
            this.grpXbmcPath.Text = "XBMC Location";
            // 
            // txtXbmcPath
            // 
            this.txtXbmcPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtXbmcPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtXbmcPath.Location = new System.Drawing.Point(6, 20);
            this.txtXbmcPath.Multiline = true;
            this.txtXbmcPath.Name = "txtXbmcPath";
            this.txtXbmcPath.ReadOnly = true;
            this.txtXbmcPath.Size = new System.Drawing.Size(236, 20);
            this.txtXbmcPath.TabIndex = 2;
            this.txtXbmcPath.TextChanged += new System.EventHandler(this.txtXbmcPath_TextChanged);
            // 
            // btnCheckUpdate
            // 
            this.btnCheckUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheckUpdate.Location = new System.Drawing.Point(93, 299);
            this.btnCheckUpdate.Name = "btnCheckUpdate";
            this.btnCheckUpdate.Size = new System.Drawing.Size(124, 32);
            this.btnCheckUpdate.TabIndex = 9;
            this.btnCheckUpdate.Text = "Check for updates";
            this.btnCheckUpdate.UseVisualStyleBackColor = true;
            this.btnCheckUpdate.Click += new System.EventHandler(this.btnCheckUpdate_Click);
            // 
            // downloadRefreshTimer
            // 
            this.downloadRefreshTimer.Interval = 200;
            this.downloadRefreshTimer.Tick += new System.EventHandler(this.downloadRefreshTimer_Tick);
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblStatus.Location = new System.Drawing.Point(6, 16);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(267, 41);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "Latest Build: 12345\r\nInstalled 3 day(s) ago";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iconToolTip
            // 
            this.iconToolTip.AutoPopDelay = 10000;
            this.iconToolTip.InitialDelay = 500;
            this.iconToolTip.ReshowDelay = 100;
            // 
            // picInstall
            // 
            this.picInstall.Image = global::XbmcUpdate.Runtime.Properties.Resources.install_blue;
            this.picInstall.InitialImage = global::XbmcUpdate.Runtime.Properties.Resources.install_blue;
            this.picInstall.Location = new System.Drawing.Point(195, 15);
            this.picInstall.Name = "picInstall";
            this.picInstall.Size = new System.Drawing.Size(32, 32);
            this.picInstall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picInstall.TabIndex = 15;
            this.picInstall.TabStop = false;
            this.iconToolTip.SetToolTip(this.picInstall, "Install Status");
            // 
            // picDownload
            // 
            this.picDownload.ErrorImage = global::XbmcUpdate.Runtime.Properties.Resources.download_blue;
            this.picDownload.Image = global::XbmcUpdate.Runtime.Properties.Resources.download_blue;
            this.picDownload.Location = new System.Drawing.Point(101, 15);
            this.picDownload.Name = "picDownload";
            this.picDownload.Size = new System.Drawing.Size(32, 32);
            this.picDownload.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picDownload.TabIndex = 14;
            this.picDownload.TabStop = false;
            this.iconToolTip.SetToolTip(this.picDownload, "Download Status");
            // 
            // picUnzip
            // 
            this.picUnzip.Image = global::XbmcUpdate.Runtime.Properties.Resources.unzip_blue;
            this.picUnzip.Location = new System.Drawing.Point(148, 15);
            this.picUnzip.Name = "picUnzip";
            this.picUnzip.Size = new System.Drawing.Size(32, 32);
            this.picUnzip.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picUnzip.TabIndex = 13;
            this.picUnzip.TabStop = false;
            this.iconToolTip.SetToolTip(this.picUnzip, "Extract Status");
            // 
            // picUpdateCheck
            // 
            this.picUpdateCheck.Image = global::XbmcUpdate.Runtime.Properties.Resources.feed_blue;
            this.picUpdateCheck.Location = new System.Drawing.Point(52, 15);
            this.picUpdateCheck.Name = "picUpdateCheck";
            this.picUpdateCheck.Size = new System.Drawing.Size(32, 32);
            this.picUpdateCheck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picUpdateCheck.TabIndex = 12;
            this.picUpdateCheck.TabStop = false;
            this.iconToolTip.SetToolTip(this.picUpdateCheck, "Check Updates Status");
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPageUpdate);
            this.tabMain.Controls.Add(this.tabSettings);
            this.tabMain.Controls.Add(this.tabPageLog);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(320, 363);
            this.tabMain.TabIndex = 14;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // tabPageUpdate
            // 
            this.tabPageUpdate.Controls.Add(this.grpStatIcons);
            this.tabPageUpdate.Controls.Add(this.btnCheckUpdate);
            this.tabPageUpdate.Controls.Add(this.grpStatus);
            this.tabPageUpdate.Controls.Add(this.pictureBox1);
            this.tabPageUpdate.Controls.Add(this.grpXbmcPath);
            this.tabPageUpdate.Location = new System.Drawing.Point(4, 22);
            this.tabPageUpdate.Name = "tabPageUpdate";
            this.tabPageUpdate.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUpdate.Size = new System.Drawing.Size(312, 337);
            this.tabPageUpdate.TabIndex = 0;
            this.tabPageUpdate.Text = "Update";
            this.tabPageUpdate.UseVisualStyleBackColor = true;
            // 
            // grpStatIcons
            // 
            this.grpStatIcons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.grpStatIcons.Controls.Add(this.picInstall);
            this.grpStatIcons.Controls.Add(this.picUpdateCheck);
            this.grpStatIcons.Controls.Add(this.picUnzip);
            this.grpStatIcons.Controls.Add(this.picDownload);
            this.grpStatIcons.Location = new System.Drawing.Point(16, 164);
            this.grpStatIcons.Name = "grpStatIcons";
            this.grpStatIcons.Size = new System.Drawing.Size(280, 62);
            this.grpStatIcons.TabIndex = 19;
            this.grpStatIcons.TabStop = false;
            // 
            // grpStatus
            // 
            this.grpStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.grpStatus.Controls.Add(this.lblStatus);
            this.grpStatus.Location = new System.Drawing.Point(16, 226);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(280, 67);
            this.grpStatus.TabIndex = 18;
            this.grpStatus.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(36, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(241, 101);
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.groupBox2);
            this.tabSettings.Controls.Add(this.groupBox1);
            this.tabSettings.Controls.Add(this.btnSave);
            this.tabSettings.Controls.Add(this.btnCancel);
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Size = new System.Drawing.Size(312, 337);
            this.tabSettings.TabIndex = 2;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtReleaseUrl);
            this.groupBox1.Location = new System.Drawing.Point(16, 167);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 63);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Release URL";
            // 
            // txtReleaseUrl
            // 
            this.txtReleaseUrl.Location = new System.Drawing.Point(7, 18);
            this.txtReleaseUrl.Multiline = true;
            this.txtReleaseUrl.Name = "txtReleaseUrl";
            this.txtReleaseUrl.Size = new System.Drawing.Size(267, 36);
            this.txtReleaseUrl.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(28, 289);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(124, 32);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(158, 289);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(124, 32);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.rtxtLog);
            this.tabPageLog.Location = new System.Drawing.Point(4, 22);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Size = new System.Drawing.Size(312, 337);
            this.tabPageLog.TabIndex = 1;
            this.tabPageLog.Text = "Log";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // rtxtLog
            // 
            this.rtxtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtLog.Location = new System.Drawing.Point(0, 0);
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.ReadOnly = true;
            this.rtxtLog.Size = new System.Drawing.Size(312, 337);
            this.rtxtLog.TabIndex = 0;
            this.rtxtLog.Text = "";
            this.rtxtLog.TextChanged += new System.EventHandler(this.rtxtLog_TextChanged);
            // 
            // ShutdownTimer
            // 
            this.ShutdownTimer.Interval = 1000;
            this.ShutdownTimer.Tick += new System.EventHandler(this.ShutdownTimer_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.chkUpdateIfXbmcIsRunning);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(16, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 114);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "XBMC Shutdown";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-422, -217);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // chkUpdateIfXbmcIsRunning
            // 
            this.chkUpdateIfXbmcIsRunning.AutoSize = true;
            this.chkUpdateIfXbmcIsRunning.Location = new System.Drawing.Point(7, 91);
            this.chkUpdateIfXbmcIsRunning.Name = "chkUpdateIfXbmcIsRunning";
            this.chkUpdateIfXbmcIsRunning.Size = new System.Drawing.Size(225, 17);
            this.chkUpdateIfXbmcIsRunning.TabIndex = 5;
            this.chkUpdateIfXbmcIsRunning.Text = "Skip Automatic Update if XBMC is Running";
            this.iconToolTip.SetToolTip(this.chkUpdateIfXbmcIsRunning, "This will cause XBMCUpdate to download but skip installation an instance of XBMC " +
                    "is detected");
            this.chkUpdateIfXbmcIsRunning.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Never start XBMC after an update",
            "Always start XBMC after an update",
            "Only start XBMC if it was closed by XBMCUpdate"});
            this.comboBox1.Location = new System.Drawing.Point(7, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(267, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(74, 48);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 21);
            this.textBox1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Arguments";
            // 
            // UpdateGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 363);
            this.Controls.Add(this.tabMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UpdateGui";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XBMCUpdate";
            this.Load += new System.EventHandler(this.UpdateGui_Load);
            this.Shown += new System.EventHandler(this.UpdateGui_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UpdateGui_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpdateGui_FormClosing);
            this.grpXbmcPath.ResumeLayout(false);
            this.grpXbmcPath.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInstall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDownload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUnzip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUpdateCheck)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tabPageUpdate.ResumeLayout(false);
            this.grpStatIcons.ResumeLayout(false);
            this.grpStatIcons.PerformLayout();
            this.grpStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabSettings.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageLog.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtReleaseUrl;
        private System.Windows.Forms.Button btnCancel;
        private Timer ShutdownTimer;
        private RichTextBox rtxtLog;
        private GroupBox groupBox2;
        private Label label2;
        private CheckBox chkUpdateIfXbmcIsRunning;
        private ComboBox comboBox1;
        private Label label1;
        private TextBox textBox1;
    }
}

