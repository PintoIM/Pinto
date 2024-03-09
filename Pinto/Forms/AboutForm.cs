using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            ViewAllPopupContentForm form = new ViewAllPopupContentForm();
            form.Text = "Commit";
            form.rtxtContent.Text = Program.VERSION_COMMIT;
            form.ShowDialog();
        }
    }
}
