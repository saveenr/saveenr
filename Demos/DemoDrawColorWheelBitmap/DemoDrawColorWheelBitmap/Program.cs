using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoDrawColorWheelBitmap
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            draw2();
        }

        private static void draw2()
        {
            string output_filename = "D:\\colorwheel.png";

            int padding = 10;
            int inner_radius = 200;
            int outer_radius = inner_radius + 50;

            int bmp_width = (2 * outer_radius) + (2 * padding);
            int bmp_height = bmp_width;

            var center = new System.Drawing.Point(bmp_width / 2, bmp_height / 2);
            var c = System.Drawing.Color.Red;

            using (var bmp = new System.Drawing.Bitmap(bmp_width, bmp_height))
            {
                using (var g = System.Drawing.Graphics.FromImage(bmp))
                {
                    g.FillRectangle(System.Drawing.Brushes.White, 0, 0, bmp.Width, bmp.Height);
                }
                for (int y = 0; y < bmp_width; y++)
                {
                    int dy = (center.Y - y);

                    for (int x = 0; x < bmp_width; x++)
                    {
                        int dx = (center.X - x);

                        double dist = System.Math.Sqrt(dx * dx + dy * dy);


                        if (dist >= inner_radius && dist <= outer_radius)
                        {
                            double theta = System.Math.Atan2(dy, dx);
                            // theta can go from -pi to pi

                            double hue = (theta + System.Math.PI) / (2 * System.Math.PI);

                            // HSV -> RGB
                            const double sat = 1.0;
                            const double val = 1.0;
                            c = HSVToRGB2(hue, sat, val);

                            // HSL -> RGB
                            // const double sat = 1.0;
                            // const double light = 0.5;
                            // c = HSLToRGB2(hue, sat, light);
                           

                            bmp.SetPixel(x, y, c);
                        }
                    }
                }
                bmp.Save(output_filename);
            }
        }

        private static void draw3()
        {
            string output_filename = "D:\\colorwheel.png";

            int padding = 10;
            int inner_radius = 200;
            int outer_radius = inner_radius + 50;

            int bmp_width = (2 * outer_radius) + (2 * padding);
            int bmp_height = bmp_width;

            var center = new System.Drawing.Point(bmp_width / 2, bmp_height / 2);

            const double sat = 1.0;
            const double val = 1.0;

            using (var bmp = new System.Drawing.Bitmap(bmp_width, bmp_height))
            {
                using (var g = System.Drawing.Graphics.FromImage(bmp))
                {
                    g.FillRectangle(System.Drawing.Brushes.White, 0, 0, bmp.Width, bmp.Height);
                }

                for (int y = 0; y <= center.Y; y++)
                {
                    int dy = (center.Y - y);

                    for (int x = 0; x <= center.X; x++)
                    {
                        int dx = (center.X - x);

                        double dist = System.Math.Sqrt(dx * dx + dy * dy);

                        if (dist >= inner_radius && dist <= outer_radius)
                        {
                            double theta = System.Math.Atan2(dy, dx);
                            double hue1 = (theta + System.Math.PI) / (2 * System.Math.PI);

                            double hue2 = 1.0 - hue1 + 0.5;
                            double hue3 = hue1 - 0.5;
                            double hue4 = 1.0 - hue1;
                            bmp.SetPixel(center.X - dx, center.Y - dy, HSVToRGB2(hue1, sat, val));
                            bmp.SetPixel(center.X + dx, center.Y - dy, HSVToRGB2(hue2, sat, val));
                            bmp.SetPixel(center.X + dx, center.Y + dy, HSVToRGB2(hue3, sat, val));
                            bmp.SetPixel(center.X - dx, center.Y + dy, HSVToRGB2(hue4, sat, val));
                        }
                    }
                }
                bmp.Save(output_filename);
            }
        }


        public static void HSVToRGB(double H, double S, double V, out double R, out double G, out double B)
        {
            if (H == 1.0)
            {
                H = 0.0;
            }

            double step = 1.0 / 6.0;
            double vh = H / step;

            int i = (int)System.Math.Floor(vh);

            double f = vh - i;
            double p = V * (1.0 - S);
            double q = V * (1.0 - (S * f));
            double t = V * (1.0 - (S * (1.0 - f)));

            switch (i)
            {
                case 0:
                    {
                        R = V;
                        G = t;
                        B = p;
                        break;
                    }
                case 1:
                    {
                        R = q;
                        G = V;
                        B = p;
                        break;
                    }
                case 2:
                    {
                        R = p;
                        G = V;
                        B = t;
                        break;
                    }
                case 3:
                    {
                        R = p;
                        G = q;
                        B = V;
                        break;
                    }
                case 4:
                    {
                        R = t;
                        G = p;
                        B = V;
                        break;
                    }
                case 5:
                    {
                        R = V;
                        G = p;
                        B = q;
                        break;
                    }
                default:
                    {
                        // not possible - if we get here it is an internal error
                        throw new ArgumentException();
                        R = V;
                        G = t;
                        B = p;
                        break;
                    }
            }
        }

        public static System.Drawing.Color HSVToRGB2(double H, double S, double V)
        {
            double dr, dg, db;
            HSVToRGB(H, S, V, out dr, out dg, out db);
            var c = System.Drawing.Color.FromArgb((int)(dr * 255), (int)(dg * 255), (int)(db * 255));
            return c;

        }

        public static System.Drawing.Color HSLToRGB2(double H, double S, double L)
        {
            double dr, dg, db;
            HSLToRGB(H, S, L, out dr, out dg, out db);
            var c = System.Drawing.Color.FromArgb((int)(dr * 255), (int)(dg * 255), (int)(db * 255));
            return c;

        }


        public static void HSLToRGB(double H, double S, double L, out double r, out double g, out double b)
        {
            if (double.IsNaN(H) || S == 0) //HSL values = From 0 to 1
            {
                r = L; //RGB results = From 0 to 255
                g = L;
                b = L;
                return;
            }

            double m2 = (L < 0.5) ? L * (1.0 + S) : (L + S) - (S * L);
            double m1 = (2.0 * L) - m2;
            const double onethird = (1.0 / 3.0);
            r = 1.0 * hue_2_rgb(m1, m2, H + onethird);
            g = 1.0 * hue_2_rgb(m1, m2, H);
            b = 1.0 * hue_2_rgb(m1, m2, H - onethird);
        }

        private static double hue_2_rgb(double m1, double m2, double h)
        {
            h = norm_hue(h);

            if ((6.0 * h) < 1.0)
            {
                return (m1 + (m2 - m1) * 6.0 * h);
            }

            if ((2.0 * h) < 1.0)
            {
                return m2;
            }

            if ((3.0 * h) < 2.0)
            {
                return m1 + (m2 - m1) * ((2.0 / 3.0) - h) * 6.0;
            }

            return m1;
        }

        public static double norm_hue(double h)
        {
            if (h < 0)
            {
                return h + 1.0;
            }

            if (h > 1)
            {
                return h - 1.0;
            }

            return h;
        }

    }
}