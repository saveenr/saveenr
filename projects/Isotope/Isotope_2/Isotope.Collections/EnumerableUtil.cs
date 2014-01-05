using System.Collections.Generic;
using System.Linq;

namespace Isotope.Collections
{
    public static partial class EnumerableUtil
    {
        public static IEnumerable<T> Single<T>(T item)
        {
            yield return item;
        }
        
        /// <summary>
        /// Given an enumeration of returns them back as pairs
        /// </summary>
        /// <example>
        /// given input of (1,2,3,4,5,6,7,8)
        /// yields (1,2) (3,4), (5,6), (7,8)
        /// </example>
        /// <param name="values">int input values</param>
        /// <returns>an enumeration of coordinates</returns>
        public static IEnumerable<Pair<T,T>> SelectPairs<T>(IEnumerable<T> values)
        {
            if (values==null)
            {
                throw new System.ArgumentNullException("values");
            }

            int count = 0;
            T even_value = default(T);
            foreach (var value in values)
            {
                if ( (count % 2)==0 )
                {
                    even_value = value;
                }
                else
                {
                    yield return new Pair<T,T>(even_value, value);
                }
                count++;
            }
        }

        /// <summary>
        /// Given an enumeration of returns them back as overlapping pairs
        /// </summary>
        /// <example>
        /// given input of (1,2,3,4,5,6,7,8)
        /// yields (1,2) (2,3), (3,4), (4,5), (5,6) (6,7), (7,8)
        /// </example>
        /// <param name="values">int input values</param>
        /// <returns>an enumeration of coordinates</returns>
        public static IEnumerable<Pair<T, T>> SelectPairsOverlapped<T>(IEnumerable<T> values)
        {

            if (values == null)
            {
                throw new System.ArgumentNullException("values");
            }


            int count = 0;

            T first_value = default(T);
            foreach (var value in values)
            {
                if (count > 0)
                {
                    yield return new Pair<T, T>(first_value, value);
                }
                first_value = value;
                count++;
            }
        }


        /// <summary>
        /// Given a range (start,end) and a number of steps, will yield that a number for each step
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="steps"></param>
        /// <returns></returns>
        public static IEnumerable<double> RangeSteps(double start, double end, int steps)
        {
            // for non-positive number of steps, yield no points
            if (steps < 1)
            {
                yield break;
            }

            // for exactly 1 step, yield the start value
            if (steps == 1)
            {
                yield return start;
                yield break;
            }

            // for exactly 2 stesp, yield the start value, and then the end value
            if (steps == 2)
            {
                yield return start;
                yield return end;
                yield break;
            }

            // for 3 steps or above, start yielding the segments
            // notice that the start and end values are explicitly returned so that there
            // is no possibility of rounding error affecting their values
            int segments = steps - 1;
            double total_length = end - start;
            double stepsize = total_length / segments;
            yield return start;
            for (int i = 1; i < (steps-1); i++)
            {
                double p = start + (stepsize*i);
                yield return p;
            }
            yield return end;
        }

        public static IEnumerable<TGROUP> GroupByCount<T, TGROUP>(
            IEnumerable<T> items, 
            int group_size,
            System.Func<int, TGROUP> func_new_col,
            System.Action<TGROUP, int, T> func_add) where TGROUP : class
        {


            if (items == null)
            {
                throw new System.ArgumentNullException("items");
            }

            if (group_size < 1 )
            {
                throw new System.ArgumentOutOfRangeException("group_size");
            }

            if (func_new_col == null)
            {
                throw new System.ArgumentNullException("func_new_col");
            }


            if (func_add == null)
            {
                throw new System.ArgumentNullException("func_add");
            }



            int cur_group_size = 0;
            TGROUP cur_group =null;
            foreach (var item in items)
            {
                if (cur_group_size==0)
                {
                    if (cur_group == null)
                    {
                        cur_group = func_new_col(group_size);
                    }
                    else
                    {
                        throw new System.InvalidOperationException();
                    }
                }

                func_add(cur_group, cur_group_size, item);
                cur_group_size++;
                if (cur_group_size==group_size)
                {

                    yield return cur_group;
                    cur_group = null;
                    cur_group_size = 0;
                }
                
            }

            if (cur_group_size>0)
            {
                if (cur_group==null)
                {
                    throw new System.InvalidOperationException();                  
                }
                yield return cur_group;
            }

        }

        public static IEnumerable<TGROUP> GroupByCount<T, TGROUP>(IList<T> items, IList<int> group_sizes, System.Func<int, TGROUP> func_new_col, System.Action<TGROUP, int, T> func_add)
        {


            if (items == null)
            {
                throw new System.ArgumentNullException("items");
            }

            if (group_sizes == null)
            {
                throw new System.ArgumentNullException("group_sizes");
            }

            if (func_new_col == null)
            {
                throw new System.ArgumentNullException("func_new_col");
            }


            if (func_add == null)
            {
                throw new System.ArgumentNullException("func_add");
            }



            int total_group_sizes = group_sizes.Sum();
            if (total_group_sizes != items.Count)
            {
                throw new System.ArgumentException("group_sizes must account for all items");
            }

            int items_added = 0;
            for (int group_index = 0; group_index < group_sizes.Count; group_index++)
            {
                int cur_group_size = group_sizes.ElementAt(group_index);

                if (cur_group_size < 0)
                {
                    throw new System.ArgumentException("group_sizes contains a negative numver");
                }
                var cur_group = func_new_col(cur_group_size);
                for (int row_index = 0; row_index < cur_group_size; row_index++)
                {
                    var cur_item = items[items_added];
                    func_add(cur_group, row_index, cur_item);
                    items_added++;
                }
                yield return cur_group;

            }

        }


        public static IDictionary<K, List<V>> Bucketize<K, V>(IEnumerable<V> items, System.Func<V, K> func_get_key, IEqualityComparer<K> ieq)
        {
            if (items == null)
            {
                throw new System.ArgumentNullException("items");
            }

            if (func_get_key == null)
            {
                throw new System.ArgumentNullException("func_get_key");
            }

            var dic = new Dictionary<K, List<V>>(ieq);
            foreach (var item in items)
            {
                var key = func_get_key(item);
                List<V> list = null;
                bool found = dic.TryGetValue(key, out list);
                if (!found)
                {
                    list = new List<V>();
                    dic[key] = list;
                }
                list.Add(item);

            }

            return dic;
        }


        public static IDictionary<K, List<V>> Bucketize<K, V>(IEnumerable<V> items, System.Func<V, K> func_get_key)
        {
            IEqualityComparer<K> ieq = null;
            return Bucketize<K, V>(items,func_get_key,ieq);
        }


        public static IDictionary<K, int> Histogram<K, V>(IEnumerable<V> items, System.Func<V, K> func_get_key, IEqualityComparer<K> ieq)
        {
            if (items == null)
            {
                throw new System.ArgumentNullException("items");
            }

            if (func_get_key == null)
            {
                throw new System.ArgumentNullException("func_get_key");
            }

            var dic = new Dictionary<K, int>(ieq);
            foreach (var item in items)
            {
                var key = func_get_key(item);
                int old_value = 0;
                bool found = dic.TryGetValue(key, out old_value);
                if (!found)
                {
                    dic[key] = 1;
                }
                else
                {
                    dic[key] = old_value + 1;
                }

            }

            return dic;
        }

        public static IDictionary<T, int> Histogram<T>(IEnumerable<T> items) 
        {
           var dic = Histogram(items, i => i, null);
           return dic;
        }

        public static List<List<T>> Chunkify<T>(IEnumerable<T> items, int chunksize)
        {
            var chunks = new List<List<T>>();
            List<T> cur_chunk = null;
            Chunkify(items, chunksize, 
                () =>
                    {
                        cur_chunk = new List<T>(chunksize); chunks.Add(cur_chunk);
                    }, 
                item =>
                    {
                        cur_chunk.Add(item);
                    });

            return chunks;
        }

        public static void Chunkify<T>(IEnumerable<T> items, int chunksize, System.Action create_chunk, System.Action<T> add_item)
        {
            if (items == null)
            {
                throw new System.ArgumentNullException("items");
            }

            if (chunksize < 1)
            {
                throw new System.ArgumentOutOfRangeException("chunksize");
            }

            int item_count = 0;
            int curchunk_size = 0;
            foreach (T item in items)
            {
                if ((item_count % chunksize) == 0)
                {
                    create_chunk();
                    curchunk_size = 0;
                }
                add_item(item);
                item_count++;
                curchunk_size++;
            }
 
        }
    }
}