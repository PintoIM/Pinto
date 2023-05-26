using System;
using System.Drawing;
using System.Windows.Forms;

namespace PintoNS.Forms
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            Icon = Logo.LOGO2;
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            lVersion.Text = $"Version {Program.VERSION_STRING}";
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);
            g.DrawRectangle(new Pen(Color.DeepSkyBlue, 2.5f), 0, 0, Width - 1, Height - 1);
        }
        private void AboutForm_Deactivate(object sender, EventArgs e)
        {
            Close();
        }
    }
}
