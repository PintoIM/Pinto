using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS.Forms.Notification
{
    public static class MsgBox
    {
        public static void ShowNotification(Form parent, string body, string title = "Notification", 
            MsgBoxIconType icon = MsgBoxIconType.INFORMATION, 
            bool nonBlocking = false, Action < MsgBoxButtonType> callback = null) 
        {
            if (callback == null) callback = delegate (MsgBoxButtonType button) { };
            MsgBoxForm notification = new MsgBoxForm();

            notification.Text = title;
            notification.lTitle.Text = title;
            notification.lBody.Text = body;
            notification.UserPressedButton = callback;
            notification.btnYes.Enabled = false;
            notification.btnNo.Enabled = false;
            notification.btnYes.Visible = false;
            notification.btnNo.Visible = false;

            switch (icon) 
            {
                case MsgBoxIconType.INFORMATION:
                    notification.pbIcon.Image = SystemIcons.Information.ToBitmap();
                    break;
                case MsgBoxIconType.QUESTION:
                    notification.pbIcon.Image = SystemIcons.Question.ToBitmap();
                    break;
                case MsgBoxIconType.WARNING:
                    notification.pbIcon.Image = SystemIcons.Warning.ToBitmap();
                    break;
                case MsgBoxIconType.ERROR:
                    notification.pbIcon.Image = SystemIcons.Error.ToBitmap();
                    break;
                default:
                    notification.pbIcon.Image = null;
                    break;
            }

            if (nonBlocking) 
                notification.Show(parent);
            else
                notification.ShowDialog(parent);
        }

        public static void ShowPromptNotification(Form parent, string body, string title = "Notification",
            MsgBoxIconType icon = MsgBoxIconType.INFORMATION,
            bool nonBlocking = false, Action<MsgBoxButtonType> callback = null)
        {
            if (callback == null) callback = delegate (MsgBoxButtonType button) { };
            MsgBoxForm notification = new MsgBoxForm();

            notification.Text = title;
            notification.lTitle.Text = title;
            notification.lBody.Text = body;
            notification.UserPressedButton = callback;
            notification.btnOK.Enabled = false;
            notification.btnOK.Visible = false;
            
            switch (icon)
            {
                case MsgBoxIconType.INFORMATION:
                    notification.pbIcon.Image = SystemIcons.Information.ToBitmap();
                    break;
                case MsgBoxIconType.QUESTION:
                    notification.pbIcon.Image = SystemIcons.Question.ToBitmap();
                    break;
                case MsgBoxIconType.WARNING:
                    notification.pbIcon.Image = SystemIcons.Warning.ToBitmap();
                    break;
                case MsgBoxIconType.ERROR:
                    notification.pbIcon.Image = SystemIcons.Error.ToBitmap();
                    break;
                default:
                    notification.pbIcon.Image = null;
                    break;
            }

            if (nonBlocking)
                notification.Show(parent);
            else
                notification.ShowDialog(parent);
        }
    }
}
