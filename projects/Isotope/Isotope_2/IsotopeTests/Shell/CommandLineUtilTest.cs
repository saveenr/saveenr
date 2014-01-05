using Isotope.CommandLine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    [TestClass]
    public class CommandLineUtilTest
    {
        [TestMethod]
        public void TestYesNoToBool()
        {
            Assert.AreEqual(true, CommandLineUtil.ParseYesNo(null, true));
            Assert.AreEqual(false, CommandLineUtil.ParseYesNo(null, false));
            Assert.AreEqual(true, CommandLineUtil.ParseYesNo("", true));
            Assert.AreEqual(false, CommandLineUtil.ParseYesNo("", false));

            Assert.AreEqual(true, CommandLineUtil.ParseYesNo("y", false));
            Assert.AreEqual(true, CommandLineUtil.ParseYesNo("y", true));
            Assert.AreEqual(false, CommandLineUtil.ParseYesNo("ye", false));
            Assert.AreEqual(true, CommandLineUtil.ParseYesNo("yes", true));
            Assert.AreEqual(false, CommandLineUtil.ParseYesNo("yex", false));
            Assert.AreEqual(true, CommandLineUtil.ParseYesNo("yesx", true));

            Assert.AreEqual(false, CommandLineUtil.ParseYesNo("n", false));
            //Assert.AreEqual(false, CommandLineUtil.ParseYesNo("n", true));
            //Assert.AreEqual(false, CommandLineUtil.ParseYesNo("no", false));
            //Assert.AreEqual(false, CommandLineUtil.ParseYesNo("no", true));
            //Assert.AreEqual(false, CommandLineUtil.ParseYesNo("nox", false));
            //Assert.AreEqual(true, CommandLineUtil.ParseYesNo("nox", true));
        }
    }
}