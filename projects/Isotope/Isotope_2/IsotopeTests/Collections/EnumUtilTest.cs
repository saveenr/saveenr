using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    [TestClass]
    public class EnumUtilTest
    {
        public enum ENUMA
        {
        }

        public enum ENUMB
        {
            X,
            Y = 3
        }

        [TestMethod]
        public void Test_0()
        {
            bool caught = false;
            try {var vals = Isotope.EnumUtil.GetTypedValues<string>();}
            catch (System.ArgumentException )
            {
                caught = true;
            }

            if (caught == false)
            {
                Assert.Fail("Did not catch expected exception");
            }
        }

        [TestMethod]
        public void Test_1()
        {
            var vals = Isotope.EnumUtil.GetTypedValues<ENUMA>();
            Assert.AreEqual(0, vals.Length);
        }

        [TestMethod]
        public void Test_2()
        {
            var vals = Isotope.EnumUtil.GetTypedValues<ENUMB>();
            Assert.AreEqual(2, vals.Length);
            Assert.AreEqual(ENUMB.X, vals[0]);
            Assert.AreEqual(ENUMB.Y, vals[1]);
        }

        [TestMethod]
        public void Test_3()
        {
            var dic = Isotope.EnumUtil.MapNamesToValues<ENUMB>();
            Assert.AreEqual(2, dic.Count);
            Assert.AreEqual(ENUMB.X, dic["X"]);
            Assert.AreEqual(ENUMB.Y, dic["Y"]);
        }

        [TestMethod]
        public void EnumTryParseTest0()
        {
            var result1 = Isotope.EnumUtil.TryParse<ENUMB>("x",true);
            Assert.IsTrue(result1.HasValue);
            Assert.AreEqual(ENUMB.X,result1.Value);

            var result2 = Isotope.EnumUtil.TryParse<ENUMB>("x", false);
            Assert.IsFalse(result2.HasValue);

            var result3 = Isotope.EnumUtil.TryParse<ENUMB>("xxx", false);
            Assert.IsFalse(result3.HasValue);

            var result4 = Isotope.EnumUtil.TryParse<ENUMB>("", false);
            Assert.IsFalse(result4.HasValue);

            var result5 = Isotope.EnumUtil.TryParse<ENUMB>(null, false);
            Assert.IsFalse(result5.HasValue);

        }


        [TestMethod]
        public void EnumParseTest0()
        {
            bool caught = false;
            try {var c = Isotope.EnumUtil.Parse<ENUMB>("x", false);}
            catch (System.ArgumentException )
            {
                caught = true;
            }

            if (caught == false)
            {
                Assert.Fail("Did not catch expected exception");
            }
        }

        [TestMethod]
        public void EnumParseTest1()
        {
            Assert.AreEqual(ENUMB.X, Isotope.EnumUtil.Parse<ENUMB>("rfrfefre", false, ENUMB.X));
        }

        [TestMethod]
        public void EnumParseTest2()
        {
            Assert.AreEqual(ENUMB.X, Isotope.EnumUtil.Parse<ENUMB>("x", true));
        }
    }
}