using System.Collections.Generic;
using System.Linq;

namespace Isotope.Types
{
    public static class EnumUtil
    {
        /// <summary>
        /// given an enum type will return a strongly typed array of all the values 
        /// </summary>
        /// <typeparam name="T">The enum type</typeparam>
        /// <returns>The array of typed values</returns>
        public static T[] GetTypedValues<T>()
        {

            var systemarray = System.Enum.GetValues(typeof (T));
            const int dimension = 0;
            int len = systemarray.GetLength(dimension);
            var typedarray = new T[len];
            for (int i = 0; i < len; i++)
            {
                typedarray[i] = (T) systemarray.GetValue(i);
            }
            return typedarray;

        }



        /// <summary>
        /// returns a map of name -> enum value for all the enum values in the type
        /// </summary>
        /// <typeparam name="T">the enum type</typeparam>
        /// <returns>the dictionary</returns>
        public static IDictionary<string, T> MapNamesToValues<T>()
        {
            var t_type = typeof (T);
            var names = System.Enum.GetNames(t_type);
            var dic = names.ToDictionary(
                name => name,
                name => (T) System.Enum.Parse(t_type, name));
            return dic;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <param name="ignorecase"></param>
        /// <returns></returns>
        public static T? TryParse<T>(string s, bool ignorecase) where T : struct
        {
            if (s == null)
            {
                return null;
            }
            try
            {
                T outval = Parse<T>(s, ignorecase);
                return outval;
            }
            catch (System.ArgumentException)
            {
                return null;
            }
        }

        /// <summary>
        /// Parse a string into an enum value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <param name="ignorecase"></param>
        /// <param name="defval"></param>
        /// <returns>the enum value</returns>
        public static T Parse<T>(string s, bool ignorecase, T defval) where T : struct
        {
            T? result = TryParse<T>(s, ignorecase);
            if (result.HasValue)
            {
                return result.Value;
            }
            else
            {
                return defval;                
            }
        }

        /// <summary>
        /// Parse a string into an enum value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <param name="ignorecase"></param>
        /// <returns>the enum value</returns>
        public static T Parse<T>(string s, bool ignorecase)
        {
            T outval = (T) System.Enum.Parse(typeof(T), s, ignorecase);
            return outval;
        }


    }
}