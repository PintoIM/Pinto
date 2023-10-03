using PintoNS.General;
using System;
using System.Media;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PintoNS.Networking
{
    public class NetworkManager
    {
        private MainForm mainForm;
        public NetworkClient NetClient;
        public NetworkHandler NetHandler;
        public bool IsActive;
        public bool InCall;

        public NetworkManager(MainForm mainForm)
        {
            this.mainForm = mainForm;
            NetClient = new NetworkClient();
            NetHandler = new NetworkHandler(mainForm, NetClient);
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
            (bool, Exception) connectResult = await NetClient.Connect(ip, port);
            return connectResult;
        }

        public void Disconnect(string reason)
        {
            if (NetClient != null && NetClient.IsConnected)
                NetClient.Disconnect(reason);
            NetClient = null;
            NetHandler = null;
            mainForm = null;
            IsActive = false;
        }

        public void Login(string username, string password) 
        {
            string passwordHash = BitConverter.ToString(
                new SHA256Managed()
                .ComputeHash
                (Encoding
                .UTF8
                .GetBytes(password)))
                .Replace("-", "")
                .ToUpper();
            NetHandler.SendLoginPacket(Program.PROTOCOL_VERSION, 
                Program.VERSION_STRING, username, passwordHash);
        }

        public void Register(string username, string password)
        {
            string passwordHash = BitConverter.ToString(
                new SHA256Managed()
                .ComputeHash
                (Encoding
                .UTF8
                .GetBytes(password)))
                .Replace("-", "")
                .ToUpper();
            NetHandler.SendRegisterPacket(Program.PROTOCOL_VERSION,
                Program.VERSION_STRING, username, passwordHash);
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
            Program.Console.WriteMessage($"[Networking] Disconnected: {reason.Replace("\n", "\\n")}");

            bool wasActive = IsActive;
            IsActive = false;

            if (reason != null && !reason.Equals("User requested disconnect")) 
            {
                mainForm.Invoke(new Action(() =>
                {
                    mainForm.Disconnect();
                    if (!NetHandler.LoggedIn && wasActive)
                        MsgBox.Show(mainForm, reason, "Error", MsgBoxIconType.ERROR);
                }));
            }

            Disconnect(reason);
        }

        public async void StartCall(string contact)
        {
            InCall = true;
            mainForm.OnCallStatusChanged(CallStatus.CONNECTING, contact);
            new SoundPlayer(Sounds.CALL_INIT).Play();
            await Task.Delay(500);
            if (!InCall) return;
            FailCall("Calls aren't yet implemented");
        }

        public void FailCall(string reason)
        {
            InCall = false;
            mainForm.OnCallStatusChanged(CallStatus.ERROR);
            mainForm.InWindowPopupController.CreatePopup($"Call failed: {reason}");
            new SoundPlayer(Sounds.CALL_ERROR1).Play();
        }

        public void EndCall()
        {
            InCall = false;
            mainForm.OnCallStatusChanged(CallStatus.ENDED);
            new SoundPlayer(Sounds.HANGUP).Play();
        }
    }
}
