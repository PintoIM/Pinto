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
    // From https://stackoverflow.com/a/24087828
    public class MenuButton : Button
    {
        [DefaultValue(null)]
        public ContextMenuStrip Menu { get; set; }

        [DefaultValue(false)]
        public bool ShowMenuUnderCursor { get; set; }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (Menu != null && e.Button == MouseButtons.Left)
            {
                Point menuLocation;

                if (ShowMenuUnderCursor)
                    menuLocation = e.Location;
                else
                    menuLocation = new Point(0, Height - 1);
                
                Menu.Show(this, menuLocation);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Menu != null)
            {
                int arrowX = ClientRectangle.Width - Padding.Right - 14;
                int arrowY = (ClientRectangle.Height / 2) - 1;
                
                e.Graphics.FillPolygon(new SolidBrush(Enabled ? ForeColor : SystemColors.ControlDark), 
                new Point[]
                {
                    new Point(arrowX, arrowY),
                    new Point(arrowX + 7, arrowY),
                    new Point(arrowX + 3, arrowY + 4)
                });
            }
        }
    }
}
