namespace PintoNS
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.tsddbMenuPinto = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddbMenuTools = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddbMenuHelp = new System.Windows.Forms.ToolStripDropDownButton();
            this.pLoginContainer = new System.Windows.Forms.Panel();
            this.pError = new System.Windows.Forms.Panel();
            this.lErrorText = new System.Windows.Forms.Label();
            this.pbErrorIcon = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pOptions = new System.Windows.Forms.Panel();
            this.cbSavePassword = new System.Windows.Forms.CheckBox();
            this.nudPort = new System.Windows.Forms.NumericUpDown();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.ComboBox();
            this.llServers = new System.Windows.Forms.LinkLabel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.llForgotPassword = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnConnect = new PintoNS.Controls.LoginButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pLoggingin = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.loader1 = new PintoNS.Controls.Loader();
            this.tsMenu.SuspendLayout();
            this.pLoginContainer.SuspendLayout();
            this.pError.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbErrorIcon)).BeginInit();
            this.pOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
            this.pLoggingin.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsMenu
            // 
            this.tsMenu.AutoSize = false;
            this.tsMenu.BackColor = System.Drawing.SystemColors.Window;
            this.tsMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsddbMenuPinto,
            this.tsddbMenuTools,
            this.tsddbMenuHelp});
            this.tsMenu.Location = new System.Drawing.Point(0, 0);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsMenu.Size = new System.Drawing.Size(509, 22);
            this.tsMenu.TabIndex = 10;
            this.tsMenu.Text = "tsMenuBar";
            // 
            // tsddbMenuPinto
            // 
            this.tsddbMenuPinto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddbMenuPinto.Image = ((System.Drawing.Image)(resources.GetObject("tsddbMenuPinto.Image")));
            this.tsddbMenuPinto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbMenuPinto.Name = "tsddbMenuPinto";
            this.tsddbMenuPinto.ShowDropDownArrow = false;
            this.tsddbMenuPinto.Size = new System.Drawing.Size(39, 19);
            this.tsddbMenuPinto.Text = "Pinto";
            // 
            // tsddbMenuTools
            // 
            this.tsddbMenuTools.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddbMenuTools.Image = ((System.Drawing.Image)(resources.GetObject("tsddbMenuTools.Image")));
            this.tsddbMenuTools.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbMenuTools.Name = "tsddbMenuTools";
            this.tsddbMenuTools.ShowDropDownArrow = false;
            this.tsddbMenuTools.Size = new System.Drawing.Size(38, 19);
            this.tsddbMenuTools.Text = "Tools";
            // 
            // tsddbMenuHelp
            // 
            this.tsddbMenuHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddbMenuHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsddbMenuHelp.Image")));
            this.tsddbMenuHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbMenuHelp.Name = "tsddbMenuHelp";
            this.tsddbMenuHelp.ShowDropDownArrow = false;
            this.tsddbMenuHelp.Size = new System.Drawing.Size(36, 19);
            this.tsddbMenuHelp.Text = "Help";
            // 
            // pLoginContainer
            // 
            this.pLoginContainer.BackColor = System.Drawing.Color.Transparent;
            this.pLoginContainer.BackgroundImage = global::PintoNS.Assets._22066;
            this.pLoginContainer.Controls.Add(this.pError);
            this.pLoginContainer.Controls.Add(this.label5);
            this.pLoginContainer.Controls.Add(this.pOptions);
            this.pLoginContainer.Controls.Add(this.nudPort);
            this.pLoginContainer.Controls.Add(this.txtIP);
            this.pLoginContainer.Controls.Add(this.txtUsername);
            this.pLoginContainer.Controls.Add(this.llServers);
            this.pLoginContainer.Controls.Add(this.txtPassword);
            this.pLoginContainer.Controls.Add(this.llForgotPassword);
            this.pLoginContainer.Controls.Add(this.label3);
            this.pLoginContainer.Controls.Add(this.btnConnect);
            this.pLoginContainer.Controls.Add(this.label4);
            this.pLoginContainer.Controls.Add(this.label1);
            this.pLoginContainer.Controls.Add(this.label2);
            this.pLoginContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pLoginContainer.Location = new System.Drawing.Point(0, 22);
            this.pLoginContainer.Name = "pLoginContainer";
            this.pLoginContainer.Size = new System.Drawing.Size(509, 364);
            this.pLoginContainer.TabIndex = 12;
            // 
            // pError
            // 
            this.pError.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(153)))));
            this.pError.Controls.Add(this.lErrorText);
            this.pError.Controls.Add(this.pbErrorIcon);
            this.pError.Location = new System.Drawing.Point(134, 79);
            this.pError.Name = "pError";
            this.pError.Size = new System.Drawing.Size(240, 30);
            this.pError.TabIndex = 12;
            this.pError.Visible = false;
            // 
            // lErrorText
            // 
            this.lErrorText.Location = new System.Drawing.Point(25, 3);
            this.lErrorText.Name = "lErrorText";
            this.lErrorText.Size = new System.Drawing.Size(212, 27);
            this.lErrorText.TabIndex = 1;
            this.lErrorText.Text = "Error";
            // 
            // pbErrorIcon
            // 
            this.pbErrorIcon.Image = global::PintoNS.Assets._21617;
            this.pbErrorIcon.Location = new System.Drawing.Point(3, 3);
            this.pbErrorIcon.Name = "pbErrorIcon";
            this.pbErrorIcon.Size = new System.Drawing.Size(16, 16);
            this.pbErrorIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbErrorIcon.TabIndex = 0;
            this.pbErrorIcon.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(240)))));
            this.label5.Location = new System.Drawing.Point(161, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(186, 28);
            this.label5.TabIndex = 11;
            this.label5.Text = "Welcome to Pinto.";
            // 
            // pOptions
            // 
            this.pOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(247)))), ((int)(((byte)(253)))));
            this.pOptions.Controls.Add(this.cbSavePassword);
            this.pOptions.Location = new System.Drawing.Point(119, 297);
            this.pOptions.Name = "pOptions";
            this.pOptions.Size = new System.Drawing.Size(271, 55);
            this.pOptions.TabIndex = 10;
            // 
            // cbSavePassword
            // 
            this.cbSavePassword.AutoSize = true;
            this.cbSavePassword.BackColor = System.Drawing.Color.Transparent;
            this.cbSavePassword.Checked = true;
            this.cbSavePassword.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSavePassword.Location = new System.Drawing.Point(15, 14);
            this.cbSavePassword.Name = "cbSavePassword";
            this.cbSavePassword.Size = new System.Drawing.Size(162, 17);
            this.cbSavePassword.TabIndex = 8;
            this.cbSavePassword.Text = "Sign me in when Pinto! starts";
            this.cbSavePassword.UseVisualStyleBackColor = false;
            this.cbSavePassword.CheckedChanged += new System.EventHandler(this.cbSavePassword_CheckedChanged);
            // 
            // nudPort
            // 
            this.nudPort.Location = new System.Drawing.Point(409, 71);
            this.nudPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPort.Name = "nudPort";
            this.nudPort.Size = new System.Drawing.Size(97, 20);
            this.nudPort.TabIndex = 5;
            this.nudPort.Value = new decimal(new int[] {
            2407,
            0,
            0,
            0});
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(409, 25);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(97, 20);
            this.txtIP.TabIndex = 4;
            this.txtIP.Text = "127.0.0.1";
            // 
            // txtUsername
            // 
            this.txtUsername.FormattingEnabled = true;
            this.txtUsername.Location = new System.Drawing.Point(134, 131);
            this.txtUsername.MaxLength = 16;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(240, 21);
            this.txtUsername.TabIndex = 2;
            // 
            // llServers
            // 
            this.llServers.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(229)))));
            this.llServers.AutoSize = true;
            this.llServers.BackColor = System.Drawing.Color.Transparent;
            this.llServers.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(229)))));
            this.llServers.Location = new System.Drawing.Point(131, 155);
            this.llServers.Name = "llServers";
            this.llServers.Size = new System.Drawing.Size(116, 13);
            this.llServers.TabIndex = 7;
            this.llServers.TabStop = true;
            this.llServers.Text = "Looking for all servers?";
            this.llServers.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(229)))));
            this.llServers.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llServers_LinkClicked);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(134, 198);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(240, 20);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // llForgotPassword
            // 
            this.llForgotPassword.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(229)))));
            this.llForgotPassword.AutoSize = true;
            this.llForgotPassword.BackColor = System.Drawing.Color.Transparent;
            this.llForgotPassword.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(229)))));
            this.llForgotPassword.Location = new System.Drawing.Point(131, 221);
            this.llForgotPassword.Name = "llForgotPassword";
            this.llForgotPassword.Size = new System.Drawing.Size(114, 13);
            this.llForgotPassword.TabIndex = 6;
            this.llForgotPassword.TabStop = true;
            this.llForgotPassword.Text = "Forgot your password?";
            this.llForgotPassword.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(229)))));
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(131, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Pinto! Name";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(134, 253);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(240, 24);
            this.btnConnect.TabIndex = 9;
            this.btnConnect.Text = "Sign in";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(131, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(408, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(406, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Port:";
            // 
            // pLoggingin
            // 
            this.pLoggingin.Controls.Add(this.label6);
            this.pLoggingin.Controls.Add(this.loader1);
            this.pLoggingin.Location = new System.Drawing.Point(105, 22);
            this.pLoggingin.Name = "pLoggingin";
            this.pLoggingin.Size = new System.Drawing.Size(300, 364);
            this.pLoggingin.TabIndex = 12;
            this.pLoggingin.Visible = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(240)))));
            this.label6.Location = new System.Drawing.Point(57, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(186, 28);
            this.label6.TabIndex = 12;
            this.label6.Text = "Signing in";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // loader1
            // 
            this.loader1.Location = new System.Drawing.Point(86, 90);
            this.loader1.Name = "loader1";
            this.loader1.Size = new System.Drawing.Size(128, 128);
            this.loader1.TabIndex = 0;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(509, 386);
            this.Controls.Add(this.pLoggingin);
            this.Controls.Add(this.pLoginContainer);
            this.Controls.Add(this.tsMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pinto!";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.Shown += new System.EventHandler(this.LoginForm_Shown);
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.pLoginContainer.ResumeLayout(false);
            this.pLoginContainer.PerformLayout();
            this.pError.ResumeLayout(false);
            this.pError.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbErrorIcon)).EndInit();
            this.pOptions.ResumeLayout(false);
            this.pOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
            this.pLoggingin.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public PintoNS.Controls.LoginButton btnConnect;
        public System.Windows.Forms.TextBox txtPassword;
        public System.Windows.Forms.ComboBox txtUsername;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.Panel pLoginContainer;
        public System.Windows.Forms.NumericUpDown nudPort;
        public System.Windows.Forms.TextBox txtIP;
        public System.Windows.Forms.LinkLabel llServers;
        public System.Windows.Forms.LinkLabel llForgotPassword;
        public System.Windows.Forms.CheckBox cbSavePassword;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pOptions;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripDropDownButton tsddbMenuPinto;
        private System.Windows.Forms.ToolStripDropDownButton tsddbMenuTools;
        private System.Windows.Forms.ToolStripDropDownButton tsddbMenuHelp;
        private System.Windows.Forms.Panel pLoggingin;
        private Controls.Loader loader1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pError;
        private System.Windows.Forms.PictureBox pbErrorIcon;
        private System.Windows.Forms.Label lErrorText;
    }
}