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
    public class ExRichTextBox : RichTextBox
    {
        public static IntPtr LoadLibrary(string library)
        {
            IntPtr module = PInvoke.LoadLibraryW(library);

            if (module == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            return module;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;

                try
                {
                    LoadLibrary("MsftEdit.dll");
                    createParams.ClassName = "RichEdit50W";
                }
                catch { }

                return createParams;
            }
        }
    }
}
