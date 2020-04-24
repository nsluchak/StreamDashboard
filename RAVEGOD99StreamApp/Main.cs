using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StreamApp
{
    public partial class Dashboard : Form
    {

        SoundInputHandler soundInputHandler;
        VisualizerController visualizerController;

        public Dashboard()
        {
            InitializeComponent();

            String[] deviceNames = SoundInputHandler.GetAvailableDevices();
            AudioInputSelector.Items.AddRange(deviceNames);
            AudioInputSelector.SelectedIndex = 0;

            visualizerController = new VisualizerController(LEDProjector.Width, LEDProjector.Height);
            

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
        }

        private void ListenButton_Click(object sender, EventArgs e)
        {
            soundInputHandler = new SoundInputHandler(AudioInputSelector.SelectedIndex);
            UpdateTimer.Enabled = true;
            
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateTimer.Enabled = false;
            /////////
            visualizerController.ParseData(soundInputHandler.GetWorkingData());

            VisualizerDisplay display = visualizerController.getDisplay();
            Bitmap led_projection = new Bitmap(display.width, display.height);
            DisplayPixel[,] pixels = display.getDisplay();

            for (int i = 0; i < pixels.GetLength(0); ++i)
                for(int j = 0; j < pixels.GetLength(1); ++j)
                 led_projection.SetPixel(pixels[i,j].COOR.Item1, pixels[i,j].COOR.Item2, Color.FromArgb(pixels[i,j].ToARGB()));

            LEDProjector.Image = led_projection;
            //////////
            UpdateTimer.Enabled = true;


        }

        private void useDBCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            visualizerController.profile.VisualizerProfile._useDB = useDBCheckbox.Checked;
        }

        private void ceilingInputBox_TextChanged(object sender, EventArgs e)
        {
            int newCeiling = 0;

            int MINIMUM_VALUE = 1;

            if (int.TryParse(ceilingInput.Text, out newCeiling))
            {
                if(newCeiling < MINIMUM_VALUE)
                {
                    newCeiling = MINIMUM_VALUE;
                    ceilingInput.Text = newCeiling.ToString();
                }
                visualizerController.profile.VisualizerProfile.visualizerCeiling = newCeiling;
            }
            else
                ceilingInput.Text = visualizerController.profile.VisualizerProfile.visualizerCeiling.ToString();
        }

        private void peakInputBox_TextChanged(object sender, EventArgs e)
        {
            int newThreshold = 0;

            if (int.TryParse(peakInput.Text, out newThreshold))
            {
                if (newThreshold < 0)
                {
                    newThreshold = 0;
                    listeningThresholdInput.Text = newThreshold.ToString();
                }
                visualizerController.profile.VisualizerProfile.activationThreshold = newThreshold;
            }
            else
                peakInput.Text = visualizerController.profile.VisualizerProfile.activationThreshold.ToString();
        }

        private void grayscaleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            visualizerController.profile.VisualizerProfile._grayscale = grayscaleCheckBox.Checked;
        }

        private void listeningThresholdInput_TextChanged(object sender, EventArgs e)
        {
            int newThreshold = 0;

            if (int.TryParse(listeningThresholdInput.Text, out newThreshold)) {

                if (newThreshold < 0)
                {
                    newThreshold = 0;
                    listeningThresholdInput.Text = newThreshold.ToString();
                }
                visualizerController.profile.VisualizerProfile.listeningThreshold = newThreshold;
            }
            else
                listeningThresholdInput.Text = visualizerController.profile.VisualizerProfile.listeningThreshold.ToString();
        }

        private void beatSensitivityInput_TextChanged(object sender, EventArgs e)
        {
            double newSensitivity = 0;

            if (double.TryParse(beatSensitivityInput.Text, out newSensitivity))
            {

                if (newSensitivity < 1)
                {
                    newSensitivity = 1;
                    beatSensitivityInput.Text = newSensitivity.ToString();
                }
                visualizerController.profile.VisualizerProfile.beatSensitivity = newSensitivity;
            }
            else
                beatSensitivityInput.Text = visualizerController.profile.VisualizerProfile.beatSensitivity.ToString();
        }
    }
}
