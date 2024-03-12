using Ionic.Zip;
using PintoSetupNS.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoSetupNS
{
    public partial class MainForm : Form
    {
        public bool IsUpgrading;
        public bool Installing { get; private set; }
        public bool Installed { get; private set; }

        public MainForm()
        {
            InitializeComponent();
            Icon = Resources.PINTO_BOX;
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
                (Height / 2) + (Height / 3.5f), Width, Height);

            // Outline
            g.DrawRectangle(new Pen(Color.FromArgb(0x14, 0xb8, 0xa6), 18f), 0, 0, Width - 8, Height - 32);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!IsUpgrading) txtPath.Text = Setup.DefaultInstallPath;
            if (IsUpgrading) btnInstall.PerformClick();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = fbdBrowse.ShowDialog();

            if (result == DialogResult.OK)
                txtPath.Text = fbdBrowse.SelectedPath;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Installing)
            {
                e.Cancel = true;
                return;
            }

            if (!Installed)
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
            bool createDesktopShortcut = cbCreateDesktopIcon.Checked;
            bool startAfterInstall = cbLaunchAfterInstall.Checked;

            if (!Setup.IsValidPath(installFolder))
            {
                MessageBox.Show($"You must enter a full path with drive letter; for example:" +
                    $"{Environment.NewLine}{Environment.NewLine}C:\\APP", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Directory.Exists(installFolder) && Directory.GetFiles(installFolder).Length > 0 && !IsUpgrading)
            {
                MessageBox.Show($"The specified folder is not empty", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Installing = true;
                pFirstStage.Enabled = false;
                pFirstStage.Visible = false;
                pSecondStage.Enabled = true;
                pSecondStage.Visible = true;
                lTitle.Text = "Installing...";

                string uninstallerPath = Path.Combine(installFolder, "Uninstaller.exe");
                lInstallStatus2.Text = "Creating uninstall registry...";
                Setup.CreateSetupRegistry(uninstallerPath, installFolder, createDesktopShortcut);

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

                        entry.Extract(installFolder, ExtractExistingFileAction.OverwriteSilently);
                        Thread.Sleep(100);
                    }
                }));

                lInstallStatus2.Text = $"Extracting files...{Environment.NewLine}{uninstallerPath}";
                File.Copy(Application.ExecutablePath, uninstallerPath, true);
                Setup.CreateShortcuts(installFolder, createDesktopShortcut);

                Installing = false;
                Installed = true;

                if (!IsUpgrading)
                {
                    lTitle.Text = "Pinto! Setup Complete";
                    ShowInstallEnd($"Pinto! has been successfully installed on your computer and may be started by" +
                        $"{Environment.NewLine}selecting the installed icons.",
                        "Click Finish to exit Pinto! Setup.", "Finish");
                }
                else
                    Close();

                Process process = new Process();
                process.StartInfo = new ProcessStartInfo()
                {
                    FileName = Path.Combine(installFolder, Setup.PROGRAM_EXE),
                    WorkingDirectory = installFolder
                };
                process.Start();
            }
            catch (Exception ex)
            {
                bool unableToDeleteFiles = false;
                if (!IsUpgrading) Setup.PerformUninstall(installFolder, out unableToDeleteFiles);

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
            Installing = false;
            Installed = true;
        }
    }
}
