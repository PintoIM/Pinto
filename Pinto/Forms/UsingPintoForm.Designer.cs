namespace PintoNS.Forms
{
    partial class UsingPintoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsingPintoForm));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.nudPort = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pLoginControls = new System.Windows.Forms.Panel();
            this.llForgotPassword = new System.Windows.Forms.LinkLabel();
            this.cbSavePassword = new System.Windows.Forms.CheckBox();
            this.rbCreate = new System.Windows.Forms.RadioButton();
            this.rbLogin = new System.Windows.Forms.RadioButton();
            this.lStatus = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
            this.pLoginControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(299, 402);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(218, 402);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(234, 65);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(106, 20);
            this.txtIP.TabIndex = 2;
            this.txtIP.Text = "127.0.0.1";
            // 
            // nudPort
            // 
            this.nudPort.Location = new System.Drawing.Point(243, 89);
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
            this.nudPort.TabIndex = 3;
            this.nudPort.Value = new decimal(new int[] {
            2407,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(208, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Port:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(83, 62);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(119, 20);
            this.txtUsername.TabIndex = 6;
            this.txtUsername.Text = "vlod";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(83, 89);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(119, 20);
            this.txtPassword.TabIndex = 7;
            this.txtPassword.Text = "login123";
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Username:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Password:";
            // 
            // pLoginControls
            // 
            this.pLoginControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pLoginControls.Controls.Add(this.rbLogin);
            this.pLoginControls.Controls.Add(this.rbCreate);
            this.pLoginControls.Controls.Add(this.cbSavePassword);
            this.pLoginControls.Controls.Add(this.llForgotPassword);
            this.pLoginControls.Controls.Add(this.label1);
            this.pLoginControls.Controls.Add(this.label4);
            this.pLoginControls.Controls.Add(this.txtIP);
            this.pLoginControls.Controls.Add(this.label3);
            this.pLoginControls.Controls.Add(this.nudPort);
            this.pLoginControls.Controls.Add(this.txtPassword);
            this.pLoginControls.Controls.Add(this.label2);
            this.pLoginControls.Controls.Add(this.txtUsername);
            this.pLoginControls.Location = new System.Drawing.Point(26, 53);
            this.pLoginControls.Name = "pLoginControls";
            this.pLoginControls.Size = new System.Drawing.Size(348, 184);
            this.pLoginControls.TabIndex = 10;
            // 
            // llForgotPassword
            // 
            this.llForgotPassword.AutoSize = true;
            this.llForgotPassword.Location = new System.Drawing.Point(20, 116);
            this.llForgotPassword.Name = "llForgotPassword";
            this.llForgotPassword.Size = new System.Drawing.Size(114, 13);
            this.llForgotPassword.TabIndex = 10;
            this.llForgotPassword.TabStop = true;
            this.llForgotPassword.Text = "Forgot your password?";
            // 
            // cbSavePassword
            // 
            this.cbSavePassword.AutoSize = true;
            this.cbSavePassword.Checked = true;
            this.cbSavePassword.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSavePassword.Enabled = false;
            this.cbSavePassword.Location = new System.Drawing.Point(23, 141);
            this.cbSavePassword.Name = "cbSavePassword";
            this.cbSavePassword.Size = new System.Drawing.Size(196, 17);
            this.cbSavePassword.TabIndex = 11;
            this.cbSavePassword.Text = "Store my password on this computer";
            this.cbSavePassword.UseVisualStyleBackColor = true;
            // 
            // rbCreate
            // 
            this.rbCreate.AutoSize = true;
            this.rbCreate.Enabled = false;
            this.rbCreate.Location = new System.Drawing.Point(22, 11);
            this.rbCreate.Name = "rbCreate";
            this.rbCreate.Size = new System.Drawing.Size(224, 17);
            this.rbCreate.TabIndex = 12;
            this.rbCreate.Text = "I would like to create a new Pinto account";
            this.rbCreate.UseVisualStyleBackColor = true;
            // 
            // rbLogin
            // 
            this.rbLogin.AutoSize = true;
            this.rbLogin.Checked = true;
            this.rbLogin.Enabled = false;
            this.rbLogin.Location = new System.Drawing.Point(23, 34);
            this.rbLogin.Name = "rbLogin";
            this.rbLogin.Size = new System.Drawing.Size(170, 17);
            this.rbLogin.TabIndex = 13;
            this.rbLogin.TabStop = true;
            this.rbLogin.Text = "I already have a Pinto account";
            this.rbLogin.UseVisualStyleBackColor = true;
            // 
            // lStatus
            // 
            this.lStatus.AutoSize = true;
            this.lStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lStatus.Location = new System.Drawing.Point(22, 9);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(101, 20);
            this.lStatus.TabIndex = 11;
            this.lStatus.Text = "Using Pinto";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Image = global::PintoNS.Assets.LOGIN_PLACEHOLDER;
            this.pictureBox1.Location = new System.Drawing.Point(26, 250);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(348, 136);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // UsingPintoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 437);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lStatus);
            this.Controls.Add(this.pLoginControls);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UsingPintoForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pinto! - Using Pinto";
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
            this.pLoginControls.ResumeLayout(false);
            this.pLoginControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.NumericUpDown nudPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pLoginControls;
        private System.Windows.Forms.LinkLabel llForgotPassword;
        private System.Windows.Forms.CheckBox cbSavePassword;
        private System.Windows.Forms.RadioButton rbLogin;
        private System.Windows.Forms.RadioButton rbCreate;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}