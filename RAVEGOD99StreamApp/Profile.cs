using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamApp
{
    public class Profile
    {
        public string ProfileName { get; set; } = "Default ";
        public RandomizerSettings RandomizerProfile { get; set; } = new RandomizerSettings();
        public SoundProcessorSettings SoundProcessorProfile { get; set; } = new SoundProcessorSettings();
        public VisualizerSettings VisualizerProfile { get; set; } = new VisualizerSettings();
        public SoundSettings SoundProfile { get; set; } = new SoundSettings();
        public ColorSettings ColorProfile { get; set; } = new ColorSettings();
        public NetworkSettings NetworkProfile { get; set; } = new NetworkSettings();

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

        public Profile() { } //default

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

        public SoundSettings() { } //default

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
            BackgroundColors[0] = new RGBS(255, 255, 0, .2);
            BackgroundColors[1] = new RGBS(255, 255, 0, 1);
            ColumnColors[0] = new RGBS(255, 0, 255);
            ActivationColors[0] = new RGBS(0, 255, 255);

            this.BackgroundColors = BackgroundColors;
            this.ColumnColors = ColumnColors;
            this.ActivationColors = ActivationColors;
        }

    }

    public class VisualizerSettings
    {
        public bool _grayscale { get; set; } = false;
        public bool _reactToBeat { get; set; } = false;
        public int visualizerCeiling { get; set; } = 2800;
        public int activationThreshold { get; set; } = 2000;

        public VisualizerSettings(bool _grayscale, bool _reactToBeat, int visualizerCeiling, int activationThreshold)
        {
            this._grayscale = _grayscale;
            this._reactToBeat = _reactToBeat;
            this.visualizerCeiling = visualizerCeiling;
            this.activationThreshold = activationThreshold;
        }

        public VisualizerSettings() { } //default

    }

    public class SoundProcessorSettings
    {
        public bool _useDB { get; set; } = false;
        public int listeningThreshold { get; set; } = 0;
        public int[] frequencyRanges { get; set; } = new int[] { 43, 86, 172, 258, 350, 450, 1225, 2000, 4000, 10000 };
        public int[] frequencySizes { get; set; } = new int[] { 43, 43, 86, 86, 92, 100, 775, 775, 2000, 6000 };
        public double beatSensitivity { get; set; } = 1.35;

        public SoundProcessorSettings(bool _useDB, int listeningThreshold, int[] frequencyRanges, int beatSensitivity)
        {
            this._useDB = _useDB;
            this.listeningThreshold = listeningThreshold;
            this.frequencyRanges = frequencyRanges;
            this.beatSensitivity = beatSensitivity;

            int[] frequencySizes = new int[frequencyRanges.Length];
            frequencySizes[0] = frequencyRanges[0];

            for (int i = 1; i < frequencyRanges.Length; ++i) frequencySizes[i] = frequencyRanges[i] - frequencyRanges[i - 1];

            this.frequencySizes = frequencySizes;
        }

        public SoundProcessorSettings() { } //default
    }

    public class NetworkSettings
    {
        public int LEDDataSize { get; } = 5;
        public string HOST_IP { get; } = "XXX.XXX.XXX.XXX";
        public int HOST_PORT { get; } = XXXX;
        public byte _KEY_ { get; } = XXX;
    }

}
