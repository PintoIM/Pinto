using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS.Forms.Notification
{
    public partial class NotificationOkButton : Notification
    {
        private Button btnOk;

        public NotificationOkButton() : base() 
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.btnOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(94, 114);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // NotificationOkButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(256, 152);
            this.Controls.Add(this.btnOk);
            this.Name = "NotificationOkButton";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Controls.SetChildIndex(this.pbIcon, 0);
            this.Controls.SetChildIndex(this.lTitle, 0);
            this.Controls.SetChildIndex(this.lBody, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            UserPressedButton.Invoke(NotificationButtonType.OK);
            Close();
        }
    }
}
