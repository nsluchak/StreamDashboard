using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamApp
{
    internal struct RGBS
    {
        private byte r;
        public byte R
        {
            get { return r; }
            set { if (value < 0 || value > 255) throw new System.IndexOutOfRangeException("Value must be between 0 and 255"); else r = value; }
        }
        private byte g;
        public byte G
        {
            get { return g; }
            set { if (value < 0 || value > 255) throw new System.IndexOutOfRangeException("Value must be between 0 and 255"); else g = value; }
        }
        private byte b;
        public byte B
        {
            get { return b; }
            set { if (value < 0 || value > 255) throw new System.IndexOutOfRangeException("Value must be between 0 and 255"); else b = value; }
        }
        
        private double strength;
        public double STRENGTH
        {
            get { return strength; }
            set { if (value < 0 || value > 1) throw new System.IndexOutOfRangeException("Value must be between 0.0 and 1.0"); else strength = value; }
        }

        public void SetColor(byte r, byte g, byte b, double strength = 1)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.strength = strength;
        }

        public Int32 ToArgb()
        {
            byte a = 255; // 1111 1111
            Int32 ARGB = 0; // (0000 0000) (0000 0000) (0000 0000) (0000 0000)
            ARGB = (a << 24 | r << 16 | g << 8 | b); // 0000 0000 

            return ARGB;
        }

        public RGBS(byte r, byte g, byte b, double strength = 1)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.strength = strength;
        }
    }

    internal struct DisplayPixel {

        public RGBS rgbs;
        public Tuple<int, int> COOR { get; }

        public DisplayPixel(byte r, byte g, byte b, Tuple<int,int> coor, double strength = 1)
        {
            rgbs = new RGBS(r, g, b, strength);
            COOR = coor;         
        }

        public void SetColor(byte r, byte g, byte b, double strength = 1)
        {
            rgbs.SetColor(r, g, b, strength);
        }
        public void SetColor(RGBS rgbs)
        {
            this.rgbs = rgbs;
        }

        public Int32 ToArgb()
        {
            return rgbs.ToArgb();
        }
    }

    class VisualizerDisplay
    {
        DisplayPixel[,] display;
        public int width { get; set; }
        public int height { get; set; }

        public VisualizerDisplay(int w, int h)
        {
            width = w;
            height = h;
            display = new DisplayPixel[w,h];
            for (int i = 0; i < w; ++i)
                for (int j = 0; j < h; ++j)
                    display[i, j] = new DisplayPixel(0,0,0, new Tuple<int,int>(i,j));
        }

        public void SetPixel(int x, int y, byte r, byte g, byte b, int strength = 1)
        {
            //Console.WriteLine("x, y: " + x + ", " + y);
            
            display[x, y].SetColor(r, g, b, strength);
        }

        public void SetPixel(int x, int y, RGBS rgbs)
        {
            //Console.WriteLine("x, y: " + x + ", " + y);
            display[x, y].SetColor(rgbs);
        }

        public DisplayPixel[,] getDisplay()
        {
            return display;
        }

    }
}
