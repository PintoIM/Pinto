using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PintoNS.General
{
    public class PInvoke
    {
        public const int SC_RESTORE = 0xF120;
        public const int WM_SYSCOMMAND = 0x0112;

        [DllImport("ntdll.dll", EntryPoint = "wine_get_version")]
        public static extern string GetWineVersion();
    }
}
