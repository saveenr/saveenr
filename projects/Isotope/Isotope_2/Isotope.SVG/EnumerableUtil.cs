using System.Collections.Generic;

namespace Isotope.SVG
{
    internal static class EnumerableUtil
    {
        public static IEnumerable<Pair<T, T>> SelectPairs<T>(IEnumerable<T> values)
        {
            if (values == null)
            {
                throw new System.ArgumentNullException("values");
            }

            int count = 0;
            T even_value = default(T);
            foreach (var value in values)
            {
                if ((count%2) == 0)
                {
                    even_value = value;
                }
                else
                {
                    yield return new Pair<T, T>(even_value, value);
                }
                count++;
            }
        }
    }
}