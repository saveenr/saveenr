using System.Collections.Generic;

namespace Isotope.Reporting.RDL2005
{
    public class NodeCollection<T> where T : Node
    {
        private List<T> items;

        public NodeCollection()
        {
            this.items = null;
        }


        public IEnumerable<T> Items()
        {
            if (this.items==null)
            {
                yield break;
            }
            else
            {
                foreach (var n in this.items)
                {
                    yield return n;
                }
                
            }

        }

        public int Count
        {
            get
            {
                return (this.items==null) ? 0 : this.items.Count;
            }
        }

        public void Add(T n)
        {
            if (this.items==null)
            {
                this.items = new List<T>();
            }
            this.items.Add(n);
        }

    }
}