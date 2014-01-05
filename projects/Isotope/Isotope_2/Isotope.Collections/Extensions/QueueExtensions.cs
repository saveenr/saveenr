using System.Collections.Generic;

namespace Isotope.Collections.Extensions
{
    public static class QueueExtensions
    {
        public static void EnqueueRange<T>(this Queue<T> q, IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                q.Enqueue(item);
            }  
        }
    }
}