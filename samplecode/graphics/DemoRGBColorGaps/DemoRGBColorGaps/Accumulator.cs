using System.Collections.Generic;
using System.Linq;

namespace DemoRGBColorGaps
{
    public class Accumulator
    {
        private int[] arr;
        private int Cap;
        public Accumulator(int capacity)
        {
            this.Cap = capacity;
            this.arr = new int[capacity];
            
        }

        public void Clear()
        {
            // clear the accumulator
            for (int i = 0; i < this.Cap; i++)
            {
                this.arr[i] = 0;
            }            
        }

        public int this[int index]
        {
            get { return this.arr[index]; }
        }

        public void Increment(int index)
        {
            this.arr[index]++;
        }

        public IEnumerable<int> Values
        {
            get { return this.arr; }
        }
        public AccumulatorStats GetStats()
        {
            var stats = new AccumulatorStats();
            stats.NotFound = this.Values.Where(i => i == 0).Count();
            stats.Found = this.Values.Where(i => i >= 1).Count();
            stats.Multiple = this.Values.Where(i => i > 1).Count();
            stats.Exact = this.Values.Where(i => i == 1).Count();
            return stats;
        }
    }
}