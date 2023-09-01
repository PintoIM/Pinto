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
        public const int MF_BYCOMMAND = 0;
        public const int MF_DISABLED = 2;
        public const int SC_CLOSE = 0xF060;
        public const int SC_RESTORE = 0xF120;
        public const int WM_COPYDATA = 0x004A;
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SW_SHOWNOACTIVATE = 4;
        public const uint SWP_NOACTIVATE = 0x0010;
        public const int HWND_TOPMOST = -1;
        public const int HWND_BOTTOM = 1;

        [DllImport("ntdll.dll", EntryPoint = "wine_get_version")]
        public static extern string GetWineVersion();

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr LoadLibraryW(string s_File);

        [DllImport("user32.dll")]
        public static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(int hWnd, int hWndInsertAfter, int x, int y,
            int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void ShowFormInactive(Form form, int hWndInsertAfter)
        {
            ShowWindow(form.Handle, SW_SHOWNOACTIVATE);
            SetWindowPos(
                form.Handle.ToInt32(),
                hWndInsertAfter, 
                form.Left, 
                form.Top, 
                form.Width, 
                form.Height, 
                SWP_NOACTIVATE);
        }
    }
}
