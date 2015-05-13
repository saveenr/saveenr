using SSRSCommon.Extensions;
using SD = System.Drawing;

namespace SSRSCommon
{
    public static class GraphicsUtil
    {
        public static double GetFitToAreaScalingFactor(SD.Size input, SD.Size max, bool only_scale_down)
        {
            if (only_scale_down &&  input.FitsInside(max))
            {
                return 1.0;
            }

            return GetFitToAreaScalingFactor(input.Width, input.Height, max.Width, max.Height);

        }

        public static double GetFitToAreaScalingFactor(SD.SizeF input, SD.SizeF max, bool only_scale_down)
        {
            if (only_scale_down &&  input.FitsInside(max))
            {
                return 1.0;
            }

            return GetFitToAreaScalingFactor(input.Width, input.Height, max.Width, max.Height);

        }

        public static double GetFitToAreaScalingFactor(SD.Size input, SD.Size max)
        {
            return GetFitToAreaScalingFactor(input.Width, input.Height, max.Width, max.Height);
        }

        public static double GetFitToAreaScalingFactor(SD.SizeF input, SD.SizeF max)
        {
            return GetFitToAreaScalingFactor(input.Width, input.Height, max.Width, max.Height);
        }

        public static double GetFitToAreaScalingFactor(double inputwidth, double inputheight, double maxwidth, double maxheight)
        {
            double input_aspect_ratio = inputheight / inputwidth;
            double bounding_apect_ratio = maxheight / maxwidth;
            double scaling_factor;

            if (input_aspect_ratio <= bounding_apect_ratio)
            {
                scaling_factor = maxwidth / inputwidth;
            }
            else
            {
                scaling_factor = maxheight / inputheight;
            }
            return scaling_factor;
        }

        public static SD.SizeF ResizeDownToFit(SD.SizeF original, SD.SizeF max)
        {
            if (original.FitsInside(max))
            {
                return original;
            }
            else
            {
                double scale = GetFitToAreaScalingFactor(original, max);
                return original.MultiplyBy((float) scale);
            }
        }
    }
}