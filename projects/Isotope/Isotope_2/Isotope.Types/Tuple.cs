using System.Collections.Generic;

namespace Isotope.Types
{
    public struct Tuple<T1, T2>
    {
        readonly public T1 Item1;
        readonly public T2 Item2;

        public Tuple(T1 item1, T2 item2)
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

    public struct Tuple<T1, T2, T3>
    {
        readonly public T1 Item1;
        readonly public T2 Item2;
        readonly public T3 Item3;

        public Tuple(T1 item1, T2 item2, T3 item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }

        public IEnumerable<object> EnumItems()
        {
            yield return Item1;
            yield return Item2;
            yield return Item3;
        }

        public override string ToString()
        {
            return string.Format("({0},{1},{2})", this.Item1, this.Item2, this.Item3);
        }
    }

    public struct Tuple<T1, T2, T3, T4>
    {
        readonly public T1 Item1;
        readonly public T2 Item2;
        readonly public T3 Item3;
        readonly public T4 Item4;

        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
        }

        public IEnumerable<object> EnumItems()
        {
            yield return Item1;
            yield return Item2;
            yield return Item3;
            yield return Item4;
        }

        public override string ToString()
        {
            return string.Format("({0},{1},{2},{3})", this.Item1, this.Item2, this.Item3, this.Item4);
        }
    }
}