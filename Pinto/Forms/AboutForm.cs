using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PintoNS.Forms
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            Icon = Program.GetFormIcon();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            lVersion.Text = string.Format(lVersion.Text, Program.VERSION_STRING);
        }

        private void AboutForm_Deactivate(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutForm_MouseDown(object sender, MouseEventArgs e)
        {
            Close();
        }
    }
}
