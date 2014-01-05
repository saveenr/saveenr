using System.Linq;
using Isotope.Collections.Ranges;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    [TestClass]
    public class TestSparseRanges
    {
        /// <summary>
        /// Empty ranges are allowed to be created
        ///</summary>
        [TestMethod]
        public void TestEmptyRanges()
        {
            var r1 = IntRange.FromInteger(0);
            var r2 = IntRange.FromInteger(1);
            var r3 = IntRange.FromInteger(-1);

            Assert.AreEqual(1, r1.Length);
            Assert.AreEqual(1, r2.Length);
            Assert.AreEqual(1, r2.Length);
            Assert.AreEqual(1, r2.Length);

            Assert.IsTrue(r1.Touches(r2));
            Assert.IsTrue(r2.Touches(r1));
            Assert.IsTrue(r1.Touches(r3));
            Assert.IsTrue(r3.Touches(r1));
            Assert.IsTrue(!r2.Touches(r3));
            Assert.IsTrue(!r3.Touches(r2));

            Assert.IsFalse(r1.Intersects(r2));
            Assert.IsFalse(r2.Intersects(r3));
            Assert.IsFalse(r1.Intersects(r3));
            Assert.IsFalse(r3.Intersects(r2));
            Assert.IsFalse(r3.Intersects(r1));
            Assert.IsFalse(r2.Intersects(r1));
        }

        [TestMethod]
        public void TestEmptyRangesTouches()
        {
            var r1 = IntRange.FromEndpoints(3, 4);
            var r2 = IntRange.FromEndpoints(2, 2);

            Assert.IsFalse(r1.Intersects(r2));
            Assert.IsFalse(r2.Intersects(r1));
            Assert.IsTrue(r2.Touches(r1));
            Assert.IsTrue(r1.Touches(r2));
        }

        /// <summary>
        /// An empty mergedrangecollection has no length and does not contain any thing
        ///</summary>
        [TestMethod]
        public void TestEmptyRangeCOllection()
        {
            var rc = new SparseIntRange();
            Assert.IsFalse(rc.Contains(0));
            Assert.AreEqual(0, rc.Count);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var rc = new SparseIntRange();
            Assert.AreEqual(0, rc.RangeCount);
            rc.Add(1);
            Assert.AreEqual(1, rc.RangeCount);
            Assert.IsFalse(rc.Contains(0));
            Assert.IsTrue(rc.Contains(1));
            Assert.IsFalse(rc.Contains(2));
            Assert.AreEqual(1, rc.Count);
            Assert.AreEqual(1, rc.RangeCount);
        }

        [TestMethod]
        public void TestMethodBuildingRange()
        {
            var rc = new SparseIntRange();
            Assert.AreEqual(0, rc.RangeCount);
            // rc = [] -> []
            rc.Add(1);
            // rc = [(1,1)] -> [1]

            Assert.AreEqual(1, rc.RangeCount);
            Assert.IsFalse(rc.Contains(0));
            Assert.IsTrue(rc.Contains(1));
            Assert.IsFalse(rc.Contains(2));
            Assert.AreEqual(1, rc.Count);
            Assert.AreEqual(1, rc.Lower);
            Assert.AreEqual(1, rc.Upper);
            rc.AddInclusive(3, 4);
            // rc = [(1,1),(3,4)] -> [1,3,4]
            Assert.AreEqual(2, rc.RangeCount);
            Assert.IsFalse(rc.Contains(0));
            Assert.IsTrue(rc.Contains(1));
            Assert.IsFalse(rc.Contains(2));
            Assert.IsTrue(rc.Contains(3));
            Assert.IsTrue(rc.Contains(4));
            Assert.IsFalse(rc.Contains(5));
            Assert.AreEqual(3, rc.Count);
            Assert.AreEqual(1, rc.Lower);
            Assert.AreEqual(4, rc.Upper);

            rc.Add(2);
            // rc = [(1,4)] -> [1,2,3,4]

            Assert.AreEqual(1, rc.RangeCount);
            Assert.IsFalse(rc.Contains(0));
            Assert.IsTrue(rc.Contains(1));
            Assert.IsTrue(rc.Contains(2));
            Assert.IsTrue(rc.Contains(3));
            Assert.IsTrue(rc.Contains(4));
            Assert.IsFalse(rc.Contains(5));
            Assert.AreEqual(4, rc.Count);
            Assert.AreEqual(1, rc.Lower);
            Assert.AreEqual(4, rc.Upper);
        }

        [TestMethod]
        public void TestMethodShrinkingRange()
        {
            var rc = new SparseIntRange();
            rc.AddInclusive(1, 4);
            // rc = [(1,4)] -> [1,2,3,4]
            Assert.AreEqual(1, rc.RangeCount);
            Assert.IsFalse(rc.Contains(0));
            Assert.IsTrue(rc.Contains(1));
            Assert.IsTrue(rc.Contains(2));
            Assert.IsTrue(rc.Contains(3));
            Assert.IsTrue(rc.Contains(4));
            Assert.IsFalse(rc.Contains(5));
            Assert.AreEqual(4, rc.Count);
            Assert.AreEqual(1, rc.Lower);
            Assert.AreEqual(4, rc.Upper);

            rc.Remove(IntRange.FromInteger(-2));
            // rc = [(1,4)] -> [1,2,3,4]
            Assert.AreEqual(1, rc.RangeCount);
            Assert.IsFalse(rc.Contains(0));
            Assert.IsTrue(rc.Contains(1));
            Assert.IsTrue(rc.Contains(2));
            Assert.IsTrue(rc.Contains(3));
            Assert.IsTrue(rc.Contains(4));
            Assert.IsFalse(rc.Contains(5));
            Assert.AreEqual(4, rc.Count);
            Assert.AreEqual(1, rc.Lower);
            Assert.AreEqual(4, rc.Upper);

            rc.Remove(IntRange.FromInteger(2));
            // rc = [(1,1),(3,4)] -> [1,3,4]
            Assert.AreEqual(2, rc.RangeCount);
            Assert.IsFalse(rc.Contains(0));
            Assert.IsTrue(rc.Contains(1));
            Assert.IsFalse(rc.Contains(2));
            Assert.IsTrue(rc.Contains(3));
            Assert.IsTrue(rc.Contains(4));
            Assert.IsFalse(rc.Contains(5));
            Assert.AreEqual(3, rc.Count);
            Assert.AreEqual(1, rc.Lower);
            Assert.AreEqual(4, rc.Upper);

            rc.RemoveInclusive(3, 4);
            // rc = [(1,1)] -> [1]
            Assert.AreEqual(1, rc.RangeCount);
            Assert.IsFalse(rc.Contains(0));
            Assert.IsTrue(rc.Contains(1));
            Assert.IsFalse(rc.Contains(2));
            Assert.AreEqual(1, rc.Count);
            Assert.AreEqual(1, rc.Lower);
            Assert.AreEqual(1, rc.Upper);

            rc.Remove(1);
            // rc = [] -> []

            Assert.AreEqual(0, rc.RangeCount);
            Assert.IsFalse(rc.Contains(0));
            Assert.IsFalse(rc.Contains(1));
            Assert.IsFalse(rc.Contains(2));
            Assert.AreEqual(0, rc.Count);
        }
    }

    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class TestMergingDoubleRanges
    {
        public TestMergingDoubleRanges()
        {
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        /// <summary>
        /// Empty ranges are allowed to be created
        ///</summary>
        [TestMethod]
        public void TestEmptyRanges()
        {
            var r1 = DoubleRange.FromEndpoints(0, 1);
            var r2 = DoubleRange.FromEndpoints(1, 2);
            var r3 = DoubleRange.FromEndpoints(-1, 0);

            Assert.AreEqual(1, r1.Length);
            Assert.AreEqual(1, r2.Length);
            Assert.AreEqual(1, r3.Length);

            Assert.IsTrue(r1.Touches(r2));
            Assert.IsTrue(r2.Touches(r1));
            Assert.IsTrue(r1.Touches(r3));
            Assert.IsTrue(r3.Touches(r1));
            Assert.IsTrue(!r2.Touches(r3));
            Assert.IsTrue(!r3.Touches(r2));

            Assert.IsFalse(r1.IntersectsExclusive(r2));
            Assert.IsFalse(r2.IntersectsExclusive(r3));
            Assert.IsFalse(r1.IntersectsExclusive(r3));
            Assert.IsFalse(r3.IntersectsExclusive(r2));
            Assert.IsFalse(r3.IntersectsExclusive(r1));
            Assert.IsFalse(r2.IntersectsExclusive(r1));
        }

        [TestMethod]
        public void TestEmptyRangesTouches()
        {
            var r1 = DoubleRange.FromEndpoints(3, 4);
            var r2 = DoubleRange.FromEndpoints(2, 2);

            Assert.IsFalse(r1.IntersectsExclusive(r2));
            Assert.IsFalse(r2.IntersectsExclusive(r1));
            Assert.IsFalse(r2.Touches(r1));
            Assert.IsFalse(r1.Touches(r2));
        }

        /// <summary>
        /// An empty mergedrangecollection has no length and does not contain any thing
        ///</summary>
        [TestMethod]
        public void TestEmptyRangeCOllection()
        {
            var rc = new SparseDoubleRange();
            Assert.IsFalse(rc.Contains(0));
            Assert.AreEqual(0, rc.Count);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var rc = new SparseDoubleRange();
            Assert.AreEqual(0, rc.RangeCount);
            rc.AddInclusive(1, 2);
            Assert.IsFalse(rc.Contains(0));
            Assert.IsTrue(rc.Contains(1));
            Assert.IsTrue(rc.Contains(1.5));
            Assert.IsTrue(rc.Contains(2));
            Assert.IsFalse(rc.Contains(2.5));
            Assert.AreEqual(1, rc.Count);
            Assert.AreEqual(1, rc.RangeCount);
        }

        [TestMethod]
        public void TestMethodBuildingRange()
        {
            var rc = new SparseDoubleRange();
            Assert.AreEqual(0, rc.RangeCount);
            // rc = [] 
            rc.AddInclusive(1, 2);
            // rc = [(1,2)] 

            Assert.AreEqual(1, rc.RangeCount);
            Assert.IsFalse(rc.Contains(0));
            Assert.IsTrue(rc.Contains(1));
            Assert.IsTrue(rc.Contains(2));
            Assert.AreEqual(1, rc.Count);
            Assert.AreEqual(1, rc.Lower);
            Assert.AreEqual(2, rc.Upper);
            rc.AddInclusive(3, 4);
            // rc = [(1,2),(3,4)] 
            Assert.AreEqual(2, rc.RangeCount);
            Assert.IsFalse(rc.Contains(0));
            Assert.IsTrue(rc.Contains(1));
            Assert.IsTrue(rc.Contains(2));
            Assert.IsFalse(rc.Contains(2.5));
            Assert.IsTrue(rc.Contains(3));
            Assert.IsTrue(rc.Contains(4));
            Assert.IsFalse(rc.Contains(4.5));
            Assert.AreEqual(2, rc.Count);
            Assert.AreEqual(1, rc.Lower);
            Assert.AreEqual(4, rc.Upper);

            rc.AddInclusive(2, 3);
            // rc = [(1,4)] -> [1,2,3,4]

            Assert.AreEqual(1, rc.RangeCount);
            Assert.IsFalse(rc.Contains(0));
            Assert.IsTrue(rc.Contains(1));
            Assert.IsTrue(rc.Contains(2));
            Assert.IsTrue(rc.Contains(3));
            Assert.IsTrue(rc.Contains(4));
            Assert.IsFalse(rc.Contains(5));
            Assert.AreEqual(3, rc.Count);
            Assert.AreEqual(1, rc.Lower);
            Assert.AreEqual(4, rc.Upper);
        }

        [TestMethod]
        public void TestMethodShrinkingRange()
        {
            var rc = new SparseDoubleRange();
            rc.AddInclusive(1, 4);
            // rc = [(1,4)] -> [1,2,3,4]
            Assert.AreEqual(1, rc.RangeCount);
            Assert.IsFalse(rc.Contains(0));
            Assert.IsTrue(rc.Contains(1));
            Assert.IsTrue(rc.Contains(2));
            Assert.IsTrue(rc.Contains(3));
            Assert.IsTrue(rc.Contains(4));
            Assert.IsFalse(rc.Contains(5));
            Assert.AreEqual(3, rc.Count);
            Assert.AreEqual(1, rc.Lower);
            Assert.AreEqual(4, rc.Upper);

            rc.RemoveInclusive(DoubleRange.FromEndpoints(-2, -1));
            // rc = [(1,4)] -> [1,2,3,4]
            Assert.AreEqual(1, rc.RangeCount);
            Assert.IsFalse(rc.Contains(0));
            Assert.IsTrue(rc.Contains(1));
            Assert.IsTrue(rc.Contains(2));
            Assert.IsTrue(rc.Contains(3));
            Assert.IsTrue(rc.Contains(4));
            Assert.IsFalse(rc.Contains(5));
            Assert.AreEqual(3, rc.Count);
            Assert.AreEqual(1, rc.Lower);
            Assert.AreEqual(4, rc.Upper);

            rc.RemoveExclusive(2, 3);
            // rc = [(1,2),(3,4)] 
            Assert.AreEqual(2, rc.RangeCount);
            Assert.IsFalse(rc.Contains(0));
            Assert.IsTrue(rc.Contains(1));
            Assert.IsTrue(rc.Contains(2));
            Assert.IsFalse(rc.Contains(2.5));
            Assert.IsTrue(rc.Contains(3));
            Assert.IsTrue(rc.Contains(4));
            Assert.IsFalse(rc.Contains(4.5));
            Assert.AreEqual(2, rc.Count);
            Assert.AreEqual(1, rc.Lower);
            Assert.AreEqual(4, rc.Upper);

            rc.RemoveInclusive(3, 4);
            // rc = [(1,2)] 
            Assert.AreEqual(1, rc.RangeCount);
            Assert.IsFalse(rc.Contains(0));
            Assert.IsTrue(rc.Contains(1));
            Assert.IsTrue(rc.Contains(2));
            Assert.AreEqual(1, rc.Count);
            Assert.AreEqual(1, rc.Lower);
            Assert.AreEqual(2, rc.Upper);

            rc.RemoveInclusive(1, 2);
            // rc = [] -> []

            Assert.AreEqual(0, rc.RangeCount);
            Assert.IsFalse(rc.Contains(0));
            Assert.IsFalse(rc.Contains(1));
            Assert.IsFalse(rc.Contains(2));
        }

        [TestMethod]
        public void TestMethod_GetValues()
        {
            var r = DoubleRange.FromEndpoints(1.5, 3.0);

            var ax = r.GetValues(2).ToList();
            Assert.AreEqual(1, ax.Count);
            Assert.AreEqual(1.5, ax[0]);

            var a0 = r.GetValues(1.5).ToList();
            Assert.AreEqual(2, a0.Count);
            Assert.AreEqual(1.5, a0[0]);
            Assert.AreEqual(3.0, a0[1]);

            var a1 = r.GetValues(1.0).ToList();
            Assert.AreEqual(2, a1.Count);
            Assert.AreEqual(1.5, a1[0]);
            Assert.AreEqual(2.5, a1[1]);

            var a2 = r.GetValues(0.5).ToList();
            Assert.AreEqual(4, a2.Count);
            Assert.AreEqual(1.5, a2[0]);
            Assert.AreEqual(2.0, a2[1]);
            Assert.AreEqual(2.5, a2[2]);
            Assert.AreEqual(3.0, a2[3]);
        }

        [TestMethod]
        public void TestMethod_GetValues2x()
        {
            var r = DoubleRange.FromEndpoints(1.0, 2.3);

            var a0 = r.GetValues(0.5).ToList();
            Assert.AreEqual(3, a0.Count);
            Assert.AreEqual(1, a0[0]);
            Assert.AreEqual(1.5, a0[1]);
            Assert.AreEqual(2.0, a0[2]);
        }

        [TestMethod]
        public void TestMethod_GetValues2()
        {
            var r = new SparseDoubleRange();
            r.AddInclusive(1, 2.3);
            r.AddInclusive(4, 4.5);

            var ax = r.Values.ToList();
            Assert.AreEqual(3, ax.Count);
            Assert.AreEqual(1, ax[0]);
            Assert.AreEqual(2, ax[1]);
            Assert.AreEqual(4, ax[2]);

            var ay = r.GetValues(0.5).ToList();
            Assert.AreEqual(5, ay.Count);
            Assert.AreEqual(1, ay[0]);
            Assert.AreEqual(1.5, ay[1]);
            Assert.AreEqual(2.0, ay[2]);

            Assert.AreEqual(4.0, ay[3]);
            Assert.AreEqual(4.5, ay[4]);
        }
    }
}