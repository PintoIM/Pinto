using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS.Forms.Notification
{
    public partial class NotificationYesNoButton : Notification
    {
        private Button btnYes;
        private Button btnNo;

        public NotificationYesNoButton() : base()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.btnYes = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(42, 114);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 23);
            this.btnYes.TabIndex = 3;
            this.btnYes.Text = "Yes";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnNo
            // 
            this.btnNo.Location = new System.Drawing.Point(137, 114);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(75, 23);
            this.btnNo.TabIndex = 4;
            this.btnNo.Text = "No";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // NotificationYesNoButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(256, 152);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.Name = "NotificationYesNoButton";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Controls.SetChildIndex(this.pbIcon, 0);
            this.Controls.SetChildIndex(this.lTitle, 0);
            this.Controls.SetChildIndex(this.lBody, 0);
            this.Controls.SetChildIndex(this.btnYes, 0);
            this.Controls.SetChildIndex(this.btnNo, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            UserPressedButton.Invoke(NotificationButtonType.YES);
            Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            UserPressedButton.Invoke(NotificationButtonType.NO);
            Close();
        }
    }
}
