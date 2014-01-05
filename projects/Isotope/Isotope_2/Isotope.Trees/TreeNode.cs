using System.Collections.Generic;

namespace Isotope.Trees
{
    public sealed class TreeNode<T>
    {
        private TreeNode<T> m_parent;
        private Tree<T> m_tree;
        private List<TreeNode<T>> m_children;

        public T Data { get; set; }

        public TreeNode<T> Parent
        {
            get { return this.m_parent; }
        }

        public Tree<T> Tree
        {
            get { return this.m_tree; }
        }

        public TreeNode()
        {
        }

        public TreeNode(T data)
        {
            this.Data = data;
        }

        /// <summary>
        /// Addes a child node to this node
        /// </summary>
        /// <param name="child"></param>
        public void AddChild(TreeNode<T> child)
        {
            if (child.Tree != null)
            {
                throw new System.ArgumentException("node to add already attached to tree");
            }

            if (child.Parent != null)
            {
                throw new System.ArgumentException("node to add already attached to parent");
            }

            if (this.m_children == null)
            {
                this.m_children = new List<TreeNode<T>>();
            }
            this.m_children.Add(child);
            child.m_parent = this;
            child.m_tree = this.Tree;
        }

        public void RemoveChild(TreeNode<T> child)
        {
            if (child == null)
            {
                throw new System.ArgumentNullException("child");
            }

            if (child.Tree != this.Tree)
            {
                throw new System.ArgumentException("node belongs to a different tree");
            }

            if (child.Parent != this)
            {
                throw new System.ArgumentException("not a child");
            }

            if (this.m_children == null)
            {
                throw new System.ArgumentException("not a child - node has no children");
            }

            if (this.m_children.Count < 1)
            {
                throw new System.ArgumentException("not a child - node has no children");
            }

            this.m_children.Remove(child);

            child.m_parent = null;
            child.m_tree = null;
        }


        public IEnumerable<TreeNode<T>> Children
        {
            get
            {
                if (this.m_children == null)
                {
                    yield break;
                }
                else
                {
                    foreach (var i in this.m_children)
                    {
                        yield return i;
                    }
                }
            }
        }

        public IEnumerable<TreeNode<T>> ChildrenRecursive
        {
            get
            {
                foreach (var ev in this.Walk())
                {
                    if (ev.HasEnteredNode)
                    {
                        yield return ev.Node;
                    }
                }
            }
        }

        public IEnumerable<WalkEvent<TreeNode<T>>> Walk()
        {
            return Traversal.Walk<TreeNode<T>>(this, n => n.Children);
        }

        public TreeNode<T> GetChild(int index)
        {
            return this.m_children[index];
        }

        public int ChildCount
        {
            get
            {
                if (this.m_children == null)
                {
                    return 0;
                }
                else
                {
                    return this.m_children.Count;
                }
            }
        }
    }
}