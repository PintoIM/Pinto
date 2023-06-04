using Ionic.Zip;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoSetupNS
{
    public partial class MainForm : Form
    {
        private bool installing;
        private bool installed;

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            Graphics g = e.Graphics;
            g.Clear(Color.White);

            // Pinto! color is #14b8a6
            // Use a slightly lighter color
            g.FillRectangle(new SolidBrush(Color.FromArgb(0x54, 0xf8, 0xe6)), 0, 0, Width, Height / 7);
            g.FillRectangle(new SolidBrush(Color.FromArgb(0x54, 0xf8, 0xe6)), 0,
                Height / 2 + Height / 3.5f, Width, Height);

            // Outline
            g.DrawRectangle(new Pen(Color.FromArgb(0x14, 0xb8, 0xa6), 18f), 0, 0, Width - 8, Height - 32);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            txtPath.Text = Setup.DEFAULT_INSTALL_PATH;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = fbdBrowse.ShowDialog();

            if (result == DialogResult.OK)
            {
                txtPath.Text = fbdBrowse.SelectedPath;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (installing)
            {
                e.Cancel = true;
                return;
            }

            if (!installed) 
            {
                DialogResult result = MessageBox.Show($"Pinto! installation is not complete." +
                    $" Try again later by running the Pinto! installer.{Environment.NewLine}{Environment.NewLine}" +
                    $"Are you sure you want to exit?", "Exit Installer",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btnInstall_Click(object sender, EventArgs e)
        {
            string installFolder = txtPath.Text.Trim();

            if (!Setup.IsValidPath(installFolder))
            {
                MessageBox.Show($"You must enter a full path with drive letter; for example:" +
                    $"{Environment.NewLine}{Environment.NewLine}C:\\APP", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Directory.Exists(installFolder) && Directory.GetFiles(installFolder).Length > 0)
            {
                MessageBox.Show($"The specified folder is not empty", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try 
            {
                installing = true;
                pFirstStage.Enabled = false;
                pFirstStage.Visible = false;
                pSecondStage.Enabled = true;
                pSecondStage.Visible = true;
                lTitle.Text = "Installing...";

                string uninstallerPath = Path.Combine(installFolder, "Uninstaller.exe");
                lInstallStatus2.Text = "Creating uninstall registry...";
                Setup.CreateUninstallRegistry(uninstallerPath, installFolder);

                lInstallStatus2.Text = "Creating install directory...";
                Directory.CreateDirectory(installFolder);

                lInstallStatus2.Text = "Loading install files into memory...";
                MemoryStream memoryStream = new MemoryStream(Properties.Resources.INSTALL);
                ZipFile zipFile = ZipFile.Read(memoryStream);

                pbInstallProgress.Value = 0;
                pbInstallProgress.Maximum = zipFile.Count;

                await Task.Factory.StartNew(new Action(() => 
                {
                    for (int i = 0; i < zipFile.Count; i++)
                    {
                        ZipEntry entry = zipFile[i];

                        Invoke(new Action(() =>
                        {
                            lInstallStatus2.Text = $"Extracting files...{Environment.NewLine}" +
                                $"{Path.Combine(installFolder, entry.FileName)}";
                            pbInstallProgress.Value++;
                        }));

                        entry.Extract(installFolder);
                        Thread.Sleep(100);
                    }
                }));

                lInstallStatus2.Text = $"Extracting files...{Environment.NewLine}{uninstallerPath}";
                File.Copy(Application.ExecutablePath, uninstallerPath);
                Setup.CreateShortcuts(installFolder);

                lTitle.Text = "Pinto! Setup Complete";
                ShowInstallEnd($"Pinto! has been successfully installed on your computer and may be started by" +
                    $"{Environment.NewLine}selecting the installed icons.",
                    "Click Finish to exit Pinto! Setup.", "Finish");
            }
            catch (Exception ex) 
            {
                Setup.PerformUninstall(installFolder, out bool unableToDeleteFiles);

                lTitle.Text = "Pinto! Setup Failed";
                ShowInstallEnd("Pinto! was unable to be successfully installed on your computer", 
                    "Click Close to exit Pinto! Setup.", "Close");
                MessageBox.Show($"Pinto! was unable to be successfully" +
                    $" installed on your computer:{Environment.NewLine}{ex}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (unableToDeleteFiles) 
                    MessageBox.Show($"Pinto! Setup was unable to delete the installation files. ({installFolder})" +
                        $"{Environment.NewLine}They can be safely removed manually.",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ShowInstallEnd(string status1, string status2, string cancelText) 
        {
            lInstallStatus1.Text = status1;
            lInstallStatus2.Text = status2;
            pbInstallProgress.Visible = false;
            btnCancel.Parent = pSecondStage;
            btnCancel.Text = cancelText;
            installing = false;
            installed = true;
        }
    }
}
