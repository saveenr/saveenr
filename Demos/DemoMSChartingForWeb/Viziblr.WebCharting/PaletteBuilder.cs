using System.Collections.Generic;

namespace Viziblr.WebCharting
{
    public static class PaletteBuilder
    {

        public static Palette GetDefaultPaletteOld()
        {
            var pal = new Palette();
            pal.AddARGB(0xff4572a7);
            pal.AddARGB(0xffc0504d);
            pal.AddARGB(0xff9bbb28);
            pal.AddARGB(0xff8064a2);
            pal.AddARGB(0xff4bacc6);
            pal.AddARGB(0xfff79646);
            pal.AddARGB(0xfffdd600);
            pal.AddARGB(0xff2185ff);
            pal.AddARGB(0xffce3b37);
            pal.AddARGB(0xff6bbd46);
            pal.AddARGB(0xff834ec5);
            pal.AddARGB(0xff21c1ed);
            pal.AddARGB(0xfffa6d10);
            pal.AddARGB(0xfffff114);
            pal.AddARGB(0xff9db3d9);
            pal.AddARGB(0xffdb9d9d);
            pal.AddARGB(0xffc2d7a1);
            pal.AddARGB(0xffb4a7c8);
            return pal;
        }

        public static Palette GetDefaultPalette()
        {
            var swatches = new Swatches(); 
            var pal = new Palette();
            pal.Add(swatches.FadedDarkBlue);
            pal.Add(swatches.BrightOrange);
            pal.Add(swatches.SimpleGreen);
            pal.Add(swatches.PurpleByzantium);
            pal.Add(swatches.Jonquil);
            pal.Add(swatches.FireEngineRed);
            pal.Add(swatches.LightGray);
            pal.Add(swatches.DeepBlue);
            pal.Add(swatches.DarkGray);
            pal.Add(swatches.ForestGreen);
            pal.Add(swatches.Carmine);
            pal.Add(swatches.BrightPink);
            pal.Add(swatches.Eggplant);
            pal.Add(swatches.Byzantine);
            pal.Add(swatches.JungleGreen);
            pal.Add(swatches.Black);
            pal.Add(swatches.Jonquil);
            pal.Add(swatches.Chamoisee);

            return pal;
        }

        public static Palette GetPaletteFromRange(uint startcolor, int steps)
        {
            var c0 = new Viziblr.Colorspace.ColorHSL(new Viziblr.Colorspace.ColorRGB(startcolor));
            var c1 = new Viziblr.Colorspace.ColorHSL(new Viziblr.Colorspace.ColorRGB(0xff000000));

            var pal = new Palette();
            foreach (double x in RangeSteps(c0.L, c1.L, steps))
            {
                var cx = new Viziblr.Colorspace.ColorHSL(c0.H, c0.S, x);
                var cx_rgb = new Viziblr.Colorspace.ColorRGB32Bit(cx);
                pal.AddARGB(cx_rgb.ToUInt());
            }
            return pal;
        }

        /// <summary>
        /// Given a range (start,end) and a number of steps, will yield that a number for each step
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="steps"></param>
        /// <returns></returns>
        private static IEnumerable<double> RangeSteps(double start, double end, int steps)
        {
            // for non-positive number of steps, yield no points
            if (steps < 1)
            {
                yield break;
            }

            // for exactly 1 step, yield the start value
            if (steps == 1)
            {
                yield return start;
                yield break;
            }

            // for exactly 2 stesp, yield the start value, and then the end value
            if (steps == 2)
            {
                yield return start;
                yield return end;
                yield break;
            }

            // for 3 steps or above, start yielding the segments
            // notice that the start and end values are explicitly returned so that there
            // is no possibility of rounding error affecting their values
            int segments = steps - 1;
            double total_length = end - start;
            double stepsize = total_length / segments;
            yield return start;
            for (int i = 1; i < (steps - 1); i++)
            {
                double p = start + (stepsize * i);
                yield return p;
            }
            yield return end;
        }

    }
}