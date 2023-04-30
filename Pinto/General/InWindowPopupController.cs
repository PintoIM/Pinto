using PintoNS.Controls;
using PintoNS.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS.General
{
    public class InWindowPopupController
    {
        private Form form;
        private int baseY;
        private List<InWindowPopupControl> popups = new List<InWindowPopupControl>();

        public InWindowPopupController(Form form, int baseY)
        {
            this.form = form;
            this.baseY = baseY;
        }

        private int GetYPosForNew()
        {
            int y = baseY;

            foreach (InWindowPopupControl popup in popups.ToArray())
            {
                y += popup.Height;
            }

            return y;
        }

        public void UpdatePopupPositions() 
        {
            form.Invoke(new Action(() =>
            {
                int y = baseY;

                foreach (InWindowPopupControl popup in popups.ToArray())
                {
                    popup.Location = new Point(0, y);
                    y += popup.Height;
                }
            }));
        }

        public void CreatePopup(string text)
        {
            form.Invoke(new Action(() => 
            {
                InWindowPopupControl popup = new InWindowPopupControl(text);
                popup.btnClose.Click += (object sender, EventArgs e) =>
                {
                    ClosePopup(popup);
                };
                popup.Parent = form;
                popup.Width = form.Width - 15;
                popup.Height = 21;
                popup.Location = new Point(0, GetYPosForNew());
                popup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                popup.Show();
                popup.BringToFront();
                popups.Add(popup);
            }));
        }

        public void ClosePopup(InWindowPopupControl popup)
        {
            form.Invoke(new Action(() =>
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
                form.Invoke(new Action(() =>
                {
                    popup.Hide();
                    popup.Dispose();
                }));
            }

            popups.Clear();
        }
    }
}
