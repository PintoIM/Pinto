using PintoNS.UI.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PintoNS.UI
{
    public class InWindowPopupController : IDisposable
    {
        private Form form;
        private int baseY;
        private List<InWindowPopupControl> popups = new List<InWindowPopupControl>();
        private Timer autoCloseTimer = new Timer();

        public InWindowPopupController(Form form, int baseY)
        {
            this.form = form;
            this.baseY = baseY;
            autoCloseTimer.Interval = 100;
            autoCloseTimer.Tick += AutoCloseTimer_Tick;
            autoCloseTimer.Start();
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
            if (form.IsDisposed || form.Disposing) return;

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

        public void CreatePopup(string text, bool isInfo = false, float autoClosureTime = -1)
        {
            if (form.IsDisposed || form.Disposing) return;

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
                popup.BackColor = isInfo ? Color.FromArgb(225, 255, 255) : Color.FromArgb(255, 255, 225);
                popup.pbIcon.Image = isInfo ? Assets.INFO_ENABLED : Assets.WARNING;
                popup.TimeBeforeClosure = autoClosureTime;
                popup.Show();
                popup.BringToFront();
                popups.Add(popup);
            }));
        }

        public void ClosePopup(InWindowPopupControl popup)
        {
            if (form.IsDisposed || form.Disposing) return;

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
            if (form.IsDisposed || form.Disposing) return;

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

        private void AutoCloseTimer_Tick(object sender, EventArgs e)
        {
            if (form.IsDisposed || form.Disposing) return;

            foreach (InWindowPopupControl popup in popups.ToArray())
            {
                if (popup.TimeBeforeClosure == -1.0f) continue;
                popup.TimeBeforeClosure -= 0.1f;
                if (popup.TimeBeforeClosure <= 0) ClosePopup(popup);
            }
        }

        public void Dispose()
        {
            foreach (InWindowPopupControl popup in popups.ToArray()) popup.Dispose();
            popups.Clear();
            autoCloseTimer.Stop();
            autoCloseTimer.Dispose();
        }
    }
}
