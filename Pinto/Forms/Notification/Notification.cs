using PintoNS.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS.Forms.Notification
{
    public partial class Notification : Form
    {
        #region Win32
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        #endregion
        public Action<NotificationButtonType> UserPressedButton = delegate (NotificationButtonType button) { };

        public Notification()
        {
            InitializeComponent();
        }

        private void Notification_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            UserPressedButton.Invoke(NotificationButtonType.CLOSE);
            Close();
        }

        private void Notification_Shown(object sender, EventArgs e)
        {
            CenterToParent();
        }
    }
}
