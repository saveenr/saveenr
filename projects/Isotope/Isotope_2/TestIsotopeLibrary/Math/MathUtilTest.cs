using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    [TestClass]
    public class MathUtilTest
    {
        /// <summary>
        ///A test for WrapAngle_0_1
        ///</summary>
        [TestMethod]
        public void WrapToRange_0_1Test()
        {
            double delta = 0.0000000000000005;
            Assert.AreEqual(0.0, Isotope.Math.MathUtil.WrapAngle_0_1(0.0), delta);
            Assert.AreEqual(0.5, Isotope.Math.MathUtil.WrapAngle_0_1(0.5), delta);
            Assert.AreEqual(1.0, Isotope.Math.MathUtil.WrapAngle_0_1(1.0), delta);
            Assert.AreEqual(0.1, Isotope.Math.MathUtil.WrapAngle_0_1(1.1), delta);
            Assert.AreEqual(0.5, Isotope.Math.MathUtil.WrapAngle_0_1(1.5), delta);
            Assert.AreEqual(0.0, Isotope.Math.MathUtil.WrapAngle_0_1(2.0), delta);
            Assert.AreEqual(0.2, Isotope.Math.MathUtil.WrapAngle_0_1(2.2), delta);

            Assert.AreEqual(0.0, Isotope.Math.MathUtil.WrapAngle_0_1(-0.0), delta);
            Assert.AreEqual(0.9, Isotope.Math.MathUtil.WrapAngle_0_1(-0.1), delta);
            Assert.AreEqual(0.8, Isotope.Math.MathUtil.WrapAngle_0_1(-2.2), delta);
            Assert.AreEqual(0.5, Isotope.Math.MathUtil.WrapAngle_0_1(-2.5), delta);
            Assert.AreEqual(0.3, Isotope.Math.MathUtil.WrapAngle_0_1(-3.7), delta);
            Assert.AreEqual(0.0, Isotope.Math.MathUtil.WrapAngle_0_1(-1.0), delta);
            Assert.AreEqual(0.9, Isotope.Math.MathUtil.WrapAngle_0_1(-1.1), delta);
            Assert.AreEqual(0.1, Isotope.Math.MathUtil.WrapAngle_0_1(-0.9), delta);
            Assert.AreEqual(0.0, Isotope.Math.MathUtil.WrapAngle_0_1(-2.0), delta);
        }

        /// <summary>
        ///A test for SnapToNearestValue
        ///</summary>
        [TestMethod]
        public void SnapToNearestValueTest()
        {
            Assert.AreEqual(0.0, Isotope.Math.MathUtil.Round(0.0, 0.5));

            Assert.AreEqual(0.0, Isotope.Math.MathUtil.Round(0.24, 0.5));
            Assert.AreEqual(0.5, Isotope.Math.MathUtil.Round(0.25, 0.5));
            Assert.AreEqual(0.5, Isotope.Math.MathUtil.Round(0.26, 0.5));

            Assert.AreEqual(0.0, Isotope.Math.MathUtil.Round(-0.24, 0.5));
            Assert.AreEqual(-0.5, Isotope.Math.MathUtil.Round(-0.25, 0.5));
            Assert.AreEqual(-0.5, Isotope.Math.MathUtil.Round(-0.251, 0.5));
            Assert.AreEqual(-0.5, Isotope.Math.MathUtil.Round(-0.5, 0.5));

            Assert.AreEqual(0.0, Isotope.Math.MathUtil.Round(0.0, 2.0));

            Assert.AreEqual(0.0, Isotope.Math.MathUtil.Round(0.0, 2.0));
            Assert.AreEqual(2.0, Isotope.Math.MathUtil.Round(1.0, 2.0));
            Assert.AreEqual(2.0, Isotope.Math.MathUtil.Round(2.0, 2.0));
            Assert.AreEqual(4.0, Isotope.Math.MathUtil.Round(3.0, 2.0));
            Assert.AreEqual(4.0, Isotope.Math.MathUtil.Round(4.0, 2.0));

            Assert.AreEqual(0.0, Isotope.Math.MathUtil.Round(-0.0, 2.0));
            Assert.AreEqual(-2.0, Isotope.Math.MathUtil.Round(-1.0, 2.0));
            Assert.AreEqual(-2.0, Isotope.Math.MathUtil.Round(-2.0, 2.0));
            Assert.AreEqual(-4.0, Isotope.Math.MathUtil.Round(-3.0, 2.0));
            Assert.AreEqual(-4.0, Isotope.Math.MathUtil.Round(-4.0, 2.0));
        }

        public void check_blend(double e, double i0, double i1, double ratio)
        {
            var result = Isotope.Math.MathUtil.Blend_0_1(i0, i1, ratio);
            Assert.AreEqual(e, result);
        }

        /// <summary>
        ///A test for Blend_0_1
        ///</summary>
        [TestMethod]
        public void Blend_0_1Test()
        {
            check_blend(1.0, 1.0, 1.0, 1.1);
            check_blend(1.0, 1.0, 1.0, 1.0);
            check_blend(1.0, 1.0, 1.0, 0.25);
            check_blend(1.0, 1.0, 1.0, 0.5);
            check_blend(1.0, 1.0, 1.0, 0.75);
            check_blend(1.0, 1.0, 1.0, 0.0);
            check_blend(1.0, 1.0, 1.0, -1.0);

            check_blend(1.0, 0, 1.0, 0.0);
            check_blend(0.5, 0, 1.0, 0.5);
            check_blend(0.0, 0, 1.0, 1.0);

            check_blend(10.0, 0, 10.0, 0.0);
            check_blend(4, 0, 8.0, 0.5);
            check_blend(1 + 6, 4, 8, 0.25);
        }
    }
}