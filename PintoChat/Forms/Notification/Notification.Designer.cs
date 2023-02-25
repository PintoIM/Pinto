namespace PintoNS.Forms.Notification
{
    partial class Notification
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
            this.lBody = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lTitle
            // 
            this.lTitle.AutoSize = true;
            this.lTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lTitle.Location = new System.Drawing.Point(50, 12);
            this.lTitle.Name = "lTitle";
            this.lTitle.Size = new System.Drawing.Size(88, 20);
            this.lTitle.TabIndex = 0;
            this.lTitle.Text = "Notification";
            // 
            // lBody
            // 
            this.lBody.Location = new System.Drawing.Point(13, 40);
            this.lBody.Name = "lBody";
            this.lBody.Size = new System.Drawing.Size(231, 103);
            this.lBody.TabIndex = 1;
            this.lBody.Text = "This is a notification";
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(226, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(18, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbIcon
            // 
            this.pbIcon.Location = new System.Drawing.Point(12, 5);
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.Size = new System.Drawing.Size(32, 32);
            this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbIcon.TabIndex = 3;
            this.pbIcon.TabStop = false;
            // 
            // Notification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 152);
            this.Controls.Add(this.pbIcon);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lBody);
            this.Controls.Add(this.lTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Notification";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Notification";
            this.Shown += new System.EventHandler(this.Notification_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Notification_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.Label lTitle;
        public System.Windows.Forms.Label lBody;
        public System.Windows.Forms.PictureBox pbIcon;
    }
}