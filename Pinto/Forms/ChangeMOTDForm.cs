using PintoNS.General;
using System;
using System.Windows.Forms;

namespace PintoNS.Forms
{
    public partial class ChangeMOTDForm : Form
    {
        private MainForm mainForm;

        public ChangeMOTDForm(MainForm mainForm)
        {
            InitializeComponent();
            Icon = Program.GetFormIcon();
            this.mainForm = mainForm;
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            string motd = txtMOTD.Text.Trim();
            Close();
            if (mainForm.LoginFrm.NetManager != null)
                mainForm.LoginFrm.NetManager.NetHandler.SendStatusPacket(mainForm.LoginFrm.CurrentUser.Status, motd);
        }

        private void txtMOTD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnChange.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void ChangeMOTDForm_Load(object sender, EventArgs e)
        {
            txtMOTD.Text = mainForm.LoginFrm.CurrentUser.MOTD;
        }
    }
}
