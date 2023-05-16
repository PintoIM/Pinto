using PintoNS.Forms;
using PintoNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using PintoNS.Forms.Notification;
using PintoNS.General;

namespace PintoNS
{
    public static class Program
    {
        [DllImport("ntdll.dll", EntryPoint = "wine_get_version")]
        private static extern string GetWineVersion();
        public static ConsoleForm Console;
        public const string VERSION = "a1.5";
        public const int PROTOCOL_VERSION = 15;

        [STAThread]
        static void Main()
        {
            // Enable visual styles
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Unhandled exception handler
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // Setup console
            Console = new ConsoleForm();
            Console.Show();
            Console.Hide();

            // Detect what runtime we are being ran under
            bool underWine = false;
            try
            {
                string wineVer = GetWineVersion();
                Console.WriteMessage($"[General] Running under wine ({wineVer})");
                underWine = true;
            }
            catch (Exception ex)
            {
                Console.WriteMessage($"[General] Not running under wine: {ex}");
            }

            if (Type.GetType("Mono.Runtime") != null) 
            {
                Console.WriteMessage("[General] Running under mono");

                if (!underWine) 
                {
                    MsgBox.ShowNotification(Console,
                        $"Pinto! has detected it is being ran" +
                        $" under mono but not under wine!{Environment.NewLine}" +
                        $"This execution configuration will never be supported!",
                        "Unsupported Execution Configuration",
                        MsgBoxIconType.ERROR);
                    return;
                }
            }

            if (Environment.OSVersion.Version.Major == 6 &&
                Environment.OSVersion.Version.Minor <= 1) 
            {
                MsgBox.ShowNotification(Console,
                    $"Pinto! has detected it is being ran on a" +
                    $" potentially unsupported operating system!{Environment.NewLine}" +
                    $"This execution configuration will not receive support!",
                    "Unsupported Execution Configuration",
                    MsgBoxIconType.WARNING);
            }
            
            // Start Pinto!
            Application.Run(new MainForm());
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            UnhandledExceptionHandler(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            UnhandledExceptionHandler(e.ExceptionObject);
        }

        private static void UnhandledExceptionHandler(object ex) 
        {
            FatalErrorForm fatalErrorForm = new FatalErrorForm();
            fatalErrorForm.rtxtLog.Text = $"{ex}";
            fatalErrorForm.ShowDialog();
            Environment.Exit(0);
        }
    }
}
