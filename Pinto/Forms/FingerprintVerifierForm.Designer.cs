namespace PintoNS.Forms
{
    partial class FingerprintVerifierForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FingerprintVerifierForm));
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnOnlyOnce = new System.Windows.Forms.Button();
            this.rtxtFingerprint = new System.Windows.Forms.RichTextBox();
            this.tcSections = new System.Windows.Forms.TabControl();
            this.tpUnknown = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tpFailed = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tEnableButtons = new System.Windows.Forms.Timer(this.components);
            this.tcSections.SuspendLayout();
            this.tpUnknown.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tpFailed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDisconnect.Location = new System.Drawing.Point(440, 344);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 0;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.Enabled = false;
            this.btnAccept.Location = new System.Drawing.Point(273, 344);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 1;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnOnlyOnce
            // 
            this.btnOnlyOnce.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOnlyOnce.Enabled = false;
            this.btnOnlyOnce.Location = new System.Drawing.Point(354, 344);
            this.btnOnlyOnce.Name = "btnOnlyOnce";
            this.btnOnlyOnce.Size = new System.Drawing.Size(80, 23);
            this.btnOnlyOnce.TabIndex = 2;
            this.btnOnlyOnce.Text = "Only Once";
            this.btnOnlyOnce.UseVisualStyleBackColor = true;
            this.btnOnlyOnce.Click += new System.EventHandler(this.btnOnlyOnce_Click);
            // 
            // rtxtFingerprint
            // 
            this.rtxtFingerprint.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtFingerprint.BackColor = System.Drawing.SystemColors.Window;
            this.rtxtFingerprint.Location = new System.Drawing.Point(12, 236);
            this.rtxtFingerprint.Name = "rtxtFingerprint";
            this.rtxtFingerprint.ReadOnly = true;
            this.rtxtFingerprint.Size = new System.Drawing.Size(503, 96);
            this.rtxtFingerprint.TabIndex = 3;
            this.rtxtFingerprint.Text = "";
            // 
            // tcSections
            // 
            this.tcSections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcSections.Controls.Add(this.tpUnknown);
            this.tcSections.Controls.Add(this.tpFailed);
            this.tcSections.Location = new System.Drawing.Point(12, 12);
            this.tcSections.Name = "tcSections";
            this.tcSections.SelectedIndex = 0;
            this.tcSections.Size = new System.Drawing.Size(503, 218);
            this.tcSections.TabIndex = 4;
            // 
            // tpUnknown
            // 
            this.tpUnknown.Controls.Add(this.label2);
            this.tpUnknown.Controls.Add(this.pictureBox1);
            this.tpUnknown.Controls.Add(this.label1);
            this.tpUnknown.Location = new System.Drawing.Point(4, 22);
            this.tpUnknown.Name = "tpUnknown";
            this.tpUnknown.Padding = new System.Windows.Forms.Padding(3);
            this.tpUnknown.Size = new System.Drawing.Size(495, 192);
            this.tpUnknown.TabIndex = 0;
            this.tpUnknown.Text = "Unknown";
            this.tpUnknown.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(393, 91);
            this.label2.TabIndex = 2;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(44, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(305, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Unknown Server Fingerprint";
            // 
            // tpFailed
            // 
            this.tpFailed.Controls.Add(this.label6);
            this.tpFailed.Controls.Add(this.label5);
            this.tpFailed.Controls.Add(this.label3);
            this.tpFailed.Controls.Add(this.pictureBox2);
            this.tpFailed.Controls.Add(this.label4);
            this.tpFailed.Location = new System.Drawing.Point(4, 22);
            this.tpFailed.Name = "tpFailed";
            this.tpFailed.Padding = new System.Windows.Forms.Padding(3);
            this.tpFailed.Size = new System.Drawing.Size(495, 192);
            this.tpFailed.TabIndex = 1;
            this.tpFailed.Text = "Failed";
            this.tpFailed.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(265, 39);
            this.label6.TabIndex = 7;
            this.label6.Text = "This could mean 2 things:\r\n- YOUR CONNECTION IS BEING TAMPERED\r\n- The fingerprint" +
    " has changed (less likely)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(479, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "The server you are trying to connect to (%SERVER%) has a different fingerprint th" +
    "an the known one.\r\n";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(417, 78);
            this.label3.TabIndex = 5;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(6, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 32);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(44, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(335, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Mismatched Server Fingerprint";
            // 
            // tEnableButtons
            // 
            this.tEnableButtons.Interval = 1000;
            this.tEnableButtons.Tick += new System.EventHandler(this.tEnableButtons_Tick);
            // 
            // FingerprintVerifierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 379);
            this.Controls.Add(this.tcSections);
            this.Controls.Add(this.rtxtFingerprint);
            this.Controls.Add(this.btnOnlyOnce);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnDisconnect);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(543, 418);
            this.Name = "FingerprintVerifierForm";
            this.Text = "Pinto! - Server Fingerprint Verification";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RSAKeyVerifierForm_FormClosing);
            this.Load += new System.EventHandler(this.RSAKeyVerifierForm_Load);
            this.tcSections.ResumeLayout(false);
            this.tpUnknown.ResumeLayout(false);
            this.tpUnknown.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tpFailed.ResumeLayout(false);
            this.tpFailed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnOnlyOnce;
        private System.Windows.Forms.RichTextBox rtxtFingerprint;
        private System.Windows.Forms.TabControl tcSections;
        private System.Windows.Forms.TabPage tpUnknown;
        private System.Windows.Forms.TabPage tpFailed;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer tEnableButtons;
    }
}