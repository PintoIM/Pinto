using NAudio.Wave;
using System;
using System.IO;
using System.Threading;

namespace PintoNS.General
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
        public event EventHandler<byte[]> MicrophoneDataAvailable;

        public void Start()
        {
            waveIn = new WaveIn();
            waveIn.BufferMilliseconds = 100;
            waveIn.NumberOfBuffers = 10;
            waveIn.DeviceNumber = Device;
            waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(waveIn_DataAvailable);
            waveIn.WaveFormat = new WaveFormat(44100, 2);
            waveIn.StartRecording();
            IsRecording = true;
        }

        private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (MicrophoneDataAvailable != null)
                MicrophoneDataAvailable.Invoke(this, e.Buffer);
        }

        public void Stop()
        {
            try
            {
                if (waveIn != null) waveIn.Dispose();
            }
            catch (Exception) { }

            waveIn = null;
            IsRecording = false;
        }
    }
}
