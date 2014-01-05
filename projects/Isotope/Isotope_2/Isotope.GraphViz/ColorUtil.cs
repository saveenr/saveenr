using System;
using System.Linq;

namespace Isotope.GraphViz
{
    internal static class ColorUtil
    {
        public static void GetARGBBytes(uint rgb, out byte a, out byte r, out byte g, out byte b)
        {
            a = (byte)((rgb & 0xff000000) >> 24);
            r = (byte)((rgb & 0x00ff0000) >> 16);
            g = (byte)((rgb & 0x0000ff00) >> 8);
            b = (byte)((rgb & 0x000000ff) >> 0);
        }

        public static void GetRGBBytes(uint rgb, out byte r, out byte g, out byte b)
        {
            byte a;
            GetARGBBytes(rgb, out a, out r, out g, out b);
        }

        public static string ToWebColorString(byte r, byte g, byte b)
        {
            return ToWebColorString(0xff, r, g, b);
        }

        public static string ToWebColorString(byte a, byte r, byte g, byte b)
        {
            if (a == 0xff)
            {
                const string format_string_rgb = "#{0:x2}{1:x2}{2:x2}";
                string color_string = string.Format(System.Globalization.CultureInfo.InvariantCulture, format_string_rgb, r, g, b);
                return color_string;
            }
            else
            {
                const string format_string_rgba = "#{0:x2}{1:x2}{2:x2}{3:x2}";
                string color_string = string.Format(System.Globalization.CultureInfo.InvariantCulture, format_string_rgba, a, r, g, b);
                return color_string;                                
            }
        }

        public static string ToWebColorString(uint argb)
        {
            byte a;
            byte r;
            byte b;
            byte g;
            GetARGBBytes(argb,out a, out r, out g, out b);
            return ToWebColorString(a,r, g, b);
        }
    }
}