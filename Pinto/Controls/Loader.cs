using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS.Controls
{
    public partial class Loader : UserControl
    {
        public const int WIDTH = 122;
        public const int HEIGHT = 107;
        private Bitmap loadingGIF = Logo.LOADING.Clone(
            new Rectangle(0, 0, Logo.LOADING.Width, Logo.LOADING.Height), PixelFormat.DontCare);

        public Loader()
        {
            InitializeComponent();
            OnSizeChanged(EventArgs.Empty);
            ImageAnimator.Animate(loadingGIF, drawFrame);
        }

        private void drawFrame(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            ImageAnimator.UpdateFrames(loadingGIF);
            Graphics g = e.Graphics;
            g.DrawImage(Logo.LOGO_FRAME, 0, 0, WIDTH, HEIGHT);
            g.DrawImage(loadingGIF, 13, 11);
            g.Flush();
            g.Dispose();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            AutoSize = false;
            Size = new Size(WIDTH, HEIGHT);
            MinimumSize = new Size(WIDTH, HEIGHT);
            MaximumSize = new Size(WIDTH, HEIGHT);
            SetBounds(0, 0, WIDTH, HEIGHT, BoundsSpecified.Size);
            SetBoundsCore(0, 0, WIDTH, HEIGHT, BoundsSpecified.Size);
            base.OnSizeChanged(e);
        }
    }
}
