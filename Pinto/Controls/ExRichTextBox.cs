using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PintoNS.Controls
{
    public class ExRichTextBox : RichTextBox
    {
        [DllImport("kernel32.dll", EntryPoint = "LoadLibraryW", 
            CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr LoadLibraryW(string s_File);

        public static IntPtr LoadLibrary(string s_File)
        {
            IntPtr module = LoadLibraryW(s_File);
            if (module != IntPtr.Zero)
                return module;
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;

                try
                {
                    LoadLibrary("MsftEdit.dll"); // Available since XP SP1
                    createParams.ClassName = "RichEdit50W";
                }
                catch { /* Windows XP without any Service Pack. */ }

                return createParams;
            }
        }
    }
}
