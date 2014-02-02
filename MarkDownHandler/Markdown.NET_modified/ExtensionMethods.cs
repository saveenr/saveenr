using System.Collections.Generic;

namespace MarkdownDotNET
{


    internal static class CollectionExtensions
    {
        public static string GetValue( this Dictionary<string,string> dic, string key, string defval)
        {
            string outval = null;
            if (dic.TryGetValue(key, out outval))
            {
                return outval;
            }
            return defval;
        }
    }
}