using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PintoNS.General
{
    public class AudioRecorderPlayer
    {
        private bool isPlaying;
        private Thread audioPlayerThread;
        private BufferedWaveProvider playBuffer;
        private MemoryStream sound;
        private WaveIn waveIn;
        private WaveOut waveOut;
        public int MicrophoneDevice;
        public event EventHandler<byte[]> MicrophoneDataAvailable;

        public void Start()
        {
            waveIn = new WaveIn();
            waveIn.BufferMilliseconds = 100;
            waveIn.NumberOfBuffers = 10;
            waveOut = new WaveOut();

            waveIn.DeviceNumber = MicrophoneDevice;
            waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(waveIn_DataAvailable);
            waveIn.WaveFormat = new WaveFormat(44100, 2);

            sound = new MemoryStream();
            waveIn.StartRecording();

            audioPlayerThread = new Thread(new ThreadStart(AudioPlayerThread_Func));
            isPlaying = true;
            audioPlayerThread.Start();
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
            try
            {
                if (waveOut != null) waveOut.Dispose();
            }
            catch (Exception) { }
            
            waveIn = null;
            waveOut = null;
            isPlaying = false;
        }

        public void Play(byte[] data)
        {
            playBuffer.AddSamples(data, 0, data.Length);
        }

        private void AudioPlayerThread_Func()
        {
            playBuffer = new BufferedWaveProvider(waveIn.WaveFormat);
            waveOut.Init(playBuffer);
            waveOut.Play();

            while (isPlaying)
            {
                Thread.Sleep(1);
            }
        }
    }
}
