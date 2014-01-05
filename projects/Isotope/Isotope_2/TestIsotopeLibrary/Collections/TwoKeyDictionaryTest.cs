using Isotope.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    [TestClass]
    public class TwoKeyDictionaryTest
    {
        [TestMethod]
        public void Test1()
        {
            var dic = new Isotope.Collections.TwoKeyDictionary<int, string, double>();
            Assert.AreEqual(0, dic.Count);
        }

        [TestMethod]
        public void Test2()
        {
            var dic = new Isotope.Collections.TwoKeyDictionary<int, string, double>();
            Assert.AreEqual(0, dic.Count);
            dic[0, "foo"] = 1.0;
            Assert.AreEqual(1, dic.Count);
            dic[0, "foo"] = 1.0;
            Assert.AreEqual(1, dic.Count);
            dic[0, "bar"] = 2.0;
            Assert.AreEqual(2, dic.Count);
            dic[1, "beer"] = 3.0;
            dic[1, "baz"] = 4.0;
            Assert.IsTrue(dic.ContainsKey(0, "foo"));
            Assert.IsTrue(dic.ContainsKey(0, "bar"));
            Assert.IsTrue(dic.ContainsKey(1, "beer"));
            Assert.IsTrue(dic.ContainsKey(1, "baz"));

            Assert.AreEqual(1.0, dic[0, "foo"]);
            Assert.AreEqual(2.0, dic[0, "bar"]);
            Assert.AreEqual(3.0, dic[1, "beer"]);
            Assert.AreEqual(4.0, dic[1, "baz"]);

            Assert.AreEqual(4, dic.Count);
        }
    }
}