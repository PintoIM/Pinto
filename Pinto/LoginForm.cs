using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PintoNS.Forms;
using PintoNS.General;
using PintoNS.Networking;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS
{
    public partial class LoginForm : Form
    {
        private MainForm mainForm;
        public NetworkManager NetManager;
        private Thread loginPacketCheckThread;
        public User CurrentUser = new User();

        public LoginForm()
        {
            InitializeComponent();
            Icon = Program.GetFormIcon();
            mainForm = new MainForm(this);
            mainForm.OnLogout(true);
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

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            string ip = txtIP.Text.Trim();
            int port = (int)nudPort.Value;
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MsgBox.Show(this, "Blank username or password!",
                    "Error", MsgBoxIconType.ERROR);
                return;
            }

            if (cbSavePassword.Checked)
                SaveLogin();

            pLoggingin.Visible = true;
            await Connect(ip, port, username, password);
        }

        public async Task Connect(string ip, int port, string username, string password)
        {
            Program.Console.WriteMessage($"[Networking] Signing in as {username} at {ip}:{port}...");

            NetManager = new NetworkManager(this, mainForm);
            (bool, Exception) connectResult = await NetManager.Connect(ip, port);

            if (!connectResult.Item1)
            {
                Disconnect();
                Program.Console.WriteMessage($"[Networking] Unable to connect to {ip}:{port}: {connectResult.Item2}");
                MsgBox.Show(this, $"Unable to connect to {ip}:{port}:" +
                    $" {connectResult.Item2.Message}", "Connection Error", MsgBoxIconType.ERROR);
            }
            else
            {
                CurrentUser.Name = username;
                NetManager.Login(username, password);

                if (loginPacketCheckThread != null)
                    loginPacketCheckThread.Abort();

                loginPacketCheckThread = new Thread(new ThreadStart(() =>
                {
                    try
                    {
                        Thread.Sleep(5000);

                        if (NetManager != null &&
                            NetManager.NetHandler != null &&
                            !NetManager.NetHandler.LoggedIn)
                        {
                            Invoke(new Action(() =>
                            {
                                Disconnect();
                                Program.Console.WriteMessage($"[Networking] Unable to connect to {ip}:{port}:" +
                                    $" No login packet received from the server in an acceptable time frame");
                                MsgBox.Show(this,
                                    $"No login packet received from the server in an acceptable time frame",
                                    "Connection Error", MsgBoxIconType.ERROR);
                            }));
                        }
                    }
                    catch { }
                }));
                loginPacketCheckThread.Start();
            }
        }

        /*
        public async Task ConnectRegister(string ip, int port, string username, string password)
        {
            Program.Console.WriteMessage($"[Networking] Registering in as {username} at {ip}:{port}...");

            NetManager = new NetworkManager(this, mainForm);
            (bool, Exception) connectResult = await NetManager.Connect(ip, port);

            if (!connectResult.Item1)
            {
                Disconnect();
                Program.Console.WriteMessage($"[Networking] Unable to connect to {ip}:{port}: {connectResult.Item2}");
                MsgBox.Show(this, $"Unable to connect to {ip}:{port}:" +
                    $" {connectResult.Item2.Message}", "Connection Error", MsgBoxIconType.ERROR);
            }
            else
            {
                CurrentUser.Name = username;
                NetManager.Register(username, password);

                if (loginPacketCheckThread != null)
                    loginPacketCheckThread.Abort();

                loginPacketCheckThread = new Thread(new ThreadStart(() =>
                {
                    try
                    {
                        Thread.Sleep(5000);

                        if (NetManager != null &&
                            NetManager.NetHandler != null &&
                            !NetManager.NetHandler.LoggedIn)
                        {
                            Invoke(new Action(() =>
                            {
                                Disconnect();
                                Program.Console.WriteMessage($"[Networking] Unable to connect to {ip}:{port}:" +
                                    $" No login packet received from the server in an acceptable time frame");
                                MsgBox.Show(this,
                                    $"No login packet received from the server in an acceptable time frame",
                                    "Connection Error", MsgBoxIconType.ERROR);
                            }));
                        }
                    }
                    catch { }
                }));
                loginPacketCheckThread.Start();
            }
        }*/

        public void Disconnect()
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
            OnLogout(wasLoggedIn);

            Program.CallExtensionsEvent("OnDisconnect");
        }

        internal void OnLogin() 
        {
            mainForm.Show();
            mainForm.OnLogin();
            Hide();
        }

        internal void OnLogout(bool wasLoggedIn = false)
        {
            if (wasLoggedIn)
            {
                mainForm.OnLogout();
                mainForm.Hide();
            }

            pLoggingin.Visible = false;
            Show();
        }

        private void UsingPintoForm_Load(object sender, EventArgs e)
        {
            LoadLogin();
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

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.Console.WriteMessage("Quitting...");
            Disconnect();
            if (loginPacketCheckThread != null) loginPacketCheckThread.Abort();
        }
    }
}
