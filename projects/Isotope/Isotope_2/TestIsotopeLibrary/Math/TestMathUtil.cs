using System.Linq;
using Isotope.Collections;
using Isotope.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    [TestClass]
    public class TestMathUtil
    {
        [TestMethod]
        public void TestRoundToNearest()
        {
            Assert.AreEqual(0.0, MathUtil.Round(0.27, System.MidpointRounding.ToEven, 1.0));

            Assert.AreEqual(0.0, MathUtil.Round(0.10, System.MidpointRounding.ToEven, 0.5));
            Assert.AreEqual(0.5, MathUtil.Round(0.27, System.MidpointRounding.ToEven, 0.5));
            Assert.AreEqual(0.5, MathUtil.Round(0.55, System.MidpointRounding.ToEven, 0.5));
            Assert.AreEqual(1.0, MathUtil.Round(0.75, System.MidpointRounding.ToEven, 0.5));
            Assert.AreEqual(1.0, MathUtil.Round(0.78, System.MidpointRounding.ToEven, 0.5));

            Assert.AreEqual(1.0, MathUtil.Round(1.3, System.MidpointRounding.ToEven, 1.0));
            Assert.AreEqual(1.5, MathUtil.Round(1.3, System.MidpointRounding.ToEven, 0.5));

            Assert.AreEqual(0.0, MathUtil.Round(0.27, System.MidpointRounding.AwayFromZero, 1.0));

            Assert.AreEqual(0.0, MathUtil.Round(0.10, System.MidpointRounding.AwayFromZero, 0.5));
            Assert.AreEqual(0.5, MathUtil.Round(0.27, System.MidpointRounding.AwayFromZero, 0.5));
            Assert.AreEqual(0.5, MathUtil.Round(0.55, System.MidpointRounding.AwayFromZero, 0.5));
            Assert.AreEqual(1.0, MathUtil.Round(0.75, System.MidpointRounding.AwayFromZero, 0.5));
            Assert.AreEqual(1.0, MathUtil.Round(0.78, System.MidpointRounding.AwayFromZero, 0.5));

            Assert.AreEqual(1.0, MathUtil.Round(1.3, System.MidpointRounding.AwayFromZero, 1.0));
            Assert.AreEqual(1.5, MathUtil.Round(1.3, System.MidpointRounding.AwayFromZero, 0.5));

            Assert.AreEqual(0.0, MathUtil.RoundUp(0.0, 0.5));
            Assert.AreEqual(0.5, MathUtil.RoundUp(0.1, 0.5));
            Assert.AreEqual(1.0, MathUtil.RoundUp(0.5, 0.5));
            Assert.AreEqual(1.0, MathUtil.RoundUp(0.7, 0.5));
            Assert.AreEqual(1.0, MathUtil.RoundUp(0.99, 0.5));
            Assert.AreEqual(1.0, MathUtil.RoundUp(1.0, 0.5));
        }

        [TestMethod]
        public void TestEnumDoubleRange()
        {
            var a1 = EnumerableUtil.RangeSteps(0.0, 1.0, 0).ToArray();
            Assert.AreEqual(0, a1.Length);

            var a2 = EnumerableUtil.RangeSteps(0.0, 1.0, 1).ToArray();
            Assert.AreEqual(1, a2.Length);
            Assert.AreEqual(0.0, a2[0]);

            var a3 = EnumerableUtil.RangeSteps(0.0, 1.0, 2).ToArray();
            Assert.AreEqual(2, a3.Length);
            Assert.AreEqual(0.0, a3[0]);
            Assert.AreEqual(1.0, a3[1]);

            var a4 = EnumerableUtil.RangeSteps(0.0, 1.0, 3).ToArray();
            Assert.AreEqual(3, a4.Length);
            Assert.AreEqual(0.0, a4[0]);
            Assert.AreEqual(0.5, a4[1]);
            Assert.AreEqual(1.0, a4[2]);

            var a5 = EnumerableUtil.RangeSteps(0.0, 1.0, 5).ToArray();
            Assert.AreEqual(5, a5.Length);
            Assert.AreEqual(0.0, a5[0]);
            Assert.AreEqual(0.25, a5[1]);
            Assert.AreEqual(0.50, a5[2]);
            Assert.AreEqual(0.75, a5[3]);
            Assert.AreEqual(1.0, a5[4]);
        }

        [TestMethod]
        public void ClampToRangeTest()
        {
            Assert.AreEqual(0.5, MathUtil.ClampToRange(0.5, 0.0, 1.0));
            Assert.AreEqual(0.0, MathUtil.ClampToRange(-0.1, 0.0, 1.0));
            Assert.AreEqual(1.0, MathUtil.ClampToRange(1.1, 0.0, 1.0)); //SS
        }
    }
}