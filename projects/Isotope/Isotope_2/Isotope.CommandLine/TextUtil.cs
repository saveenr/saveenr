using System.Collections.Generic;
using System.Linq;

namespace Isotope.CommandLine
{
    internal static class TextUtil
    {
        public static string Join(this string sep, IEnumerable<string> tokens)
        {
            int num_tokens = tokens.Count();
            int num_seps = num_tokens >= 1 ? num_tokens - 1 : 0;

            int total_length = tokens.Select(t => t.Length).Sum() + (num_seps * sep.Length);

            var sb = new System.Text.StringBuilder(total_length);
            int token_index = 0;
            foreach (var token in tokens)
            {
                if (token_index > 0)
                {
                    sb.Append(sep);
                }
                sb.Append(token);
                token_index++;
            }

            string s = sb.ToString();
            if (s.Length != total_length)
            {
                throw new System.InvalidOperationException("Incorrectly calculated length");
            }
            return sb.ToString();
        }

    }
}