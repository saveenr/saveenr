using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    [TestClass]
    public class TraversalTest
    {
        private static string xml0 = @"
<a>
</a>

";

        private static string xml1 = @"
<a>
    <b/>
    <c/>
</a>
";

        private string xml2 =
            @"
<a>
    <b>
        <e/>
    </b>

    <c>
            <f/>
            <g>
                <h/>
            </g>

    </c>
</a>
";

        private static string get_preorder_names(string xml)
        {
            var dom = System.Xml.Linq.XDocument.Parse(xml, System.Xml.Linq.LoadOptions.None);
            var root = dom.Root;
            var elements = Isotope.Trees.Traversal.PreOrder(root, i => i.Elements()).ToList();
            var actual = string.Join(",", elements.Select(e => e.Name.LocalName).ToArray());
            return actual;
        }

        private static string get_postorder_names(string xml)
        {
            var dom = System.Xml.Linq.XDocument.Parse(xml, System.Xml.Linq.LoadOptions.None);
            var root = dom.Root;
            var elements = Isotope.Trees.Traversal.PostOrder(root, i => i.Elements()).ToList();
            var actual = string.Join(",", elements.Select(e => e.Name.LocalName).ToArray());
            return actual;
        }

        private static string get_xorder_names(string xml)
        {
            var dom = System.Xml.Linq.XDocument.Parse(xml, System.Xml.Linq.LoadOptions.None);
            var root = dom.Root;
            var walkevents = Isotope.Trees.Traversal.Walk<System.Xml.Linq.XElement>(root, i => i.Elements()).ToList();
            var tokens = new List<string>();
            foreach (var walkevent in walkevents)
            {
                if (walkevent.HasEnteredNode)
                {
                    tokens.Add(string.Format("<{0}>", walkevent.Node.Name.LocalName));
                }
                else if (walkevent.HasExitedNode)
                {
                    tokens.Add(string.Format("</{0}>", walkevent.Node.Name.LocalName));
                }
            }

            var actual = string.Join("", tokens.ToArray());
            return actual;
        }


        [TestMethod]
        public void Case_0_SingleNode()
        {
            Assert.AreEqual("a", get_preorder_names(xml0));
            Assert.AreEqual("a", get_postorder_names(xml0));
            Assert.AreEqual("<a></a>", get_xorder_names(xml0));
        }


        [TestMethod]
        public void Case_1_()
        {
            Assert.AreEqual("a,b,c", get_preorder_names(xml1));
            Assert.AreEqual("b,c,a", get_postorder_names(xml1));
            Assert.AreEqual("<a><b></b><c></c></a>", get_xorder_names(xml1));
        }

        [TestMethod]
        public void Case_2_()
        {
            Assert.AreEqual("a,b,e,c,f,g,h", get_preorder_names(xml2));
            Assert.AreEqual("e,b,f,h,g,c,a", get_postorder_names(xml2));
            Assert.AreEqual("<a><b><e></e></b><c><f></f><g><h></h></g></c></a>", get_xorder_names(xml2));
        }
    }
}