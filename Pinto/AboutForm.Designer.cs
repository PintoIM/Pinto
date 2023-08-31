namespace PintoNS.Forms
{
    partial class AboutForm
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
            this.lTitle = new System.Windows.Forms.Label();
            this.lVersion = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.pbGPLV3 = new System.Windows.Forms.PictureBox();
            this.lBody = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGPLV3)).BeginInit();
            this.SuspendLayout();
            // 
            // lTitle
            // 
            this.lTitle.AutoSize = true;
            this.lTitle.BackColor = System.Drawing.Color.Transparent;
            this.lTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lTitle.Location = new System.Drawing.Point(182, 9);
            this.lTitle.Name = "lTitle";
            this.lTitle.Size = new System.Drawing.Size(184, 37);
            this.lTitle.TabIndex = 1;
            this.lTitle.Text = "Pinto! Beta";
            this.lTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AboutForm_MouseDown);
            // 
            // lVersion
            // 
            this.lVersion.AutoSize = true;
            this.lVersion.BackColor = System.Drawing.Color.Transparent;
            this.lVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lVersion.Location = new System.Drawing.Point(186, 49);
            this.lVersion.Name = "lVersion";
            this.lVersion.Size = new System.Drawing.Size(104, 13);
            this.lVersion.TabIndex = 3;
            this.lVersion.Text = "Version unknown";
            this.lVersion.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AboutForm_MouseDown);
            // 
            // pbLogo
            // 
            this.pbLogo.BackColor = System.Drawing.Color.Transparent;
            this.pbLogo.Image = global::PintoNS.Logo.LOGO;
            this.pbLogo.Location = new System.Drawing.Point(12, 12);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(168, 161);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            this.pbLogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AboutForm_MouseDown);
            // 
            // pbGPLV3
            // 
            this.pbGPLV3.BackColor = System.Drawing.Color.Transparent;
            this.pbGPLV3.Image = global::PintoNS.Assets.GPLV3;
            this.pbGPLV3.Location = new System.Drawing.Point(559, 123);
            this.pbGPLV3.Name = "pbGPLV3";
            this.pbGPLV3.Size = new System.Drawing.Size(100, 50);
            this.pbGPLV3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbGPLV3.TabIndex = 5;
            this.pbGPLV3.TabStop = false;
            this.pbGPLV3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AboutForm_MouseDown);
            // 
            // lBody
            // 
            this.lBody.BackColor = System.Drawing.Color.Transparent;
            this.lBody.Location = new System.Drawing.Point(186, 72);
            this.lBody.Name = "lBody";
            this.lBody.Size = new System.Drawing.Size(673, 48);
            this.lBody.TabIndex = 6;
            this.lBody.Text = "An open-source chatting application designed from the ground up to bring back nos" +
    "talgia\r\n";
            this.lBody.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AboutForm_MouseDown);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(671, 182);
            this.Controls.Add(this.lBody);
            this.Controls.Add(this.pbGPLV3);
            this.Controls.Add(this.lVersion);
            this.Controls.Add(this.lTitle);
            this.Controls.Add(this.pbLogo);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pinto! - About";
            this.Deactivate += new System.EventHandler(this.AboutForm_Deactivate);
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AboutForm_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGPLV3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox pbLogo;
        public System.Windows.Forms.Label lTitle;
        public System.Windows.Forms.Label lVersion;
        public System.Windows.Forms.PictureBox pbGPLV3;
        public System.Windows.Forms.Label lBody;
    }
}