using PintoChat.Controls;
using PintoChat.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoChat.General
{
    public class InWindowPopupController
    {
        private Form form;
        private int baseY;
        private Dictionary<InWindowPopupControl, int> popups = new Dictionary<InWindowPopupControl, int>();

        public InWindowPopupController(Form form, int baseY)
        {
            this.form = form;
            this.baseY = baseY;
        }

        private int GetYPosForNew()
        {
            int y = baseY;

            foreach (InWindowPopupControl popup in popups.Keys)
            {
                y += popup.Height;
            }

            return y;
        }

        public void UpdatePopupPositions() 
        {
            int y = baseY;

            foreach (InWindowPopupControl popup in popups.Keys)
            {
                popup.Location = new Point(0, y);
                y += popup.Height;
            }
        }

        public void CreatePopup(string text, int maxTime)
        {
            InWindowPopupControl popup = new InWindowPopupControl(text);
            popup.btnClose.Click += (object sender, EventArgs e) =>
            {
                ClosePopup(popup);
            };
            popup.Parent = form;
            popup.Width = form.Width - 5;
            popup.Height = 21;
            popup.Location = new Point(0, GetYPosForNew());
            popup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            popup.Show();
            popup.BringToFront();
            popups.Add(popup, maxTime);
        }

        public void ClosePopup(InWindowPopupControl popup)
        {
            if (popup == null) return;
            popup.Hide();
            popup.Dispose();
            popups.Remove(popup);
            UpdatePopupPositions();
        }

        public void ClearPopups()
        {
            foreach (InWindowPopupControl popup in popups.Keys)
            {
                popup.Hide();
                popup.Dispose();
            }

            popups.Clear();
        }
    }
}
