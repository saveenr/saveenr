using System.Collections.Generic;
using System.Linq;

namespace Isotope.CommandLine
{
    public static class EnumUtil
    {
        /// <summary>
        /// Parse a string into an enum value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <param name="ignorecase"></param>
        /// <returns>the enum value</returns>
        public static T Parse<T>(string s, bool ignorecase)
        {
            T outval = (T)System.Enum.Parse(typeof(T), s, ignorecase);
            return outval;
        }
    }
}