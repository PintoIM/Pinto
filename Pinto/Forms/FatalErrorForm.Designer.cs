namespace PintoNS.Forms
{
    partial class FatalErrorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FatalErrorForm));
            this.lFatalError = new System.Windows.Forms.Label();
            this.lReport = new System.Windows.Forms.Label();
            this.rtxtLog = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lFatalError
            // 
            this.lFatalError.AutoSize = true;
            this.lFatalError.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lFatalError.Location = new System.Drawing.Point(149, 12);
            this.lFatalError.Name = "lFatalError";
            this.lFatalError.Size = new System.Drawing.Size(218, 25);
            this.lFatalError.TabIndex = 0;
            this.lFatalError.Text = "Catastrophic failure";
            // 
            // lReport
            // 
            this.lReport.AutoSize = true;
            this.lReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lReport.Location = new System.Drawing.Point(151, 37);
            this.lReport.Name = "lReport";
            this.lReport.Size = new System.Drawing.Size(342, 96);
            this.lReport.TabIndex = 2;
            this.lReport.Text = resources.GetString("lReport.Text");
            // 
            // rtxtLog
            // 
            this.rtxtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtLog.BackColor = System.Drawing.SystemColors.Window;
            this.rtxtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtLog.ForeColor = System.Drawing.Color.DarkRed;
            this.rtxtLog.Location = new System.Drawing.Point(15, 146);
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.ReadOnly = true;
            this.rtxtLog.Size = new System.Drawing.Size(476, 187);
            this.rtxtLog.TabIndex = 3;
            this.rtxtLog.Text = "";
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::PintoNS.Logo.LOGO;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(15, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(128, 128);
            this.panel1.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::PintoNS.Assets.ERROR;
            this.pictureBox1.Location = new System.Drawing.Point(64, 64);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // FatalErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 349);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rtxtLog);
            this.Controls.Add(this.lReport);
            this.Controls.Add(this.lFatalError);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(523, 388);
            this.Name = "FatalErrorForm";
            this.Text = "Pinto! - Catastrophic failure";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lFatalError;
        public System.Windows.Forms.Label lReport;
        public System.Windows.Forms.RichTextBox rtxtLog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}