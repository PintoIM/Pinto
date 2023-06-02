using System;
using System.Drawing;
using System.Windows.Forms;

namespace PintoNS.Controls
{
    public partial class Loader : UserControl
    {
        public static readonly Bitmap LOADING_SHEET = Logo.LOADING;
        private const int INDEX_WIDTH = 90;
        private const int INDEX_HEIGHT = 90;
        private int currentIndex;
        private int maxIndex;

        public Loader()
        {
            InitializeComponent();
            maxIndex = LOADING_SHEET.Height / INDEX_HEIGHT;
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(LOADING_SHEET, 
                new Rectangle(
                    (Width - INDEX_WIDTH) / 2,
                    (Height - INDEX_HEIGHT) / 2,
                    INDEX_WIDTH,
                    INDEX_HEIGHT), 
                0,
                currentIndex * INDEX_HEIGHT,
                INDEX_WIDTH, INDEX_HEIGHT,
                GraphicsUnit.Pixel);
            e.Graphics.Flush();
        }

        private void tAnimation_Tick(object sender, EventArgs e)
        {
            Invalidate();
            currentIndex++;
            if (currentIndex >= maxIndex) currentIndex = 0;
        }
    }
}
