
namespace PintoNS
{
    partial class ControlTestForm
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
            this.modernRichTextBox1 = new PintoNS.Controls.ModernRichTextBox();
            this.SuspendLayout();
            // 
            // modernRichTextBox1
            // 
            this.modernRichTextBox1.Location = new System.Drawing.Point(87, 38);
            this.modernRichTextBox1.Name = "modernRichTextBox1";
            this.modernRichTextBox1.Size = new System.Drawing.Size(316, 186);
            this.modernRichTextBox1.TabIndex = 0;
            // 
            // ControlTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.modernRichTextBox1);
            this.Name = "ControlTestForm";
            this.Text = "ControlTestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ModernRichTextBox modernRichTextBox1;
    }
}