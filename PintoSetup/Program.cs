using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoSetupNS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool uninstallMode = args.Length > 0 ? 
                args[0].Equals("uninstall", StringComparison.InvariantCultureIgnoreCase) : false;

            string installPath = Setup.GetInstallPath();
            if (uninstallMode && Setup.GetInstallPath() == null) 
            {
                MessageBox.Show("Pinto! is not installed on your computer", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (uninstallMode) 
            {
                if (MessageBox.Show("Are you sure you want to uninstall Pinto?" +
                    " (your user data will not be affected)", "Uninstall Pinto?", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                UninstallForm uninstallForm = new UninstallForm();

                uninstallForm.Show();
                Setup.PerformUninstall(installPath, out bool unableToDeleteFiles);
                uninstallForm.Hide();
                uninstallForm.Close();

                if (unableToDeleteFiles)
                    MessageBox.Show($"Pinto! Setup was unable to delete the installation files. ({installPath})" +
                        $"{Environment.NewLine}They can be safely removed manually.",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                MessageBox.Show($"Pinto! has been uninstalled successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Self-delete
                ProcessStartInfo Info = new ProcessStartInfo() 
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c ping 127.0.0.1 -n 1 -w 3000 > nul & del \"{Application.ExecutablePath}\" & rd \"{installPath}\"",
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                Process.Start(Info);
            }
            else
                Application.Run(new MainForm());
        }
    }
}
