using System.Linq;
using System.Collections.Generic;

namespace Isotope.UI.CommonControls
{
    internal static class XUtil
    {
        /// <summary>
        /// Given a range (start,end) and a number of steps, will yield that a number for each step
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="steps"></param>
        /// <returns></returns>
        public static IEnumerable<double> RangeSteps(double start, double end, int steps)
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

        public static void FillArray<T>(T[] array, IEnumerable<T> items)
        {
            if (array == null)
            {
                throw new System.ArgumentNullException("array");
            }

            if (items == null)
            {
                throw new System.ArgumentNullException("items");
            }

            _FillArray(array, items, () => { throw new System.ArgumentException("Not enough items to fill array", "items"); });
        }

        /// <summary>
        /// Places elements from an enumerable into an array. If there are not enough items to fill the array, the default value is used
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="items"></param>
        /// <param name="default_value"></param>
        public static void FillArray<T>(T[] array, IEnumerable<T> items, T default_value)
        {
            if (array == null)
            {
                throw new System.ArgumentNullException("array");
            }

            if (items == null)
            {
                throw new System.ArgumentNullException("items");
            }

            _FillArray(array, items, () => default_value);
        }

        private static void _FillArray<T>(T[] array, IEnumerable<T> items, System.Func<T> func_default)
        {
            if (array == null)
            {
                throw new System.ArgumentNullException("array");
            }

            if (items == null)
            {
                throw new System.ArgumentNullException("items");
            }

            if (func_default == null)
            {
                throw new System.ArgumentNullException("func_default");
            }

            using (var e = items.GetEnumerator())
            {
                for (int i = 0; i < array.Length; i++)
                {
                    bool move_ok = e.MoveNext();
                    if (move_ok)
                    {
                        array[i] = e.Current;
                    }
                    else
                    {
                        array[i] = func_default();
                    }
                }
            }
        }
    }
    internal static class UIUtil
    {
        public static System.Drawing.Bitmap create_hue_bitmap(int width, int height)
        {
            var bitmap = new System.Drawing.Bitmap(width, height);

            using (var gfx = System.Drawing.Graphics.FromImage(bitmap))
            {
                var colorblend = new System.Drawing.Drawing2D.ColorBlend();
                const int num_steps = 34;
                var range_steps = XUtil.RangeSteps(0.0, 1.0, num_steps);

                colorblend.Colors = new System.Drawing.Color[num_steps];
                colorblend.Positions = new float[num_steps];

                double _sat = 1.0;
                double _val = 1.0;

                var colors = range_steps.Select(x => Isotope.UI.CommonControls.ColorUtil.HSVToSystemDrawingColor(x, _sat, _val));
                var positions = range_steps.Select(x => (float) x);

                XUtil.FillArray( colorblend.Colors, colors );
                XUtil.FillArray(colorblend.Positions, positions);

                using (var brush_rainbow = new System.Drawing.Drawing2D.LinearGradientBrush(
                    new System.Drawing.Point(0, 0), 
                    new System.Drawing.Point(bitmap.Width, 0),
                    System.Drawing.Color.Black,
                    System.Drawing.Color.White))
                {
                    brush_rainbow.InterpolationColors = colorblend;
                    gfx.FillRectangle(brush_rainbow, 0, 0, bitmap.Width, bitmap.Height);
                }
            }
            return bitmap;
        }

        public static System.Drawing.Bitmap create_hue_bitmap2(int width, int height)
        {
            var bitmap = new System.Drawing.Bitmap(width, height);

            using (var gfx = System.Drawing.Graphics.FromImage(bitmap))
            {
                for (int x = 0; x < width; x++)
                {
                    var h = x / (double)bitmap.Width;
                    double _sat = 1.0;
                    double _val = 1.0;
                    var c0 = Isotope.UI.CommonControls.ColorUtil.HSVToSystemDrawingColor(h, _sat, _val);
                    var rgb = 0xff << 24 | c0.R << 16 | c0.G << 8 | c0.B;
                    var c2 = System.Drawing.Color.FromArgb(rgb);
                    using (var p = new System.Drawing.Pen(c2))
                    {
                        gfx.DrawLine(p, x, 0, x, bitmap.Height);

                    }
                }
            }
            return bitmap;
        }

    }
}