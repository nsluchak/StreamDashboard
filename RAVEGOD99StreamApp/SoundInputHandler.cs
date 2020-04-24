using System;

using NAudio.Wave;

namespace StreamApp
{

    public class SoundInputHandler
    {
        public BufferedWaveProvider bwp;
        public WaveIn wi;
        public bool deviceOpen = false;

        //audio input settings
        SoundSettings settings = new SoundSettings(44100, (int)Math.Pow(2, 11), 16, 1);

        public static String[] GetAvailableDevices()
        {
            int waveInDevices = WaveIn.DeviceCount;
            String[] availableDevices = new String[waveInDevices];
            for (int device = 0; device < waveInDevices; ++device)
            {
                WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(device);
                availableDevices[device] = deviceInfo.ProductName;
            }
            return availableDevices;
        }

        public SoundInputHandler(int deviceNumber = 0)
        {
            OpenInput(deviceNumber);
        }

        public void OpenInput(int deviceNumber)
        {
            //see if we need to make new WaveIn
            if (deviceOpen)
            {
                if (wi.DeviceNumber == deviceNumber) return; //if the device number is the same do nothing
                wi.StopRecording(); //stop listening on current input
                wi.DeviceNumber = deviceNumber; //change input
            } else
            {
                //init WaveIn
                wi = new WaveIn();
                wi.DeviceNumber = deviceNumber;
                wi.WaveFormat = new WaveFormat(settings.RATE, settings.CHANNELS); 
                wi.BufferMilliseconds = (int)((double)settings.SAMPLES / (double)settings.RATE * 1000.0); //how many samples divided by samples per millisecond = buffer duration in ms
            
                //begin recording into buffer
                wi.DataAvailable += new EventHandler<WaveInEventArgs>(AudioDataAvailable);
                bwp = new BufferedWaveProvider(wi.WaveFormat);
                bwp.BufferLength = settings.SAMPLES * (settings.BITDEPTH / 8); //how many samples * bytes per sample = buffer length
                bwp.DiscardOnBufferOverflow = true; //discard extra data   
            }
            
            try
            {
                wi.StartRecording();
                deviceOpen = true; //device is open
               
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Failed to record mic", "ERROR");
            }
        }

        public Tuple<SoundSettings, BufferedWaveProvider> GetWorkingData()
        {
            return new Tuple<SoundSettings, BufferedWaveProvider>(settings, bwp);
        }

        void AudioDataAvailable(object sender, WaveInEventArgs e)
        {
            bwp.AddSamples(e.Buffer, 0, e.BytesRecorded);
        }

    }
}