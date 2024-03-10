using System;
using System.Windows.Forms;

namespace PintoNS.Forms
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            lVersion.Text = $"Version: {Program.VERSION_STRING}";
            lProtocol.Text = $"Protocol Version: {Program.PROTOCOL_VERSION}";
        }

        private void lCommit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Utils.ConstructTextOnlyForm(Program.VERSION_COMMIT, "Build Commit").ShowDialog();
        }
    }
}
