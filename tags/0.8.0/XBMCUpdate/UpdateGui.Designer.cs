using System.Deployment;
using System.Windows.Forms;
namespace XbmcUpdate
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
            this.xbmcFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.downloadRefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.iconToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.chkUpdateIfXbmcIsRunning = new System.Windows.Forms.CheckBox();
            this.picDownload = new System.Windows.Forms.PictureBox();
            this.picUnzip = new System.Windows.Forms.PictureBox();
            this.picUpdateCheck = new System.Windows.Forms.PictureBox();
            this.picInstall = new System.Windows.Forms.PictureBox();
            this.ShutdownTimer = new System.Windows.Forms.Timer(this.components);
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.chkPreventStandby = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtXbmcStartArgs = new System.Windows.Forms.TextBox();
            this.cmbXbmcStart = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpSource = new System.Windows.Forms.GroupBox();
            this.txtSrcRegex = new System.Windows.Forms.TextBox();
            this.lblSrcRegex = new System.Windows.Forms.Label();
            this.lnkSource = new System.Windows.Forms.LinkLabel();
            this.cmbSources = new System.Windows.Forms.ComboBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.tabPageUpdate = new System.Windows.Forms.TabPage();
            this.grpStatIcons = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnCheckUpdate = new System.Windows.Forms.Button();
            this.grpXbmcVersion = new System.Windows.Forms.GroupBox();
            this.lblXbmcVersion = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.grpXbmcPath = new System.Windows.Forms.GroupBox();
            this.btnBrows = new System.Windows.Forms.Button();
            this.txtXbmcPath = new System.Windows.Forms.TextBox();
            this.tabMain = new System.Windows.Forms.TabControl();
            ((System.ComponentModel.ISupportInitialize)(this.picDownload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUnzip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUpdateCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picInstall)).BeginInit();
            this.tabSettings.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpSource.SuspendLayout();
            this.tabPageUpdate.SuspendLayout();
            this.grpStatIcons.SuspendLayout();
            this.grpXbmcVersion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpXbmcPath.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // xbmcFolderDialog
            // 
            this.xbmcFolderDialog.Description = "XBMC Installation Folder";
            this.xbmcFolderDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // downloadRefreshTimer
            // 
            this.downloadRefreshTimer.Interval = 200;
            this.downloadRefreshTimer.Tick += new System.EventHandler(this.downloadRefreshTimer_Tick);
            // 
            // iconToolTip
            // 
            this.iconToolTip.AutoPopDelay = 10000;
            this.iconToolTip.InitialDelay = 500;
            this.iconToolTip.ReshowDelay = 100;
            // 
            // chkUpdateIfXbmcIsRunning
            // 
            this.chkUpdateIfXbmcIsRunning.AutoSize = true;
            this.chkUpdateIfXbmcIsRunning.Location = new System.Drawing.Point(7, 75);
            this.chkUpdateIfXbmcIsRunning.Name = "chkUpdateIfXbmcIsRunning";
            this.chkUpdateIfXbmcIsRunning.Size = new System.Drawing.Size(222, 17);
            this.chkUpdateIfXbmcIsRunning.TabIndex = 5;
            this.chkUpdateIfXbmcIsRunning.Text = "Automatically close XBMC prior to update";
            this.iconToolTip.SetToolTip(this.chkUpdateIfXbmcIsRunning, "This will cause XBMCUpdate to download but skip installation an instance of XBMC " +
                    "is detected");
            this.chkUpdateIfXbmcIsRunning.UseVisualStyleBackColor = true;
            // 
            // picDownload
            // 
            this.picDownload.ErrorImage = global::XbmcUpdate.Properties.Resources.download_blue;
            this.picDownload.Image = global::XbmcUpdate.Properties.Resources.download_blue;
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
            this.picUnzip.Image = global::XbmcUpdate.Properties.Resources.unzip_blue;
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
            this.picUpdateCheck.Image = global::XbmcUpdate.Properties.Resources.feed_blue;
            this.picUpdateCheck.Location = new System.Drawing.Point(52, 15);
            this.picUpdateCheck.Name = "picUpdateCheck";
            this.picUpdateCheck.Size = new System.Drawing.Size(32, 32);
            this.picUpdateCheck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picUpdateCheck.TabIndex = 12;
            this.picUpdateCheck.TabStop = false;
            this.iconToolTip.SetToolTip(this.picUpdateCheck, "Check Updates Status");
            // 
            // picInstall
            // 
            this.picInstall.Image = global::XbmcUpdate.Properties.Resources.install_blue;
            this.picInstall.InitialImage = global::XbmcUpdate.Properties.Resources.install_blue;
            this.picInstall.Location = new System.Drawing.Point(195, 15);
            this.picInstall.Name = "picInstall";
            this.picInstall.Size = new System.Drawing.Size(32, 32);
            this.picInstall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picInstall.TabIndex = 15;
            this.picInstall.TabStop = false;
            this.iconToolTip.SetToolTip(this.picInstall, "Install Status");
            // 
            // ShutdownTimer
            // 
            this.ShutdownTimer.Interval = 1000;
            this.ShutdownTimer.Tick += new System.EventHandler(this.ShutdownTimer_Tick);
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.chkPreventStandby);
            this.tabSettings.Controls.Add(this.groupBox2);
            this.tabSettings.Controls.Add(this.grpSource);
            this.tabSettings.Controls.Add(this.btnApply);
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Size = new System.Drawing.Size(312, 366);
            this.tabSettings.TabIndex = 2;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // chkPreventStandby
            // 
            this.chkPreventStandby.AutoSize = true;
            this.chkPreventStandby.Location = new System.Drawing.Point(16, 231);
            this.chkPreventStandby.Name = "chkPreventStandby";
            this.chkPreventStandby.Size = new System.Drawing.Size(176, 17);
            this.chkPreventStandby.TabIndex = 6;
            this.chkPreventStandby.Text = "Prevent standby during update";
            this.chkPreventStandby.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtXbmcStartArgs);
            this.groupBox2.Controls.Add(this.cmbXbmcStart);
            this.groupBox2.Controls.Add(this.chkUpdateIfXbmcIsRunning);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(16, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 101);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "XBMC";
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
            // txtXbmcStartArgs
            // 
            this.txtXbmcStartArgs.Location = new System.Drawing.Point(74, 48);
            this.txtXbmcStartArgs.Name = "txtXbmcStartArgs";
            this.txtXbmcStartArgs.Size = new System.Drawing.Size(200, 21);
            this.txtXbmcStartArgs.TabIndex = 7;
            // 
            // cmbXbmcStart
            // 
            this.cmbXbmcStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbXbmcStart.FormattingEnabled = true;
            this.cmbXbmcStart.Items.AddRange(new object[] {
            "Never start XBMC after an update",
            "Always start XBMC after an update",
            "Only start XBMC if it was closed by XBMCUpdate"});
            this.cmbXbmcStart.Location = new System.Drawing.Point(7, 20);
            this.cmbXbmcStart.Name = "cmbXbmcStart";
            this.cmbXbmcStart.Size = new System.Drawing.Size(267, 21);
            this.cmbXbmcStart.TabIndex = 6;
            this.cmbXbmcStart.SelectedIndexChanged += new System.EventHandler(this.cmbXbmcStart_SelectedIndexChanged);
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
            // grpSource
            // 
            this.grpSource.Controls.Add(this.txtSrcRegex);
            this.grpSource.Controls.Add(this.lblSrcRegex);
            this.grpSource.Controls.Add(this.lnkSource);
            this.grpSource.Controls.Add(this.cmbSources);
            this.grpSource.Location = new System.Drawing.Point(16, 122);
            this.grpSource.Name = "grpSource";
            this.grpSource.Size = new System.Drawing.Size(280, 103);
            this.grpSource.TabIndex = 4;
            this.grpSource.TabStop = false;
            this.grpSource.Text = "Update Source";
            // 
            // txtSrcRegex
            // 
            this.txtSrcRegex.BackColor = System.Drawing.SystemColors.Window;
            this.txtSrcRegex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSrcRegex.Location = new System.Drawing.Point(62, 73);
            this.txtSrcRegex.Name = "txtSrcRegex";
            this.txtSrcRegex.ReadOnly = true;
            this.txtSrcRegex.Size = new System.Drawing.Size(208, 14);
            this.txtSrcRegex.TabIndex = 8;
            // 
            // lblSrcRegex
            // 
            this.lblSrcRegex.AutoSize = true;
            this.lblSrcRegex.Location = new System.Drawing.Point(9, 74);
            this.lblSrcRegex.Name = "lblSrcRegex";
            this.lblSrcRegex.Size = new System.Drawing.Size(47, 13);
            this.lblSrcRegex.TabIndex = 7;
            this.lblSrcRegex.Text = "Pattern:";
            // 
            // lnkSource
            // 
            this.lnkSource.AutoSize = true;
            this.lnkSource.Location = new System.Drawing.Point(9, 51);
            this.lnkSource.Name = "lnkSource";
            this.lnkSource.Size = new System.Drawing.Size(53, 13);
            this.lnkSource.TabIndex = 5;
            this.lnkSource.TabStop = true;
            this.lnkSource.Text = "linkLabel1";
            // 
            // cmbSources
            // 
            this.cmbSources.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSources.FormattingEnabled = true;
            this.cmbSources.Location = new System.Drawing.Point(10, 20);
            this.cmbSources.Name = "cmbSources";
            this.cmbSources.Size = new System.Drawing.Size(262, 21);
            this.cmbSources.TabIndex = 4;
            this.cmbSources.SelectedIndexChanged += new System.EventHandler(this.cmbSources_SelectedIndexChanged);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(114, 322);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(85, 26);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // tabPageUpdate
            // 
            this.tabPageUpdate.Controls.Add(this.grpStatIcons);
            this.tabPageUpdate.Controls.Add(this.btnCheckUpdate);
            this.tabPageUpdate.Controls.Add(this.grpXbmcVersion);
            this.tabPageUpdate.Controls.Add(this.pictureBox1);
            this.tabPageUpdate.Controls.Add(this.grpXbmcPath);
            this.tabPageUpdate.Location = new System.Drawing.Point(4, 22);
            this.tabPageUpdate.Name = "tabPageUpdate";
            this.tabPageUpdate.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUpdate.Size = new System.Drawing.Size(312, 366);
            this.tabPageUpdate.TabIndex = 0;
            this.tabPageUpdate.Text = "Update";
            this.tabPageUpdate.UseVisualStyleBackColor = true;
            // 
            // grpStatIcons
            // 
            this.grpStatIcons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpStatIcons.Controls.Add(this.lblStatus);
            this.grpStatIcons.Controls.Add(this.picInstall);
            this.grpStatIcons.Controls.Add(this.picUpdateCheck);
            this.grpStatIcons.Controls.Add(this.picUnzip);
            this.grpStatIcons.Controls.Add(this.picDownload);
            this.grpStatIcons.Location = new System.Drawing.Point(16, 164);
            this.grpStatIcons.Name = "grpStatIcons";
            this.grpStatIcons.Size = new System.Drawing.Size(280, 96);
            this.grpStatIcons.TabIndex = 19;
            this.grpStatIcons.TabStop = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(133)))), ((int)(((byte)(42)))));
            this.lblStatus.Location = new System.Drawing.Point(7, 50);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(267, 43);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "Update Status";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCheckUpdate
            // 
            this.btnCheckUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheckUpdate.Location = new System.Drawing.Point(92, 328);
            this.btnCheckUpdate.Name = "btnCheckUpdate";
            this.btnCheckUpdate.Size = new System.Drawing.Size(124, 32);
            this.btnCheckUpdate.TabIndex = 9;
            this.btnCheckUpdate.Text = "Check for updates";
            this.btnCheckUpdate.UseVisualStyleBackColor = true;
            this.btnCheckUpdate.Click += new System.EventHandler(this.btnCheckUpdate_Click);
            // 
            // grpXbmcVersion
            // 
            this.grpXbmcVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpXbmcVersion.Controls.Add(this.lblXbmcVersion);
            this.grpXbmcVersion.Location = new System.Drawing.Point(16, 260);
            this.grpXbmcVersion.Name = "grpXbmcVersion";
            this.grpXbmcVersion.Size = new System.Drawing.Size(280, 65);
            this.grpXbmcVersion.TabIndex = 18;
            this.grpXbmcVersion.TabStop = false;
            // 
            // lblXbmcVersion
            // 
            this.lblXbmcVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblXbmcVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblXbmcVersion.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXbmcVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(194)))), ((int)(((byte)(38)))));
            this.lblXbmcVersion.Location = new System.Drawing.Point(7, 17);
            this.lblXbmcVersion.Name = "lblXbmcVersion";
            this.lblXbmcVersion.Size = new System.Drawing.Size(267, 38);
            this.lblXbmcVersion.TabIndex = 12;
            this.lblXbmcVersion.Text = "Analyzing your XBMC installation";
            this.lblXbmcVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(16, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(280, 101);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // grpXbmcPath
            // 
            this.grpXbmcPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpXbmcPath.Controls.Add(this.btnBrows);
            this.grpXbmcPath.Controls.Add(this.txtXbmcPath);
            this.grpXbmcPath.Location = new System.Drawing.Point(16, 109);
            this.grpXbmcPath.Name = "grpXbmcPath";
            this.grpXbmcPath.Size = new System.Drawing.Size(280, 55);
            this.grpXbmcPath.TabIndex = 8;
            this.grpXbmcPath.TabStop = false;
            this.grpXbmcPath.Text = "XBMC Location";
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
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPageUpdate);
            this.tabMain.Controls.Add(this.tabSettings);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(320, 392);
            this.tabMain.TabIndex = 14;
            // 
            // UpdateGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(320, 392);
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
            ((System.ComponentModel.ISupportInitialize)(this.picDownload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUnzip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUpdateCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picInstall)).EndInit();
            this.tabSettings.ResumeLayout(false);
            this.tabSettings.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpSource.ResumeLayout(false);
            this.grpSource.PerformLayout();
            this.tabPageUpdate.ResumeLayout(false);
            this.grpStatIcons.ResumeLayout(false);
            this.grpStatIcons.PerformLayout();
            this.grpXbmcVersion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpXbmcPath.ResumeLayout(false);
            this.grpXbmcPath.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog xbmcFolderDialog;
        private System.Windows.Forms.Timer downloadRefreshTimer;
        private System.Windows.Forms.ToolTip iconToolTip;
        private Timer ShutdownTimer;
        private TabPage tabSettings;
        private CheckBox chkPreventStandby;
        private GroupBox groupBox2;
        private Label label1;
        private TextBox txtXbmcStartArgs;
        private ComboBox cmbXbmcStart;
        private CheckBox chkUpdateIfXbmcIsRunning;
        private Label label2;
        private GroupBox grpSource;
        private Button btnApply;
        private TabPage tabPageUpdate;
        private GroupBox grpStatIcons;
        private Label lblStatus;
        private PictureBox picInstall;
        private PictureBox picUpdateCheck;
        private PictureBox picUnzip;
        private PictureBox picDownload;
        private Button btnCheckUpdate;
        private GroupBox grpXbmcVersion;
        private Label lblXbmcVersion;
        private PictureBox pictureBox1;
        private GroupBox grpXbmcPath;
        private Button btnBrows;
        private TextBox txtXbmcPath;
        private TabControl tabMain;
        private ComboBox cmbSources;
        private TextBox txtSrcRegex;
        private Label lblSrcRegex;
        private LinkLabel lnkSource;
    }
}

