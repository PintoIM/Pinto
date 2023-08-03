
namespace PintoNS.Controls
{
    partial class ModernRichTextBox
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
            this.rtxtTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtxtTextBox
            // 
            this.rtxtTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtTextBox.Location = new System.Drawing.Point(3, 3);
            this.rtxtTextBox.Name = "rtxtTextBox";
            this.rtxtTextBox.Size = new System.Drawing.Size(209, 103);
            this.rtxtTextBox.TabIndex = 0;
            this.rtxtTextBox.Text = "";
            // 
            // ModernRichTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtxtTextBox);
            this.Name = "ModernRichTextBox";
            this.Size = new System.Drawing.Size(215, 109);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtTextBox;
    }
}
