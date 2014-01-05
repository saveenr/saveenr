using System.Collections.Generic;

namespace Isotope.Trees
{
    internal struct TreePathItem<PATHT, NODET>
    {
        readonly public NODET node;
        readonly public Dictionary<PATHT, TreePathItem<PATHT, NODET>> dic;

        public TreePathItem(NODET node, Dictionary<PATHT, TreePathItem<PATHT, NODET>> dic)
        {
            this.node = node;
            this.dic = dic;

        }
    }
}