using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS.Controls
{
    public class LoginButton : Button
    {
        private bool shouldDrawHover;
        private bool shouldDrawClicked;

        public LoginButton() 
        {
            Width = 240;
            Height = 24;
        }

        protected void PaintTransparentBackground(Graphics graphics, Rectangle clipRect)
        {
            graphics.Clear(Color.Transparent);

            if (Parent != null)
            {
                clipRect.Offset(Location);
                PaintEventArgs e = new PaintEventArgs(graphics, clipRect);
                GraphicsState state = graphics.Save();
                graphics.SmoothingMode = SmoothingMode.HighSpeed;

                try
                {
                    graphics.TranslateTransform(-Location.X, -Location.Y);
                    InvokePaintBackground(Parent, e);
                    InvokePaint(Parent, e);
                }
                finally
                {
                    graphics.Restore(state);
                    clipRect.Offset(-Location.X, -Location.Y);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle area = e.ClipRectangle;
            Graphics g = e.Graphics;
            
            PaintTransparentBackground(g, area);
            g.DrawImageUnscaled(Enabled 
                ? shouldDrawClicked 
                    ? Assets.LoginButtonClicked : shouldDrawHover 
                        ? Assets.LoginButtonHovered : Assets.LoginButtonEnabled 
                : Assets.LoginButtonDisabled, area);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            shouldDrawClicked = true;
            Refresh();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            shouldDrawClicked = false;
            Refresh();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            shouldDrawHover = true;
            Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            shouldDrawHover = false;
            shouldDrawClicked = false;
            Refresh();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Width = 240;
            Height = 24;
            base.OnSizeChanged(e);
        }
    }
}
