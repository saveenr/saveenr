using System.Collections.Generic;

namespace Isotope.Text
{
    public static class TextUtil
    {
        /// <summary>
        /// Removes all instances of the specified characters from a string
        /// </summary>
        /// <param name="s"> the input string</param>
        /// <param name="chars_to_remove"></param>
        /// <returns></returns>
        public static string RemoveCharacters(string s, IList<char> chars_to_remove)
        {
            if (s == null)
            {
                throw new System.ArgumentNullException("s");
            }

            var sb = new System.Text.StringBuilder(s.Length);
            foreach (var c in s)
            {
                if (!chars_to_remove.Contains(c))
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        public static string Multiply(string s, int n)
        {
            if (s == null)
            {
                throw new System.ArgumentNullException("s");
            }

            if (n <= 0)
            {
                return string.Empty;
            }

            int total_len = s.Length * n;
            var sb = new System.Text.StringBuilder(total_len);
            for (int i = 0; i < n; i++)
            {
                sb.Append(s);
            }

            string result = sb.ToString();
            return result;
        }

        public static int GetLengthOfMatchingPrefix(string s1, string s2)
        {
            if (s1==null)
            {
                throw new System.ArgumentNullException("s1");
            }

            if (s2 == null)
            {
                throw new System.ArgumentNullException("s2");
            }

            int length = System.Math.Min(s1.Length, s2.Length);
            int prefix_length = 0;

            for (int i = 0; i < length; i++)
            {
                if (s1[i] != s2[i])
                {
                    break;
                }

                prefix_length++;
            }

            return prefix_length;
        }
    }
}

