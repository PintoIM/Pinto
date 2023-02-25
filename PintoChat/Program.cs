using PintoChat.Forms;
using PintoNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoChat
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.Run(new MainForm());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            FatalErrorForm fatalErrorForm = new FatalErrorForm();
            fatalErrorForm.rtxtLog.Text = $"{e.ExceptionObject}";
            fatalErrorForm.ShowDialog();
            Environment.Exit(0);
        }
    }
}
