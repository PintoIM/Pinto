using PintoNS.General;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PintoNS.Networking
{
    public class NetworkManager
    {
        private LoginForm loginForm;
        private MainForm mainForm;
        public NetworkClient NetClient;
        public NetworkHandler NetHandler;
        public bool IsActive;

        public NetworkManager(LoginForm loginForm, MainForm mainForm)
        {
            this.loginForm = loginForm;
            this.mainForm = mainForm;
            NetClient = new NetworkClient();
            NetHandler = new NetworkHandler(loginForm, mainForm, NetClient);
            IsActive = true;

            NetClient.ReceivedPacket = delegate (IPacket packet)
            {
                NetClient_ReceivedPacket(packet);
            };

            NetClient.Disconnected = delegate (string reason)
            {
                NetClient_Disconnected(reason);
            };
        }

        public async Task<(bool, Exception)> Connect(string ip, int port)
        {
            return await NetClient.Connect(ip, port);
        }

        public void Disconnect(string reason)
        {
            if (NetClient != null && NetClient.IsConnected)
                NetClient.Disconnect(reason);
            NetClient = null;
            NetHandler = null;
            loginForm = null;
            mainForm = null;
            IsActive = false;
        }

        public void Login(string token) 
        {
            NetHandler.SendLoginPacket(Program.PROTOCOL_VERSION, Program.VERSION_STRING, token);
        }

        public void ChangeStatus(UserStatus status, string motd) 
        {
            NetHandler.SendStatusPacket(status, motd);
        }

        private void NetClient_ReceivedPacket(IPacket packet)
        {
            NetHandler.HandlePacket(packet);
        }

        private void NetClient_Disconnected(string reason)
        {
            Program.Console.WriteMessage($"[Networking] Disconnected: {reason}");

            bool wasActive = IsActive;
            IsActive = false;

            if (!reason.Equals("User requested disconnect")) 
            {
                loginForm.Invoke(new Action(() =>
                {
                    loginForm.Disconnect();
                    if (!NetHandler.LoggedIn && wasActive)
                        MsgBox.Show(loginForm, reason, "Error", MsgBoxIconType.ERROR);
                }));
            }

            Disconnect(reason);
        }
    }
}
