using PintoNS.Forms;
using PintoNS.Forms.Notification;
using PintoNS.General;
using System;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace PintoNS
{
    public static class Program
    {
        public static ConsoleForm Console;
        public const string VERSION_STRING = "exp1";
        public const int PROTOCOL_VERSION = 0;
        public static bool RunningUnderWine;

        [STAThread]
        static void Main()
        {
            // Enable TLS 1.0, 1.1, 1.2
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = 
                Environment.OSVersion.Version.Major < 6 || 
                Environment.OSVersion.Version.Minor < 1 ? SecurityProtocolType.Tls : 
                // 768 = TLS 1.1
                // 3072 = TLS 1.2
                // These are not available in a .NET 4.0 runtime, but available in a .NET 4.5
                SecurityProtocolType.Tls | (SecurityProtocolType)768 | (SecurityProtocolType)3072;

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
            try
            {
                string wineVer = PInvoke.GetWineVersion();
                Console.WriteMessage($"[General] Running under wine ({wineVer})");
                RunningUnderWine = true;
            }
            catch (Exception ex)
            {
                Console.WriteMessage($"[General] Not running under wine: {ex}");
            }

            if (Type.GetType("Mono.Runtime") != null) 
            {
                Console.WriteMessage("[General] Running under mono");

                if (!RunningUnderWine) 
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

            // Start Pinto!
            Application.Run(new MainForm());
        }

        public static Icon GetFormIcon() => 
            Environment.OSVersion.Version.Major < 6 ? Logo.LOGO_XP : Logo.LOGO2;

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
