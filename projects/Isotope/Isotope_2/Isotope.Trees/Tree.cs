using System.Collections.Generic;

namespace Isotope.Trees
{
    public sealed class Tree<T>
    {
        private TreeNode<T> _root;

        public TreeNode<T> Root
        {
            get { return this._root; }
            set { this._root = value; }
        }

        public int GetNodeCount()
        {
            if (this.Root == null)
            {
                return 0;
            }
            int count = 0;
            foreach (var e in this.Root.Walk())
            {
                if (e.HasEnteredNode)
                {
                    count++;
                }
            }
            return count;
        }

        public static string GetTreeString(TreeNode<T> node, System.Func<T, string> get_start_delim,
                                           System.Func<T, string> get_end_delim, System.Func<T, string> get_content)
        {
            var nodes = new TreeNode<T>[] {node};
            return GetTreeString(nodes, get_start_delim, get_end_delim, get_content);
        }

        public static string GetTreeString(IEnumerable<TreeNode<T>> nodes, System.Func<T, string> get_start_delim,
                                           System.Func<T, string> get_end_delim, System.Func<T, string> get_content)
        {
            var sb = new System.Text.StringBuilder();
            foreach (var node in nodes)
            {
                foreach (var walkevent in Traversal.Walk<TreeNode<T>>(node, n => n.Children))
                {
                    if (walkevent.HasEnteredNode)
                    {
                        sb.Append(get_start_delim(walkevent.Node.Data));
                        sb.Append(get_content(walkevent.Node.Data));
                    }
                    else if (walkevent.HasExitedNode)
                    {
                        sb.Append(get_end_delim(walkevent.Node.Data));
                    }
                }
            }
            return sb.ToString();
        }
    }
}