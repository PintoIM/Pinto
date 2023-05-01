using PintoNS.Forms;
using PintoNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS
{
    public static class Program
    {
        public static ConsoleForm Console;
        public const string VERSION = "a1.0";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Console = new ConsoleForm();
            Console.Show();
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
