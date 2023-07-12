using PintoNS.JacksonTabControl;
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
    public class ModernTabControlStyleProvider : TabStyleProvider
    {
        public ModernTabControlStyleProvider(CustomTabControl tabControl) : base(tabControl) 
        {
            Padding = new Point(20, 5);
            BorderColor = Color.DarkGray;
            BorderColorSelected = Color.DarkGray;
            HotTrack = false;
            FocusTrack = false;
            IgnoreImageOnTextDraw = true;
            TextAlign = ContentAlignment.MiddleLeft;
        }

        public override void AddTabBorder(GraphicsPath path, Rectangle tabBounds)
        {
            path.AddRectangle(tabBounds);
        }

        protected override Brush GetTabBackgroundBrush(int index)
        {
            return new SolidBrush(_TabControl.SelectedIndex == index ? Color.White : Color.LightGray);
        }
    }
}
