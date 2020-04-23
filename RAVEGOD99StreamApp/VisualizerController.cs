using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

using NAudio.Wave;

namespace StreamApp
{
    class VisualizerController
    {
        //Working data      
        SoundSettings settings;
        int[] frequency_ranges = new int[] { 43, 86, 172, 258, 350, 450, 1225, 2000, 4000, 10000 };
        int[] frequency_sizes = new int[] { 43, 43, 86, 86, 92, 100, 775, 775, 2000, 2000, 6000 };
        Random rand = new Random();
        RGBS[] sectionColor;
        bool _colorSet = false;

        //output
        VisualizerDisplay display;
        String result = "0";

        public VisualizerController(int w, int h)
        {
            display = new VisualizerDisplay(w, h);
        }

        public void ParseData(Tuple<SoundSettings, BufferedWaveProvider> data)
        {
            this.settings = data.Item1;
            FormatThenVisualize(data.Item2);
        }

        private void FormatThenVisualize(BufferedWaveProvider bwp)
        {
            int frameSize = settings.SAMPLES;
            var audioBytes = new byte[frameSize]; //create working buffer for audio
            bwp.Read(audioBytes, 0, frameSize); //fill it with input
            if (audioBytes.Length == 0 || audioBytes[frameSize - 2] == 0) return; //if its empty, return

            int BYTES_PER_SAMPLE = settings.BITDEPTH / 8; // BITS / 8 BITS PER BYTE = BYTES
            int samples = audioBytes.Length / BYTES_PER_SAMPLE; //gets number of samples


            //different datasets to work with 
            double[] pcm = new double[samples]; //Pulse-code modulation: Each value is a sample's quantized amplitude
            double[] fft = new double[samples]; //Fast-Fourier Transformed PCM data: Gets the amplitude at linearly spaced frequencies
            double[] cqt = new double[samples]; //Constant-Q Transformed PCM data: Gets the amplitude at exponentially spaced frequencies (matches human hearing)

            //populate PCM data
            for (int i = 0; i < samples; ++i)
            {
                Int16 sample = BitConverter.ToInt16(audioBytes, i * 2); //16 bit sample from 2 bytes of data

                pcm[i] = (double)(sample); // MAX_16BIT_VALUE * 200.0;
            }

            fft = FFT(pcm); //FFTs the populated pcm;   

            Visualize(pcm, fft.Take(samples / 2).ToArray()); //Visualize the formatted data
        }

       /* private void testCQT(double[] pcm)
        {
            double[] cqt = CQT(pcm, 12, 20, 20000, 0);
        }*/

        private void Visualize(double[] pcm, double[] fftReal)
        {
            double RMS = Math.Sqrt(pcm.Select(x => Math.Pow(x, 2)).ToList().Average()); //root-mean-square (energy-level)
            double dB = 20 * Math.Log10(RMS);
            int[] ampsAtFreq = GetThresholdedAvgAmplitudesAtFrequencyRanges(fftReal, frequency_ranges, 0);
            double MIN_RANGE = 43;
            //double[] ampsAtFreq = CQT(pcm, 2, 20, 350, 0);
            //Array.Reverse(ampsAtFreq);


            int sections = ampsAtFreq.Length;
            int sectionWidth = display.width / sections;
            int sectionMaxHeight = display.height;

            int[] normalizedAmpsAtFreq = new int[sections];

            for (int i = 0; i < sections; ++i)
            {
                double RLE = 1;
                //double RLE = 1 / (Math.Log(MIN_RANGE, 10) / Math.Log(frequency_sizes[i], 10));
                //double RLE = 1 / (MIN_RANGE / frequency_sizes[i]);
                //double RLE = 1 / (MIN_RANGE / ( frequency_sizes[i]/ Math.Log( frequency_sizes[i], MIN_RANGE) ) );
                normalizedAmpsAtFreq[i] = (int)RLE * (int)ampsAtFreq[i];
            }

            result = RMS.ToString();

            int MAX_INTENSITY = 2000;
            double intensityPerPixel = MAX_INTENSITY / sectionMaxHeight;

            if (!_colorSet) {
                sectionColor = new RGBS[sections];
                for (int i = 0; i < sections; ++i)
                {
                    double h = rand.Next(360), s = rand.NextDouble();
                    sectionColor[i] = new RGBS(RGBS.HSVtoARGB(300, s, 1.0));
                }
                _colorSet = true;
            }
            for (int section = 0; section < sections; ++section)
            {

                int amps = normalizedAmpsAtFreq[section];
                amps = amps >= 0 ? amps : 0;

                int sectionHeight = (int)(amps / intensityPerPixel) + 1;

                if (sectionHeight > sectionMaxHeight)
                {
                    double h = rand.Next(360), s = rand.NextDouble();
                    sectionColor[section].SetColor(RGBS.HSVtoARGB(300, s, 1.0));
                    sectionHeight = sectionMaxHeight;
                }
                int x_padding = section * sectionWidth;

                for (int x = 0; x < sectionWidth; ++x) {
                    for (int y = 0; y < sectionHeight; ++y)
                        display.SetPixel(x_padding + x, (sectionMaxHeight - 1) - y, sectionColor[section]);
                    for (int y = sectionHeight; y < sectionMaxHeight; ++y)
                        display.SetPixel(x_padding + x, (sectionMaxHeight - 1) - y, 255, 255, 255);
                }
            }

        }

        private int[] GetAvgAmplitudesAtFrequencyRanges(double[] fft, int[] frequencyRanges)
        {
            int ranges = frequencyRanges.Length;
            int bufSize = fft.Length;

            int[] avgAmplitudesAtFrequencyRanges = new int[ranges];
            int MAX_FREQ = settings.RATE / 2;
            int frequencySpacing = MAX_FREQ / bufSize;

            for (int range = 0, i = 0; range < ranges; ++range)
            {
                int ceiling = frequencyRanges[range];
                double runningSum = 0;

                while (i * frequencySpacing < ceiling && i < bufSize) //add up loudness of each frequency data point in specified data range
                    runningSum += fft[i++];

                int average = (int)(runningSum / (ceiling / frequencySpacing));
                avgAmplitudesAtFrequencyRanges[range] = average;
            }

            return avgAmplitudesAtFrequencyRanges;
        }

        private int[] GetThresholdedAvgAmplitudesAtFrequencyRanges(double[] fft, int[] frequencyRanges, int threshold)
        {
            int ranges = frequencyRanges.Length;
            int bufSize = fft.Length;

            int[] aboveThresholdedAmplitudesAtFrequencyRanges = new int[ranges];
            int MAX_FREQ = settings.RATE / 2;
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
                //fft[i] = Math.Log10(fft[i]); // convert to dB
            }
            return fft;
        }

        private double[] CQT(double[] pcm, double binsPerOctave, int minFreq, int maxFreq, int thresh)
        {
            double Q_const = 1 / (Math.Pow(2, (1 / binsPerOctave)) - 1);
            double bins = Math.Ceiling(binsPerOctave * Math.Log((double)maxFreq / (double)minFreq, 2));

            double[] cqt = new double[(int)bins];

            for (double bin = 0; bin < bins; ++ bin)
            {
                double currentFrequency =  (Math.Pow(2, bin / binsPerOctave) * (double) minFreq);
                int currentWindowLength = (int) Math.Round(Q_const * (double) settings.RATE / currentFrequency);

                //Console.WriteLine($"Bin Frequency: {currentFrequency}");

                Complex runningSum = 0;
                int counter = 0;
                for(int i = 0; i < currentWindowLength; ++ i)
                {
                    //Complex complexSum = pcm[i*pcm.Length/currentWindowLength] * 1 * Complex.Exp(new Complex(0.0, -2*Math.PI*Q_const*i));
                    Complex complexSum = 0;
                    for(int pcmI = 0; pcmI < pcm.Length; ++pcmI)
                    {
                        complexSum += pcm[pcmI] * hamming(currentWindowLength, i) * Complex.Exp(new Complex(0.0, -2 * Math.PI * Q_const * i));
                    }
                    if (complexSum.Magnitude > thresh)
                    {
                        runningSum += complexSum;
                        ++counter;
                    }
                }
                double runningSumMag = runningSum.Magnitude / (double) counter;

                cqt[(int)bin] = runningSumMag;
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

    }

}
