using ISD = Isotope.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    [TestClass]
    public class LineSegmentTest
    {
        [TestMethod]
        public void LineSegmentConstructorTest()
        {
            var lineseg1 = new ISD.LineSegment(new ISD.Point(0, 1), new ISD.Point(2, 3));
            Assert.AreEqual(new ISD.Point(0, 1), lineseg1.Start);
            Assert.AreEqual(new ISD.Point(2, 3), lineseg1.End);

            var lineseg2 = new ISD.LineSegment(new[] { new ISD.Point(0, 1), new ISD.Point(2, 3) });
            Assert.AreEqual(new ISD.Point(0, 1), lineseg2.Start);
            Assert.AreEqual(new ISD.Point(2, 3), lineseg2.End);

            var points = lineseg2.ToPoints();
            Assert.AreEqual(new ISD.Point(0, 1), points[0]);
            Assert.AreEqual(new ISD.Point(2, 3), points[1]);
        }
    }
}