using Isotope.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    
    [TestClass()]
    public class TextUtilTest
    {
        [TestMethod()]
        [DeploymentItem("Isotope.dll")]
        public void GetLengthOfMatchingPrefixTest()
        {
            Assert.AreEqual( 0, Isotope.Text.TextUtil.GetLengthOfMatchingPrefix("",""));
            Assert.AreEqual(0, Isotope.Text.TextUtil.GetLengthOfMatchingPrefix("", "A"));
            Assert.AreEqual(0, Isotope.Text.TextUtil.GetLengthOfMatchingPrefix("B", "A"));
            Assert.AreEqual(1, Isotope.Text.TextUtil.GetLengthOfMatchingPrefix("A", "A"));
            Assert.AreEqual(1, Isotope.Text.TextUtil.GetLengthOfMatchingPrefix("AB", "Ab"));
            Assert.AreEqual(1, Isotope.Text.TextUtil.GetLengthOfMatchingPrefix("A", "AA"));
            Assert.AreEqual(1, Isotope.Text.TextUtil.GetLengthOfMatchingPrefix("AA", "A"));
            Assert.AreEqual(3, Isotope.Text.TextUtil.GetLengthOfMatchingPrefix("FooBar", "FooXXX"));
        }


        [TestMethod()]
        [DeploymentItem("Isotope.dll")]
        public void TestMultiply()
        {
            Assert.AreEqual("", Isotope.Text.TextUtil.Multiply("", 0));
            Assert.AreEqual("", Isotope.Text.TextUtil.Multiply("", 5));
            Assert.AreEqual("AbAbAbAbAb", Isotope.Text.TextUtil.Multiply("Ab", 5));
        }



        [TestMethod()]
        [DeploymentItem("Isotope.dll")]
        public void TestRemoveCharacters()
        {
            Assert.AreEqual("", Isotope.Text.TextUtil.RemoveCharacters("", new char[] { }));
            Assert.AreEqual("Fox", Isotope.Text.TextUtil.RemoveCharacters("Fox", new char[] { }));
            Assert.AreEqual("ox", Isotope.Text.TextUtil.RemoveCharacters("Fox", new char[] { 'F' }));
            Assert.AreEqual("Fw", Isotope.Text.TextUtil.RemoveCharacters("Follow", new char[] {'l', 'o'}));
            Assert.AreEqual("", Isotope.Text.TextUtil.RemoveCharacters("Follow", new char[] { 'F', 'l', 'o', 'w' }));
        }
    }
}
