using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    [TestClass]
    public class BoxHierarchyLayoutTest
    {
        [TestMethod]
        public void Test1()
        {
            var layout =
                new Isotope.Layout.BoxHierarchy.BoxHierarchyLayout<object>(
                    Isotope.Layout.BoxHierarchy.LayoutDirection.Vertical);

            var g0 = layout.Root;
            g0.AlignmentHorizontal = Isotope.Drawing.AlignmentHorizontal.Right;

            var g1 = g0.AddNode(Isotope.Layout.BoxHierarchy.LayoutDirection.Vertical);
            g1.AlignmentHorizontal = Isotope.Drawing.AlignmentHorizontal.Center;
            g1.AddNode(1, 0.25);
            g1.AddNode(1.25, 0.25);
            g1.AddNode(1.50, 0.25);
            g1.AddNode(1.75, 0.25);
            g1.AddNode(2, 0.25);

            var g2 = g0.AddNode(Isotope.Layout.BoxHierarchy.LayoutDirection.Horizontal);
            g2.AlignmentVertical = Isotope.Drawing.AlignmentVertical.Center;
            g2.AddNode(0.25, 0.26);
            g2.AddNode(3.5, 0.5);
            g2.AddNode(0.5, 0.5);
            g2.AddNode(0.5, 0.6);
            g2.AddNode(0.5, 0.7);
            g2.AddNode(0.5, 0.8);

            var g22 = g2.AddNode(Isotope.Layout.BoxHierarchy.LayoutDirection.Vertical);
            g22.AddNode(0.30, 0.25);
            g22.AddNode(0.25, 0.25);
            g22.AddNode(0.20, 0.25);
            g22.AddNode(0.15, 0.25);

            var origin = new Isotope.Drawing.Point(0, 0);

            layout.PerformLayout(origin);

            var options = new Isotope.Layout.BoxHierarchy.RenderOptions<object>();
            options.RenderAction = (node, rect) => drawnode(node, rect, null);
            layout.Render(options);
        }

        private static void drawnode(Isotope.Layout.BoxHierarchy.Node<object> node, Isotope.Drawing.Rectangle rect,
                                     object data)
        {
        }

        [TestMethod]
        public void Test2()
        {
            var layout =
                new Isotope.Layout.BoxHierarchy.BoxHierarchyLayout<object>(
                    Isotope.Layout.BoxHierarchy.LayoutDirection.Vertical);

            var g0 = layout.Root;

            var g1 = g0.AddNode(Isotope.Layout.BoxHierarchy.LayoutDirection.Vertical);
            var b11 = g1.AddNode(2, 1, Isotope.Drawing.AlignmentHorizontal.Left);
            var b12 = g1.AddNode(2, 1, Isotope.Drawing.AlignmentHorizontal.Center);
            var b13 = g1.AddNode(2, 1, Isotope.Drawing.AlignmentHorizontal.Right);
            var b14 = g1.AddNode(6, 1);

            var g2 = g0.AddNode(Isotope.Layout.BoxHierarchy.LayoutDirection.Horizontal);
            var b21 = g2.AddNode(1, 2, Isotope.Drawing.AlignmentVertical.Bottom);
            var b22 = g2.AddNode(1, 2, Isotope.Drawing.AlignmentVertical.Center);
            var b23 = g2.AddNode(1, 2, Isotope.Drawing.AlignmentVertical.Top);
            var b24 = g2.AddNode(1, 6);

            var origin = new Isotope.Drawing.Point(0, 0);

            layout.LayoutOptions.DirectionHorizontal = Isotope.Drawing.DirectionHorizontal.Right;
            layout.LayoutOptions.DirectionVertical = Isotope.Drawing.DirectionVertical.Up;
            layout.PerformLayout(origin);

            Assert.AreEqual(new Isotope.Drawing.Rectangle(0, 0, 2, 1), b11.Rectangle);
            Assert.AreEqual(new Isotope.Drawing.Rectangle(2, 1, 4, 2), b12.Rectangle);
            Assert.AreEqual(new Isotope.Drawing.Rectangle(4, 2, 6, 3), b13.Rectangle);
            Assert.AreEqual(new Isotope.Drawing.Rectangle(0, 0, 6, 4), g1.Rectangle);

            Assert.AreEqual(new Isotope.Drawing.Rectangle(0, 0, 6, 10), g0.Rectangle);

            Assert.AreEqual(new Isotope.Drawing.Rectangle(0, 4, 1, 6), b21.Rectangle);
            Assert.AreEqual(new Isotope.Drawing.Rectangle(1, 6, 2, 8), b22.Rectangle);
            Assert.AreEqual(new Isotope.Drawing.Rectangle(2, 8, 3, 10), b23.Rectangle);
            Assert.AreEqual(new Isotope.Drawing.Rectangle(0, 4, 4, 10), g2.Rectangle);
        }

        [TestMethod]
        public void Test3()
        {
            var layout =
                new Isotope.Layout.BoxHierarchy.BoxHierarchyLayout<object>(
                    Isotope.Layout.BoxHierarchy.LayoutDirection.Vertical);

            var g0 = layout.Root;

            var g1 = g0.AddNode(Isotope.Layout.BoxHierarchy.LayoutDirection.Vertical);
            g1.Width = 6;

            var b11 = g1.AddNode(2, 1, Isotope.Drawing.AlignmentHorizontal.Left);
            var b12 = g1.AddNode(2, 1, Isotope.Drawing.AlignmentHorizontal.Center);
            var b13 = g1.AddNode(2, 1, Isotope.Drawing.AlignmentHorizontal.Right);

            var g2 = g0.AddNode(Isotope.Layout.BoxHierarchy.LayoutDirection.Horizontal);
            g2.Height = 6;
            var b21 = g2.AddNode(1, 2, Isotope.Drawing.AlignmentVertical.Bottom);
            var b22 = g2.AddNode(1, 2, Isotope.Drawing.AlignmentVertical.Center);
            var b23 = g2.AddNode(1, 2, Isotope.Drawing.AlignmentVertical.Top);

            var origin = new Isotope.Drawing.Point(0, 0);

            layout.LayoutOptions.DirectionHorizontal = Isotope.Drawing.DirectionHorizontal.Right;
            layout.LayoutOptions.DirectionVertical = Isotope.Drawing.DirectionVertical.Up;
            layout.PerformLayout(origin);

            Assert.AreEqual(new Isotope.Drawing.Rectangle(0, 0, 6, 3), g1.Rectangle);
            Assert.AreEqual(new Isotope.Drawing.Rectangle(0, 0, 2, 1), b11.Rectangle);
            Assert.AreEqual(new Isotope.Drawing.Rectangle(2, 1, 4, 2), b12.Rectangle);
            Assert.AreEqual(new Isotope.Drawing.Rectangle(4, 2, 6, 3), b13.Rectangle);

            Assert.AreEqual(new Isotope.Drawing.Rectangle(0, 3, 3, 9), g2.Rectangle);

            Assert.AreEqual(new Isotope.Drawing.Rectangle(0, 0, 6, 9), g0.Rectangle);

            Assert.AreEqual(new Isotope.Drawing.Rectangle(0, 3, 1, 5), b21.Rectangle);
            Assert.AreEqual(new Isotope.Drawing.Rectangle(1, 5, 2, 7), b22.Rectangle);
            Assert.AreEqual(new Isotope.Drawing.Rectangle(2, 7, 3, 9), b23.Rectangle);
        }

        [TestMethod]
        public void Test4()
        {
            var layout =
                new Isotope.Layout.BoxHierarchy.BoxHierarchyLayout<object>(
                    Isotope.Layout.BoxHierarchy.LayoutDirection.Vertical);

            var g0 = layout.Root;
            g0.AlignmentHorizontal = Isotope.Drawing.AlignmentHorizontal.Right;
            g0.Padding = 0.5;

            var g1 = g0.AddNode(Isotope.Layout.BoxHierarchy.LayoutDirection.Vertical);
            g1.AlignmentHorizontal = Isotope.Drawing.AlignmentHorizontal.Center;
            g1.Padding = 0.25;
            g1.ChildSeparation = 0.25;
            var n1 = g1.AddNode(1, 0.25, Isotope.Drawing.AlignmentHorizontal.Left);
            var n2 = g1.AddNode(1.25, 0.25, Isotope.Drawing.AlignmentHorizontal.Center);
            var n3 = g1.AddNode(1.50, 0.25, Isotope.Drawing.AlignmentHorizontal.Right);
            var n4 = g1.AddNode(2, 0.25);

            var g2 = g0.AddNode(Isotope.Layout.BoxHierarchy.LayoutDirection.Horizontal);
            g2.AlignmentVertical = Isotope.Drawing.AlignmentVertical.Center;
            g2.Padding = 0.10;
            g2.ChildSeparation = 0.05;
            var n5 = g2.AddNode(0.25, 0.26, Isotope.Drawing.AlignmentVertical.Top);
            var n6 = g2.AddNode(3.5, 0.5, Isotope.Drawing.AlignmentVertical.Center);
            var n7 = g2.AddNode(0.5, 0.5, Isotope.Drawing.AlignmentVertical.Bottom);

            var g3 = g2.AddNode(Isotope.Layout.BoxHierarchy.LayoutDirection.Vertical);
            g3.Padding = 0.25;
            g3.ChildSeparation = 0.20;
            var n8 = g3.AddNode(0.30, 0.25, Isotope.Drawing.AlignmentHorizontal.Right);
            var n9 = g3.AddNode(0.25, 0.25, Isotope.Drawing.AlignmentHorizontal.Center);
            var n10 = g3.AddNode(0.20, 0.25, Isotope.Drawing.AlignmentHorizontal.Left);

            var origin = new Isotope.Drawing.Point(0, 0);

            //layout.LayoutOptions.DirectionVertical = Isotope.Drawing.DirectionVertical.Down;
            //layout.LayoutOptions.DirectionHorizontal = Isotope.Drawing.DirectionHorizontal.Left;
            layout.PerformLayout(origin);

            var options = new Isotope.Layout.BoxHierarchy.RenderOptions<object>();
            options.RenderAction = (node, rect) => drawnode(node, rect, null);
            layout.Render(options);

            g0.Rectangle.IsEqual(0, 0, 7.1, 5.1);
            g1.Rectangle.IsEqual(2.3, 0.5, 4.8, 2.75);
            g2.Rectangle.IsEqual(0.5, 2.75, 6.6, 4.6);
            g3.Rectangle.IsEqual(5, 2.85, 6.5, 4.5);

            n1.Rectangle.IsEqual(2.55, 0.75, 3.55, 1);
            n2.Rectangle.IsEqual(2.925, 1.25, 4.175, 1.5);
            n3.Rectangle.IsEqual(3.05, 1.75, 4.55, 2);
            n4.Rectangle.IsEqual(2.55, 2.25, 4.55, 2.5);

            n5.Rectangle.IsEqual(0.6, 4.24, 0.85, 4.5);
            n6.Rectangle.IsEqual(0.9, 3.425, 4.4, 3.925);
            n7.Rectangle.IsEqual(4.45, 2.85, 4.95, 3.35);
            n8.Rectangle.IsEqual(5.95, 3.1, 6.25, 3.35);
            n9.Rectangle.IsEqual(5.625, 3.55, 5.875, 3.8);
            n10.Rectangle.IsEqual(5.25, 4, 5.45, 4.25);
        }
    }
}