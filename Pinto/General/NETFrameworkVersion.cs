using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PintoNS.General
{
    // From https://stackoverflow.com/a/49442161
    public static class NETFrameworkVersion
    {
        public static Version GetVersion() 
            => GetVersionHigher4() ?? GetVersionLowerOr4();

        private static Version GetVersionLowerOr4()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP");
            string[] names = key.GetSubKeyNames();

            // Version names start with 'v', eg, 'v3.5' which needs to be trimmed off before conversion
            string text = names.LastOrDefault().Remove(0, 1);
            if (string.IsNullOrEmpty(text)) return null;

            key.Close();
            return text.Contains('.') ? new Version(text) : new Version(Convert.ToInt32(text), 0);
        }

        private static Version GetVersionHigher4()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full");
            object value = key.GetValue("Release");
            if (value == null) return null;

            // Checking the version using >= will enable forward compatibility,  
            // however you should always compile your code on newer versions of 
            // the framework to ensure your app works the same. 
            int releaseKey = Convert.ToInt32(value);
            if (releaseKey >= 528040) return new Version(4, 8, 0);
            if (releaseKey >= 461808) return new Version(4, 7, 2);
            if (releaseKey >= 461308) return new Version(4, 7, 1);
            if (releaseKey >= 460798) return new Version(4, 7);
            if (releaseKey >= 394747) return new Version(4, 6, 2);
            if (releaseKey >= 394254) return new Version(4, 6, 1);
            if (releaseKey >= 381029) return new Version(4, 6);
            if (releaseKey >= 379893) return new Version(4, 5, 2);
            if (releaseKey >= 378675) return new Version(4, 5, 1);
            if (releaseKey >= 378389) return new Version(4, 5);

            // This line should never execute. A non-null release key should mean 
            // that 4.5 or later is installed. 
            return new Version(4, 5);
        }
    }
}
