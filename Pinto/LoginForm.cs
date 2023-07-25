using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PintoNS.Forms;
using PintoNS.General;
using PintoNS.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace PintoNS
{
    public partial class LoginForm : Form
    {
        public static readonly string[] AUTH_TOKEN_ERRORS = { "invalid_username", "invalid_password", 
            "outdated_client", "server_error" };
        private bool isPortable;
        private MainForm mainForm;
        public NetworkManager NetManager;
        public string AuthIP;
        public int AuthPort;
        public string AuthToken;
        public User CurrentUser = new User();
        public bool AllowClosing;

        public LoginForm()
        {
            InitializeComponent();
            Icon = Program.GetFormIcon();
            mainForm = new MainForm(this);
            mainForm.OnLogout(true);
            mainForm.Show();
            mainForm.Hide();
        }

        public string GetAuthTokenError(string error) 
        {
            switch (error) 
            {
                case "ccp":
                    return "The CCP will find your home address 你是迪克小 -9999999999 social credits";
                case "invalid_credentials":
                    return "Your Pinto! Name or password were not recognized. Please check and try again";
                case "outdated_client":
                    return "Your client is outdated, please update to the latest version of Pinto!";
                case "server_error":
                    return "Pinto! can't connect due to a server error";
                default:
                    return $"Pinto! can't connect: {error}";
            }
        }

        private async void LoginForm_Load(object sender, EventArgs e)
        {
            LoadLogin();

            if (File.Exists(".IS_PORTABLE_CHECK"))
                isPortable = true;
            if (Settings.AutoCheckForUpdates && !isPortable)
                await CheckForUpdates(false);

            Program.CallExtensionsEvent("OnFormLoad");
        }

        private void LoginForm_Shown(object sender, EventArgs e)
        {
            if (AuthToken != null)
                btnConnect.PerformClick();
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClosing)
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
                return;
            }

            Program.Console.WriteMessage("Quitting...");
            Disconnect();
        }

        public async Task CheckForUpdates(bool showLatestMessage)
        {
            if (isPortable)
            {
                MsgBox.Show(this, "Checking for updates is not available on the portable version!",
                    "Updates Unavailable", MsgBoxIconType.WARNING, true);
                return;
            }

            if (!await Updater.IsLatest())
                MsgBox.Show(this,
                    "An update is available, do you want to download it and install it?",
                    "Update Available",
                    MsgBoxIconType.QUESTION,
                    true, true, async (MsgBoxButtonType btn) =>
                    {
                        if (btn == MsgBoxButtonType.YES)
                        {
                            string path = Path.Combine(Program.DataFolder, "PintoSetup.exe");
                            if (File.Exists(path))
                                File.Delete(path);

                            byte[] file = await Updater.GetUpdateFile();
                            if (file == null) return;
                            File.WriteAllBytes(path, file);
                            Program.Console.WriteMessage($"[Updater] Saved update file at {path}");

                            Program.Console.WriteMessage($"[Updater] Running installer at {path}...");
                            Process process = new Process();
                            process.StartInfo.FileName = "PintoSetup.exe";
                            process.StartInfo.Arguments = " upgrade";
                            process.StartInfo.WorkingDirectory = Program.DataFolder;
                            process.Start();

                            Program.Console.WriteMessage($"[Updater] Exitting...");
                            Shutdown();
                        }
                    });
            else if (showLatestMessage)
                MsgBox.Show(this, "You are already on the latest version of Pinto!",
                    "Latest version", MsgBoxIconType.INFORMATION, true);
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            AuthIP = txtIP.Text.Trim();
            AuthPort = (int)nudPort.Value;
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MsgBox.Show(this, "Blank username or password!",
                    "Error", MsgBoxIconType.ERROR);
                return;
            }

            if (cbSavePassword.Checked) SaveLogin();
            btnConnect.Enabled = false;
            pLoggingin.Visible = true;
            pError.Visible = false;

            int loginTokenFailCount = 0;
            await Task.Delay(1000);

            while (AuthToken == null && loginTokenFailCount < 5) 
            {
                Program.Console.WriteMessage($"[Networking] PTAP server: {AuthIP}:{AuthPort}");
                Program.Console.WriteMessage($"[Networking] Obtaining authentication token for {username}...");
                AuthToken = await GetToken(AuthIP, AuthPort, username, password);
                loginTokenFailCount++;
            }
            Program.Console.WriteMessage($"[Networking] Obtained authentication token: {AuthToken ?? "(none)"}");

            if (string.IsNullOrWhiteSpace(AuthToken)) 
            {
                ShowError();
                return;
            }

            if (AuthToken.StartsWith("error.")) 
            {
                ShowError(GetAuthTokenError(AuthToken.Replace("error.", "")));
                return;
            }

            CurrentUser.Name = username;
            if (cbSavePassword.Checked) SaveLogin();
            
            OnLogin();
            mainForm.ChangeStatus(UserStatus.ONLINE, true);
        }

        public async Task<string> GetToken(string ip, int port, string username, string password)
        {
            try 
            {
                string passwordHash = BitConverter.ToString(
                    new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(password))
                ).Replace("-", "").ToUpper();

                TcpClient client = new TcpClient();
                NetworkStream stream = null;

                await client.ConnectAsync(ip, port);
                stream = client.GetStream();

                byte[] identByte = { 0xDF };
                await stream.WriteAsync(identByte, 0, identByte.Length);
                await stream.ReadAsync(identByte, 0, identByte.Length);

                if (identByte[0] != 0xFF)
                    throw new Exception("Invalid identification byte!");

                byte[] dataRaw = Encoding.UTF8.GetBytes(Convert.ToBase64String(
                    Encoding.UTF8.GetBytes($"{username}:{passwordHash}:{Program.VERSION_STRING}")));
                byte[] dataSize = { (byte)dataRaw.Length };
                await stream.WriteAsync(dataSize, 0, dataSize.Length);
                await stream.WriteAsync(dataRaw, 0, dataRaw.Length);

                await stream.ReadAsync(dataSize, 0, dataSize.Length);
                dataRaw = new byte[dataSize[0]];
                await stream.ReadAsync(dataRaw, 0, dataRaw.Length);

                client.Close();
                client.Dispose();
                stream.Dispose();

                return Encoding.UTF8.GetString(Convert.FromBase64String(Encoding.UTF8.GetString(dataRaw)));
            }
            catch (Exception ex) 
            {
                Program.Console.WriteMessage($"[Networking] Unable to obtain authentication token: {ex}");
                return null;
            }
        }

        public async Task Connect(string ip, int port, string authToken)
        {
            Program.Console.WriteMessage($"[Networking] Signing in at {ip}:{port}...");

            NetManager = new NetworkManager(this, mainForm);
            (bool, Exception) connectResult = await NetManager.Connect(ip, port);

            if (!connectResult.Item1)
            {
                Disconnect();
                Program.Console.WriteMessage($"[Networking] Unable to connect to {ip}:{port}: {connectResult.Item2}");
            }
            else
            {
                NetManager.Login(authToken);
            }
        }

        public void Disconnect(bool actualLogout = false)
        {
            Program.Console.WriteMessage("[Networking] Disconnecting...");
            bool wasLoggedIn = false;

            if (NetManager != null)
            {
                wasLoggedIn = NetManager.NetHandler.LoggedIn;
                if (NetManager.IsActive)
                    NetManager.Disconnect("User requested disconnect");
            }

            NetManager = null;
            if (!actualLogout && mainForm.Visible)
                mainForm.StartConnectingToServer();
            else
                OnLogout();

            Program.CallExtensionsEvent("OnDisconnect");
        }

        internal void OnLogin() 
        {
            mainForm.Show();
            mainForm.OnLogin();
            Hide();
        }

        internal void OnLogout()
        {
            mainForm.OnLogout(!mainForm.Visible);
            mainForm.Hide();
            mainForm.SyncTray();
            
            btnConnect.Enabled = true;
            pLoggingin.Visible = false;
            pError.Visible = false;

            CurrentUser = new User();
            AuthToken = null;
            SaveLogin();

            Show();
        }

        private void cbSavePassword_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbSavePassword.Checked)
                DeleteLogin();
        }

        private void llServers_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ServerListForm serverListForm = new ServerListForm();
            serverListForm.ServerUse += (object sender2, ServerUseEventArgs e2) => 
            {
                txtIP.Text = e2.IP;
                nudPort.Value = e2.Port;
            };
            serverListForm.ShowDialog();
        }

        private void LoadLogin()
        {
            Program.Console.WriteMessage("[General] Loading saved login information...");
            try
            {
                string filePath = Path.Combine(Program.DataFolder, "login.json");
                if (!File.Exists(filePath)) return;

                string fileData = File.ReadAllText(filePath);
                JObject data = JsonConvert.DeserializeObject<JObject>(fileData);

                txtUsername.Text = data["username"].Value<string>();
                txtPassword.Text = data["password"].Value<string>();
                AuthToken = data.ContainsKey("auth_token") ? data["auth_token"].Value<string>() : null;
                txtIP.Text = data["ip"].Value<string>();
                nudPort.Value = data["port"].Value<int>();
            }
            catch (Exception ex)
            {
                Program.Console.WriteMessage($"[General]" +
                    $" Unable to load the saved login information: {ex}");
                MsgBox.Show(this,
                    "Unable to load the saved login information!",
                    "Error", MsgBoxIconType.ERROR);
            }
        }

        private void SaveLogin()
        {
            Program.Console.WriteMessage("[General] Saving login information...");
            try
            {
                string filePath = Path.Combine(Program.DataFolder, "login.json");
                JObject data = new JObject();

                data.Add("username", txtUsername.Text);
                data.Add("password", txtPassword.Text);
                if (AuthToken != null) data.Add("auth_token", AuthToken);
                data.Add("ip", txtIP.Text);
                data.Add("port", (int)nudPort.Value);

                File.WriteAllText(filePath, data.ToString(Formatting.Indented));
            }
            catch (Exception ex)
            {
                Program.Console.WriteMessage($"[General]" +
                    $" Unable to save the login information: {ex}");
                MsgBox.Show(this,
                    "Unable to save the login information!",
                    "Error", MsgBoxIconType.ERROR);
            }
        }

        private void DeleteLogin()
        {
            Program.Console.WriteMessage("[General] Deleting saved login information...");
            try
            {
                string filePath = Path.Combine(Program.DataFolder, "login.json");
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
            catch (Exception ex)
            {
                Program.Console.WriteMessage($"[General]" +
                    $" Unable to delete the saved login information: {ex}");
                MsgBox.Show(this,
                    "Unable to delete the saved login information!",
                    "Error", MsgBoxIconType.ERROR);
            }
        }

        public void ShowError(string error = "Pinto! can't connect") 
        {
            OnLogout();
            pError.Visible = true;
            lErrorText.Text = error;
        }

        public void Shutdown()
        {
            AllowClosing = true;
            Close();
            
            mainForm.AllowClosing = true;
            mainForm.Close();
        }
    }
}
