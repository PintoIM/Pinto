using NAudio.Wave;
using System;
using System.Threading;

namespace PintoNS.Calls
{
    public class AudioPlayer
    {
        public bool IsPlaying { get; private set; }
        private Thread audioPlayerThread;
        private BufferedWaveProvider playBuffer;
        private WaveOut waveOut;
        private WaveFormat waveFormat = new WaveFormat(44100, 2);
        private int device;
        public int Device
        {
            get => device;
            set
            {
                device = value;
                if (IsPlaying)
                {
                    Stop();
                    Start();
                }
            }
        }

        public void Start()
        {
            IsPlaying = true;

            waveOut = new WaveOut();
            waveOut.DeviceNumber = Device;

            audioPlayerThread = new Thread(new ThreadStart(AudioPlayerThread_Func));
            audioPlayerThread.Start();
        }

        public void Stop()
        {
            try
            {
                if (waveOut != null) waveOut.Dispose();
            }
            catch (Exception) { }

            waveOut = null;
            IsPlaying = false;
        }

        public void Play(byte[] data)
        {
            playBuffer.AddSamples(data, 0, data.Length);
        }

        private void AudioPlayerThread_Func()
        {
            playBuffer = new BufferedWaveProvider(waveFormat);
            playBuffer.DiscardOnBufferOverflow = true;
            waveOut.Init(playBuffer);
            waveOut.Play();

            while (IsPlaying)
                Thread.Sleep(1);
        }
    }
}
