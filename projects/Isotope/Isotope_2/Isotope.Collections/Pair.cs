using System.Collections.Generic;

namespace Isotope.Collections
{
    public struct Pair<T1, T2>
    {
        readonly public T1 Item1;
        readonly public T2 Item2;

        public Pair(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public IEnumerable<object> EnumItems()
        {
            yield return Item1;
            yield return Item2;
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", this.Item1, this.Item2);
        }
    }
}