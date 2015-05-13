using System;

namespace DemoRGBColorGaps
{
    public struct ColorRGBBit
    {
        public byte R;
        public byte G;
        public byte B;

        public ColorRGBBit(byte r, byte g, byte b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        public int ToInt()
        {
            int rgbint = (R << 16) | (G << 8) | B;
            return rgbint;
        }


        // Given H,S,L in range of 0-1

        // Returns a Color (RGB struct) in range of 0-255

        public static ColorRGBBit HSL_To_RGB(double H, double S, double L)
        {
            double R, G, B;
            if (S == 0) //HSL from 0 to 1
            {
                R = L*255.0; //RGB results from 0 to 255
                G = L*255.0;
                B = L*255.0;
            }
            else
            {
                double var_2 = (L < 0.5) ? L*(1 + S) : (L + S) - (S*L);
                double var_1 = 2*L - var_2;

                R = 255*Hue_2_RGB(var_1, var_2, H + (1.0/3.0));
                G = 255*Hue_2_RGB(var_1, var_2, H);
                B = 255*Hue_2_RGB(var_1, var_2, H - (1.0/3.0));
            }

            var rgb = new ColorRGBBit(
                Convert.ToByte(R),
                Convert.ToByte(G),
                Convert.ToByte(B)
                );
            return rgb;
        }

        public static int HSL_To_RGBInt(double H, double S, double L)
        {
            var rgb = HSL_To_RGB(H, S, L);
            return rgb.ToInt();
        }

        private static double Hue_2_RGB(double v1, double v2, double vH) //Function Hue_2_RGB
        {
            if (vH < 0) vH += 1.0;
            if (vH > 1) vH -= 1.0;
            if ((6*vH) < 1) return (v1 + (v2 - v1)*6.0*vH);
            if ((2*vH) < 1) return (v2);
            if ((3*vH) < 2) return (v1 + (v2 - v1)*((2.0/3.0) - vH)*6.0);
            return (v1);
        }
    }
}