using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using PintoNS;
using PintoNS.General;

namespace PintoNS.Forms
{
    public partial class CallManager : Form
    {
        public const int PORT = 65413;
        private MainForm mainForm;
        private readonly AudioRecorderPlayer audioRecPlay = new AudioRecorderPlayer();
        private IPEndPoint remote;
        private Socket client;
        private byte[] receiveBuffer = new byte[0x10000];
        private NATUPNPLib.UPnPNATClass router;
        private NATUPNPLib.IStaticPortMapping routerMapping;
        public bool AllowClose;
        private readonly Ping ping = new Ping();
        private Thread measureLatency;
        private int receivedPacketsReceived;
        private Thread measureReceivedPacketsReceivedPerSecond;
        private TimeSpan timeInCall;

        public CallManager(MainForm mainForm)
        {
            InitializeComponent();
            Icon = Program.GetFormIcon();
            this.mainForm = mainForm;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            IntPtr systemMenu = PInvoke.GetSystemMenu(Handle, false);
            PInvoke.EnableMenuItem(systemMenu, PInvoke.SC_CLOSE,
                PInvoke.MF_BYCOMMAND | PInvoke.MF_DISABLED);

            UpdateAudioDevicesLists();
            audioRecPlay.MicrophoneDataAvailable += AudioRecPlay_MicrophoneDataAvailable;
            router = new NATUPNPLib.UPnPNATClass();

            MessageBox.Show($"Pinto! calls currently are p2p and require you" +
                $" to enter the remote's host and vice-versa!{Environment.NewLine}" +
                $"Make sure to use calls only with people you trust" +
                $" or using a VPN service such as Hamachi",
                "Security Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void UpdateAudioDevicesLists() 
        {
            cbMicrophones.Items.Clear();
            cbSpeakers.Items.Clear();

            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                WaveInCapabilities device = WaveIn.GetCapabilities(i);
                cbMicrophones.Items.Add($"{i}: {device.ProductName}");
            }

            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                WaveOutCapabilities device = WaveOut.GetCapabilities(i);
                cbSpeakers.Items.Add($"{i}: {device.ProductName}");
            }

            if (cbMicrophones.Items.Count > 0) cbMicrophones.Text = (string)cbMicrophones.Items[0];
            if (cbSpeakers.Items.Count > 1) cbSpeakers.Text = (string)cbSpeakers.Items[0];
        }

        private void btnStart_Click(object sender, EventArgs e) => Start(txtRemoteIP.Text);

        private void btnStop_Click(object sender, EventArgs e) => Stop();

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose) 
            {
                e.Cancel = true;
                return;
            }
            Stop();
        }

        private void Start(string ip) 
        {
            Invoke(new Action(() => 
            {
                if (client != null) return;

                if (string.IsNullOrWhiteSpace(cbMicrophones.Text) ||
                    string.IsNullOrWhiteSpace(cbSpeakers.Text))
                {
                    MessageBox.Show("Invalid audio configuration! Make sure to select atleast one device!",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(ip) || !IPAddress.TryParse(ip, out IPAddress ipAddr))
                {
                    MessageBox.Show("Invalid remote details!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    audioRecPlay.MicrophoneDevice = int.Parse(cbMicrophones.Text.Split(':')[0]);
                    audioRecPlay.SpeakerDevice = int.Parse(cbSpeakers.Text.Split(':')[0]);
                    audioRecPlay.Start();

                    client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    // -1744830452 = SIO_UDP_CONNRESET
                    client.IOControl(
                        (IOControlCode)(-1744830452),
                        new byte[] { 0, 0, 0, 0 },
                        null
                    );
                    client.Bind(new IPEndPoint(IPAddress.Any, PORT));
                    remote = new IPEndPoint(ipAddr, PORT);

                    if (router.StaticPortMappingCollection == null)
                        MessageBox.Show($"Unable to use UPnP to open the listen port!{Environment.NewLine}" +
                            $"Make sure your router/modem has UPnP enabled!{Environment.NewLine}" +
                            $"If you are unable to use UPnP, please open the port manually!{Environment.NewLine}" +
                            $"The port is {PORT} on UDP",
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        try
                        {
                            routerMapping = router.StaticPortMappingCollection.Add(PORT, "UDP",
                                PORT, GetAllLocalIPv4(NetworkInterfaceType.Ethernet)
                                    .FirstOrDefault(), true, "UDPAudio");
                            lExternalIP.Text = $"External IP: {routerMapping.ExternalIPAddress}";
                        }
                        catch (Exception ex) 
                        {
                            MessageBox.Show($"Unable to use UPnP to open the listen port:" +
                                $"{Environment.NewLine}{ex}{Environment.NewLine}{Environment.NewLine}" +
                                $"Please do not report this issue, as UPnP support in C# is very unstable",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    tCall.Start();
                    lTime.Text = "Time: 00:00:00";

                    txtRemoteIP.Enabled = false;
                    cbMicrophones.Enabled = false;
                    cbSpeakers.Enabled = false;
                    btnStart.Enabled = false;
                    btnStop.Enabled = true;

                    measureLatency = new Thread(new ThreadStart(MeasureLatencyThread_Func));
                    measureReceivedPacketsReceivedPerSecond = new Thread(new ThreadStart(MeasureReceivedPacketsReceivedPerSecond_Func));
                    measureLatency.Start();
                    measureReceivedPacketsReceivedPerSecond.Start();

                    EndPoint endPoint = remote;
                    client.BeginReceiveFrom(receiveBuffer, 0, receiveBuffer.Length,
                        SocketFlags.None, ref endPoint, Client_Receive, null);
                }
                catch (Exception ex)
                {
                    Stop();
                    MessageBox.Show($"Unable to start:{Environment.NewLine}{ex}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }));
        }

        private void Stop() 
        {
            Invoke(new Action(() =>
            {
                if (measureLatency != null) measureLatency.Abort();
                if (measureReceivedPacketsReceivedPerSecond != null) measureReceivedPacketsReceivedPerSecond.Abort();
                audioRecPlay.Stop();

                if (router.StaticPortMappingCollection != null && routerMapping != null)
                    router.StaticPortMappingCollection.Remove(
                        routerMapping.InternalPort, routerMapping.Protocol);

                tCall.Stop();
                timeInCall = TimeSpan.Zero;
                if (client != null) client.Close();

                remote = null;
                client = null;
                routerMapping = null;
                receivedPacketsReceived = 0;
                measureLatency = null;
                measureReceivedPacketsReceivedPerSecond = null;

                txtRemoteIP.Enabled = true;
                cbMicrophones.Enabled = true;
                cbSpeakers.Enabled = true;
                btnStart.Enabled = true;
                btnStop.Enabled = false;

                lTime.Text = "Time: -";
                lExternalIP.Text = "External IP: 0.0.0.0";
                lLatency.Text = $"Latency: -";
                lPacketsPerSecond.Text = $"Packets/Second: -";
            }));
        }

        private void AudioRecPlay_MicrophoneDataAvailable(object sender, byte[] data)
        {
            if (client == null || remote == null) return;
            try 
            {
                client.SendTo(data, 0, data.Length, SocketFlags.None, remote);
            }
            catch (Exception ex) 
            {
                Stop();
                MessageBox.Show($"Unable to send to the remote host:{Environment.NewLine}{ex}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Client_Receive(IAsyncResult result) 
        {
            try 
            {
                if (client == null || remote == null) return;
                EndPoint endPoint = remote;
                int audioDataAmount = client.EndReceiveFrom(result, ref endPoint);
                receivedPacketsReceived++;
                audioRecPlay.Play(receiveBuffer.Take(audioDataAmount).ToArray());
                client.BeginReceiveFrom(receiveBuffer, 0, receiveBuffer.Length,
                    SocketFlags.None, ref endPoint, Client_Receive, null);
            }
            catch (Exception ex) 
            {
                if (client != null && remote != null) 
                {
                    Stop();
                    MessageBox.Show($"Unable to receive from the remote host:{Environment.NewLine}{ex}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // "Borrowed" from https://stackoverflow.com/a/24814027
        public static string[] GetAllLocalIPv4(NetworkInterfaceType _type)
        {
            List<string> ipAddrList = new List<string>();

            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipAddrList.Add(ip.Address.ToString());
                        }
                    }
                }
            }

            return ipAddrList.ToArray();
        }

        private void MeasureLatencyThread_Func()
        {
            while (client != null && remote != null) 
            {
                try 
                {
                    PingReply pingReply = ping.Send(remote.Address);
                    Invoke(new Action(() =>
                    {
                        lLatency.Text = $"Latency: {pingReply.RoundtripTime}ms";
                    }));
                    Thread.Sleep(1);
                }
                catch { }
            }
        }

        private void MeasureReceivedPacketsReceivedPerSecond_Func()
        {
            int i = 0;
            int j = 0;
            TimeSpan timePassed = TimeSpan.Zero;

            while (client != null && remote != null)
            {
                try
                {
                    if ((DateTime.Now.TimeOfDay - timePassed).TotalMilliseconds < 1000) 
                    {
                        i += receivedPacketsReceived;
                        receivedPacketsReceived = 0;
                    }
                    else 
                    {
                        j = i;
                        i = 0;
                        receivedPacketsReceived = 0;
                        timePassed = DateTime.Now.TimeOfDay;

                        Invoke(new Action(() =>
                        {
                            lPacketsPerSecond.Text = $"Packets/Second: {j}";
                        }));
                    }
                    Thread.Sleep(1);
                }
                catch { }
            }
        }

        private void tCall_Tick(object sender, EventArgs e)
        {
            timeInCall += TimeSpan.FromSeconds(1);
            lTime.Text = $"Time: {timeInCall}";
        }
    }
}