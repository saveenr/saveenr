
using System.Collections.Generic;
using System.Linq;


namespace Foo
{


    public partial class Node
    {
        public readonly string _name;
        public string Name { get { return this._name; } }

        private Node parent;
        public Node Parent { get { return this.parent; } }

        private List<Node> children;
        public virtual IEnumerable<Node> Children
        {
            get
            {
                if (this.children == null)
                {
                    yield break;
                }
                else
                {
                    foreach (Node n in this.children) { yield return n; }
                }
            }
        }

        public virtual IEnumerable<T> Element<T>() where T : Node
        {
            foreach (Node n in this.Children)
            {
                if (n is T) { yield return ((T)n); }
            }
        }

        public int ChildrenCount
        {
            get
            {
                if (this.children == null)
                {
                    return 0;
                }
                else
                {
                    return this.children.Count;
                }
            }
        }

        protected Node(string name)
        {
            this._name = name;
        }


        protected void AddChild(Node n)
        {
            if (n == null)
            {
                throw new System.ArgumentNullException("n");
            }

            if (n.Parent != null)
            {
                throw new System.ArgumentException("n");
            }


            if (this.children == null)
            {
                this.children = new List<Node>();
            }

            n.parent = this;
            this.children.Add(n);
        }


        public IEnumerable<Node> Walk()
        {
            var q = new List<Node>();
            q.Add(this);

            while (q.Count > 0)
            {

                var cn = q[0];
                q.RemoveAt(0);
                yield return cn;

                foreach (var child in cn.Children)
                {
                    q.Add(child);
                }
            }
        }



        public IEnumerable<T> Walk<T>() where T : Node
        {
            foreach (var n in this.Walk())
            {
                if (n is T)
                {
                    yield return (T)n;
                }
            }
        }



    }


    public partial class Document : Node
    {
        public Document()
            : base("Document")
        {
        }
        public void Add(Page element)
        {
            this.AddChild(element);
        }
    }

    public partial class Page : Node
    {
        public Page()
            : base("Page")
        {
        }
        public System.Drawing.SizeF Size;
        public void Add(Rectangle element)
        {
            this.AddChild(element);
        }
        public void Add(Group element)
        {
            this.AddChild(element);
        }
    }

    public partial class Rectangle : Node
    {
        public Rectangle()
            : base("Rectangle")
        {
        }
        public System.Drawing.PointF PinPosition;
        public System.Drawing.SizeF Size;
    }

    public partial class Group : Node
    {
        public Group()
            : base("Group")
        {
        }
        public void Add(Rectangle element)
        {
            this.AddChild(element);
        }
        public void Add(Group element)
        {
            this.AddChild(element);
        }
    }
}
