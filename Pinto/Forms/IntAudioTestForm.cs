using PintoNS.Calls;
using System;
using System.Windows.Forms;

namespace PintoNS.Forms
{
    public partial class IntAudioTestForm : Form
    {
        private AudioRecorder recorder;
        private AudioPlayer player;
        private ALawInterface codec = new ALawInterface();

        public IntAudioTestForm()
        {
            InitializeComponent();
            Icon = Program.GetFormIcon();
        }

        public void Start()
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;

            recorder = new AudioRecorder();
            player = new AudioPlayer();

            player.Start();
            recorder.Start();
            recorder.MicrophoneDataAvailable += Recorder_MicrophoneDataAvailable;
        }

        private void Recorder_MicrophoneDataAvailable(byte[] data)
        {
            byte[] encoded = codec.Encode(data, 0, data.Length);
            Program.Console.WriteMessage($"{data.Length} -> {encoded.Length}");
            player.Play(codec.Decode(encoded, 0, encoded.Length));
        }

        public void Stop()
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            if (recorder != null) recorder.Stop();
            if (player != null) player.Stop();
            recorder = null;
            player = null;
        }

        private void IntAudioTestForm_FormClosing(object sender, FormClosingEventArgs e) => Stop();

        private void btnStart_Click(object sender, EventArgs e) => Start();

        private void btnStop_Click(object sender, EventArgs e) => Stop();
    }
}
