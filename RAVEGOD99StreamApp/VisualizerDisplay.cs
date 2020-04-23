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

        public void SetColor(Int32 ARGB, double strength = 1)
        {
            byte[] argb_bytes = BitConverter.GetBytes(ARGB);
            this.r = argb_bytes[2];
            this.g = argb_bytes[1];
            this.b = argb_bytes[0];
            this.strength = strength;
        }

        public static Int32 HSVtoARGB(double h, double s, double v) //needs strict bounds checking
        {
            Int32 ARGB = 0;

            h = h % 360;
            s = s > 1.0 ? 1 : s < 0 ? 0 : s;
            v = v > 1.0 ? 1 : v < 0 ? 0 : v;

            double C = s * v;
            int h60 = (int) (h / 60);
            double X = C * (1 - Math.Abs( (h60 % 2) - 1));

            double r1=0, g1=0, b1=0;
            if(h60 >=0 && h60 <= 1)
            {
                r1 = C; g1 = X; b1 = 0;
            }
            else if(h60 > 1 && h60 <= 2)
            {
                r1 = X; g1 = C; b1 = 0;
            }
            else if (h60 > 2 && h60 <= 3)
            {
                r1 = 0; g1 = C; b1 = X;
            }
            else if (h60 > 3 && h60 <= 4)
            {
                r1 = 0; g1 = X; b1 = C;
            }
            else if (h60 > 4 && h60 <= 5)
            {
                r1 = X; g1 = 0; b1 = C;
            }
            else if (h60 > 5 && h60 <= 6)
            {
                r1 = C; g1 = 0; b1 = X;
            }

            double m = v - C;

            double r2 = (r1 + m);
            double g2 = (g1 + m);
            double b2 = (b1 + m);

            int r = (int) (r2 * 255);
            int g = (int) (g2 * 255);
            int b = (int) (b2 * 255);

            ARGB = (255 << 24 | r << 16 | g << 8 | b);
            

            return ARGB;
        }

        public Int32 ToARGB()
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

        public RGBS(Int32 ARGB, double strength = 1)
        {
            byte[] argb_bytes = BitConverter.GetBytes(ARGB);
            this.r = argb_bytes[2];
            this.g = argb_bytes[1];
            this.b = argb_bytes[0];
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

        public Int32 ToARGB()
        {
            return rgbs.ToARGB();
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
