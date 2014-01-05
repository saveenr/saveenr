using Isotope.Xml.Linq.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using System.Linq;

namespace IsotopeTests
{
    [TestClass]
    public class XElementExtensionsTest
    {
        [TestMethod]
        public void GetAttributeValueTest0()
        {
            var e1 = new XElement("Foo");
            Assert.AreEqual("beer", e1.AttributeValue("a1", "beer"));

            e1.SetAttributeValue("a1", "baz");
            Assert.AreEqual("baz", e1.AttributeValue("a1", "beer"));
        }

        [TestMethod]
        public void GetAttributeValueTest1()
        {
            var e1 = new XElement("Foo");
            Assert.AreEqual(2, e1.AttributeValue<int>("a1", 2, s => int.Parse(s)));

            e1.SetAttributeValue("a1", "4");
            Assert.AreEqual(4, e1.AttributeValue<int>("a1", 2, s => int.Parse(s)));
            Assert.AreEqual(4.0, e1.AttributeValue<double>("a1", 2, s => double.Parse(s)));
        }

        [TestMethod]
        public void AddElementValueTest1()
        {
            var e1 = new XElement("Foo");
            e1.AddElementValue("a1", "a");
            e1.AddElementValue("a2", "b");
            e1.AddElementValue("a3", "c");

            var els = e1.Elements().ToList();
            Assert.AreEqual(3, els.Count);
            Assert.AreEqual("a1", els[0].Name);
            Assert.AreEqual("a2", els[1].Name);
            Assert.AreEqual("a3", els[2].Name);
            Assert.AreEqual("a", els[0].Value);
            Assert.AreEqual("b", els[1].Value);
            Assert.AreEqual("c", els[2].Value);
        }

        [TestMethod]
        public void AddTextTest1()
        {
            var e1 = new XElement("Foo");
            e1.AddText("Foo");
            Assert.AreEqual("Foo", e1.Value);
            e1.AddText("Bar");
            Assert.AreEqual("FooBar", e1.Value);
        }
    }
}