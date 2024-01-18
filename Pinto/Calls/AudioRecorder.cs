using NAudio.Wave;
using System;

namespace PintoNS.Calls
{
    public class AudioRecorder
    {
        public bool IsRecording { get; private set; }
        private WaveIn waveIn;
        private int device;
        public int Device
        {
            get => device;
            set
            {
                device = value;
                if (IsRecording)
                {
                    Stop();
                    Start();
                }
            }
        }
        public event Action<byte[]> MicrophoneDataAvailable;

        public void Start()
        {
            waveIn = new WaveIn();
            waveIn.DeviceNumber = Device;
            waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(waveIn_DataAvailable);
            waveIn.WaveFormat = new WaveFormat(44100, 2);
            waveIn.StartRecording();
            IsRecording = true;
        }

        private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (MicrophoneDataAvailable != null)
                MicrophoneDataAvailable.Invoke(e.Buffer);
        }

        public void Stop()
        {
            try
            {
                if (waveIn != null)
                {
                    waveIn.StopRecording();
                    waveIn.Dispose();
                }
            }
            catch (Exception) { }

            waveIn = null;
            IsRecording = false;
        }
    }
}
