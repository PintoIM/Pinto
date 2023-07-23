using System;
using System.Drawing;
using System.Windows.Forms;

namespace PintoNS.Controls
{
    public partial class Loader : UserControl
    {
        public static readonly Bitmap LOADING_SHEET = Assets._22791;
        private const int INDEX_WIDTH = 128;
        private const int INDEX_HEIGHT = 128;
        private int currentIndex;
        private int maxIndex;

        public Loader()
        {
            InitializeComponent();
            maxIndex = LOADING_SHEET.Height / INDEX_HEIGHT;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(LOADING_SHEET, 
                new RectangleF(
                    (Width - INDEX_WIDTH) / 2,
                    (Height - INDEX_HEIGHT) / 2,
                    INDEX_WIDTH,
                    INDEX_HEIGHT), 
                new RectangleF(0,
                    currentIndex * INDEX_HEIGHT,
                    INDEX_WIDTH, 
                    INDEX_HEIGHT),
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
