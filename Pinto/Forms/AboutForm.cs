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
            Icon = Program.GetFormIcon();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            lVersion.Text = $"Version {Program.VERSION_STRING}";
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);
            // #14b8a6
            g.DrawRectangle(new Pen(Color.FromArgb(0x14, 0xb8, 0xa6), 2.5f), 0, 0, Width - 1, Height - 1);
        }

        private void AboutForm_Deactivate(object sender, EventArgs e)
        {
            Close();
        }
    }
}
