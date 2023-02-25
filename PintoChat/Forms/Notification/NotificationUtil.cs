using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoChat.Forms.Notification
{
    public static class NotificationUtil
    {
        public static void ShowNotification(Form parent, string body, string title = "Notification", 
            NotificationIconType icon = NotificationIconType.INFORMATION, 
            bool nonBlocking = false, Action < NotificationButtonType> callback = null) 
        {
            if (callback == null) callback = delegate (NotificationButtonType button) { };
            NotificationOkButton notification = new NotificationOkButton();

            notification.lTitle.Text = title;
            notification.lBody.Text = body;
            notification.UserPressedButton = callback;

            switch (icon) 
            {
                case NotificationIconType.INFORMATION:
                    notification.pbIcon.Image = SystemIcons.Information.ToBitmap();
                    break;
                case NotificationIconType.QUESTION:
                    notification.pbIcon.Image = SystemIcons.Question.ToBitmap();
                    break;
                case NotificationIconType.WARNING:
                    notification.pbIcon.Image = SystemIcons.Warning.ToBitmap();
                    break;
                case NotificationIconType.ERROR:
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
            NotificationIconType icon = NotificationIconType.INFORMATION,
            bool nonBlocking = false, Action<NotificationButtonType> callback = null)
        {
            if (callback == null) callback = delegate (NotificationButtonType button) { };
            NotificationYesNoButton notification = new NotificationYesNoButton();

            notification.lTitle.Text = title;
            notification.lBody.Text = body;
            notification.UserPressedButton = callback;

            switch (icon)
            {
                case NotificationIconType.INFORMATION:
                    notification.pbIcon.Image = SystemIcons.Information.ToBitmap();
                    break;
                case NotificationIconType.QUESTION:
                    notification.pbIcon.Image = SystemIcons.Question.ToBitmap();
                    break;
                case NotificationIconType.WARNING:
                    notification.pbIcon.Image = SystemIcons.Warning.ToBitmap();
                    break;
                case NotificationIconType.ERROR:
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
