using System.Collections.Generic;
using System.Linq;

namespace WebCharting.Data
{
    public class SeriesArray<T>
    {
        private T[] values;

        public SeriesArray(int capacity)
        {
            this.values = new T[capacity];
        }

        public SeriesArray(IEnumerable<T> items)
        {
            this.values = items.ToArray();
        }

        public T this[int index]
        {
            get { return this.values[index]; }
            set { this.values[index] = value; }
        }

        public int Length
        {
            get { return this.values.Length; }
        }

        public IEnumerable<T> Items
        {
            get { return this.values; }
        }

        public T[] Array
        {
            get { return this.values; }
        }
    }
}