using Isotope.Layout.Grid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    [TestClass]
    public class TestDrawingUtil
    {
        public bool check_grid(int n, Isotope.Drawing.Size s, int dcols, int drows)
        {
            int r, c;
            GridLayout.GetSquarestGrid(n, s, out c, out r);

            Assert.AreEqual(drows, r);
            Assert.AreEqual(dcols, c);

            return true;
        }

        [TestMethod]
        public void TestKnownCases()
        {
            Assert.IsTrue(check_grid(0, new Isotope.Drawing.Size(1, 1), 0, 0));
            Assert.IsTrue(check_grid(0, new Isotope.Drawing.Size(1, 2), 0, 0));
            Assert.IsTrue(check_grid(0, new Isotope.Drawing.Size(2, 1), 0, 0));
            Assert.IsTrue(check_grid(0, new Isotope.Drawing.Size(2, 2), 0, 0));
            Assert.IsTrue(check_grid(0, new Isotope.Drawing.Size(1, 3), 0, 0));
            Assert.IsTrue(check_grid(0, new Isotope.Drawing.Size(3, 1), 0, 0));
            Assert.IsTrue(check_grid(1, new Isotope.Drawing.Size(1, 1), 1, 1));
            Assert.IsTrue(check_grid(1, new Isotope.Drawing.Size(1, 2), 1, 1));
            Assert.IsTrue(check_grid(1, new Isotope.Drawing.Size(2, 1), 1, 1));
            Assert.IsTrue(check_grid(1, new Isotope.Drawing.Size(2, 2), 1, 1));
            Assert.IsTrue(check_grid(1, new Isotope.Drawing.Size(1, 3), 1, 1));
            Assert.IsTrue(check_grid(1, new Isotope.Drawing.Size(3, 1), 1, 1));
            Assert.IsTrue(check_grid(2, new Isotope.Drawing.Size(1, 1), 1, 2));
            Assert.IsTrue(check_grid(2, new Isotope.Drawing.Size(1, 2), 2, 1));
            Assert.IsTrue(check_grid(2, new Isotope.Drawing.Size(2, 1), 1, 2));
            Assert.IsTrue(check_grid(2, new Isotope.Drawing.Size(2, 2), 1, 2));
            Assert.IsTrue(check_grid(2, new Isotope.Drawing.Size(1, 3), 2, 1));
            Assert.IsTrue(check_grid(2, new Isotope.Drawing.Size(3, 1), 1, 2));
            Assert.IsTrue(check_grid(3, new Isotope.Drawing.Size(1, 1), 2, 2));
            Assert.IsTrue(check_grid(3, new Isotope.Drawing.Size(1, 2), 3, 1));
            Assert.IsTrue(check_grid(3, new Isotope.Drawing.Size(2, 1), 1, 3));
            Assert.IsTrue(check_grid(3, new Isotope.Drawing.Size(2, 2), 2, 2));
            Assert.IsTrue(check_grid(3, new Isotope.Drawing.Size(1, 3), 3, 1));
            Assert.IsTrue(check_grid(3, new Isotope.Drawing.Size(3, 1), 1, 3));
            Assert.IsTrue(check_grid(4, new Isotope.Drawing.Size(1, 1), 2, 2));
            Assert.IsTrue(check_grid(4, new Isotope.Drawing.Size(1, 2), 3, 2));
            Assert.IsTrue(check_grid(4, new Isotope.Drawing.Size(2, 1), 2, 3));
            Assert.IsTrue(check_grid(4, new Isotope.Drawing.Size(2, 2), 2, 2));
            Assert.IsTrue(check_grid(4, new Isotope.Drawing.Size(1, 3), 4, 1));
            Assert.IsTrue(check_grid(4, new Isotope.Drawing.Size(3, 1), 1, 4));
            Assert.IsTrue(check_grid(5, new Isotope.Drawing.Size(1, 1), 3, 2));
            Assert.IsTrue(check_grid(5, new Isotope.Drawing.Size(1, 2), 3, 2));
            Assert.IsTrue(check_grid(5, new Isotope.Drawing.Size(2, 1), 2, 3));
            Assert.IsTrue(check_grid(5, new Isotope.Drawing.Size(2, 2), 3, 2));
            Assert.IsTrue(check_grid(5, new Isotope.Drawing.Size(1, 3), 4, 2));
            Assert.IsTrue(check_grid(5, new Isotope.Drawing.Size(3, 1), 2, 4));
            Assert.IsTrue(check_grid(6, new Isotope.Drawing.Size(1, 1), 3, 2));
            Assert.IsTrue(check_grid(6, new Isotope.Drawing.Size(1, 2), 3, 2));
            Assert.IsTrue(check_grid(6, new Isotope.Drawing.Size(2, 1), 2, 3));
            Assert.IsTrue(check_grid(6, new Isotope.Drawing.Size(2, 2), 3, 2));
            Assert.IsTrue(check_grid(6, new Isotope.Drawing.Size(1, 3), 4, 2));
            Assert.IsTrue(check_grid(6, new Isotope.Drawing.Size(3, 1), 2, 4));
            Assert.IsTrue(check_grid(7, new Isotope.Drawing.Size(1, 1), 3, 3));
            Assert.IsTrue(check_grid(7, new Isotope.Drawing.Size(1, 2), 4, 2));
            Assert.IsTrue(check_grid(7, new Isotope.Drawing.Size(2, 1), 2, 4));
            Assert.IsTrue(check_grid(7, new Isotope.Drawing.Size(2, 2), 3, 3));
            Assert.IsTrue(check_grid(7, new Isotope.Drawing.Size(1, 3), 4, 2));
            Assert.IsTrue(check_grid(7, new Isotope.Drawing.Size(3, 1), 2, 4));
            Assert.IsTrue(check_grid(8, new Isotope.Drawing.Size(1, 1), 3, 3));
            Assert.IsTrue(check_grid(8, new Isotope.Drawing.Size(1, 2), 4, 2));
            Assert.IsTrue(check_grid(8, new Isotope.Drawing.Size(2, 1), 2, 4));
            Assert.IsTrue(check_grid(8, new Isotope.Drawing.Size(2, 2), 3, 3));
            Assert.IsTrue(check_grid(8, new Isotope.Drawing.Size(1, 3), 4, 2));
            Assert.IsTrue(check_grid(8, new Isotope.Drawing.Size(3, 1), 2, 4));
            Assert.IsTrue(check_grid(9, new Isotope.Drawing.Size(1, 1), 3, 3));
            Assert.IsTrue(check_grid(9, new Isotope.Drawing.Size(1, 2), 5, 2));
            Assert.IsTrue(check_grid(9, new Isotope.Drawing.Size(2, 1), 2, 5));
            Assert.IsTrue(check_grid(9, new Isotope.Drawing.Size(2, 2), 3, 3));
            Assert.IsTrue(check_grid(9, new Isotope.Drawing.Size(1, 3), 5, 2));
            Assert.IsTrue(check_grid(9, new Isotope.Drawing.Size(3, 1), 2, 5));
            Assert.IsTrue(check_grid(10, new Isotope.Drawing.Size(1, 1), 4, 3));
            Assert.IsTrue(check_grid(10, new Isotope.Drawing.Size(1, 2), 5, 2));
            Assert.IsTrue(check_grid(10, new Isotope.Drawing.Size(2, 1), 2, 5));
            Assert.IsTrue(check_grid(10, new Isotope.Drawing.Size(2, 2), 4, 3));
            Assert.IsTrue(check_grid(10, new Isotope.Drawing.Size(1, 3), 5, 2));
            Assert.IsTrue(check_grid(10, new Isotope.Drawing.Size(3, 1), 2, 5));
            Assert.IsTrue(check_grid(40, new Isotope.Drawing.Size(1, 0.5), 5, 9));
        }
    }
}