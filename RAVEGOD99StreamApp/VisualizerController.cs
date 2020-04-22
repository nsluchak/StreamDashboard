﻿using System;
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
        int[] frequency_ranges = new int[] {43, 86, 172, 258, 350, 450, 1225, 2000, 5000, 8000, 12000, 16000};
        int[] frequency_sizes = new int[] {43, 43, 86, 86, 92 ,100, 775, 775, 3000, 3000, 4000, 4000};
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
            double[] fft = new double[samples]; //Fast-Fourier Transformed PCM data: Gets the amplitude at each frequency

            //populate PCM data
            for (int i = 0; i < samples; ++i)
            {
                Int16 sample = BitConverter.ToInt16(audioBytes, i * 2);
                double MAX_16BIT_VALUE = Math.Pow(2, 16);

                //converts the sample into a % amplitude (range of 200% from -100% to 100%)
                pcm[i] = (double)(sample); // MAX_16BIT_VALUE * 200.0;
            }

            fft = FFT(pcm); //FFTs the populated pcm;   

            Visualize(pcm, fft.Take(samples/2).ToArray()); //Visualize the formatted data
        }

        private void Visualize(double[] pcm, double[] fftReal)
        {
            double RMS = Math.Sqrt(pcm.Select(x => Math.Pow(x, 2)).ToList().Average()); //root-mean-square
            double dB = 20 * Math.Log10(RMS);
            int[] ampsAtFreq = GetAvgAmplitudesAtFrequencyRanges(fftReal, frequency_ranges);        
            double MIN_RANGE = 43;
            

            int sections = ampsAtFreq.Length;
            int sectionWidth = display.width / sections;
            int sectionMaxHeight = display.height;

            int[] normalizedAmpsAtFreq = new int[sections];
            
            for (int i = 0; i < sections; ++ i)
            {
                double RLE = 1 / (Math.Log(MIN_RANGE, 15) / Math.Log(frequency_sizes[i], 15));
                normalizedAmpsAtFreq[i] = (int)RLE * ampsAtFreq[i];
            }

            result = String.Join(" - ", normalizedAmpsAtFreq);

            int MAX_INTENSITY = 500;
            double intensityPerPixel = MAX_INTENSITY / sectionMaxHeight;
            
            if (!_colorSet) {
                sectionColor = new RGBS[sections];
                for (int i = 0; i < sections; ++ i) sectionColor[i] = new RGBS((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256));
                _colorSet = true;
            } 
            for (int section = 0; section < sections; ++ section)
            {
                
                int sectionHeight = (int) (normalizedAmpsAtFreq[section] / intensityPerPixel) + 1;
                if(sectionHeight > sectionMaxHeight)
                {
                    sectionColor[section].SetColor((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256));         
                    sectionHeight = sectionMaxHeight;
                }
                int x_padding = section * sectionWidth;
                //Console.WriteLine("sectionHeight: " + sectionHeight);
                for (int x = 0; x < sectionWidth; ++ x) { 
                    for (int y = 0; y < sectionHeight; ++ y)
                        display.SetPixel(x_padding+x, (sectionMaxHeight-1)-y, sectionColor[section]);
                    for (int y = sectionHeight; y < sectionMaxHeight; ++ y)
                        display.SetPixel(x_padding + x, (sectionMaxHeight-1)-y, 255, 255, 255);
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
            Console.WriteLine(frequencySpacing);

            for (int i = 0, k = 0; i < ranges; ++ i)
            {
                int currentMax = frequencyRanges[i];
                double runningSum = 0;

                while (k*frequencySpacing < currentMax && k < bufSize) //add up loudness of each frequency data point in specified data range
                    runningSum += fft[k++];

                int average = (int)(runningSum / (currentMax / frequencySpacing));
                avgAmplitudesAtFrequencyRanges[i] = average;   
            }
            return avgAmplitudesAtFrequencyRanges;
        }

        public String getResult()
        {
            return result;
        }

        public VisualizerDisplay getDisplay()
        {
            return display;
        }

        public double[] FFT(double[] data) //taken from Scott W. Harden's github
        {
            double[] fft = new double[data.Length]; // this is where we will store the output (fft)
            Complex[] fftComplex = new Complex[data.Length]; // the FFT function requires complex format
            for (int i = 0; i < data.Length; ++ i)
            {
                fftComplex[i] = new Complex(data[i], 0.0); // make it complex format (imaginary = 0)
            }
            Accord.Math.FourierTransform.FFT(fftComplex, Accord.Math.FourierTransform.Direction.Forward);
            for (int i = 0; i < data.Length; ++ i)
            {
                fft[i] = fftComplex[i].Magnitude; // back to double
                //fft[i] = Math.Log10(fft[i]); // convert to dB
            }
            return fft;
        }

    }

}