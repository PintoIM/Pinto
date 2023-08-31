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
            this.lFatalError = new System.Windows.Forms.Label();
            this.lReport = new System.Windows.Forms.Label();
            this.rtxtLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // lFatalError
            // 
            this.lFatalError.AutoSize = true;
            this.lFatalError.Location = new System.Drawing.Point(12, 9);
            this.lFatalError.Name = "lFatalError";
            this.lFatalError.Size = new System.Drawing.Size(250, 13);
            this.lFatalError.TabIndex = 0;
            this.lFatalError.Text = "Pinto! has ran into an fatal error! Sorry about that.... ";
            // 
            // lReport
            // 
            this.lReport.AutoSize = true;
            this.lReport.Location = new System.Drawing.Point(12, 22);
            this.lReport.Name = "lReport";
            this.lReport.Size = new System.Drawing.Size(377, 13);
            this.lReport.TabIndex = 2;
            this.lReport.Text = "If you want to prevent this from happening in the future, report the attached log" +
    "";
            // 
            // rtxtLog
            // 
            this.rtxtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtLog.BackColor = System.Drawing.SystemColors.Window;
            this.rtxtLog.Location = new System.Drawing.Point(15, 38);
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.ReadOnly = true;
            this.rtxtLog.Size = new System.Drawing.Size(374, 258);
            this.rtxtLog.TabIndex = 3;
            this.rtxtLog.Text = "";
            // 
            // FatalErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 312);
            this.Controls.Add(this.rtxtLog);
            this.Controls.Add(this.lReport);
            this.Controls.Add(this.lFatalError);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FatalErrorForm";
            this.Text = "Pinto! - Fatal Error";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lFatalError;
        public System.Windows.Forms.Label lReport;
        public System.Windows.Forms.RichTextBox rtxtLog;
    }
}