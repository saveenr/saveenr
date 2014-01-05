using System.Collections.Generic;
using System.Linq;

namespace Isotope.Collections
{
    public static partial class EnumerableUtil
    {
        public static IEnumerable<Z> Zip<A, B, Z>(IEnumerable<A> a_items, IEnumerable<B> b_items, System.Func<A, B, Z> func)
        {
            using (var iter1 = a_items.GetEnumerator())
            using (var iter2 = b_items.GetEnumerator())
            {
                while (iter1.MoveNext() && iter2.MoveNext())
                {
                    yield return func(iter1.Current, iter2.Current);
                }
            }
        }

        public static IEnumerable<Z> Zip<A, B, C, Z>(IEnumerable<A> a_items, IEnumerable<B> b_items,
                                                     IEnumerable<C> c_items, System.Func<A, B, C, Z> func)
        {
            using (var iter1 = a_items.GetEnumerator())
            using (var iter2 = b_items.GetEnumerator())
            using (var iter3 = c_items.GetEnumerator())
            {
                while (iter1.MoveNext() && iter2.MoveNext() && iter3.MoveNext())
                {
                    yield return func(iter1.Current, iter2.Current, iter3.Current);
                }
            }
        }

        public static IEnumerable<Z> Zip<A, B, C, D, Z>(IEnumerable<A> a_items, IEnumerable<B> b_items,
                                                        IEnumerable<C> c_items, IEnumerable<D> d_items,
                                                        System.Func<A, B, C, D, Z> func)
        {
            using (var iter1 = a_items.GetEnumerator())
            using (var iter2 = b_items.GetEnumerator())
            using (var iter3 = c_items.GetEnumerator())
            using (var iter4 = d_items.GetEnumerator())
            {
                while (iter1.MoveNext() && iter2.MoveNext() && iter3.MoveNext() && iter4.MoveNext())
                {
                    yield return func(iter1.Current, iter2.Current, iter3.Current, iter4.Current);
                }
            }
        }

        /// <summary>
        /// Given two collections: items_a and items_b where the collections are of different sizes
        /// this yields (a,b) pairs. 
        /// If the b items are exhausted before the a items , then the b items will be reused.
        /// If the a items are exchausted before the values, then the enumeration ends.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="items_a"></param>
        /// <param name="items_b"></param>
        /// <returns></returns>
        public static IEnumerable<Pair<A, B>> ZipRepeatSecond<A, B>(IList<A> items_a,
                                                                             IList<B> items_b)
        {
            int maxlen = items_a.Count;
            int num_items = items_b.Count;

            for (int x = 0; x < maxlen; x++)
            {
                int index0 = x;
                int index1 = x%num_items;
                var s0 = items_a[index0];
                var s1 = items_b[index1];
                yield return new Pair<A, B>(s0, s1);
            }
        }
    }
}