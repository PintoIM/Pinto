using PintoNS.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PintoNS.General
{
    public class InWindowPopupController
    {
        private Control parent;
        private int width;
        private int baseY;
        private List<InWindowPopupControl> popups = new List<InWindowPopupControl>();

        public InWindowPopupController(Control parent, int width, int baseY)
        {
            this.parent = parent;
            this.width = width;
            this.baseY = baseY;
        }

        public void UpdateSizes(int width, int baseY) 
        {
            this.width = width;
            this.baseY = baseY;
            UpdatePopupPositions();
        }

        private int GetYPosForNew()
        {
            int y = baseY;

            foreach (InWindowPopupControl popup in popups.ToArray())
            {
                y -= popup.Height;
            }

            return y;
        }

        public void UpdatePopupPositions() 
        {
            parent.Invoke(new Action(() =>
            {
                int y = baseY;

                foreach (InWindowPopupControl popup in popups.ToArray())
                {
                    popup.Width = width;
                    popup.Location = new Point(0, y);
                    y -= popup.Height;
                }
            }));
        }

        public void CreatePopup(string text)
        {
            parent.Invoke(new Action(() => 
            {
                InWindowPopupControl popup = new InWindowPopupControl(text);
                popup.btnClose.Click += (object sender, EventArgs e) =>
                {
                    ClosePopup(popup);
                };
                popup.Parent = parent;
                popup.Width = width;
                popup.Location = new Point(0, GetYPosForNew());
                popup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                popup.Show();
                popup.BringToFront();
                popups.Add(popup);
            }));
        }

        public void ClosePopup(InWindowPopupControl popup)
        {
            parent.Invoke(new Action(() =>
            {
                if (popup == null) return;
                popup.Hide();
                popup.Dispose();
                popups.Remove(popup);
                UpdatePopupPositions();
            }));
        }

        public void ClearPopups()
        {
            foreach (InWindowPopupControl popup in popups.ToArray())
            {
                parent.Invoke(new Action(() =>
                {
                    popup.Hide();
                    popup.Dispose();
                }));
            }

            popups.Clear();
        }
    }
}
