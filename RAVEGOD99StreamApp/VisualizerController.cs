using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

using NAudio.Wave;

namespace StreamApp
{
    public class Profile
    {
        public string ProfileName { get; set; } = "Default ";
        public RandomizerSettings RandomizerProfile { get; set; } = new RandomizerSettings();
        public VisualizerSettings VisualizerProfile { get; set; } = new VisualizerSettings();
        public SoundSettings SoundProfile { get; set; } = new SoundSettings();
        public ColorSettings ColorProfile { get; set; } = new ColorSettings();

        public Profile(string profileName, RandomizerSettings randomizerProfile, 
            VisualizerSettings visualizerProfile, SoundSettings soundProfile, 
            ColorSettings colorProfile) //custom
        {
            ProfileName = profileName;
            RandomizerProfile = randomizerProfile;
            VisualizerProfile = visualizerProfile;
            SoundProfile = soundProfile;
            ColorProfile = colorProfile;
        }

        public Profile() {} //default

    }

    public class SoundSettings
    {
        public int RATE { get; set; } = 44100;      //samples per second
        public int SAMPLES { get; set; } = 2048;    //# of samples
        public int BITDEPTH { get; set; } = 16;     //bits per sample
        public int CHANNELS { get; set; } = 1;      //channels for audio

        public SoundSettings(int rate, int samples, int bitdepth, int channels) //custom
        {
            RATE = rate;
            SAMPLES = samples;
            BITDEPTH = bitdepth;
            CHANNELS = channels;
        }

        public SoundSettings(){} //default

    }

    public class DitherHSVSettings
    {
        public bool _AbsoluteDither { get; set; } = false;
        public bool _DitherHue { get; set; } = false;
        public bool _DitherSaturation { get; set; } = false;
        public bool _DitherValue { get; set; } = false;
        public double DitherStrength { get; set; } = 0.0;

        public DitherHSVSettings(bool _AbsoluteDither, bool _DitherHue, 
            bool _DitherSaturation, bool _DitherValue, double DitherStrength) //custom
        {
            this._AbsoluteDither = _AbsoluteDither;
            this._DitherHue = _DitherHue;
            this._DitherSaturation = _DitherSaturation;
            this._DitherValue = _DitherValue;
            this.DitherStrength = DitherStrength;
        }
        public DitherHSVSettings() { } //default

    }

    public class RandomizerSettings
    {
        public bool _RandomizeColumnColorOnActivation { get; set; } = false;
        public bool _RandomizeColumnColorOnRender { get; set; } = false;
        public bool _RandomizeBackgroundColorOnActivation { get; set; } = false;
        public bool _RandomizeBackgroundColorOnRender { get; set; } = false;
        public bool _DitherColumnColorOnActivation { get; set; } = false;
        public bool _DitherColumnColorOnRender { get; set; } = false;
        public bool _DitherBackgroundColorOnActivation { get; set; } = false;
        public bool _DitherBackgroundColorOnRender { get; set; } = false;

        public DitherHSVSettings ColumnActivationDither { get; set; } = new DitherHSVSettings();
        public DitherHSVSettings ColumnRenderDither { get; set; } = new DitherHSVSettings();
        public DitherHSVSettings BackgroundActivationDither { get; set; } = new DitherHSVSettings();
        public DitherHSVSettings BackgroundRenderDither { get; set; } = new DitherHSVSettings();

        public RandomizerSettings(bool _RandomizeColumnColorOnActivation, bool _RandomizeColumnColorOnRender, bool _RandomizeBackgroundColorOnActivation,
            bool _RandomizeBackgroundColorOnRender, bool _DitherColumnColorOnActivation, bool _DitherColumnColorOnRender, bool _DitherBackgroundColorOnActivation,
            bool _DitherBackgroundColorOnRender, DitherHSVSettings ColumnActivationDither, DitherHSVSettings ColumnRenderDither,
            DitherHSVSettings BackgroundActivationDither, DitherHSVSettings BackgroundRenderDither)
        {
            this._RandomizeBackgroundColorOnActivation = _RandomizeBackgroundColorOnActivation;
            this._RandomizeColumnColorOnRender = _RandomizeColumnColorOnRender;
            this._RandomizeBackgroundColorOnActivation = _RandomizeBackgroundColorOnActivation;
            this._RandomizeBackgroundColorOnRender = _RandomizeBackgroundColorOnRender;
            this._DitherColumnColorOnActivation = _DitherColumnColorOnActivation;
            this._DitherColumnColorOnRender = _DitherColumnColorOnRender;
            this._DitherBackgroundColorOnActivation = _DitherBackgroundColorOnActivation;
            this._DitherBackgroundColorOnRender = _DitherBackgroundColorOnRender;

            this.ColumnActivationDither = ColumnActivationDither;
            this.ColumnRenderDither = ColumnRenderDither;
            this.BackgroundActivationDither = BackgroundActivationDither;
            this.BackgroundRenderDither = BackgroundRenderDither;
        }

        public RandomizerSettings() { } // default

    }

    public class ColorSettings
    {
        public RGBS[] BackgroundColors { get; set; }
        public RGBS[] ColumnColors { get; set; }
        public RGBS[] ActivationColors { get; set; }

        public ColorSettings(RGBS[] BackgroundColors, RGBS[] ColumnColors, RGBS[] ActivationColors)
        {
            this.BackgroundColors = BackgroundColors;
            this.ColumnColors = ColumnColors;
            this.ActivationColors = ActivationColors;
        }

        public ColorSettings() //default
        {
            RGBS[] BackgroundColors = new RGBS[2];
            RGBS[] ColumnColors = new RGBS[1];
            RGBS[] ActivationColors = new RGBS[1];
            BackgroundColors[0] = new RGBS(255, 255, 0,.2);
            BackgroundColors[1] = new RGBS(255, 255, 0,1);
            ColumnColors[0] = new RGBS(255,0, 255);
            ActivationColors[0] = new RGBS(0, 255, 255);

            this.BackgroundColors = BackgroundColors;
            this.ColumnColors = ColumnColors;
            this.ActivationColors = ActivationColors;
        }

    }

    public class VisualizerSettings
    {
        public bool _useDB { get; set; } = false;
        public bool _grayscale { get; set; } = false;
        public int visualizerCeiling { get; set; } = 5000;
        public int activationThreshold { get; set; } = 5000;
        public int listeningThreshold { get; set; } = 0;
        public int[] frequencyRanges { get; set; } = new int[] { 43, 86, 172, 258, 350, 450, 1225, 2000, 4000, 10000 };
        public int[] frequencySizes { get; set; } = new int[] { 43, 43, 86, 86, 92, 100, 775, 775, 2000, 6000 };
        public int min_range { get; set; } = 43;
        public double beatSensitivity { get; set; } = 1.35;

        public VisualizerSettings(bool _useDB, bool _grayscale, int visualizerCeiling, int activationThreshold,
            int listeningThreshold,int[] frequencyRanges, double beatSensitivity)
        {
            this._useDB = _useDB;
            this._grayscale = _grayscale;
            this.visualizerCeiling = visualizerCeiling;
            this.activationThreshold = activationThreshold;
            this.listeningThreshold = listeningThreshold;
            this.frequencyRanges = frequencyRanges;
            this.beatSensitivity = beatSensitivity;

            int[] frequencySizes = new int[frequencyRanges.Length];
            frequencySizes[0] = frequencyRanges[0];

            for (int i = 1; i < frequencyRanges.Length; ++i) frequencySizes[i] = frequencyRanges[i] - frequencyRanges[i - 1];

            this.frequencySizes = frequencySizes;
        }

        public VisualizerSettings() { } //default

    }

    internal class HistoryBuffer<T>
    {
        internal T[] data { get; }
        internal int Length;
        private bool _full;
        internal bool _FULL { get
            {
                return _full;
            }
        }

        private int nextIndex = 0;

        internal void addData(T data)
        {
            this.data[nextIndex] = data;
            if (nextIndex == Length - 1)
            {
                nextIndex = 0;
                if (!_full) _full = true;
            }
            else nextIndex++;
        }

        internal HistoryBuffer(int Length) 
        {
            this.Length = Length;
            data = new T[Length];
        }
    }

    class VisualizerController
    {
        //Working data      
        public Profile profile;


        HistoryBuffer<double> energyHistoryBuffer;
        Random rand = new Random();
        RGBS[] sectionColor;

        //output
        VisualizerDisplay display;
        String result = "0";

        public VisualizerController(int w, int h)
        {
            display = new VisualizerDisplay(w, h);
            profile = new Profile();

            energyHistoryBuffer = new HistoryBuffer<double>(  (profile.SoundProfile.RATE / profile.SoundProfile.SAMPLES)) ;

            int freq_ranges = profile.VisualizerProfile.frequencyRanges.Length;

            sectionColor = new RGBS[freq_ranges];
            for (int i = 0; i < freq_ranges; ++i) sectionColor[i] = new RGBS(profile.ColorProfile.ColumnColors[0].ToARGB());

        }

        public void ParseData(Tuple<SoundSettings, BufferedWaveProvider> data)
        {
            profile.SoundProfile = data.Item1;
            FormatThenVisualize(data.Item2);
        }

        private void FormatThenVisualize(BufferedWaveProvider bwp)
        {
            int frameSize = profile.SoundProfile.SAMPLES;
            var audioBytes = new byte[frameSize]; //create working buffer for audio

            bwp.Read(audioBytes, 0, frameSize); //fill it with input
            if (audioBytes.Length == 0 || audioBytes[frameSize - 2] == 0) return; //if its empty, return

            

            int BYTES_PER_SAMPLE = profile.SoundProfile.BITDEPTH / 8; // BITS / 8 BITS PER BYTE = BYTES
            int samples = audioBytes.Length / BYTES_PER_SAMPLE; //gets number of samples


            //different datasets to work with 
            double[] pcm = new double[samples]; //Pulse-code modulation: Each value is a sample's quantized amplitude
            double[] fft = new double[samples]; //Fast-Fourier Transformed PCM data: Gets the amplitude at linearly spaced frequencies
            //double[] cqt = new double[samples]; //Constant-Q Transformed PCM data: Gets the amplitude at exponentially spaced frequencies (matches human hearing)

            //populate PCM data
            for (int i = 0; i < samples; ++i)
            {
                Int16 sample = BitConverter.ToInt16(audioBytes, i * 2); //16 bit sample from 2 bytes of data
                pcm[i] = (double)(sample); // MAX_16BIT_VALUE * 200.0;
            }

            fft = FFT(pcm); //FFTs the populated pcm;   

            Visualize(pcm, fft.Take(samples / 2).ToArray()); //Visualize the formatted data
        }

        private void Visualize(double[] pcm, double[] fftReal)
        {


            bool _isBeat = isBeatPresent(pcm, profile.VisualizerProfile.listeningThreshold);
            bool _useDB = profile.VisualizerProfile._useDB;

            int[] ampsAtFreq = GetThresholdedAvgAmplitudesAtFrequencyRanges(fftReal, profile.VisualizerProfile.frequencyRanges,
                profile.VisualizerProfile.listeningThreshold, _useDB);

            int sections = ampsAtFreq.Length;
            int sectionWidth = display.width / sections;
            int sectionMaxHeight = display.height;


            double MAX_INTENSITY = profile.VisualizerProfile.visualizerCeiling ;
            int ACTIVATION_THRESHOLD = profile.VisualizerProfile.activationThreshold;

            double intensityPerPixel = MAX_INTENSITY / sectionMaxHeight; 

            for (int section = 0; section < sections; ++section)
            {

                int amps = ampsAtFreq[section] ;
                amps = amps >= 0 ? amps : 0;

                int sectionHeight = (int)(amps / intensityPerPixel) + 1;
                sectionHeight = sectionHeight > sectionMaxHeight ? sectionMaxHeight : sectionHeight;

                if (_isBeat) display.brightness = 1;
                else display.brightness = 0.3;

                if (amps > ACTIVATION_THRESHOLD)
                {
                    Int32 newARGB = 0;
                    if (profile.VisualizerProfile._grayscale)
                        newARGB = profile.ColorProfile.ActivationColors[0].ToGrayscale();
                    else
                        newARGB = profile.ColorProfile.ActivationColors[0].ToARGB();
                    sectionColor[section].SetColor(newARGB);         
                } else
                {
                    Int32 newARGB = 0;
                    if (profile.VisualizerProfile._grayscale)
                        newARGB = profile.ColorProfile.ColumnColors[0].ToGrayscale();
                    else
                        newARGB = profile.ColorProfile.ColumnColors[0].ToARGB();
                    sectionColor[section].SetColor(newARGB, display.brightness);    
                }
                int x_padding = section * sectionWidth;

                for (int x = 0; x < sectionWidth; ++x)
                {
                    for (int y = 0; y < sectionHeight; ++y)
                        display.SetPixel(x_padding + x, (sectionMaxHeight - 1) - y, sectionColor[section]);
                    for (int y = sectionHeight; y < sectionMaxHeight; ++y)
                        display.SetPixel(x_padding + x, (sectionMaxHeight - 1) - y, profile.ColorProfile.BackgroundColors[0],display.brightness);
                }
            }

        }

        private bool isBeatPresent(double[] pcm, int threshold)
        {
            bool _beat = false;

            double BEAT_SENSITIVITY = profile.VisualizerProfile.beatSensitivity;

            double instantEnergy = Math.Sqrt(pcm.Select(x => Math.Pow(x, 2)).ToList().Average()); //root-mean-square (energy level)
            if (energyHistoryBuffer._FULL) {
                double avgLocalEnergy = energyHistoryBuffer.data.Average();
                _beat = instantEnergy > BEAT_SENSITIVITY * avgLocalEnergy && instantEnergy > threshold;
            }
            energyHistoryBuffer.addData(instantEnergy);
            return _beat;
        }

        private int[] GetThresholdedAvgAmplitudesAtFrequencyRanges(double[] fft, int[] frequencyRanges, int threshold, bool _dB)
        {
            int ranges = frequencyRanges.Length;
            int bufSize = fft.Length;


     
            int[] aboveThresholdedAmplitudesAtFrequencyRanges = new int[ranges];
            int MAX_FREQ = profile.SoundProfile.RATE / 2;
            int frequencySpacing = MAX_FREQ / bufSize;

            for (int range = 0, i = 0; range < ranges; ++range)
            {
                int ceiling = frequencyRanges[range];
                int counter = 0;
                double runningSum = 0;

                

                while (i * frequencySpacing < ceiling && i < bufSize) //only counts and adds values above the threshold
                {
                    if (fft[i] > threshold)
                    {
                        runningSum += fft[i];
                        ++counter;
                    }
                    ++i;
                }

                

                int average = counter > 0 ? (int)(runningSum / counter) : 0;
                if (_dB) average = (int) (20 * Math.Log10(average));
                aboveThresholdedAmplitudesAtFrequencyRanges[range] = average;
            }

            return aboveThresholdedAmplitudesAtFrequencyRanges;
        }

        public String getResult()
        {
            return result;
        }

        public VisualizerDisplay getDisplay()
        {
            return display;
        }

        private double[] FFT(double[] data) //taken from Scott W. Harden's github
        {
            double[] fft = new double[data.Length]; // this is where we will store the output (fft)
            Complex[] fftComplex = new Complex[data.Length]; // the FFT function requires complex format
            for (int i = 0; i < data.Length; ++i)
            {
                fftComplex[i] = new Complex(data[i], 0.0); // make it complex format (imaginary = 0)
            }
            Accord.Math.FourierTransform.FFT(fftComplex, Accord.Math.FourierTransform.Direction.Forward);
            for (int i = 0; i < data.Length; ++i)
            {
                
                fft[i] = fftComplex[i].Magnitude; // back to double
                //Console.WriteLine($"FFT[{i}]: {fft[i]}");
                //if(_dB) fft[i] = 20* Math.Log10(fft[i]); // convert to dB
            }
            return fft;
        }
        /*
        private double[] CQT(double[] pcm, double binsPerOctave, int minFreq, int maxFreq, int thresh)
        {
            double Q_const = 1 / (Math.Pow(2, (1 / binsPerOctave)) - 1);
            double bins = Math.Ceiling(binsPerOctave * Math.Log((double)maxFreq / (double)minFreq, 2));

            double[] cqt = new double[(int)bins];

            for (double bin = 0; bin < bins; ++ bin)
            {
                double currentFrequency =  (Math.Pow(2, bin / binsPerOctave) * (double) minFreq);
                double currentWindowLength = Math.Round(Q_const * (settings.RATE / currentFrequency));
                //Console.WriteLine($"Bin Frequency: {currentFrequency}");

                Complex runningSum = 0;
                int counter = 0;
                for(int i = 0; i < currentWindowLength; ++ i)
                {
                    //Complex complexSum = pcm[i*pcm.Length/currentWindowLength] * 1 * Complex.Exp(new Complex(0.0, -2*Math.PI*Q_const*i));
                    Complex complexSum = 0;

                    complexSum += pcm[(int)(i % (pcm.Length - 1))] * hamming((int)currentWindowLength, i) * new Complex(Math.Cos(2 * Math.PI * Q_const * i / currentWindowLength), Math.Sin(2 * Math.PI * Q_const * i / currentWindowLength));
                    if (complexSum.Magnitude > thresh)
                    {
                        runningSum += complexSum;
                        ++counter;
                    }
                }
                double runningSumMag = runningSum.Magnitude / ((double) counter * (currentWindowLength / pcm.Length + 1));

                cqt[(int)bin] = 20 * Math.Log10(runningSumMag);
            }

            return cqt;
        }
        private Complex hamming(int N, int n)
        {
            Complex result = new Complex();
            double omega = 2 * Math.PI / N;
            double HAMMING_CONST = 0.54;
            result = HAMMING_CONST - ((1 - HAMMING_CONST) * Math.Cos(omega * n));
            return result;
        }
        */
    }

}
