using PintoNS.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PintoNS.Controls
{
    // Stolen from https://stackoverflow.com/a/32618479
    public class ExRichTextBox : RichTextBox
    {
        public static IntPtr LoadLibrary(string s_File)
        {
            IntPtr module = PInvoke.LoadLibraryW(s_File);
            if (module != IntPtr.Zero)
                return module;
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;

                if (!Program.ExecutingUnderWine) 
                {
                    try
                    {
                        LoadLibrary("MsftEdit.dll"); // Available since XP SP1
                        createParams.ClassName = "RichEdit50W";
                    }
                    catch { /* Windows XP without any Service Pack. */ }
                }

                return createParams;
            }
        }
    }
}
