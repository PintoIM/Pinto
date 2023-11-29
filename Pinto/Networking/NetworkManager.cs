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
        public bool IsForceTermination;
        public bool InCall;
        public string InCallWith;
        public CallManager CallMgr;
        public AudioPlayer AudioPlyr = new AudioPlayer();
        public AudioRecorder AudioRcrd = new AudioRecorder();

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

            // Always keep the audio player and recorder started
            // This is due to a bug with Naudio
            AudioRcrd.MicrophoneDataAvailable += AudioRcrd_MicrophoneDataAvailable;
            AudioPlyr.Start();
            AudioRcrd.Start();
        }

        public async Task<(bool, Exception)> Connect(string ip, int port, Action<string> changeConnectionStatus)
        {
            (bool, Exception) connectResult = await NetClient.Connect(ip, port, changeConnectionStatus);
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
            EndCall(true);
            AudioPlyr.Stop();
            AudioRcrd.Stop();
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
            EndCall(true);

            if (reason != null && !IsForceTermination) 
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

        public void StartCall(string contact)
        {
            if (NetHandler.Options.GetValueOrDefault("exp_calls", "0") != "1") return;
            Program.Console.WriteMessage($"[Networking] Starting call with {contact}");

            EndCall(true);
            InCall = true;
            InCallWith = contact;
            mainForm.OnCallStatusChanged(CallStatus.CONNECTING, contact);

            CallMgr = new CallManager();
            CallMgr.CallStarted += CallMgr_CallStarted;
            CallMgr.CallReceivedAudio += CallMgr_CallReceivedAudio;
            CallMgr.CallFailed += CallMgr_CallFailed;
            CallMgr.CallEnded += CallMgr_CallEnded;
            CallMgr.StartCall();
            if (CallMgr == null) return;

            NetHandler.SendCallChangeStatusPacket(CallStatus.CONNECTING, 
                $"{contact}@{CallMgr.ExternalLocalIP}@{CallMgr.ClientPort}");
            new SoundPlayer(Sounds.CALL_INIT).Play();
        }

        public void JoinCall(string contact)
        {
            string[] contactSplitted = contact.Split('@');
            string userName = contactSplitted[0];
            string userNameIP = contactSplitted[1];
            int userNamePort = int.Parse(contactSplitted[2]);
            Program.Console.WriteMessage($"[Networking] Joining {userName}'s call ({userNameIP}:{userNamePort})");

            EndCall(true);
            InCall = true;
            InCallWith = userName;

            CallMgr = new CallManager();
            CallMgr.CallStarted += CallMgr_CallStarted;
            CallMgr.CallReceivedAudio += CallMgr_CallReceivedAudio;
            CallMgr.CallFailed += CallMgr_CallFailed;
            CallMgr.CallEnded += CallMgr_CallEnded;
            CallMgr.JoinCall(userNameIP, userNamePort, mainForm.CurrentUser.Name);
        }

        private void CallMgr_CallStarted()
        {
            mainForm.Invoke(new Action(() =>
            {
                ChangeCallStatus(CallStatus.CONNECTED, "");
            }));
        }

        private void CallMgr_CallReceivedAudio(byte[] obj)
        {
            if (!InCall || CallMgr == null || !CallMgr.InCall || !CallMgr.Started) return;
            AudioPlyr.Play(obj);
        }

        private void CallMgr_CallFailed(string reason)
        {
            mainForm.Invoke(new Action(() =>
            {
                ChangeCallStatus(CallStatus.ERROR, reason);
            }));
        }

        private void CallMgr_CallEnded()
        {
            mainForm.Invoke(new Action(() =>
            {
                ChangeCallStatus(CallStatus.ENDED, "");
            }));
        }

        private void AudioRcrd_MicrophoneDataAvailable(byte[] obj)
        {
            if (!InCall || CallMgr == null || !CallMgr.InCall || !CallMgr.Started) return;
            CallManager.CallPacket packet = new CallManager.CallPacket(0x02, obj);

            if (CallMgr.IsHost)
                CallMgr.SendToParticipants(packet);
            else
                CallMgr.SendPacket(packet, CallMgr.CallHost);
        }

        public void ChangeCallStatus(CallStatus status, string details)
        {
            try
            {
                switch (status)
                {
                    case CallStatus.CONNECTING:
                        new SoundPlayer(Sounds.CALL_INIT).Play();
                        JoinCall(details);
                        details = InCallWith;
                        break;
                    case CallStatus.CONNECTED:
                        details = InCallWith;
                        break;
                    case CallStatus.ERROR:
                        Program.Console.WriteMessage($"[Networking] Call failed: {details}");
                        EndCall(true);
                        mainForm.InWindowPopupController.CreatePopup($"Call failed: {details}");
                        new SoundPlayer(Sounds.CALL_ERROR1).Play();
                        break;
                    case CallStatus.ENDED:
                        EndCall();
                        break;
                }

                mainForm.OnCallStatusChanged(status, details);
            }
            catch (Exception ex)
            {
                Program.Console.WriteMessage($"[Networking] Unable to change the call status: {ex}");
            }
        }

        public void EndCall(bool onlyCleanup = false)
        {
            if (!onlyCleanup) Program.Console.WriteMessage($"[Networking] Ending call...");

            if (CallMgr != null) CallMgr.StopCall();
            if (IsActive && NetHandler != null && InCall) NetHandler.SendCallChangeStatusPacket(CallStatus.ENDED, "");
            InCall = false;
            InCallWith = null;
            CallMgr = null;

            if (onlyCleanup) return;
            mainForm.OnCallStatusChanged(CallStatus.ENDED);
            new SoundPlayer(Sounds.HANGUP).Play();
        }
    }
}
