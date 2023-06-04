
namespace PintoSetupNS
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnInstall = new System.Windows.Forms.Button();
            this.lTitle = new System.Windows.Forms.Label();
            this.lLocation = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.cbCreateDesktopIcon = new System.Windows.Forms.CheckBox();
            this.cbLaunchAfterInstall = new System.Windows.Forms.CheckBox();
            this.cbStartOnBoot = new System.Windows.Forms.CheckBox();
            this.fbdBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.pFirstStage = new System.Windows.Forms.Panel();
            this.pbInstall = new System.Windows.Forms.PictureBox();
            this.pSecondStage = new System.Windows.Forms.Panel();
            this.pbInstallProgress = new System.Windows.Forms.ProgressBar();
            this.lInstallStatus2 = new System.Windows.Forms.Label();
            this.lInstallStatus1 = new System.Windows.Forms.Label();
            this.pFirstStage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbInstall)).BeginInit();
            this.pSecondStage.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(371, 296);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnInstall
            // 
            this.btnInstall.Location = new System.Drawing.Point(286, 296);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(75, 23);
            this.btnInstall.TabIndex = 2;
            this.btnInstall.Text = "Install";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // lTitle
            // 
            this.lTitle.AutoSize = true;
            this.lTitle.BackColor = System.Drawing.Color.Transparent;
            this.lTitle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lTitle.Location = new System.Drawing.Point(18, 27);
            this.lTitle.Name = "lTitle";
            this.lTitle.Size = new System.Drawing.Size(264, 16);
            this.lTitle.TabIndex = 3;
            this.lTitle.Text = "Hello! Thank you for downloading Pinto!";
            // 
            // lLocation
            // 
            this.lLocation.AutoSize = true;
            this.lLocation.BackColor = System.Drawing.Color.Transparent;
            this.lLocation.Location = new System.Drawing.Point(24, 168);
            this.lLocation.Name = "lLocation";
            this.lLocation.Size = new System.Drawing.Size(192, 13);
            this.lLocation.TabIndex = 5;
            this.lLocation.Text = "Select where Pinto! should be installed:";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(27, 185);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(189, 20);
            this.txtPath.TabIndex = 6;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(222, 183);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 7;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // cbCreateDesktopIcon
            // 
            this.cbCreateDesktopIcon.AutoSize = true;
            this.cbCreateDesktopIcon.BackColor = System.Drawing.Color.Transparent;
            this.cbCreateDesktopIcon.Checked = true;
            this.cbCreateDesktopIcon.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCreateDesktopIcon.Location = new System.Drawing.Point(27, 211);
            this.cbCreateDesktopIcon.Name = "cbCreateDesktopIcon";
            this.cbCreateDesktopIcon.Size = new System.Drawing.Size(130, 17);
            this.cbCreateDesktopIcon.TabIndex = 8;
            this.cbCreateDesktopIcon.Text = "Create a desktop icon";
            this.cbCreateDesktopIcon.UseVisualStyleBackColor = false;
            // 
            // cbLaunchAfterInstall
            // 
            this.cbLaunchAfterInstall.AutoSize = true;
            this.cbLaunchAfterInstall.BackColor = System.Drawing.Color.Transparent;
            this.cbLaunchAfterInstall.Checked = true;
            this.cbLaunchAfterInstall.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLaunchAfterInstall.Location = new System.Drawing.Point(27, 233);
            this.cbLaunchAfterInstall.Name = "cbLaunchAfterInstall";
            this.cbLaunchAfterInstall.Size = new System.Drawing.Size(168, 17);
            this.cbLaunchAfterInstall.TabIndex = 9;
            this.cbLaunchAfterInstall.Text = "Launch Pinto! after installation";
            this.cbLaunchAfterInstall.UseVisualStyleBackColor = false;
            // 
            // cbStartOnBoot
            // 
            this.cbStartOnBoot.AutoSize = true;
            this.cbStartOnBoot.BackColor = System.Drawing.Color.Transparent;
            this.cbStartOnBoot.Enabled = false;
            this.cbStartOnBoot.Location = new System.Drawing.Point(27, 255);
            this.cbStartOnBoot.Name = "cbStartOnBoot";
            this.cbStartOnBoot.Size = new System.Drawing.Size(200, 17);
            this.cbStartOnBoot.TabIndex = 10;
            this.cbStartOnBoot.Text = "Start Pinto! when the computer starts";
            this.cbStartOnBoot.UseVisualStyleBackColor = false;
            // 
            // fbdBrowse
            // 
            this.fbdBrowse.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // pFirstStage
            // 
            this.pFirstStage.BackColor = System.Drawing.Color.Transparent;
            this.pFirstStage.Controls.Add(this.cbStartOnBoot);
            this.pFirstStage.Controls.Add(this.cbLaunchAfterInstall);
            this.pFirstStage.Controls.Add(this.cbCreateDesktopIcon);
            this.pFirstStage.Controls.Add(this.btnBrowse);
            this.pFirstStage.Controls.Add(this.txtPath);
            this.pFirstStage.Controls.Add(this.lLocation);
            this.pFirstStage.Controls.Add(this.pbInstall);
            this.pFirstStage.Controls.Add(this.btnInstall);
            this.pFirstStage.Controls.Add(this.btnCancel);
            this.pFirstStage.Location = new System.Drawing.Point(21, 60);
            this.pFirstStage.Name = "pFirstStage";
            this.pFirstStage.Size = new System.Drawing.Size(454, 336);
            this.pFirstStage.TabIndex = 11;
            // 
            // pbInstall
            // 
            this.pbInstall.BackColor = System.Drawing.Color.Transparent;
            this.pbInstall.Image = global::PintoSetupNS.Properties.Resources.SETUP;
            this.pbInstall.Location = new System.Drawing.Point(27, 8);
            this.pbInstall.Name = "pbInstall";
            this.pbInstall.Size = new System.Drawing.Size(390, 153);
            this.pbInstall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbInstall.TabIndex = 4;
            this.pbInstall.TabStop = false;
            // 
            // pSecondStage
            // 
            this.pSecondStage.BackColor = System.Drawing.Color.Transparent;
            this.pSecondStage.Controls.Add(this.pbInstallProgress);
            this.pSecondStage.Controls.Add(this.lInstallStatus2);
            this.pSecondStage.Controls.Add(this.lInstallStatus1);
            this.pSecondStage.Enabled = false;
            this.pSecondStage.Location = new System.Drawing.Point(21, 60);
            this.pSecondStage.Name = "pSecondStage";
            this.pSecondStage.Size = new System.Drawing.Size(454, 336);
            this.pSecondStage.TabIndex = 11;
            this.pSecondStage.Visible = false;
            // 
            // pbInstallProgress
            // 
            this.pbInstallProgress.Location = new System.Drawing.Point(6, 97);
            this.pbInstallProgress.Name = "pbInstallProgress";
            this.pbInstallProgress.Size = new System.Drawing.Size(440, 23);
            this.pbInstallProgress.TabIndex = 3;
            // 
            // lInstallStatus2
            // 
            this.lInstallStatus2.AutoSize = true;
            this.lInstallStatus2.Location = new System.Drawing.Point(7, 67);
            this.lInstallStatus2.Name = "lInstallStatus2";
            this.lInstallStatus2.Size = new System.Drawing.Size(84, 13);
            this.lInstallStatus2.TabIndex = 1;
            this.lInstallStatus2.Text = "Extracting files...";
            // 
            // lInstallStatus1
            // 
            this.lInstallStatus1.AutoSize = true;
            this.lInstallStatus1.Location = new System.Drawing.Point(7, 8);
            this.lInstallStatus1.Name = "lInstallStatus1";
            this.lInstallStatus1.Size = new System.Drawing.Size(254, 13);
            this.lInstallStatus1.TabIndex = 0;
            this.lInstallStatus1.Text = "Please wait while Pinto! is installed on your computer";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 401);
            this.Controls.Add(this.pSecondStage);
            this.Controls.Add(this.pFirstStage);
            this.Controls.Add(this.lTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pinto! - Install";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pFirstStage.ResumeLayout(false);
            this.pFirstStage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbInstall)).EndInit();
            this.pSecondStage.ResumeLayout(false);
            this.pSecondStage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnInstall;
        public System.Windows.Forms.Label lTitle;
        public System.Windows.Forms.PictureBox pbInstall;
        public System.Windows.Forms.Label lLocation;
        public System.Windows.Forms.TextBox txtPath;
        public System.Windows.Forms.Button btnBrowse;
        public System.Windows.Forms.CheckBox cbCreateDesktopIcon;
        public System.Windows.Forms.CheckBox cbLaunchAfterInstall;
        public System.Windows.Forms.CheckBox cbStartOnBoot;
        public System.Windows.Forms.FolderBrowserDialog fbdBrowse;
        public System.Windows.Forms.Panel pFirstStage;
        public System.Windows.Forms.Panel pSecondStage;
        public System.Windows.Forms.Label lInstallStatus1;
        public System.Windows.Forms.Label lInstallStatus2;
        public System.Windows.Forms.ProgressBar pbInstallProgress;
    }
}

