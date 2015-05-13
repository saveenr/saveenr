using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Viziblr.WebCharting.Data
{
    public class SeriesList<T> : IEnumerable<T>
    {
        private List<T> values;

        public SeriesList()
        {
            this.values = new List<T>();
        }

        public SeriesList(int capacity)
        {
            this.values = new List<T>(capacity);
        }

        public SeriesList(IEnumerable<T> items)
        {
            this.values = items.ToList();
        }

        public T this[int index]
        {
            get { return this.values[index]; }
            set { this.values[index] = value; }
        }

        public int Count
        {
            get { return this.values.Count; }
        }

        public T[] ToArray()
        {
            return this.values.ToArray();
        }

        public void Add(T item)
        {
            this.values.Add(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var i in this.values)
            {
                yield return i;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}