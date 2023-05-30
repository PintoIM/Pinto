using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace PintoNS.General
{
    public class CallManager
    {
        private AudioRecorderPlayer audioRecPlay = new AudioRecorderPlayer();
        private IPEndPoint remote;
        private UdpClient client;
        private NATUPNPLib.UPnPNATClass router;
        private NATUPNPLib.IStaticPortMapping routerMapping;

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateAudioDevicesLists();
            audioRecPlay.MicrophoneDataAvailable += AudioRecPlay_MicrophoneDataAvailable;
            router = new NATUPNPLib.UPnPNATClass();
        }

        private void UpdateAudioDevicesLists()
        {
            /*
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
            if (cbSpeakers.Items.Count > 1) cbSpeakers.Text = (string)cbSpeakers.Items[0];*/
        }


        private void Start()
        {
            if (client != null) return;

            /*
            if (string.IsNullOrWhiteSpace(cbMicrophones.Text) ||
                string.IsNullOrWhiteSpace(cbSpeakers.Text))
            {
                MessageBox.Show("Invalid audio configuration! Make sure to select atleast one device!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }*/

            try
            {
                //audioRecPlay.MicrophoneDevice = int.Parse(cbMicrophones.Text.Split(':')[0]);
                //audioRecPlay.SpeakerDevice = int.Parse(cbSpeakers.Text.Split(':')[0]);
                audioRecPlay.Start();
                client = new UdpClient(0);
                int listenPort = ((IPEndPoint)client.Client.LocalEndPoint).Port;

                if (router.StaticPortMappingCollection == null)
                    MessageBox.Show("Unable to use UPnP to open the listen port!" +
                        " Make sure your router/modem has UPnP enabled!" +
                        " If you are unable to use UPnP, please open the port manually!",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    routerMapping = router.StaticPortMappingCollection.Add(listenPort, "UDP",
                        listenPort, GetAllLocalIPv4(NetworkInterfaceType.Ethernet)
                            .FirstOrDefault(), true, "UDPAudio");
                    //lExternalIP.Text = $"External IP: {routerMapping.ExternalIPAddress}";
                }
                /*
                lListeningPort.Text = $"Listening on port: {listenPort}";

                cbMicrophones.Enabled = false;
                cbSpeakers.Enabled = false;
                btnConnect.Enabled = true;
                btnStart.Enabled = false;
                btnStop.Enabled = true;*/
            }
            catch (Exception ex)
            {
                Stop();
                MessageBox.Show($"Unable to start:{Environment.NewLine}{ex}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Connect(string ip)
        {
            if (client == null || remote != null) return;

            if (string.IsNullOrWhiteSpace(ip))
            {
                MessageBox.Show("Invalid remote details!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                remote = new IPEndPoint(IPAddress.Parse(ip), 0);
                client.BeginReceive(Client_Receive, null);
                //txtRemoteIP.Enabled = false;
                //btnConnect.Text = "Disconnect";
            }
            catch (Exception ex)
            {
                Disconnect();
                MessageBox.Show($"Unable to connect to remote host:{Environment.NewLine}{ex}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Disconnect()
        {
            remote = null;
            //txtRemoteIP.Enabled = true;
            //btnConnect.Text = "Connect";
        }

        private void Stop()
        {
            Disconnect();
            if (client != null) client.Close();
            audioRecPlay.Stop();

            if (router.StaticPortMappingCollection != null && routerMapping != null)
                router.StaticPortMappingCollection.Remove(
                    routerMapping.InternalPort, routerMapping.Protocol);
            routerMapping = null;
            /*
            lExternalIP.Text = "External IP: 0.0.0.0";
            lListeningPort.Text = "Listening on port: -";

            client = null;
            cbMicrophones.Enabled = true;
            cbSpeakers.Enabled = true;
            btnConnect.Enabled = false;
            btnStart.Enabled = true;
            btnStop.Enabled = false;*/
        }

        private void AudioRecPlay_MicrophoneDataAvailable(byte[] data)
        {
            if (client == null || remote == null) return;
            try
            {
                client.Send(data, data.Length, remote);
            }
            catch (Exception ex)
            {
                Disconnect();
                MessageBox.Show($"Unable to send to the remote host:{Environment.NewLine}{ex}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Client_Receive(IAsyncResult result)
        {
            try
            {
                if (client == null || remote == null) return;
                byte[] audioData = client.EndReceive(result, ref remote);
                audioRecPlay.Play(audioData);
                client.BeginReceive(Client_Receive, null);
            }
            catch (Exception ex)
            {
                if (client != null && remote != null)
                {
                    Disconnect();
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
    }
}
