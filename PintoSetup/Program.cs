using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
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

            string setupMode = args.Length > 0 ? args[0].ToLower() : "install";
            string installPath = Setup.GetInstallPath();

            switch (setupMode)
            {
                case "install":
                    Application.Run(new MainForm());
                    break;
                case "uninstall":
                    SetupUninstall(installPath);
                    break;
                case "upgrade":
                    if (installPath == null)
                    {
                        MessageBox.Show("Pinto! is not installed on your computer", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Setup.PROGRAM_EXE)).Length > 0)
                    {
                        MessageBox.Show("Pinto! is currently running, make sure to close it and try again",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MainForm mainForm = new MainForm();
                    mainForm.IsUpgrading = true;
                    mainForm.txtPath.Text = installPath;
                    mainForm.cbCreateDesktopIcon.Checked = Setup.GetCreatedDesktopShortcut();
                    Application.Run(mainForm);

                    break;
                default:
                    MessageBox.Show("Invalid arguments provided!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private static void SetupUninstall(string installPath)
        {
            if (installPath == null)
            {
                MessageBox.Show("Pinto! is not installed on your computer", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Setup.PROGRAM_EXE)).Length > 0)
            {
                MessageBox.Show("Pinto! is currently running, make sure to close it and try again",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to uninstall Pinto?" +
                " (your user data will not be affected)", "Uninstall Pinto?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            UninstallForm uninstallForm = new UninstallForm();
            new Thread(new ThreadStart(() => uninstallForm.ShowDialog())).Start();

            Setup.PerformUninstall(installPath);
            uninstallForm.CanClose = true;
            uninstallForm.Close();

            MessageBox.Show($"Pinto! has been uninstalled successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Self-delete
            ProcessStartInfo Info = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = $"/c ping 127.0.0.1 -n 2 -w 3000 > nul & rd /s /q \"{installPath}\"",
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(Info);
        }
    }
}
