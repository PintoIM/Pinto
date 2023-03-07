using PintoNS.Forms.Notification;
using PintoNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS.Forms
{
    public partial class UsingPintoForm : Form
    {
        private MainForm mainForm;

        public UsingPintoForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            string ip = txtIP.Text.Trim();
            int port = (int)nudPort.Value;
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(ip) || !IPAddress.TryParse(ip, out _))
            {
                NotificationUtil.ShowNotification(this,"An invalid IP address was specified!", "Error", NotificationIconType.ERROR);
                return;
            }

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                NotificationUtil.ShowNotification(this, "Blank username or password! The server might reject this...", 
                    "Warning", NotificationIconType.WARNING);
                return;
            }

            Close();
            await mainForm.Connect(ip, port, username, password);
        }
    }
}
