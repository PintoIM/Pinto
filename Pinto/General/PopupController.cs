using PintoNS.Forms;
using System;
using System.Collections.Generic;
using System.Windows;

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
                popup.TopMost = true;
                popup.Show();
                popup.BringToFront();

                popups.Add(popup);
                queuedPopups.Remove(popup);

                y -= popup.Height;
                if (y - popup.Height < 0) break;
            }
        }

        public void CreatePopup(string body, string title, int autoCloseTicks = 5)
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
            if (autoCloseTicks > 0)
            {
                popup.MaxAutoCloseTicks = autoCloseTicks;
                popup.tAutoClose.Start();
            }

            if (y < 0)
            {
                queuedPopups.Add(popup);
                return;
            }

            popup.Location = new System.Drawing.Point(GetBaseX(), GetBaseY() + 177);
            popup.TopMost = true;
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
