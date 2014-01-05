using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    [TestClass]
    public class AnonymousTypeUtilTest
    {
        [TestMethod]
        public void InstanceToDictionaryTest()
        {
            var item = new {Foo = 1, Bar = "Beer"};
            var dic = Isotope.Reflection.ReflectionUtil.GetPropertyDictionary(item);
            Assert.AreEqual(1, dic["Foo"]);
            Assert.AreEqual("Beer", dic["Bar"]);
            Assert.AreEqual(2, dic.Count);
        }
    }
}