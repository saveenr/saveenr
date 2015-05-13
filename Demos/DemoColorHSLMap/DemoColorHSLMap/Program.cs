using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//http://www.geekymonkey.com/Programming/CSharp/RGB2HSL_HSL2RGB.htm

namespace DemoColorHSLMap
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            int[] map = get_hsv_map();

            int w = 256*16;
            int h = 256*16;

            //
            int num_zeros=0;
            int total=0;
            var fp = System.IO.File.CreateText("D:\\rgbinfo.txt");
            var fp2 = System.IO.File.CreateText("D:\\rgbinfo2.txt");
            var fp3 = System.IO.File.CreateText("D:\\rgbinfo3.txt");
            foreach (var i in Enumerable.Range(0, 256 * 256 * 256))
            {
                int v = map[i];
                if (v== 0)
                {
                    num_zeros++;
                }
                total += v;

                fp.WriteLine("{0:x6},{1}",i,v);
                if (v>0) {fp2.WriteLine("{0:x6},{1}", i, v);}
                if (v > 1) { fp3.WriteLine("{0:x6},{1}", i, v); }
            }
            fp3.Flush();
            fp3.Close();

            fp2.Flush();
            fp2.Close();
            fp.Flush();
            fp.Close();

            using (var bmp = new System.Drawing.Bitmap(w, h))
            {
                foreach (int box_col in Enumerable.Range(0, 16))
                {
                    foreach (int box_row in Enumerable.Range(0, 16))
                    {
                        foreach (int g in Enumerable.Range(0, 256))
                        {
                            foreach (int b in Enumerable.Range(0, 256))
                            {
                                int r = (box_row * 16) + box_col;
                                var c = System.Drawing.Color.FromArgb((byte)r, (byte)g, (byte)b);
                                
                                int x = (box_col*(256)) + g;
                                int y = (box_row*(256)) + b;
                                
                                var rgb = ((byte)r << 16) | ((byte)g << 8) | (byte)b;
                                if (map[rgb] > 0)
                                {
                                    bmp.SetPixel(x, y, c);                                   
                                }
                                else
                                {
                                    bmp.SetPixel(x,y,System.Drawing.Color.Black);
                                }
                                
                            }
                        }
                    }
                }
                bmp.Save("D:\\rgb.png");
            }
        }

        private static int[] get_hsv_map()
        {
            int [] map = new int[256*256*256];

            int steps = 128;
            float dn = (float)steps - 1;
            foreach (double _h in Enumerable.Range(0, steps).Select(i => i / dn))
            {
                foreach (double _s in Enumerable.Range(0, steps).Select(i => i / dn))
                {
                    foreach (double _v in Enumerable.Range(0, steps).Select(i => i / dn))
                    {
                        var rgb = HSL2RGB(_h, _s, _v);
                        var n = (rgb.R << 16) | (rgb.G << 8) | rgb.B;
                        map[n]++;
                    }

                }
                
            }
            return map;
        }


        private static void xMain(string[] args)
        {
            int w = 256*16;
            int h = 256*16;

            using (var bmp = new System.Drawing.Bitmap(w, h))
            {
                foreach (int rc in Enumerable.Range(0, 16))
                {
                    foreach (int rr in Enumerable.Range(0, 16))
                    {
                        int r = (rr*16) + rc;

                        foreach (int g in Enumerable.Range(0, 256))
                        {
                            foreach (int b in Enumerable.Range(0, 256))
                            {
                                var c = System.Drawing.Color.FromArgb((byte) r, (byte) g, (byte) b);
                                int x = (rc*(256)) + g;
                                int y = (rr*(256)) + b;

                                bmp.SetPixel(x, y, c);
                            }
                        }
                    }
                }
                bmp.Save("D:\\rgb.png");
            }
        }

        public struct ColorRGB
        {
            public byte R;
            public byte G;
            public byte B;

            public ColorRGB(System.Drawing.Color value)
            {
                this.R = value.R;
                this.G = value.G;
                this.B = value.B;
            }

            public static implicit operator System.Drawing.Color(ColorRGB rgb)
            {
                System.Drawing.Color c = System.Drawing.Color.FromArgb(rgb.R, rgb.G, rgb.B);
                return c;
            }

            public static explicit operator ColorRGB(System.Drawing.Color c)
            {
                return new ColorRGB(c);
            }
        }


        // Given H,S,L in range of 0-1
        // Returns a Color (RGB struct) in range of 0-255
        public static ColorRGB HSL2RGB(double h, double sl, double l)
        {
            double v;
            double r, g, b;
            
            r = l; // default to gray
            g = l;
            b = l;
            v = (l <= 0.5) ? (l*(1.0 + sl)) : (l + sl - l*sl);
            if (v > 0)
            {
                double m;
                double sv;
                int sextant;
                double fract, vsf, mid1, mid2;
                m = l + l - v;
                sv = (v - m)/v;
                h *= 6.0;
                sextant = (int) h;
                fract = h - sextant;
                vsf = v*sv*fract;
                mid1 = m + vsf;
                mid2 = v - vsf;
                switch (sextant)
                {
                    case 0:
                        r = v;
                        g = mid1;
                        b = m;
                        break;
                        
                    case 1:
                        r = mid2;
                        g = v;
                        b = m;
                        break;

                    case 2:
                        r = m;
                        g = v;
                        b = mid1;
                        break;

                    case 3:
                        r = m;
                        g = mid2;
                        b = v;
                        break;

                    case 4:
                        r = mid1;
                        g = m;
                        b = v;
                        break;

                    case 5:
                        r = v;
                        g = m;
                        b = mid2;
                        break;
                }
            }

            ColorRGB rgb;
            rgb.R = Convert.ToByte(r*255.0f);
            rgb.G = Convert.ToByte(g*255.0f);
            rgb.B = Convert.ToByte(b*255.0f);
            return rgb;
        }
    }
}