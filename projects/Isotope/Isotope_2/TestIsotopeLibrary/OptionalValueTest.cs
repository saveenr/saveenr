using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    [TestClass]
    public class OptionalValueTest
    {
        [TestMethod]
        public void OptionalValueConstructorTest1()
        {
            var a = new Isotope.Types.OptionalValue<int>();
            Assert.IsFalse(a.Hasvalue);
        }

        [TestMethod]
        public void OptionalValueConstructorTest2()
        {
            bool caught = false;
            var a = new Isotope.Types.OptionalValue<int>();
            try {int b = a.Value;}
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
        public void OptionalTest3()
        {
            var a = new Isotope.Types.OptionalValue<string>();
            Assert.IsFalse(a.Hasvalue);
            a.Value = "foo";
            Assert.IsTrue(a.Hasvalue);
            a.Clear();
            Assert.IsFalse(a.Hasvalue);
            a.Value = null;
            Assert.IsTrue(a.Hasvalue);
            Assert.AreEqual(null, a.Value);
        }

        [TestMethod]
        public void OptionalTest4()
        {
            var a = new Isotope.Types.OptionalValue<string>();
            Assert.IsFalse(a.Hasvalue);
            a = "foo";
            Assert.IsTrue(a.Hasvalue);
            a.Clear();
            Assert.IsFalse(a.Hasvalue);
            a = null;
            Assert.IsTrue(a.Hasvalue);
            Assert.AreEqual(null, a.Value);
        }

        [TestMethod]
        public void OptionalTest5()
        {
            Isotope.Types.OptionalValue<string> a = "FOO";
            Assert.IsTrue(a.Hasvalue);
            Assert.IsTrue("FOO" == a);
            a.Clear();
            Assert.IsFalse(a.Hasvalue);
            a = null;
            Assert.IsTrue(a.Hasvalue);
            Assert.AreEqual(null, a.Value);
        }

        [TestMethod]
        public void OptionalTest6()
        {
            // tests that assignment is copy of the value
            var a = new Isotope.Types.OptionalValue<string>();
            var b = new Isotope.Types.OptionalValue<string>();
            Assert.IsFalse(a.Hasvalue);
            Assert.IsFalse(b.Hasvalue);
            a = "FOO";
            Assert.AreEqual("FOO", a.Value);
            b = a;
            Assert.AreEqual("FOO", b.Value);
            a = "BAR";
            Assert.AreEqual("BAR", a.Value);
            Assert.AreEqual("FOO", b.Value);
        }

        [TestMethod]
        public void OptionalTest7()
        {
            var a = new Isotope.Types.OptionalValue<string>();
            var b = new Isotope.Types.OptionalValue<string>();
            Assert.IsFalse(a.Hasvalue);
            Assert.IsFalse(b.Hasvalue);
            b.UpdateFrom(a);
            Assert.IsFalse(a.Hasvalue);
            Assert.IsFalse(b.Hasvalue);
            b.Value = "XXX";
            Assert.AreEqual("XXX", b.Value);
            b.UpdateFrom(a);
            Assert.IsFalse(a.Hasvalue);
            Assert.AreEqual("XXX", b.Value);

            a = "FOO";
            b.UpdateFrom(a);
            Assert.AreEqual("FOO", a.Value);
            Assert.AreEqual("FOO", b.Value);
        }
    }
}