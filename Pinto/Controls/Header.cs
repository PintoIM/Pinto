using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS.Controls
{
    public partial class Header : UserControl
    {
        [Browsable(true),EditorBrowsable,Category("Appearance")]
        public string Content { get; set; }

        public Header()
        {
            InitializeComponent();
            OnSizeChanged(EventArgs.Empty);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(new SolidBrush(Color.FromArgb(155, 155, 155)), 0, 0, Width, Height);
            g.DrawString(Content, new Font("Tahoma", 8.25f, FontStyle.Bold), Brushes.White, 5f, 2.5f);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Height = 20;
            base.OnSizeChanged(e);
        }
    }
}
