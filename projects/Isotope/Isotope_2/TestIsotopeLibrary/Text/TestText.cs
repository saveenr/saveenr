using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    [TestClass]
    public class TestText
    {
        [TestMethod]
        public void TestRemoveCharacters()
        {
            Assert.AreEqual("", Isotope.Text.TextUtil.RemoveCharacters("", new[] {'a'}));
            Assert.AreEqual("", Isotope.Text.TextUtil.RemoveCharacters("a", new[] {'a'}));
            Assert.AreEqual("", Isotope.Text.TextUtil.RemoveCharacters("aaaa", new[] {'a'}));
            Assert.AreEqual("bb", Isotope.Text.TextUtil.RemoveCharacters("ababa", new[] {'a'}));
            Assert.AreEqual("", Isotope.Text.TextUtil.RemoveCharacters("ababa", new[] {'b', 'a'}));
        }
    }
}