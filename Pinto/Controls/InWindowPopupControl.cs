using System.Drawing;
using System.Windows.Forms;

namespace PintoNS.Controls
{
    public partial class InWindowPopupControl : UserControl
    {
        public float TimeBeforeClosure;

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
    }
}
