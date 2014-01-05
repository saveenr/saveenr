namespace Isotope.Math
{
    public static class MathUtil
    {
        /// <summary>
        /// Combines two input numbers in some proportion. 
        /// ratio=0.0 the first number is not used at all, 
        /// ratio=0.5 they are weight equally
        /// ratio=1.0 the first number completely dominates the value
        /// </summary>
        /// <param name="val0"></param>
        /// <param name="val1"></param>
        /// <param name="ratio"></param>
        /// <returns></returns>
        public static double Blend_0_1(double val0, double val1, double ratio)
        {
            var cratio = ClampToRange_0_1(ratio);
            var v0 = val0*cratio;
            var v1 = val1*(1.0 - cratio);
            return v0 + v1;
        }

        /// <summary>
        /// Checks if a value is in a range (inclusive)
        /// </summary>
        /// <param name="val"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool IsInRange(double val, double min, double max)
        {
            return ((min <= val) && (val <= max));
        }

        /// <summary>
        /// Checks if a value is in the range 0.0 to 1.0 inclusive
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsInRange_0_1(double val)
        {
            return IsInRange(val, 0.0, 1.0);
        }

        /// <summary>
        /// Given an input value will force the value to fit within the range (min,max) inclusive
        /// </summary>
        /// <param name="v"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static double ClampToRange(double v, double min, double max)
        {
            if (v < min)
            {
                v = min;
            }
            else if (v > max)
            {
                v = max;
            }
            return v;
        }

        /// <summary>
        /// Given an input value, will limit it to the range 0.0 and 1.0 inclusive
        /// </summary>
        /// <param name="v"></param>
        /// <returns>the clamped value</returns>
        public static double ClampToRange_0_1(double v)
        {
            return ClampToRange(v, 0.0, 1.0);
        }

        /// <summary>
        /// This is a variant of mod that wraps the mod result to avoid negative results. this is what Python's mod operator does
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static double mod_wrap_angle(double x, double y)
        {
            if (y == 0)
            {
                throw new System.DivideByZeroException();
            }

            double r = x%y;
            if (r > 0 && y < 0)
            {
                r = r + y;
            }
            else if (r < 0 && y > 0)
            {
                r = r + y;
            }
            return r;
        }

        /// <summary>
        /// wraps a number around so that it always fits between 0.0 and 1.0. negative numbers will wrap around to the correct positive number
        /// </summary>
        /// <remarks>
        /// if the input number is already in the range, no change will occur
        /// </remarks>
        /// <param name="v">input value </param>
        /// <returns>the wrapped number</returns>
        public static double WrapAngle_0_1(double v)
        {
            const double min = 0.0;
            const double max = 1.0;
            if (IsInRange(v, min, max))
            {
                // the number is already in the range so do nothing
                return v;
            }
            return mod_wrap_angle(v, max);
        }

        /// <summary>
        /// Returns the maximum value of three numbers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static double Max(double a, double b, double c)
        {
            return (System.Math.Max(System.Math.Max(a, b), c));
        }

        /// <summary>
        /// Returns the minimum value of three numbers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static double Min(double a, double b, double c)
        {
            return (System.Math.Min(System.Math.Min(a, b), c));
        }

        public static double Round(double val, double snap_val)
        {
            return Round(val, System.MidpointRounding.AwayFromZero, snap_val);
        }

        /// <summary>
        /// rounds val to the nearest fractional value 
        /// </summary>
        /// <param name="val">the value tp round</param>
        /// <param name="rounding">what kind of rounding</param>
        /// <param name="frac"> round to this value (must be greater than 0.0)</param>
        /// <returns>the rounded value</returns>
        public static double Round(double val, System.MidpointRounding rounding, double frac)
        {
            /*
            if (frac <= 0)
            {
                throw new ArgumentOutOfRangeException("frac","must be greater than or equal to 0.0");
            }*/
            double retval = System.Math.Round((val/frac), rounding)*frac;
            return retval;
        }

        public static double RoundUp(double v, double amount)
        {
            const System.MidpointRounding rounding = System.MidpointRounding.ToEven;
            var result = Round(v + (amount/2.0), rounding, amount);
            return result;
        }
    }
}