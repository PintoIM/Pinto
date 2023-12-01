namespace PintoNS.Controls
{
    partial class InWindowPopupControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClose = new System.Windows.Forms.Button();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.lText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(142, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(16, 16);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // pbIcon
            // 
            this.pbIcon.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pbIcon.BackColor = System.Drawing.Color.Transparent;
            this.pbIcon.Location = new System.Drawing.Point(3, 3);
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.Size = new System.Drawing.Size(16, 16);
            this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbIcon.TabIndex = 2;
            this.pbIcon.TabStop = false;
            // 
            // lText
            // 
            this.lText.AutoSize = true;
            this.lText.BackColor = System.Drawing.Color.Transparent;
            this.lText.ForeColor = System.Drawing.Color.Black;
            this.lText.Location = new System.Drawing.Point(25, 5);
            this.lText.Name = "lText";
            this.lText.Size = new System.Drawing.Size(108, 13);
            this.lText.TabIndex = 3;
            this.lText.Text = "Default notification";
            // 
            // InWindowPopupControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lText);
            this.Controls.Add(this.pbIcon);
            this.Controls.Add(this.btnClose);
            this.Name = "InWindowPopupControl";
            this.Size = new System.Drawing.Size(161, 21);
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.PictureBox pbIcon;
        public System.Windows.Forms.Label lText;
        public System.Windows.Forms.Button btnClose;
    }
}
