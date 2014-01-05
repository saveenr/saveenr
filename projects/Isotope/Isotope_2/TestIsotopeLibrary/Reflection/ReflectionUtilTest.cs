using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace IsotopeTests
{
    public static class T0
    {
    }

    public static class T1
    {
        public static void E1(this int a)
        {
        }

        public static void E1(this string a)
        {
        }
    }

    [TestClass]
    public class ReflectionUtilTest
    {
        [TestMethod]
        public void EnumExtensionMethodsTest_1()
        {
            var em0 = Isotope.Reflection.ReflectionUtil.EnumExtensionMethods(typeof (T0)).ToList();
            Assert.AreEqual(0, em0.Count);
        }

        [TestMethod]
        public void EnumExtensionMethodsTest_2()
        {
            var em0 = Isotope.Reflection.ReflectionUtil.EnumExtensionMethods(typeof (T1)).ToList();
            Assert.AreEqual(2, em0.Count);

            Assert.AreEqual(typeof (T1), em0[0].ExtendingType);
            Assert.AreEqual(typeof (int), em0[0].ExtendedType);
            Assert.AreEqual(typeof (T1), em0[1].ExtendingType);
            Assert.AreEqual(typeof (string), em0[1].ExtendedType);
        }

        [TestMethod]
        public void NullableTest_3()
        {
            Assert.IsTrue(Isotope.Reflection.ReflectionUtil.IsNullableType(typeof (int?)));
            Assert.IsFalse(Isotope.Reflection.ReflectionUtil.IsNullableType(typeof (int)));
            Assert.IsFalse(Isotope.Reflection.ReflectionUtil.IsNullableType(typeof (string)));
        }

        [TestMethod]
        public void TestNames()
        {
            Assert.AreEqual("int", Isotope.Reflection.ReflectionUtil.GetNiceTypeName(typeof(int)));
            Assert.AreEqual("List<int>", Isotope.Reflection.ReflectionUtil.GetNiceTypeName(typeof(System.Collections.Generic.List<int>)));
            Assert.AreEqual("int?", Isotope.Reflection.ReflectionUtil.GetNiceTypeName(typeof(int?)));
            Assert.AreEqual("object", Isotope.Reflection.ReflectionUtil.GetNiceTypeName(typeof(object)));
            Assert.AreEqual("int[]", Isotope.Reflection.ReflectionUtil.GetNiceTypeName(typeof(int[])));
            Assert.AreEqual("Array", Isotope.Reflection.ReflectionUtil.GetNiceTypeName(typeof(System.Array)));
            Assert.AreEqual("DBNull", Isotope.Reflection.ReflectionUtil.GetNiceTypeName(typeof(System.DBNull)));
            Assert.AreEqual("IEnumerable<int>", Isotope.Reflection.ReflectionUtil.GetNiceTypeName(typeof(System.Collections.Generic.IEnumerable<int>)));
            Assert.AreEqual("T0", Isotope.Reflection.ReflectionUtil.GetNiceTypeName(typeof(T0)));
        }

    }
}