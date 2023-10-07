using PintoNS.Forms;
using PintoNS.General;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace PintoNS
{
    public static class Program
    {
        // Constants
        public static ConsoleForm Console;
        public const string VERSION_STRING = "b1.0";
        public const int PROTOCOL_VERSION = 2;

        // Data paths
        public static readonly string DataFolder = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData), "Pinto!");
        public static readonly string SettingsFile = Path.Combine(DataFolder, "settings.json");

        // Main variables
        public static MainForm MainFrm;
        public static bool RunningOnLegacyPlatform;

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

            // Print the operating system information
            Console.WriteMessage($"[General] Running on {Environment.OSVersion.Platform}" +
                $" ({Environment.OSVersion.VersionString})");

            if (Environment.OSVersion.Version.Major < 6)
            {
                RunningOnLegacyPlatform = true;
                Console.WriteMessage($"[General] Running on a legacy platform (<= Windows XP)");
                MsgBox.Show(null, $"You are running Pinto! on a legacy platform (<= Windows XP)." +
                    $" This means that you might experience issues unique to this platform that won't be fixed!" +
                    $" Please update to a newer operating system!", "Legacy Platform", MsgBoxIconType.WARNING);
            }

            // Detect what runtime we are being ran under
            try
            {
                string wineVer = PInvoke.GetWineVersion();
                Console.WriteMessage($"[General] Running under wine ({wineVer})");
            }
            catch (Exception ex)
            {
                Console.WriteMessage($"[General] Not running under wine: {ex}");
            }

            if (Type.GetType("Mono.Runtime") != null)
                Console.WriteMessage("[General] Running under mono");

            if (!Directory.Exists(DataFolder))
                Directory.CreateDirectory(DataFolder);
            if (!Directory.Exists(Path.Combine(DataFolder, "chats")))
                Directory.CreateDirectory(Path.Combine(DataFolder, "chats"));
            if (!Directory.Exists(Path.Combine(DataFolder, "extensions")))
                Directory.CreateDirectory(Path.Combine(DataFolder, "extensions"));

            // Create the main form
            MainFrm = new MainForm();

            // Start Pinto!
            Application.Run(MainFrm);
        }

        public static Icon GetFormIcon() => Logo.LOGO_ICO;

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

        public static IEnumerable<string> SplitStringIntoChunks(string str, int chunkSize)
        {
            for (int i = 0; i < str.Length; i += chunkSize)
                yield return str.Substring(i, Math.Min(chunkSize, str.Length - i));
        }

        public static string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }
    }
}
