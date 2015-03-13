using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            test_file_sizes();
            test_beers();
        }

        private static void test_file_sizes()
        {
            var units = new string[] {"", "KB", "MB", "GB", "TB", "PB", "EB", "ZB"};

            var p = new Prettyfier(1000.0, units);

            AssertEquals("-1", p.Prettify("-1"));
            AssertEquals("-1", p.Prettify("-1.001"));

            AssertEquals("10.3 TB", p.Prettify("10,328,109,349,120"));
            AssertEquals("0", p.Prettify("0.0"));
            AssertEquals("0", p.Prettify("0"));
            AssertEquals("1", p.Prettify("1"));
            AssertEquals("1", p.Prettify("1.001"));
            AssertEquals("1 KB", p.Prettify("1000"));
            AssertEquals("1.02 KB", p.Prettify("1024"));
            AssertEquals("974 KB", p.Prettify("973,600"));
            AssertEquals("1 MB", p.Prettify("1,000,000"));
            AssertEquals("1.05 MB", p.Prettify("1,048,576"));

                       AssertEquals("10.3 TB", p.Prettify("10,328,109,349,120"));
            AssertEquals("0", p.Prettify("-0.0"));
            AssertEquals("0", p.Prettify("-0"));
            AssertEquals("-1", p.Prettify("-1"));
            AssertEquals("-1", p.Prettify("-1.001"));
            AssertEquals("-1 KB", p.Prettify("-1000"));
            AssertEquals("-1.02 KB", p.Prettify("-1024"));
            AssertEquals("-974 KB", p.Prettify("-973,600"));
            AssertEquals("-1 MB", p.Prettify("-1,000,000"));
            AssertEquals("-1.05 MB", p.Prettify("-1,048,576"));
 
        }

        private static void test_beers()
        {
            var units = new string[] { "", "K", "M", "G", "T", "P", "E", "Z" };

            var p = new Prettyfier(1000.0, units);

            AssertEquals("10.3 T", p.Prettify("10,328,109,349,120"));
            AssertEquals("0", p.Prettify("0"));
            AssertEquals("1", p.Prettify("1"));
            AssertEquals("1 K", p.Prettify("1000"));
            AssertEquals("1.02 K", p.Prettify("1024"));
            AssertEquals("974 K", p.Prettify("973,600"));
            AssertEquals("1 M", p.Prettify("1,000,000"));
            AssertEquals("1.05 M", p.Prettify("1,048,576"));
        }


        public static void AssertEquals(string desired, string actual)
        {
            if (desired != actual)
            {
                throw new System.Exception(desired + " != " + actual);
            }
        }

    }


    public class Prettyfier
    {
        public double Base;
        public string[] Units;

        public Prettyfier(double basev, string [] units)
        {
            this.Base = basev;
            this.Units = units;
        }

        public string Prettify(string v)
        {
            v = v.Replace(",","");
            double d = double.Parse(v);
            return Prettify(d);
        }

        public string Prettify(double value)
        {
            // remember if the original value is negative
            bool neg = value < 0;
            if (neg)
            {
                value *= -1;
            }

            // Remove digits we don't care about
            double temp_value = RoundToSignificantDigits(value, 3);

            // Handle the 0 and 1 cases explicitly
            if (temp_value == 0)
            {
                return "0";                
            }
            
            if (temp_value == 1)
            {
                return neg ? "-1" : "1";
            }

            // Find the smallest log value for the value
            int logdown = (int)System.Math.Floor(System.Math.Log(temp_value, this.Base));

            // Now Scale down the number to the appropriate log
            double final_value = temp_value/(System.Math.Pow(this.Base,logdown));

            // adust back to negative if needed
            if (neg)
            {
                final_value *= -1;
            }

            int units_index = (int) logdown;

            string result_string = final_value.ToString("G3") + " " + this.Units[units_index];
            return result_string;
        }

        static double RoundToSignificantDigits(double d, int digits)
        {
            if (d == 0)
            {
                return 0;
            }

            double scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(d))) + 1);
            return scale * Math.Round(d / scale, digits);
        }
    }

    public class PrettyResult
    {
        public double OriginalValue;
        public double PrettyValue;
        public int LowerLog;

        public PrettyResult()
        {

        }
    }
}
