using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace IsotopeTests
{
    [TestClass]
    public class RectangleTest
    {
        [TestMethod]
        public void FromBoundingBoxTest()
        {
            var points = new[]
                             {
                                 new Isotope.Drawing.Point(0, 0),
                                 new Isotope.Drawing.Point(0.5, 2),
                                 new Isotope.Drawing.Point(1, 0.7),
                                 new Isotope.Drawing.Point(0.3, 0.95)
                             };

            var bb = Isotope.Drawing.Rectangle.FromBoundingBox(points);
            Assert.AreEqual(0, bb.Left);
            Assert.AreEqual(1, bb.Right);
            Assert.AreEqual(0, bb.Bottom);
            Assert.AreEqual(2, bb.Top);
        }
    }
}