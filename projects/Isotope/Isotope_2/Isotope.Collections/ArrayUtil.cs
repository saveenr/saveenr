using System.Collections.Generic;

namespace Isotope.Collections
{
    public static class ArrayUtil
    {
        /// <summary>
        /// Places elements from an enumerable into an array. If there are not enough items to fill the array an exception is thrown
        ///  </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="items"></param>
        public static void FillArray<T>(T[] array, IEnumerable<T> items)
        {
            if (array == null)
            {
                throw new System.ArgumentNullException("array");
            }

            if (items == null)
            {
                throw new System.ArgumentNullException("items");
            }

            _FillArray(array, items, () => { throw new System.ArgumentException("Not enough items to fill array", "items"); });
        }

        /// <summary>
        /// Places elements from an enumerable into an array. If there are not enough items to fill the array, the default value is used
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="items"></param>
        /// <param name="default_value"></param>
        public static void FillArray<T>(T[] array, IEnumerable<T> items, T default_value)
        {
            if (array == null)
            {
                throw new System.ArgumentNullException("array");
            }

            if (items == null)
            {
                throw new System.ArgumentNullException("items");
            }

            _FillArray(array, items, () => default_value);
        }

        private static void _FillArray<T>(T[] array, IEnumerable<T> items, System.Func<T> func_default)
        {
            if (array == null)
            {
                throw new System.ArgumentNullException("array");
            }

            if (items == null)
            {
                throw new System.ArgumentNullException("items");
            }

            if (func_default == null)
            {
                throw new System.ArgumentNullException("func_default");
            }

            using (var e = items.GetEnumerator())
            {
                for (int i = 0; i < array.Length; i++)
                {
                    bool move_ok = e.MoveNext();
                    if (move_ok)
                    {
                        array[i] = e.Current;
                    }
                    else
                    {
                        array[i] = func_default();
                    }
                }
            }
        }
    }
}