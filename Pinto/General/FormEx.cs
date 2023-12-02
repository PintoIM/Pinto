using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace PintoNS.General
{
    public static class FormEx
    {
        /// <summary>
        /// Moves this form to the center of the specified window
        /// </summary>
        /// <param name="form"></param>
        /// <param name="window">the window to move to</param>
        public static void MoveCenteredToWindow(this Form form, Form window) 
        {
            if (window == null) return;
            int x = window.Location.X + ((window.Width - form.Width) / 2);
            int y = window.Location.Y + ((window.Height - form.Height) / 2);
            form.Location = new Point(Math.Max(x, 0), Math.Max(y, 0));
        }
    }
}
