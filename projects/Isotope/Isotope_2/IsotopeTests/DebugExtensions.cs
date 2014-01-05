using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    internal static class DebugExtensions
    {
        public static void IsEqual(this Isotope.Drawing.Rectangle r0, double left, double bottom, double right,
                                   double top)
        {
            double delta = 0.0000001;
            Assert.AreEqual(r0.Left, left, delta);
            Assert.AreEqual(r0.Right, right, delta);
            Assert.AreEqual(r0.Top, top, delta);
            Assert.AreEqual(r0.Bottom, bottom, delta);
        }
    }
}