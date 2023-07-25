using System;
using System.Drawing;
using System.Windows.Forms;

namespace PintoNS.Controls
{
    public partial class Separator : UserControl
    {
        public Separator()
        {
            InitializeComponent();
            Width = 100;
            Height = 2;
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle area = e.ClipRectangle;
            Graphics g = e.Graphics;
            g.DrawLine(new Pen(Color.LightGray, 2f), 0, 0, area.Width - 1, 0);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Height = 2;
            base.OnSizeChanged(e);
        }
    }
}
