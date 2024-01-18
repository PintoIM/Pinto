using PintoNS.Calls;
using PintoNS.Contacts;
using PintoNS.Forms;
using PintoNS.UI;
using System;
using System.Media;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PintoNS.Networking
{
    [Obsolete("Networking is about to be re-written")]
    public class NetworkManager
    {
        private MainForm mainForm;
        private int connectionAttempt;
        public NetworkClient NetClient;
        public NetworkHandler NetHandler;
        public bool IsCached { get; private set; }
        public bool WasLoggedInOnce;
        public bool IsActive;
        public bool IsConnected;
        public bool IsForceTermination;
        public bool InCall;
        public string InCallWith;
        public CallManager CallMgr;
        public AudioPlayer AudioPlyr = new AudioPlayer();
        public AudioRecorder AudioRcrd = new AudioRecorder();
        private Thread reconnectorHandler;
        public string ServerIP { get; private set; }
        public int ServerPort { get; private set; }
        internal string username;
        internal string password;

        public NetworkManager(MainForm mainForm, bool cache, string ip, int port,
            string username, string password)
        {
            this.mainForm = mainForm;
            IsCached = cache;
            ServerIP = ip;
            ServerPort = port;
            this.username = username;
            this.password = password;
            IsActive = true;
            if (!IsCached) InitNetworking();

            // TODO: Fix this somehow
            // Always keep the audio player and recorder started
            // This is due to a bug with Naudio
            //AudioRcrd.MicrophoneDataAvailable += AudioRcrd_MicrophoneDataAvailable;
            //AudioPlyr.Start();
            //AudioRcrd.Start();
        }

        private void InitNetworking()
        {
            NetClient = new NetworkClient();
            NetHandler = new NetworkHandler(mainForm, NetClient);

            NetClient.ReceivedPacket = delegate (IPacket packet)
            {
                NetClient_ReceivedPacket(packet);
            };

            NetClient.Disconnected = delegate (string reason)
            {
                NetClient_Disconnected(reason);
            };
        }

        public void ScheduleConnecting()
        {
            connectionAttempt = 0;
            reconnectorHandler = new Thread(new ThreadStart(ReconnectorHandler_Func));
            reconnectorHandler.Start();
        }

        private async void ReconnectorHandler_Func()
        {
            Action<string> changeConnectionStatus = new Action<string>(str => { });

            while (IsActive)
            {
                connectionAttempt++;
                Program.Console.WriteMessage($"[Networking] Performing attempt #{connectionAttempt} " +
                    $"at connecting to {ServerIP}:{ServerPort} as {username}");
                InitNetworking();
                Thread.Sleep(3000);
                if (!IsActive) return;

                (bool, Exception) result = await Connect(ServerIP, ServerPort, changeConnectionStatus);
                if (!result.Item1)
                {
                    if (result.Item2 == null || !(result.Item2 is PintoVerificationException))
                        continue;
                    else
                    {
                        IsCached = false;
                        IsConnected = false;
                        UsingPintoForm.SetHasLoggedIn(false);
                        Program.Console.WriteMessage("[Networking] Aborting re-connect as handshaking failed");
                        Disconnect("Handshaking failed");
                        return;
                    }
                }

                Login();
                return;
            }
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
            ServerIP = null;
            ServerPort = 0;
            username = null;
            password = null;
            EndCall(true);
            AudioPlyr.Stop();
            AudioRcrd.Stop();
        }

        public void Login()
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

        public void Register()
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
            if (IsActive && !IsForceTermination && (IsCached || IsConnected))
            {
                Program.Console.WriteMessage("[Networking] Ignoring disconnect and attempting re-connection");
                IsConnected = false;
                ScheduleConnecting();

                if (mainForm.loginPacketCheckThread != null)
                    mainForm.loginPacketCheckThread.Abort();

                mainForm.Invoke(new Action(() =>
                {
                    mainForm.SetConnectingState(true);
                    new SoundPlayer() { Stream = Sounds.VC_BEEP_1 }.Play();
                    mainForm.PopupController.CreatePopup("You have lost the connection to the server.\n" +
                        "We will try to re-connect you...", "Disconnected", 0);
                    mainForm.InWindowPopupController.CreatePopup("You are being reconnected");
                }));

                return;
            }

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
            /*
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
            new SoundPlayer(Sounds.CALL_INIT).Play();*/
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
            CallMgr.JoinCall(userNameIP, userNamePort, mainForm.LocalUser.Name);
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
