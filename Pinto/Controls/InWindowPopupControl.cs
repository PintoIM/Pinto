using System.Drawing;
using System.Windows.Forms;

namespace PintoNS.Controls
{
    public partial class InWindowPopupControl : UserControl
    {
        public InWindowPopupControl()
        {
            InitializeComponent();
        }

        public InWindowPopupControl(string text)
        {
            InitializeComponent();
            lText.Text = text;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Height = lText.Height + 10;
            lText.MaximumSize = new Size(Width - 50, 0);
            base.OnPaint(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            e.Graphics.DrawRectangle(new Pen(Color.DarkGray, 1), 0, 0, Width - 1, Height - 1);
        }
    }
}
