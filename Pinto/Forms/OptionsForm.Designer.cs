
namespace PintoNS.Forms
{
    partial class OptionsForm
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
            this.tcOptions = new System.Windows.Forms.TabControl();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.tpAudio = new System.Windows.Forms.TabPage();
            this.tcOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcOptions
            // 
            this.tcOptions.Controls.Add(this.tpGeneral);
            this.tcOptions.Controls.Add(this.tpAudio);
            this.tcOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcOptions.Location = new System.Drawing.Point(0, 0);
            this.tcOptions.Name = "tcOptions";
            this.tcOptions.SelectedIndex = 0;
            this.tcOptions.Size = new System.Drawing.Size(473, 309);
            this.tcOptions.TabIndex = 0;
            // 
            // tpGeneral
            // 
            this.tpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Size = new System.Drawing.Size(465, 283);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            this.tpGeneral.UseVisualStyleBackColor = true;
            // 
            // tpAudio
            // 
            this.tpAudio.Location = new System.Drawing.Point(4, 22);
            this.tpAudio.Name = "tpAudio";
            this.tpAudio.Size = new System.Drawing.Size(465, 283);
            this.tpAudio.TabIndex = 1;
            this.tpAudio.Text = "Audio";
            this.tpAudio.UseVisualStyleBackColor = true;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 309);
            this.Controls.Add(this.tcOptions);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(489, 348);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(489, 348);
            this.Name = "OptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pinto! - Options";
            this.tcOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcOptions;
        private System.Windows.Forms.TabPage tpGeneral;
        private System.Windows.Forms.TabPage tpAudio;
    }
}