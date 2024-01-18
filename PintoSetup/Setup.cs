using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;

namespace PintoSetupNS
{
    public static class Setup
    {
        public static readonly string DISPLAY_NAME = $"Pinto! Beta";
        public static readonly string DISPLAY_VERSION = $"b1.1";
        public static readonly string DEFAULT_INSTALL_PATH =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Pinto!");
        public static readonly string PROGRAM_EXE = "Pinto.exe";
        public static readonly string DESKTOP_SHORTCUT = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.CommonDesktopDirectory), $"{DISPLAY_NAME}.lnk");
        public static readonly string STARTMENU_SHORTCUT = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.CommonStartMenu), $"{DISPLAY_NAME}.lnk");

        [ComImport]
        [Guid("00021401-0000-0000-C000-000000000046")]
        public class ShellLink
        {
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("000214F9-0000-0000-C000-000000000046")]
        public interface IShellLink
        {
            void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out IntPtr pfd, int fFlags);
            void GetIDList(out IntPtr ppidl);
            void SetIDList(IntPtr pidl);
            void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
            void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
            void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
            void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
            void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
            void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
            void GetHotkey(out short pwHotkey);
            void SetHotkey(short wHotkey);
            void GetShowCmd(out int piShowCmd);
            void SetShowCmd(int iShowCmd);
            void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
            void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
            void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
            void Resolve(IntPtr hwnd, int fFlags);
            void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
        }

        // "Borrowed" from https://stackoverflow.com/a/52907107
        public static bool IsValidPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || path.Length < 3) return false;

            string drive = path.Substring(0, 3).ToUpper();
            Regex driveCheck = new Regex(@"^[a-zA-Z]:\\$");
            if (!driveCheck.IsMatch(drive)) return false;

            IEnumerable<string> allMachineDrivers = DriveInfo.GetDrives().Select(drv => drv.Name);
            if (!allMachineDrivers.Contains(drive)) return false;

            string invalidChars = new string(Path.GetInvalidPathChars()) + @":/?*" + "\"";
            Regex hasBadChar = new Regex("[" + Regex.Escape(invalidChars) + "]");
            if (hasBadChar.IsMatch(path.Substring(3, path.Length - 3))) return false;
            if (path[path.Length - 1] == '.') return false;

            return true;
        }

        public static void CreateSetupRegistry(string uninstallerPath,
            string installLocation, bool createdDesktopShortcut)
        {
            RegistryKey key = Registry.LocalMachine
                .OpenSubKey("SOFTWARE", true)
                .OpenSubKey("Microsoft", true)
                .OpenSubKey("Windows", true)
                .OpenSubKey("CurrentVersion", true)
                .OpenSubKey("Uninstall", true)
                .CreateSubKey("Pinto!");

            key.SetValue("DisplayName", DISPLAY_NAME, RegistryValueKind.String);
            key.SetValue("DisplayVersion", DISPLAY_VERSION, RegistryValueKind.String);
            key.SetValue("DisplayIcon", Path.Combine(installLocation, PROGRAM_EXE), RegistryValueKind.String);
            key.SetValue("Publisher", "PintoIM", RegistryValueKind.String);
            key.SetValue("InstallLocation", installLocation, RegistryValueKind.String);
            key.SetValue("UninstallString", $"{uninstallerPath} uninstall", RegistryValueKind.String);
            key.SetValue("NoModify", 1, RegistryValueKind.DWord);
            key.SetValue("NoRepair", 1, RegistryValueKind.DWord);
            key.SetValue("CreatedDesktopShortcut", createdDesktopShortcut ? 1 : 0, RegistryValueKind.DWord);

            key.Flush();
            key.Close();
        }

        public static void RemoveSetupRegistry()
        {
            RegistryKey key = Registry.LocalMachine
                .OpenSubKey("SOFTWARE", true)
                .OpenSubKey("Microsoft", true)
                .OpenSubKey("Windows", true)
                .OpenSubKey("CurrentVersion", true)
                .OpenSubKey("Uninstall", true);
            key.DeleteSubKey("Pinto!");
            key.Flush();
            key.Close();
        }

        public static string GetInstallPath()
        {
            RegistryKey key = Registry.LocalMachine
                .OpenSubKey("SOFTWARE")
                .OpenSubKey("Microsoft")
                .OpenSubKey("Windows")
                .OpenSubKey("CurrentVersion")
                .OpenSubKey("Uninstall")
                .OpenSubKey("Pinto!");

            object value = null;
            string installPath = null;

            if (key != null) value = key.GetValue("InstallLocation");
            if (value is string) installPath = (string)value;

            return installPath;
        }

        public static bool GetCreatedDesktopShortcut()
        {
            RegistryKey key = Registry.LocalMachine
                .OpenSubKey("SOFTWARE")
                .OpenSubKey("Microsoft")
                .OpenSubKey("Windows")
                .OpenSubKey("CurrentVersion")
                .OpenSubKey("Uninstall")
                .OpenSubKey("Pinto!");

            object value = null;
            bool createdDesktopShortcut = false;

            if (key != null) value = key.GetValue("CreatedDesktopShortcut");
            if (value is int) createdDesktopShortcut = ((int)value) == 1;

            return createdDesktopShortcut;
        }

        public static void CreateShortcuts(string installLocation, bool desktop)
        {
            string filePath = Path.Combine(installLocation, PROGRAM_EXE);

            IShellLink link = (IShellLink)new ShellLink();
            link.SetPath(filePath);
            link.SetIconLocation(filePath, 0);
            link.SetDescription("A modernish IM client inspired by Skype 0.97");

            IPersistFile file = (IPersistFile)link;
            if (desktop) file.Save(DESKTOP_SHORTCUT, false);
            file.Save(STARTMENU_SHORTCUT, false);
        }

        public static void DeleteShortcuts()
        {
            if (File.Exists(DESKTOP_SHORTCUT)) File.Delete(DESKTOP_SHORTCUT);
            if (File.Exists(STARTMENU_SHORTCUT)) File.Delete(STARTMENU_SHORTCUT);
        }

        public static void PerformUninstall(string installLocation, out bool unableToDeleteFiles)
        {
            try
            {
                Directory.Delete(installLocation, true);
            }
            catch { unableToDeleteFiles = true; }
            unableToDeleteFiles = false;

            try
            {
                RemoveSetupRegistry();
            }
            catch { }

            try
            {
                DeleteShortcuts();
            }
            catch { }
        }
    }
}
