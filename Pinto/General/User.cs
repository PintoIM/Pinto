using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintoNS.General
{
    public class User
    {
        public string Name;
        public UserStatus Status;

        public static Bitmap StatusToBitmap(UserStatus status) 
        {
            switch (status) 
            {
                case UserStatus.ONLINE:
                    return Statuses.ONLINE;
                case UserStatus.AWAY:
                    return Statuses.AWAY;
                case UserStatus.BUSY: 
                    return Statuses.BUSY;
                case UserStatus.INVISIBLE: 
                    return Statuses.INVISIBLE;
                default:
                    return Statuses.OFFLINE;
            }
        }

        public static string StatusToText(UserStatus status)
        {
            switch (status)
            {
                case UserStatus.ONLINE:
                    return "Online";
                case UserStatus.AWAY:
                    return "Away";
                case UserStatus.BUSY:
                    return "Busy";
                case UserStatus.INVISIBLE:
                    return "Invisible";
                default:
                    return "Offline";
            }
        }
    }
}
