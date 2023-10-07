
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
            this.flpGeneralContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.tpPrivacy = new System.Windows.Forms.TabPage();
            this.flpPrivacyContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.hpHelp = new System.Windows.Forms.HelpProvider();
            this.tcOptions.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.tpPrivacy.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcOptions
            // 
            this.tcOptions.Controls.Add(this.tpGeneral);
            this.tcOptions.Controls.Add(this.tpPrivacy);
            this.tcOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcOptions.Location = new System.Drawing.Point(0, 0);
            this.tcOptions.Name = "tcOptions";
            this.tcOptions.SelectedIndex = 0;
            this.tcOptions.Size = new System.Drawing.Size(473, 309);
            this.tcOptions.TabIndex = 0;
            // 
            // tpGeneral
            // 
            this.tpGeneral.Controls.Add(this.flpGeneralContainer);
            this.tpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Size = new System.Drawing.Size(465, 283);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            this.tpGeneral.UseVisualStyleBackColor = true;
            // 
            // flpGeneralContainer
            // 
            this.flpGeneralContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpGeneralContainer.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpGeneralContainer.Location = new System.Drawing.Point(0, 0);
            this.flpGeneralContainer.Name = "flpGeneralContainer";
            this.flpGeneralContainer.Size = new System.Drawing.Size(465, 283);
            this.flpGeneralContainer.TabIndex = 0;
            // 
            // tpPrivacy
            // 
            this.tpPrivacy.Controls.Add(this.flpPrivacyContainer);
            this.tpPrivacy.Location = new System.Drawing.Point(4, 22);
            this.tpPrivacy.Name = "tpPrivacy";
            this.tpPrivacy.Padding = new System.Windows.Forms.Padding(3);
            this.tpPrivacy.Size = new System.Drawing.Size(465, 283);
            this.tpPrivacy.TabIndex = 1;
            this.tpPrivacy.Text = "Privacy";
            this.tpPrivacy.UseVisualStyleBackColor = true;
            // 
            // flpPrivacyContainer
            // 
            this.flpPrivacyContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpPrivacyContainer.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpPrivacyContainer.Location = new System.Drawing.Point(3, 3);
            this.flpPrivacyContainer.Name = "flpPrivacyContainer";
            this.flpPrivacyContainer.Size = new System.Drawing.Size(459, 277);
            this.flpPrivacyContainer.TabIndex = 0;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 309);
            this.Controls.Add(this.tcOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(489, 348);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(489, 348);
            this.Name = "OptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pinto! - Options";
            this.Load += new System.EventHandler(this.OptionsForm_Load);
            this.tcOptions.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.tpPrivacy.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TabControl tcOptions;
        public System.Windows.Forms.TabPage tpGeneral;
        public System.Windows.Forms.FlowLayoutPanel flpGeneralContainer;
        public System.Windows.Forms.HelpProvider hpHelp;
        public System.Windows.Forms.TabPage tpPrivacy;
        public System.Windows.Forms.FlowLayoutPanel flpPrivacyContainer;
    }
}