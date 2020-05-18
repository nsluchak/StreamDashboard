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

        SoundProcessor soundProcessor;
        VisualizerDisplay display;
        public VisualizerDisplay ledDisplay; //DELETE LATER

        Random rand = new Random();
        RGBS[] sectionColor;

        public VisualizerController(int w, int h)
        {
            display = new VisualizerDisplay(w, h);
            ledDisplay = new VisualizerDisplay(300, 1); //DELETE LATER
            soundProcessor = new SoundProcessor();

            int frequencies = Dashboard.WorkingProfile.SoundProcessorProfile.frequencyRanges.Length;
            sectionColor = new RGBS[frequencies];
            for (int i = 0; i < frequencies; ++i) sectionColor[i] = new RGBS(Dashboard.WorkingProfile.ColorProfile.ColumnColors[0].ToARGB());

        }

        public void ParseData(Tuple<SoundSettings, BufferedWaveProvider> data)
        {
            Dashboard.WorkingProfile.SoundProfile = data.Item1;
            if(soundProcessor.Format(data.Item2)) //if there is data to visualize
                Visualize();
        }

        public void LightUpStrip(RGBS color) //DELETE LATER
        {
            for(int i = 0; i < 300; ++ i)
            {
                ledDisplay.SetPixel(i, 0, color);
            }
        }

        private void Visualize()
        {
            Profile profile = Dashboard.WorkingProfile;

            bool _isBeat = soundProcessor.isBeatPresent(profile.SoundProcessorProfile.listeningThreshold);
            bool _useDB = profile.SoundProcessorProfile._useDB;

            int[] ampsAtFreq = soundProcessor.ThresholdedAvgEnergyByFrequencyRange(profile.SoundProcessorProfile.frequencyRanges,
                profile.SoundProcessorProfile.listeningThreshold, _useDB);

            int sections = ampsAtFreq.Length;
            int sectionWidth = display.width / sections;
            int sectionMaxHeight = display.height;


            double MAX_INTENSITY = profile.VisualizerProfile.visualizerCeiling ;
            int ACTIVATION_THRESHOLD = profile.VisualizerProfile.activationThreshold;

            double intensityPerPixel = MAX_INTENSITY / sectionMaxHeight;

            bool lightstrip = false; //DELETE LATER
            double weight = 0;

            for (int section = 0; section < sections; ++section)
            {

                int amps = ampsAtFreq[section] ;
                amps = amps >= 0 ? amps : 0;

                int sectionHeight = (int)(amps / intensityPerPixel) + 1;
                sectionHeight = sectionHeight > sectionMaxHeight ? sectionMaxHeight : sectionHeight;

                if (profile.VisualizerProfile._reactToBeat)
                {
                    if (_isBeat) display.brightness = 1;
                    else display.brightness = 0.3;
                }

                if (amps > ACTIVATION_THRESHOLD)
                {
                    Int32 newARGB = 0;
                    if (profile.VisualizerProfile._grayscale)
                        newARGB = profile.ColorProfile.ActivationColors[0].ToGrayscale();
                    else
                        newARGB = profile.ColorProfile.ActivationColors[0].ToARGB();
                    sectionColor[section].SetColor(newARGB);

                    //if (section == 1)
                    //{ //DELETE LATER
                        lightstrip = true;
                        weight = (amps - ACTIVATION_THRESHOLD) / (MAX_INTENSITY - ACTIVATION_THRESHOLD) > 1 ? 1 : (amps - ACTIVATION_THRESHOLD) / (MAX_INTENSITY - ACTIVATION_THRESHOLD);
                    //}
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


                if(lightstrip) //DELETE LATER
                {
                    LightUpStrip(new RGBS((byte) (rand.NextDouble() * 255), (byte) (rand.NextDouble() * 255), (byte) (rand.NextDouble() * 255)));
                } else
                {
                    LightUpStrip(new RGBS(0, 0, 0));
                }
            }

        }

        public VisualizerDisplay getDisplay()
        {
            return display;
        }      
    }

}
