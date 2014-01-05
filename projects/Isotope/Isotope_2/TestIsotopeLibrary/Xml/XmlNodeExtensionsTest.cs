using Isotope.Xml.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.Linq;

namespace IsotopeTests
{
    [TestClass]
    public class XmlNodeExtensionsTest
    {
        [TestMethod]
        public void SelectSingleInnerTextTest()
        {
            string xml = @"<xml><foo>A<bar>b</bar><beer>c</beer></foo></xml>";

            var dom = new System.Xml.XmlDocument();
            dom.LoadXml(xml);

            var root_el = dom.DocumentElement;
            Assert.AreEqual("Abc", root_el.SelectSingleInnerText("foo"));
        }

        [TestMethod]
        public void XmlDOmTest_2()
        {
            string xml = @"<xml><foo>A<bar>b</bar><beer>c</beer></foo></xml>";

            var dom = new System.Xml.XmlDocument();
            dom.LoadXml(xml);

            var root_el = dom.DocumentElement;
            var foo = root_el.SelectSingleNode("foo");
            var names = foo.SelectNodes("*").AsEnumerable().Select(n => n.Name).ToList();
            Assert.AreEqual(2, names.Count);
            Assert.AreEqual("bar", names[0]);
            Assert.AreEqual("beer", names[1]);
        }
    }
}