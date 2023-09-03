namespace PintoNS.Forms
{
    partial class MoreFontOptionsForm
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
            this.btnEnableItalic = new System.Windows.Forms.Button();
            this.btnEnableUnderline = new System.Windows.Forms.Button();
            this.btnEnableBold = new System.Windows.Forms.Button();
            this.btnEnableStrikeout = new System.Windows.Forms.Button();
            this.btnDisableStrikeout = new System.Windows.Forms.Button();
            this.btnDisableBold = new System.Windows.Forms.Button();
            this.btnDisableUnderline = new System.Windows.Forms.Button();
            this.btnDisableItalic = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnEnableItalic
            // 
            this.btnEnableItalic.Location = new System.Drawing.Point(12, 41);
            this.btnEnableItalic.Name = "btnEnableItalic";
            this.btnEnableItalic.Size = new System.Drawing.Size(114, 23);
            this.btnEnableItalic.TabIndex = 0;
            this.btnEnableItalic.Text = "Enable Italic";
            this.btnEnableItalic.UseVisualStyleBackColor = true;
            this.btnEnableItalic.Click += new System.EventHandler(this.btnEnableItalic_Click);
            // 
            // btnEnableUnderline
            // 
            this.btnEnableUnderline.Location = new System.Drawing.Point(12, 70);
            this.btnEnableUnderline.Name = "btnEnableUnderline";
            this.btnEnableUnderline.Size = new System.Drawing.Size(114, 23);
            this.btnEnableUnderline.TabIndex = 1;
            this.btnEnableUnderline.Text = "Enable Underline";
            this.btnEnableUnderline.UseVisualStyleBackColor = true;
            this.btnEnableUnderline.Click += new System.EventHandler(this.btnEnableUnderline_Click);
            // 
            // btnEnableBold
            // 
            this.btnEnableBold.Location = new System.Drawing.Point(12, 12);
            this.btnEnableBold.Name = "btnEnableBold";
            this.btnEnableBold.Size = new System.Drawing.Size(114, 23);
            this.btnEnableBold.TabIndex = 2;
            this.btnEnableBold.Text = "Enable Bold";
            this.btnEnableBold.UseVisualStyleBackColor = true;
            this.btnEnableBold.Click += new System.EventHandler(this.btnEnableBold_Click);
            // 
            // btnEnableStrikeout
            // 
            this.btnEnableStrikeout.Location = new System.Drawing.Point(12, 99);
            this.btnEnableStrikeout.Name = "btnEnableStrikeout";
            this.btnEnableStrikeout.Size = new System.Drawing.Size(114, 23);
            this.btnEnableStrikeout.TabIndex = 3;
            this.btnEnableStrikeout.Text = "Enable Strikeout";
            this.btnEnableStrikeout.UseVisualStyleBackColor = true;
            this.btnEnableStrikeout.Click += new System.EventHandler(this.btnEnableStrikeout_Click);
            // 
            // btnDisableStrikeout
            // 
            this.btnDisableStrikeout.Location = new System.Drawing.Point(132, 99);
            this.btnDisableStrikeout.Name = "btnDisableStrikeout";
            this.btnDisableStrikeout.Size = new System.Drawing.Size(114, 23);
            this.btnDisableStrikeout.TabIndex = 7;
            this.btnDisableStrikeout.Text = "Disable Strikeout";
            this.btnDisableStrikeout.UseVisualStyleBackColor = true;
            this.btnDisableStrikeout.Click += new System.EventHandler(this.btnDisableStrikeout_Click);
            // 
            // btnDisableBold
            // 
            this.btnDisableBold.Location = new System.Drawing.Point(132, 12);
            this.btnDisableBold.Name = "btnDisableBold";
            this.btnDisableBold.Size = new System.Drawing.Size(114, 23);
            this.btnDisableBold.TabIndex = 6;
            this.btnDisableBold.Text = "Disable Bold";
            this.btnDisableBold.UseVisualStyleBackColor = true;
            this.btnDisableBold.Click += new System.EventHandler(this.btnDisableBold_Click);
            // 
            // btnDisableUnderline
            // 
            this.btnDisableUnderline.Location = new System.Drawing.Point(132, 70);
            this.btnDisableUnderline.Name = "btnDisableUnderline";
            this.btnDisableUnderline.Size = new System.Drawing.Size(114, 23);
            this.btnDisableUnderline.TabIndex = 5;
            this.btnDisableUnderline.Text = "Disable Underline";
            this.btnDisableUnderline.UseVisualStyleBackColor = true;
            this.btnDisableUnderline.Click += new System.EventHandler(this.btnDisableUnderline_Click);
            // 
            // btnDisableItalic
            // 
            this.btnDisableItalic.Location = new System.Drawing.Point(132, 41);
            this.btnDisableItalic.Name = "btnDisableItalic";
            this.btnDisableItalic.Size = new System.Drawing.Size(114, 23);
            this.btnDisableItalic.TabIndex = 4;
            this.btnDisableItalic.Text = "Disable Italic";
            this.btnDisableItalic.UseVisualStyleBackColor = true;
            this.btnDisableItalic.Click += new System.EventHandler(this.btnDisableItalic_Click);
            // 
            // MoreFontOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 130);
            this.Controls.Add(this.btnDisableStrikeout);
            this.Controls.Add(this.btnDisableBold);
            this.Controls.Add(this.btnDisableUnderline);
            this.Controls.Add(this.btnDisableItalic);
            this.Controls.Add(this.btnEnableStrikeout);
            this.Controls.Add(this.btnEnableBold);
            this.Controls.Add(this.btnEnableUnderline);
            this.Controls.Add(this.btnEnableItalic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MoreFontOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "More Options";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEnableItalic;
        private System.Windows.Forms.Button btnEnableUnderline;
        private System.Windows.Forms.Button btnEnableBold;
        private System.Windows.Forms.Button btnEnableStrikeout;
        private System.Windows.Forms.Button btnDisableStrikeout;
        private System.Windows.Forms.Button btnDisableBold;
        private System.Windows.Forms.Button btnDisableUnderline;
        private System.Windows.Forms.Button btnDisableItalic;
    }
}