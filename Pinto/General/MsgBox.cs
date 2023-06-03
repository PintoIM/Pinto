using PintoNS.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PintoNS.General
{
    public static class MsgBox
    {
        public static void Show(Form parent, string body, string title = null,
            MsgBoxIconType icon = MsgBoxIconType.INFORMATION,
            bool nonBlocking = false, bool isPrompt = false, 
            Action<MsgBoxButtonType> callback = null) 
        {
            if (callback == null) callback = delegate (MsgBoxButtonType button) { };
            MsgBoxForm msgBox = new MsgBoxForm();

            msgBox.Text = title;
            msgBox.lTitle.Text = title;
            msgBox.lBody.Text = body;
            msgBox.UserPressedButton = callback;

            if (isPrompt)
            {
                msgBox.btnOK.Enabled = false;
                msgBox.btnOK.Visible = false;
            }
            else 
            {
                msgBox.btnYes.Enabled = false;
                msgBox.btnNo.Enabled = false;
                msgBox.btnYes.Visible = false;
                msgBox.btnNo.Visible = false;
            }

            switch (icon)
            {
                case MsgBoxIconType.INFORMATION:
                    msgBox.pbIcon.Image = SystemIcons.Information.ToBitmap();
                    break;
                case MsgBoxIconType.QUESTION:
                    msgBox.pbIcon.Image = SystemIcons.Question.ToBitmap();
                    break;
                case MsgBoxIconType.WARNING:
                    msgBox.pbIcon.Image = SystemIcons.Warning.ToBitmap();
                    break;
                case MsgBoxIconType.ERROR:
                    msgBox.pbIcon.Image = SystemIcons.Error.ToBitmap();
                    break;
                default:
                    msgBox.pbIcon.Image = null;
                    break;
            }

            if (parent != null && parent.WindowState != FormWindowState.Normal) parent = null;
            msgBox.StartPosition = parent == null ?
                FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

            if (nonBlocking)
            {
                msgBox.Show(parent);

                if (parent != null)
                {
                    int x = (parent.Location.X + (parent.Width - msgBox.Width)) / 2;
                    int y = (parent.Location.Y + (parent.Height - msgBox.Height)) / 2;
                    msgBox.Location = new Point(Math.Max(x, 0), Math.Max(y, 0));
                }
            }
            else
                msgBox.ShowDialog(parent);
        }

        [Obsolete("The Show method should be used instead")]
        public static void ShowNotification(Form parent, string body, string title = "Notification", 
            MsgBoxIconType icon = MsgBoxIconType.INFORMATION, 
            bool nonBlocking = false, Action <MsgBoxButtonType> callback = null) 
        {
            Show(parent, body, title, icon, nonBlocking, false, callback);
        }

        [Obsolete("The Show method should be used instead")]
        public static void ShowPromptNotification(Form parent, string body, string title = "Notification",
            MsgBoxIconType icon = MsgBoxIconType.INFORMATION,
            bool nonBlocking = false, Action<MsgBoxButtonType> callback = null)
        {
            Show(parent, body, title, icon, nonBlocking, true, callback);
        }
    }
}
