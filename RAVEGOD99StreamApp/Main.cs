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
            ResultLabel.Text = visualizerController.getResult();

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
      }
}
