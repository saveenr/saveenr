using System.Collections.Generic;
using System.Linq;

namespace Isotope.Trees
{
    public static class TreeBuilder
    {
        public delegate int GetNodeDepth<T>(T item);
        public delegate KeyT GetNodeKey<T,KeyT>(T item);

        public delegate NODET CreateNewNode<T,NODET>(T item);
        public delegate NODET CreateNewNodeFromPath<PATHT,NODET>(PATHT[] pathitems, PATHT path, int depth);
        public delegate void AddChild<NODET>(NODET parent, NODET child);
        public delegate IEnumerable<T> EnumerateChildren<T>(T item);
        public delegate PATHT[] SplitPath<PATHT>(PATHT path);

        public static IList<NODET> FromDepths<T, NODET>(   IEnumerable<T> items, 
                                                                    GetNodeDepth<T> getdepth,
                                                                    CreateNewNode<T, NODET> new_node, 
                                                                    AddChild<NODET> addchild)
        {
            var root_nodes = new List<NODET>();
            var stack_nodes = new Stack<NODET>();
            var stack_depth = new Stack<int>();

            foreach (var item in items)
            {
                var item_depth = getdepth(item);
                if (item_depth < 0)
                {
                    throw new System.ArgumentException("getdepth returned negative value");
                }

                var item_node = new_node(item);

                // Very important that these two stacks have the same number of items
                if (stack_depth.Count != stack_nodes.Count)
                {
                    throw new System.InvalidOperationException("Stacks out of sync");
                }

                // Remove all nodes on the stack that are too deep to be a parent
                while (stack_nodes.Count > 0)
                {
                    if (stack_depth.Peek() >= item_depth)
                    {
                        stack_nodes.Pop();
                        stack_depth.Pop();
                    }
                    else
                    {
                        break;
                    }
                }

                // if there's any node in the stack, it means the top of the stack has a node that can serce as a parent
                if (stack_nodes.Count > 0)
                {
                    var cur_node = stack_nodes.Peek();
                    int cur_depth = stack_depth.Peek();

                    if (item_depth > cur_depth)
                    {
                        addchild(cur_node, item_node);
                    }
                }

                // Add the current node as a potential parent of the next item that will be processed
                stack_nodes.Push(item_node);
                stack_depth.Push(item_depth);

                // if there is only a single node in the stack that means that this
                // node is is a root node
                if (stack_nodes.Count == 1)
                {
                    root_nodes.Add(item_node);
                }
            }

            return root_nodes;
        }

        public static Tree<string> FromPaths(IEnumerable<string> paths, char[] pathseps)
        {
            if (paths == null)
            {
                throw new System.ArgumentNullException("paths");
            }

            if (pathseps == null)
            {
                throw new System.ArgumentNullException("pathseps");
            }

            var roots = TreeBuilder.FromPaths(
                paths,
                path => path.Split(pathseps),
                (path_tokens, path, depth) => new TreeNode<string>(path),
                (parent, child) => parent.AddChild(child));

            var tree = new Tree<string>();
            tree.Root = roots;

            return tree;
        }

        public static IList<TDest> FromTreeNodes<TSrc, TDest>(
            TSrc rootnode,
            System.Func<TSrc, IEnumerable<TSrc>> enumchildren,
            System.Func<TSrc, TDest> createdstnode,
            System.Action<TDest, TDest> addchild)
        {
            var stack = new Stack<TDest>();
            var tnodes = new List<TDest>();

            var walk_items = Traversal.Walk<TSrc>(rootnode, input_node => enumchildren(input_node));
            foreach (var walk_item in walk_items)
            {
                if (walk_item.HasEnteredNode)
                {
                    var new_dst_node = createdstnode(walk_item.Node);

                    if (stack.Count > 0)
                    {
                        var parent = stack.Peek();
                        addchild(parent, new_dst_node);
                    }
                    stack.Push(new_dst_node);
                    tnodes.Add(new_dst_node);
                }
                else if (walk_item.HasExitedNode)
                {
                    stack.Pop();
                }
            }

            return tnodes;
        }

        public static IList<TNODE> FromParentChildRelationship<TROW, TKEY, TNODE>(
            IEnumerable<TROW> rows,
            GetNodeKey<TROW,TKEY> getchildkey,
            GetNodeKey<TROW,TKEY> getparentkey,
            CreateNewNode<TROW, TNODE> createnewnode,
            AddChild<TNODE> addchild)
            where TKEY : System.IComparable<TKEY>
        {
            var pairs = from row in rows
                        let childkey = getchildkey(row)
                        select new {childkey, row};

            // first create the nodes for each distinct key
            var node_dic = new Dictionary<TKEY, TNODE>();

            // assign create child nodes
            foreach (var pair in pairs)
            {
                if (node_dic.ContainsKey(pair.childkey))
                {
                    // no need to create child key
                }
                else
                {
                    // need to add the child key
                    var new_node = createnewnode(pair.row);
                    node_dic[pair.childkey] = new_node;
                }
            }

            var items = from row in rows
                        let parentkey = getparentkey(row)
                        let childkey = getchildkey(row)
                        let childnode = node_dic[childkey]
                        select new {parentkey, childkey, childnode};

            foreach (var item in items)
            {
                if (item.parentkey.CompareTo(item.childkey) == 0)
                {
                    throw new System.ArgumentException("Parent and Child Keys cannot be the same");
                }
                if (node_dic.ContainsKey(item.parentkey))
                {
                    var parentnode = node_dic[item.parentkey];
                    addchild(parentnode, item.childnode);
                }
            }

            var nodes = node_dic.Values.ToList();
            return nodes;
        }

        public static NODET FromPaths<PATHT, NODET>(
            IEnumerable<PATHT> paths, 
            SplitPath<PATHT> funcsplit,
            CreateNewNodeFromPath<PATHT,NODET> createnewnode,
            AddChild<NODET> addchild)
        {
            var root_dic = new Dictionary<PATHT, TreePathItem<PATHT, NODET>>();
            NODET current_node = default(NODET);
            bool has_current_node = false;

            PATHT root_token = default(PATHT);
            bool found_root = false;
            var pathbuilder = new List<PATHT>();
            foreach (var path in paths)
            {
                pathbuilder.Clear();

                int depth = 0;

                var tokens = funcsplit(path);

                var current_dic = root_dic;

                foreach (var token in tokens)
                {
                    pathbuilder.Add(token);
                    if (current_dic.ContainsKey(token))
                    {
                        var next_dic = current_dic[token];

                        current_dic = next_dic.dic;
                        current_node = next_dic.node;

                        has_current_node = true;
                    }
                    else
                    {
                        var new_node = createnewnode(pathbuilder.ToArray(), token, depth);
                        if (has_current_node)
                        {
                            addchild(current_node, new_node);
                        }

                        var new_dic = new Dictionary<PATHT, TreePathItem<PATHT, NODET>>();
                        current_dic[token] = new TreePathItem<PATHT, NODET>(new_node, new_dic);
                        current_dic = new_dic;
                        current_node = new_node;
                        has_current_node = true;

                        if (depth == 0)
                        {
                            if (!found_root)
                            {
                                root_token = token;
                                found_root = true;
                            }
                            else
                            {
                                throw new System.ArgumentException("Input contains multiple roots");
                            }
                        }
                    }

                    depth++;
                }
            }

            if (root_dic.Count < 1)
            {
                return default(NODET);
            }
            else if (root_dic.Count == 1)
            {
                var new_root_node = root_dic.ElementAt(0).Value.node;

                return new_root_node;
            }
            else
            {
                throw new System.InvalidOperationException("Should never get to this state");
            }
        }
    }
}