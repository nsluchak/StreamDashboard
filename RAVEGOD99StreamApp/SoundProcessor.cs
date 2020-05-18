using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace StreamApp
{
    internal class HistoryBuffer<T>
    {
        internal T[] data { get; }
        internal int Length;
        private bool _full;
        internal bool _FULL
        {
            get
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

    class SoundProcessor
    {
        HistoryBuffer<double> energyHistoryBuffer;

        private double[] pcm;
        private double[] fftReal;

        public SoundProcessor()
        {
            energyHistoryBuffer = new HistoryBuffer<double>((Dashboard.WorkingProfile.SoundProfile.RATE / Dashboard.WorkingProfile.SoundProfile.SAMPLES));
        }

        public bool Format(BufferedWaveProvider bwp) //returns true if data was successfully formatted
        {
            int frameSize = Dashboard.WorkingProfile.SoundProfile.SAMPLES;
            var audioBytes = new byte[frameSize]; //create working buffer for audio

            bwp.Read(audioBytes, 0, frameSize); //fill it with input
            if (audioBytes.Length == 0 || audioBytes[frameSize - 2] == 0) return false; //if its empty, return false

            int BYTES_PER_SAMPLE = Dashboard.WorkingProfile.SoundProfile.BITDEPTH / 8; // BITS / 8 BITS PER BYTE = BYTES
            int samples = audioBytes.Length / BYTES_PER_SAMPLE; //gets number of samples


            //different datasets to work with 
            pcm = new double[samples]; //Pulse-code modulation: Each value is a sample's quantized amplitude
            double[] fft = new double[samples]; //Fast-Fourier Transformed PCM data: Gets the amplitude at linearly spaced frequencies
            //double[] cqt = new double[samples]; //Constant-Q Transformed PCM data: Gets the amplitude at exponentially spaced frequencies (matches human hearing)

            //populate PCM data
            for (int i = 0; i < samples; ++i)
            {
                Int16 sample = BitConverter.ToInt16(audioBytes, i * 2); //16 bit sample from 2 bytes of data
                pcm[i] = (double)(sample); // MAX_16BIT_VALUE * 200.0;
            }

            fft = FFT(pcm); //FFTs the populated pcm;
            fftReal = fft.Take(samples / 2).ToArray(); //Sets fftReal to only contain real values from fft

            return true;
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

        public int[] ThresholdedAvgEnergyByFrequencyRange(int[] frequencyRanges, int threshold, bool _dB)
        {
            int ranges = frequencyRanges.Length;
            int bufSize = fftReal.Length;

            int[] aboveThresholdedAmplitudesAtFrequencyRanges = new int[ranges];
            int MAX_FREQ = Dashboard.WorkingProfile.SoundProfile.RATE / 2;
            int frequencySpacing = MAX_FREQ / bufSize;

            for (int range = 0, i = 0; range < ranges; ++range)
            {
                int ceiling = frequencyRanges[range];
                int counter = 0;
                double runningSum = 0;



                while (i * frequencySpacing < ceiling && i < bufSize) //only counts and adds values above the threshold
                {
                    if (fftReal[i] > threshold)
                    {
                        runningSum += fftReal[i];
                        ++counter;
                    }
                    ++i;
                }



                int average = counter > 0 ? (int)(runningSum / counter) : 0;
                if (_dB) average = (int)(20 * Math.Log10(average));
                aboveThresholdedAmplitudesAtFrequencyRanges[range] = average;
            }

            return aboveThresholdedAmplitudesAtFrequencyRanges;
        }

        public bool isBeatPresent(int threshold)
        {
            bool _beat = false;

            double BEAT_SENSITIVITY = Dashboard.WorkingProfile.SoundProcessorProfile.beatSensitivity;

            double instantEnergy = Math.Sqrt(pcm.Select(x => Math.Pow(x, 2)).ToList().Average()); //root-mean-square (energy level)
            if (energyHistoryBuffer._FULL)
            {
                double avgLocalEnergy = energyHistoryBuffer.data.Average();
                _beat = instantEnergy > BEAT_SENSITIVITY * avgLocalEnergy && instantEnergy > threshold;
            }
            energyHistoryBuffer.addData(instantEnergy);
            return _beat;
        }






        //unused, might try again another time










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
