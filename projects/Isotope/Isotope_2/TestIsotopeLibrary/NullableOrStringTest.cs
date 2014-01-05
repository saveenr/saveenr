using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    [TestClass]
    public class NullableOrStringTest
    {
        [TestMethod]
        public void Test1_construct_empty()
        {
            var v1 = new Isotope.Types.NullableOrString<int>();
            Assert.IsFalse(v1.HasValue);
            Assert.IsFalse(v1.HasString);
            Assert.IsTrue(v1.IsEmpty);
        }

        [TestMethod]
        public void Test1_construct_empty_fail_get_value()
        {
            bool caught = false;
            var v1 = new Isotope.Types.NullableOrString<int>();
            try {int i = v1.Value;}
            catch (System.FieldAccessException )
            {
                caught = true;
            }

            if (caught == false)
            {
                Assert.Fail("Did not catch expected exception");
            }
        }

        [TestMethod]
        public void Test1_construct_empty_fail_get_string()
        {
            bool caught = false;
            var v1 = new Isotope.Types.NullableOrString<int>();
            try {string s = v1.String;}
            catch (System.FieldAccessException )
            {
                caught = true;
            }

            if (caught == false)
            {
                Assert.Fail("Did not catch expected exception");
            }
        }

        [TestMethod]
        public void Test3_init_with_value()
        {
            var v1 = new Isotope.Types.NullableOrString<int>(1);
            Assert.IsTrue(v1.HasValue);
            Assert.IsFalse(v1.HasString);
            Assert.IsFalse(v1.IsEmpty);
            Assert.AreEqual(1, v1.Value);
        }

        [TestMethod]
        public void Test4_init_with_string()
        {
            var v1 = new Isotope.Types.NullableOrString<int>("FOO");
            Assert.IsFalse(v1.HasValue);
            Assert.IsTrue(v1.HasString);
            Assert.IsFalse(v1.IsEmpty);
            Assert.AreEqual("FOO", v1.String);
        }

        [TestMethod]
        public void Test_swap_value_and_string()
        {
            var v1 = new Isotope.Types.NullableOrString<int>("FOO");
            Assert.IsFalse(v1.HasValue);
            Assert.IsTrue(v1.HasString);
            Assert.IsFalse(v1.IsEmpty);
            Assert.AreEqual("FOO", v1.String);

            v1.Value = 10;
            Assert.IsTrue(v1.HasValue);
            Assert.IsFalse(v1.HasString);
            Assert.IsFalse(v1.IsEmpty);
            Assert.AreEqual(10, v1.Value);

            v1.String = "BAR";
            Assert.IsFalse(v1.HasValue);
            Assert.IsTrue(v1.HasString);
            Assert.IsFalse(v1.IsEmpty);
            Assert.AreEqual("BAR", v1.String);
        }

        [TestMethod]
        public void Test_clear_1()
        {
            var v1 = new Isotope.Types.NullableOrString<int>();
            v1.String = "FOO";
            Assert.IsFalse(v1.HasValue);
            Assert.IsTrue(v1.HasString);
            Assert.IsFalse(v1.IsEmpty);
            Assert.AreEqual("FOO", v1.String);

            v1.Clear();
            Assert.IsFalse(v1.HasValue);
            Assert.IsFalse(v1.HasString);
            Assert.IsTrue(v1.IsEmpty);
        }

        [TestMethod]
        public void Test_clear_2()
        {
            var v1 = new Isotope.Types.NullableOrString<int>();
            v1.Value = 10;
            Assert.IsTrue(v1.HasValue);
            Assert.IsFalse(v1.HasString);
            Assert.IsFalse(v1.IsEmpty);
            Assert.AreEqual(10, v1.Value);

            v1.Clear();
            Assert.IsFalse(v1.HasValue);
            Assert.IsFalse(v1.HasString);
            Assert.IsTrue(v1.IsEmpty);
        }

        [TestMethod]
        public void Test_defaults()
        {
            var v1 = new Isotope.Types.NullableOrString<int>();
            Assert.IsFalse(v1.HasValue);
            Assert.IsFalse(v1.HasString);
            Assert.IsTrue(v1.IsEmpty);

            Assert.AreEqual("FOO", v1.GetStringOrDefault("FOO"));
            Assert.AreEqual(20, v1.GetValueOrDefault(20));

            v1.Value = 1;
            Assert.AreEqual("FOO", v1.GetStringOrDefault("FOO"));
            Assert.AreEqual(1, v1.GetValueOrDefault(20));

            v1.String = "BAR";
            Assert.AreEqual("BAR", v1.GetStringOrDefault("FOO"));
            Assert.AreEqual(20, v1.GetValueOrDefault(20));

            v1.Clear();
            Assert.AreEqual("FOO", v1.GetStringOrDefault("FOO"));
            Assert.AreEqual(20, v1.GetValueOrDefault(20));
        }
    }
}