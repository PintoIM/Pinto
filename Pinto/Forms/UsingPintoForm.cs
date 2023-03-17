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
            if (rbCreate.Checked)
            {
                tcSections.SelectedTab = tpRegister;
            }
            else
            {
                string ip = txtIP.Text.Trim();
                int port = (int)nudPort.Value;
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text;

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    NotificationUtil.ShowNotification(this, "Blank username or password!",
                        "Error", NotificationIconType.ERROR);
                    return;
                }

                Close();
                await mainForm.Connect(ip, port, username, password);
            }
        }

        private void rbCreate_CheckedChanged(object sender, EventArgs e)
        {
            bool state = rbCreate.Checked;

            txtUsername.Enabled = !state;
            txtPassword.Enabled = !state;
            txtIP.Enabled = !state;
            nudPort.Enabled = !state;
            cbSavePassword.Enabled = !state;
            llForgotPassword.Enabled = !state;

            if (state)
                btnConnect.Text = "Continue";
            else
                btnConnect.Text = "Connect";
        }

        private void UsingPintoForm_Load(object sender, EventArgs e)
        {
            tcSections.Appearance = TabAppearance.FlatButtons;
            tcSections.ItemSize = new Size(0, 1);
            tcSections.SizeMode = TabSizeMode.Fixed;
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            string ip = txtRegisterIP.Text.Trim();
            int port = (int)nudRegisterPort.Value;
            string username = txtRegisterUsername.Text.Trim();
            string password = txtRegisterPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                NotificationUtil.ShowNotification(this, "Blank username or password!",
                    "Error", NotificationIconType.ERROR);
                return;
            }

            Close();
            await mainForm.ConnectRegister(ip, port, username, password);
        }

        private void btnRegisterBack_Click(object sender, EventArgs e)
        {
            tcSections.SelectedTab = tpMain;
        }
    }
}
