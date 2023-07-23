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

namespace PintoNS
{
    public static class Program
    {
        // Constants
        public static ConsoleForm Console;
        public const string VERSION_STRING = "b1.0-pre1";
        public const int PROTOCOL_VERSION = 1;

        // Data paths
        public static readonly string DataFolder = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData), "Pinto!");
        public static readonly string SettingsFile = Path.Combine(DataFolder, "settings.json");

        // Main variables
        public static bool Running;
        public static bool ExecutingUnderWine;
        public static readonly List<LuaExtension> Extensions = new List<LuaExtension>();

        // Check for new form event
        private static Thread checkForNewForm;
        public static event EventHandler<Form> FormOpened;

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
            Console.WriteMessage("[General] Initialization...");

            // Detect what runtime we are being ran under
            try
            {
                string wineVer = PInvoke.GetWineVersion();
                Console.WriteMessage($"[General] Running under wine ({wineVer})");
                ExecutingUnderWine = true;
            }
            catch (Exception ex)
            {
                Console.WriteMessage($"[General] Not running under wine: {ex}");
            }

            if (Type.GetType("Mono.Runtime") != null) 
            {
                Console.WriteMessage("[General] Running under mono");

                if (!ExecutingUnderWine) 
                {
                    MsgBox.Show(Console,
                        $"Pinto! has detected it is being ran" +
                        $" under mono but not under wine!{Environment.NewLine}" +
                        $"This execution configuration will never be supported!",
                        "Unsupported Execution Configuration",
                        MsgBoxIconType.ERROR);
                    return;
                }
            }

            // Start the new form checker thread
            Running = true;
            checkForNewForm = new Thread(new ThreadStart(CheckForNewFormThread_Func));
            checkForNewForm.Start();

            // Create the structure in the data folder
            if (!Directory.Exists(DataFolder))
                Directory.CreateDirectory(DataFolder);
            if (!Directory.Exists(Path.Combine(DataFolder, "chats")))
                Directory.CreateDirectory(Path.Combine(DataFolder, "chats"));
            if (!Directory.Exists(Path.Combine(DataFolder, "extensions")))
                Directory.CreateDirectory(Path.Combine(DataFolder, "extensions"));

            // Import the saved settings
            Settings.Import(SettingsFile);

            // Load extensions
            LoadExtensions();
            
            // Start Pinto!
            Application.Run(new LoginForm());

            // End the new form checker thread
            Running = false;
            checkForNewForm.Abort();
        }

        public static void LoadExtensions()
        {
            Console.WriteMessage($"[Extensions] Loading extensions...");
            List<LuaExtension> highPriority = new List<LuaExtension>();
            List<LuaExtension> mediumPriority = new List<LuaExtension>();
            List<LuaExtension> lowPriority = new List<LuaExtension>();

            foreach (string file in Directory.EnumerateFiles(
                Path.Combine(DataFolder, "extensions"), "*.lua"))
            {
                LuaExtension ext = null;
                try
                {
                    ext = new LuaExtension(file, null);
                }
                catch (Exception ex)
                {
                    Console.WriteMessage($"[Extensions] Unable to load an extension: {ex}");
                    continue;
                }

                switch (ext.Priority)
                {
                    case 0:
                        lowPriority.Add(ext);
                        break;
                    case 1:
                        mediumPriority.Add(ext);
                        break;
                    case 2:
                        highPriority.Add(ext);
                        break;
                }

                Console.WriteMessage($"[Extensions] Added extension \"{ext.Name}\" by" +
                    $" \"{ext.Author}\" (version {ext.Version}) to the list of to load extensions" +
                    $" (priority {ext.Priority})");
            }

            Action<LuaExtension> forEachExtension = (LuaExtension ext) =>
            {
                Extensions.Add(ext);
                ext.PrintLoadMessage();
                CallExtensionEvent(ext, "OnLoad");
            };

            Console.WriteMessage($"[Extensions] Loading high priority extensions...");
            highPriority.ForEach(forEachExtension);

            Console.WriteMessage($"[Extensions] Loading medium priority extensions...");
            mediumPriority.ForEach(forEachExtension);

            Console.WriteMessage($"[Extensions] Loading low priority extensions...");
            lowPriority.ForEach(forEachExtension);
        }

        public static void CallExtensionEvent(LuaExtension ext, string eventName) 
        {
            if (ext.Script[eventName] == null) return;
            ext.Script.GetFunction(eventName).Call();
        }

        public static void CallExtensionsEvent(string eventName)
        {
            foreach (LuaExtension ext in Extensions)
                CallExtensionEvent(ext, eventName);
        }

        public static void UnloadExtension(LuaExtension ext)
        {
            CallExtensionEvent(ext, "OnUnload");
            Extensions.Remove(ext);
            ext.PrintUnloadMessage();
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

        private static void CheckForNewFormThread_Func() 
        {
            List<Form> lastOpenedForms = new List<Form>();

            while (Running) 
            {
                if (lastOpenedForms.Count == Application.OpenForms.Count) 
                {
                    Thread.Sleep(1);
                    continue;
                }

                List<Form> lastOpenedFormsArr = lastOpenedForms.ToList();
                lastOpenedForms.Clear();

                foreach (Form form in Application.OpenForms.Cast<Form>().ToList())
                {
                    if (!lastOpenedFormsArr.Contains(form) && FormOpened != null)
                        FormOpened.Invoke(null, form);
                    lastOpenedForms.Add(form);
                }

                Thread.Sleep(1);
            }
        }
    }
}
