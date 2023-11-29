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
using System.Runtime.Versioning;
using System.Reflection;

namespace PintoNS
{
    public static class Program
    {
        // Constants
        public static ConsoleForm Console;
        public const string VERSION_STRING = "b1.2";
        public const byte PROTOCOL_VERSION = 10;

        // Data paths
        public static readonly string DataFolder = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData), "Pinto!");
        public static readonly string SettingsFile = Path.Combine(DataFolder, "settings.json");

        // Main variables
        public static MainForm MainFrm;
        public static bool RunningOnLegacyPlatform;
        public static List<PintoPluginHost> Plugins = new List<PintoPluginHost>();

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

            // Detect what runtime we are being ran under
            try
            {
                string wineVer = PInvoke.GetWineVersion();
                Console.WriteMessage($"[General] Running under wine ({wineVer})");
            }
            catch { Console.WriteMessage("[General] Not running under wine"); }

            bool runningUnderMono = Type.GetType("Mono.Runtime") != null;
            if (runningUnderMono)
                Console.WriteMessage("[General] Running under mono");
            else
                Console.WriteMessage("[General] Not running under mono");

            if (!runningUnderMono)
            {
                // Enable TLS 1.0, 1.1, 1.2
                Version version = NETFrameworkVersion.GetVersion();
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol =
                    version.Minor < 5 ? SecurityProtocolType.Tls :
                    // 768 = TLS 1.1
                    // 3072 = TLS 1.2
                    // These are not available in a .NET 4.0 runtime, but available in a .NET 4.5
                    SecurityProtocolType.Tls | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                Console.WriteMessage($"[General] .NET Framework runtime version: {version}");
                Console.WriteMessage($"[General] Security protocol: {ServicePointManager.SecurityProtocol}");
            }
            else 
            {
                Console.WriteMessage("[General] .NET Framework runtime version: N/A (running on mono)");
                Console.WriteMessage($"[General] Security protocol: N/A (running on mono)");
            }

            // Print the operating system information
            Console.WriteMessage($"[General] Operating system: {Environment.OSVersion.Platform}" +
                $" ({Environment.OSVersion.VersionString})");

            if (Environment.OSVersion.Version.Major < 6)
            {
                RunningOnLegacyPlatform = true;
                Console.WriteMessage($"[General] Running on a legacy platform (<= Windows XP)");
            }

            if (!Directory.Exists(DataFolder))
                Directory.CreateDirectory(DataFolder);
            if (!Directory.Exists(Path.Combine(DataFolder, "chats")))
                Directory.CreateDirectory(Path.Combine(DataFolder, "chats"));
            if (!Directory.Exists(Path.Combine(DataFolder, "extensions")))
                Directory.CreateDirectory(Path.Combine(DataFolder, "extensions"));
            if (!Directory.Exists(Path.Combine(DataFolder, "plugins")))
                Directory.CreateDirectory(Path.Combine(DataFolder, "plugins"));

            // Load plugins
            LoadPlugins();

            // Create the main form
            MainFrm = new MainForm();
            Plugins.ForEach((PintoPluginHost plugin) => { plugin.Plugin.OnMainFormLoad(MainFrm); });

            // Start Pinto!
            Application.Run(MainFrm);
        }

        public static void LoadPlugins()
        {
            Console.WriteMessage($"[Plugins] Loading plugins...");

            foreach (string assemblyPath in Directory.EnumerateFiles(Path.Combine(DataFolder, "plugins"), "*.dll"))
            {
                try
                {
                    Console.WriteMessage($"[Plugins] Loading the assembly \"{Path.GetFileName(assemblyPath)}\"...");
                    Assembly assembly = Assembly.LoadFrom(assemblyPath);
                    LoadPlugin(assemblyPath, assembly);
                }
                catch (Exception ex)
                {
                    Console.WriteMessage($"[Plugins] Unable to load the assembly \"{Path.GetFileName(assemblyPath)}\": {ex}");
                }
            }
        }

        public static void LoadPlugin(string assemblyPath, Assembly assembly)
        {
            try
            {
                Type pluginInterfaceType = typeof(IPintoPlugin);
                Console.WriteMessage($"[Plugins] Loading plugins from the assembly \"{assembly}\"...");

                foreach (Type assemblyType in assembly.GetTypes())
                {
                    if (!assemblyType.IsPublic ||
                        assemblyType.IsAbstract ||
                        !pluginInterfaceType.IsAssignableFrom(assemblyType)) continue;
                    IPintoPlugin plugin = (IPintoPlugin)Activator.CreateInstance(assemblyType);
                    PintoPluginInfo pluginInfo = plugin.GetInfo();

                    Console.WriteMessage($"[Plugins] Loading plugin \"{pluginInfo.Name}\"" +
                        $" by {pluginInfo.Author} (v{pluginInfo.Version})...");
                    if (!plugin.OnLoad()) continue;

                    Console.WriteMessage($"[Plugins] Loaded plugin \"{pluginInfo.Name}\"" +
                        $" by {pluginInfo.Author} (v{pluginInfo.Version})");
                    Plugins.Add(new PintoPluginHost()
                    {
                        AssemblyPath = assemblyPath,
                        Assembly = assembly,
                        Plugin = plugin
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteMessage($"[Plugins] Unable to load a plugin from the assembly \"{assembly}\": {ex}");
            }
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

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, 
            TKey key, TValue @default)
        {
            return dict.TryGetValue(key, out var value) ? value : @default;
        }
    }
}
