using System.Collections.Generic;
using Isotope.Text.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    [TestClass]
    public class TestStringExtensions
    {
        [TestMethod]
        public void TestJoin()
        {
            var a1 = new List<string> {};
            var a2 = new List<string> {"a"};
            var a3 = new List<string> {"a", "b"};
            var a4 = new List<string> {"a", "b", "c"};

            Assert.AreEqual("", "".Join(a1));
            Assert.AreEqual("", "X".Join(a1));
            Assert.AreEqual("a", "X".Join(a2));
            Assert.AreEqual("aXb", "X".Join(a3));
            Assert.AreEqual("aXbXc", "X".Join(a4));
            Assert.AreEqual("aXXbXXc", "XX".Join(a4));
        }
    }
}