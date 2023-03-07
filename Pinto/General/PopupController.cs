using PintoNS.Controls;
using PintoNS.Forms;
using PintoNS.Forms.Notification;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace PintoNS.General
{
    public class PopupController
    {
        private List<PopupForm> popups = new List<PopupForm>();
        private List<PopupForm> queuedPopups = new List<PopupForm>();

        private int GetBaseX() 
        {
            return (int) SystemParameters.WorkArea.Width - 200;
        }

        private int GetBaseY() 
        {
            return (int) SystemParameters.WorkArea.Height - 177;
        }

        private int GetYPosForNew()
        {
            int y = GetBaseY();

            foreach (PopupForm popup in popups.ToArray())
            {
                y -= popup.Height;
            }

            return y;
        }

        public void UpdatePopupPositions() 
        {
            int y = GetBaseY();

            foreach (PopupForm popup in popups.ToArray())
            {
                popup.Location = new System.Drawing.Point(GetBaseX(), y);
                y -= popup.Height;
            }

            foreach (PopupForm popup in queuedPopups.ToArray())
            {
                popup.ReachedTargetY = true;
                popup.Location = new System.Drawing.Point(GetBaseX(), y);
                popup.Show();
                popup.BringToFront();

                popups.Add(popup);
                queuedPopups.Remove(popup);

                y -= popup.Height;
                if (y - popup.Height < 0) break;
            }
        }

        public void CreatePopup(string body, string title)
        {
            int y = GetYPosForNew();

            PopupForm popup = new PopupForm();
            popup.TargetY = y;
            popup.lTitle.Text = title;
            popup.lBody.Text = body;
            popup.btnClose.Click += (object sender, EventArgs e) =>
            {
                ClosePopup(popup);
            };

            if (y < 0)
            {
                queuedPopups.Add(popup);
                return;
            }

            popup.Location = new System.Drawing.Point(GetBaseX(), GetBaseY() + 177);
            popup.Show();
            popup.BringToFront();

            popups.Add(popup);
        }

        public void ClosePopup(PopupForm popup)
        {
            if (popup == null) return;
            popup.Close();
            popup.Dispose();
            popups.Remove(popup);
            UpdatePopupPositions();
        }

        public void ClearPopups()
        {
            foreach (PopupForm popup in popups.ToArray())
            {
                popup.Close();
                popup.Dispose();
            }

            popups.Clear();
        }
    }
}
