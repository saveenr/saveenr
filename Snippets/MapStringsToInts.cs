using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapStringsToInts
{
    public class MapStringsToIndex
    {
        private Dictionary<string, int> dic;

        public MapStringsToIndex()
        {
            this.dic = new Dictionary<string, int>();
        }

        public int this[string text]
        {
            get
            {
                int v;
                bool found = this.dic.TryGetValue(text, out v);

                if (found)
                {
                    return v;
                }

                v = this.dic.Count;
                this.dic[text] = v;
                return v;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var map = new MapStringsToIndex();
            int i0 = map["AAA"]; // stores AAA with index 0 
            int i1 = map["AAA"]; // remembers AAA and returns index 0 
            int i2 = map["BBB"]; // stored BBB with index 1 
        }
    }
}
