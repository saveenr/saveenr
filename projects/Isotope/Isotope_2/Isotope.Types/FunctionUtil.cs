using System.Collections.Generic;

namespace Isotope.Types
{
    public static class FunctionUtil
    {
        //http://diditwith.net/PermaLink,guid,7191176b-c72a-49db-986e-466845665fa1.aspx

        public static System.Func<TArg, TResult> Memoize<TArg, TResult>(System.Func<TArg, TResult> function)
        {
            var results = new Dictionary<TArg, TResult>();

            return delegate(TArg key)
            {
                TResult value;
                if (results.TryGetValue(key, out value))
                {
                    return value;
                }

                value = function(key);
                results.Add(key, value);
                return value;
            };
        }
    }
}