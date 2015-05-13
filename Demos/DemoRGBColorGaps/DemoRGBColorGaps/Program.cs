using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoRGBColorGaps
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const int acc_size = 256 * 256 * 256;
            const string hdr = "h_steps,s_steps,l_steps,notfound,found,foundmult,foundsingle,miliseconds";
            string filename = "output_" + System.DateTime.Now.ToString("s").Replace(":", "-") + ".txt";

            var steps_col_a = Enumerable.Range(1,10).Select(i=>i*100); // hundreds 100, 200, ... 1000
            var steps_col_b = Enumerable.Range(64, 193); // 64 to 256
            var steps_col_c = new int[] { 256 }; // 

            var steps_col = steps_col_c;

            var stopwatch = new System.Diagnostics.Stopwatch();
            var acc = new Accumulator(acc_size);
            var fp = System.IO.File.CreateText(filename);

            Console.WriteLine(hdr);
            fp.WriteLine(hdr);
            
            foreach (int numsteps in steps_col)
            {
                acc.Clear();
                stopwatch.Reset();
                stopwatch.Start();

                // for each hsl value, calculate the 24bit rgb value
                // and then increment the corresponding index in the accumulator
                int h_steps = numsteps;
                int s_steps = numsteps;
                int l_steps = numsteps;

                fill_accumulator(acc,h_steps, s_steps, l_steps, ColorRGBBit.HSL_To_RGBInt );

                var stats = acc.GetStats();
                stopwatch.Stop();

                string msg = string.Format("{0},{1},{2}", h_steps, stopwatch.ElapsedMilliseconds,stats.ToCSV());
                Console.WriteLine(msg);
                fp.WriteLine(msg);
            }
           fp.Close();
        }


        static void fill_accumulator(Accumulator acc, int x_steps, int y_steps, int z_steps, System.Func<double,double,double,int> f)
        {
            foreach (var x in range(x_steps))
            {
                foreach (var y in range(y_steps))
                {
                    foreach (var z in range(z_steps))
                    {
                        int rgbint = f(x,y,z);
                        acc.Increment(rgbint);
                    }
                }
            }            
        }

        private static IEnumerable<double> range(int steps)
        {
            double hsl_max_div = (double)(steps - 1);
            return Enumerable.Range(0, steps).Select(i => ((double)i)/hsl_max_div);
        }
    }
}