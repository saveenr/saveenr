using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    [TestClass]
    public class ArrayUtilTest
    {
        [TestMethod]
        public void FillArrayTest0()
        {
            var source = new string[] {};
            var array = new string[0];
            Isotope.Collections.ArrayUtil.FillArray(array, source);
        }

        [TestMethod]
        public void FillArrayTest1()
        {
            var source = new string[] {"A"};
            var array = new string[1];
            Isotope.Collections.ArrayUtil.FillArray(array, source);
            Assert.AreEqual("A", array[0]);
        }

        [TestMethod]
        public void FillArrayTest2()
        {
            var source = new string[] {"A", "B"};
            var array = new string[2];
            Isotope.Collections.ArrayUtil.FillArray(array, source);
            Assert.AreEqual("A", array[0]);
            Assert.AreEqual("B", array[1]);
        }

        [TestMethod]
        public void FillArrayTest3()
        {
            bool caught = false;
            var source = new string[] {"A"};
            var array = new string[2];
            try{Isotope.Collections.ArrayUtil.FillArray(array, source);}
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
        public void FillArrayTest4()
        {
            var source = new string[] {"A", "B", "C"};
            var array = new string[2];
            Isotope.Collections.ArrayUtil.FillArray(array, source);
            Assert.AreEqual("A", array[0]);
            Assert.AreEqual("B", array[1]);
        }
    }
}