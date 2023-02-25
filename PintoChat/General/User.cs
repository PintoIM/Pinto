using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintoChat.General
{
    public class User
    {
        public int ID;
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
    }
}
