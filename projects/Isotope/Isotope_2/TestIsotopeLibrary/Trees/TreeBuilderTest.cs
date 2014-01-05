using Isotope.Trees;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace IsotopeTests
{
    [TestClass]
    public class TreeBuilderTest
    {
        public string GetTreeStringBracket<T>(IEnumerable<TreeNode<T>> nodes)
        {
            return Tree<T>.GetTreeString(nodes, n => n.ToString() + "{", n => "}", n => "");
        }

        private class Item
        {
            public string Parent;
            public string Child;

            public Item(string parent, string child)
            {
                this.Parent = parent;
                this.Child = child;
            }
        }

        [TestMethod]
        public void TestTreebuilder0()
        {
            // tests emty data
            var records = new Item[]
                              {
                              };

            var all_nodes = GetAllNodes(records);

            Assert.IsNotNull(all_nodes);
            Assert.AreEqual(0, all_nodes.Count);

            string treestring = GetTreeStringBracket(all_nodes);
            Assert.AreEqual("", treestring);
        }

        [TestMethod]
        public void TestTreebuilder1()
        {
            // tests single root node
            // NOTE: you cannot use null as a key for a node
            var records = new Item[]
                              {
                                  new Item("X", "1"),
                              };

            var all_nodes = GetAllNodes(records);

            Assert.IsNotNull(all_nodes);
            Assert.AreEqual(1, all_nodes.Count);

            string treestring = GetTreeStringBracket(all_nodes);
            Assert.AreEqual("1{}", treestring);
        }

        [TestMethod]
        public void TestTreebuilderSingleRootOneLevel()
        {
            var records = new Item[]
                              {
                                  new Item("1", "1.1"),
                                  new Item("1", "1.2"),
                                  new Item("X", "1"),
                              };

            var all_nodes = GetAllNodes(records);

            Assert.IsNotNull(all_nodes);
            Assert.AreEqual(3, all_nodes.Count);

            var root_nodes = all_nodes.Where(n => n.Parent == null).ToList();
            Assert.AreEqual(1, root_nodes.Count);
            Assert.AreEqual("1", root_nodes[0].Data);
            Assert.AreEqual(2, root_nodes[0].Children.Count());
            var root_children = root_nodes[0].Children.ToList();
            Assert.AreEqual("1.1", root_children[0].Data);
            Assert.AreEqual("1.2", root_children[1].Data);

            string treestring = GetTreeStringBracket(root_nodes);
            Assert.AreEqual("1{1.1{}1.2{}}", treestring);
        }

        [TestMethod]
        public void TestTreebuilderTwoRootsOneLevel()
        {
            var records = new Item[]
                              {
                                  new Item("1", "1.1"),
                                  new Item("2", "2.1"),
                                  new Item("X", "1"),
                                  new Item("X", "2"),
                              };

            var all_nodes = GetAllNodes(records);

            Assert.IsNotNull(all_nodes);
            Assert.AreEqual(4, all_nodes.Count);

            var root_nodes = all_nodes.Where(n => n.Parent == null).ToList();
            Assert.AreEqual(2, root_nodes.Count);
            Assert.AreEqual("1", root_nodes[0].Data);
            Assert.AreEqual("2", root_nodes[1].Data);
            Assert.AreEqual(1, root_nodes[0].Children.Count());
            Assert.AreEqual(1, root_nodes[1].Children.Count());

            string treestring = GetTreeStringBracket(root_nodes);
            Assert.AreEqual("1{1.1{}}2{2.1{}}", treestring);
        }

        [TestMethod]
        public void TestTreebuilderTestLoop()
        {
            // loops should not be allows

            bool caught = false;
            // tests single root node
            // NOTE: you cannot use null as a key for a node
            var records = new Item[]
                              {
                                  new Item("1", "1"),
                              };

            try { var all_nodes = GetAllNodes(records); }
            catch (System.ArgumentException)
            {
                caught = true;
            }

            if (caught == false)
            {
                Assert.Fail("Did not catch expected exception");
            }

        }

        [TestMethod]
        public void TestTreebuilderRepeat()
        {
            var records = new Item[]
                              {
                                  new Item("1", "1.1"),
                                  new Item("1", "1.1"),
                                  new Item("1", "1.1"),
                                  new Item("1", "1.1")
                              };

            var all_nodes = GetAllNodes(records);
            Assert.IsNotNull(all_nodes);
            Assert.AreEqual(1, all_nodes.Count);
            var root_nodes = all_nodes.Where(n => n.Parent == null).ToList();
            Assert.AreEqual(1, root_nodes.Count);
            Assert.AreEqual("1.1", root_nodes[0].Data);
            Assert.AreEqual(0, root_nodes[0].Children.Count());
            string treestring = GetTreeStringBracket(all_nodes);
            Assert.AreEqual("1.1{}", treestring);
        }

        private IList<TreeNode<string>> GetAllNodes(Item[] records)
        {
            return TreeBuilder.FromParentChildRelationship(
                records,
                i => i.Child,
                i => i.Parent,
                i => new TreeNode<string>(i.Child),
                (parent_node, child) => parent_node.AddChild(child));
        }

        [TestMethod]
        public void TreeNodesFromPathsTest_00()
        {
            var paths = new string[] { };
            var seps = new char[] { '/' };

            var tree = TreeBuilder.FromPaths(paths, seps);
            var count = tree.GetNodeCount();
            Assert.AreEqual(0, tree.GetNodeCount());
        }

        [TestMethod]
        public void TreeNodesFromPathsTest_01()
        {
            var paths = new string[] { "A" };
            var seps = new char[] { '/' };

            var tree = TreeBuilder.FromPaths(paths, seps);
            var count = tree.GetNodeCount();
            Assert.AreEqual(1, tree.GetNodeCount());
        }

        [TestMethod]
        public void TreeNodesFromPathsTest_012()
        {
            var paths = new string[] { "A", "A/B/C", "A/B/X" };
            var seps = new char[] { '/' };

            var tree = TreeBuilder.FromPaths(paths, seps);
            var count = tree.GetNodeCount();
            Assert.AreEqual(4, tree.GetNodeCount());

            var node_a = tree.Root;
            Assert.AreEqual(1, node_a.ChildCount);
            Assert.AreEqual("A", node_a.Data);

            var node_b = node_a.GetChild(0);
            Assert.AreEqual(2, node_b.ChildCount);
            Assert.AreEqual("B", node_b.Data);

            var node_c = node_b.GetChild(0);
            Assert.AreEqual(0, node_c.ChildCount);
            Assert.AreEqual("C", node_c.Data);

            var node_x = node_b.GetChild(1);
            Assert.AreEqual(0, node_c.ChildCount);
            Assert.AreEqual("X", node_x.Data);
        }

        [TestMethod]
        public void TreeNodesFromPathsTest_02()
        {
            var paths = new string[] { "A/B/C/D/E", "A/B/C" };
            var seps = new char[] { '/' };

            var tree = TreeBuilder.FromPaths(paths, seps);
            var count = tree.GetNodeCount();
            Assert.AreEqual(5, tree.GetNodeCount());

            var nodes = tree.Root.ChildrenRecursive.ToList();

            var node0 = tree.Root;
            Assert.AreEqual("A", node0.Data);
            Assert.AreEqual(1, node0.ChildCount);

            var node1 = tree.Root.GetChild(0);
            Assert.AreEqual("B", node1.Data);
        }

        [TestMethod]
        public void TreeNodesFromPathsTest_x1()
        {
            bool caught = false;
            var paths = new string[] { "A", "B" };
            var seps = new char[] { '/' };

            try { var tree = TreeBuilder.FromPaths(paths, seps); }
            catch (System.ArgumentException)
            {
                caught = true;
            }

            if (caught == false)
            {
                Assert.Fail("Did not catch expected exception");
            }
        }

        [TestMethod]
        public void TreeNodesFromPathsTest_xml()
        {
            var paths = new string[] { "A", "A/B/C", "A/B/X" };
            var seps = new char[] { '/' };

            var rootnode = TreeBuilder.FromPaths(
                paths,
                path => path.Split(seps),
                (path_tokens, path, depth) =>
                {
                    string elname = path_tokens[path.Length - 1];
                    return new System.Xml.Linq.XElement(path);
                },
                (parent, child) => parent.Add(child));

            var node_a = rootnode;
            Assert.AreEqual(1, node_a.Elements().Count());
            Assert.AreEqual("A", node_a.Name);

            var node_b = node_a.Elements().ElementAt(0);
            Assert.AreEqual(2, node_b.Elements().Count());
            Assert.AreEqual("B", node_b.Name);

            var node_c = node_b.Elements().ElementAt(0);
            Assert.AreEqual(0, node_c.Elements().Count());
            Assert.AreEqual("C", node_c.Name);

            var node_x = node_b.Elements().ElementAt(1);
            Assert.AreEqual(0, node_x.Elements().Count());
            Assert.AreEqual("X", node_x.Name);
        }

        private class XXX
        {
            public int depth;
            public string val;
        }

        [TestMethod]
        public void TreeNodesFromDepths()
        {
            var items = new[]
                            {
                                new XXX {depth = 1, val = "1"},
                                new XXX {depth = 3, val = "1.1"},
                                new XXX {depth = 7, val = "1.1.1"},
                                new XXX {depth = 5, val = "1.1.2"},
                                new XXX {depth = 0, val = "2"},
                                new XXX {depth = 1, val = "2.1"},
                                new XXX {depth = 1, val = "2.2"}
                            };
            var nodes =
                TreeBuilder.FromDepths(items, x => x.depth, i => new TreeNode<string>(i.val),
                                                (p, c) => p.AddChild(c)).ToList();

            var s = GetTreeStringBracket(nodes);
            Assert.AreEqual(2, nodes.Count);
            Assert.AreEqual("1{1.1{1.1.1{}1.1.2{}}}2{2.1{}2.2{}}", s);
        }

        [TestMethod]
        public void TreeNodesFromDepths_0()
        {
            var items = new XXX[]
                            {
                            };
            var nodes =
                TreeBuilder.FromDepths(items, x => x.depth, i => new TreeNode<string>(i.val),
                                                (p, c) => p.AddChild(c)).ToList();

            var s = GetTreeStringBracket(nodes);
            Assert.AreEqual(0, nodes.Count);
            Assert.AreEqual("", s);
        }
    }

}