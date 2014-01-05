using Isotope.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace IsotopeTests
{
    [TestClass]
    public class EnumerableUtilTest
    {
        [TestMethod]
        public void GroupByCountTest_Fixed_1()
        {
            var items = new int[] {100, 200, 300, 400, 500, 600, 700, 800, 900, 1000};

            var grouped_items =
                EnumerableUtil.GroupByCount(items, 1, size => new List<int>(size), (list, count, item) => list.Add(item))
                    .ToList();
            Assert.AreEqual(10, grouped_items.Count);
            Assert.AreEqual(10, grouped_items.Select(g => g.Count()).Sum());
        }

        [TestMethod]
        public void GroupByCountTest_Fixed_2()
        {
            var items = new int[] {100, 200, 300, 400, 500, 600, 700, 800, 900, 1000};
            var grouped_items =
                EnumerableUtil.GroupByCount(items, 2, size => new List<int>(size), (list, count, item) => list.Add(item))
                    .ToList();
            Assert.AreEqual(5, grouped_items.Count);
            Assert.AreEqual(10, grouped_items.Select(g => g.Count()).Sum());
        }

        [TestMethod]
        public void GroupByCountTest_Fixed_3()
        {
            var items = new int[] {100, 200, 300, 400, 500, 600, 700, 800, 900, 1000};
            var grouped_items =
                EnumerableUtil.GroupByCount(items, 7, size => new List<int>(size), (list, count, item) => list.Add(item))
                    .ToList();

            Assert.AreEqual(2, grouped_items.Count);
            Assert.AreEqual(10, grouped_items.Select(g => g.Count()).Sum());
            Assert.AreEqual(7, grouped_items[0].Count);
            Assert.AreEqual(3, grouped_items[1].Count);
        }

        [TestMethod]
        public void GroupByCountTest_Fixed_4()
        {
            var items = new int[] {100, 200, 300, 400, 500, 600, 700, 800, 900, 1000};
            var grouped_items =
                EnumerableUtil.GroupByCount(items, 10, size => new List<int>(size),
                                            (list, count, item) => list.Add(item)).ToList();

            Assert.AreEqual(1, grouped_items.Count);
            Assert.AreEqual(10, grouped_items.Select(g => g.Count()).Sum());
            Assert.AreEqual(10, grouped_items[0].Count);
        }

        [TestMethod]
        public void GroupByCountTest_Dynamic_1()
        {
            var items = new int[] {100, 200, 300, 400, 500, 600, 700, 800, 900, 1000};
            var sizes = new int[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1};

            var grouped_items =
                EnumerableUtil.GroupByCount(items, sizes, size => new List<int>(size),
                                            (list, index, item) => list.Add(item)).ToList();
            Assert.AreEqual(10, grouped_items.Count);
            Assert.AreEqual(10, grouped_items.Select(g => g.Count()).Sum());
        }

        [TestMethod]
        public void GroupByCountTest_Dynamic_2()
        {
            bool caught = false;
            var items = new int[] {100, 200, 300, 400, 500, 600, 700, 800, 900, 1000};
            var sizes = new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 1};
            try{var grouped_items =
                EnumerableUtil.GroupByCount(items, sizes, size => new List<int>(size),
                                            (list, index, item) => list.Add(item)).ToList();}
            catch (System.ArgumentException )
            {
                caught = true;
            }

            if (caught == false)
            {
                Assert.Fail("Did not catch expected exception");
            }
        }

        [TestMethod]
        public void GroupByCountTest_Dynamic_3()
        {
            bool caught = false;
            var items = new int[] {100, 200, 300, 400, 500, 600, 700, 800, 900, 1000};
            var sizes = new int[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 2};
            try{var grouped_items =
                EnumerableUtil.GroupByCount(items, sizes, size => new List<int>(size),
                                            (list, index, item) => list.Add(item)).ToList();}
            catch (System.ArgumentException )
            {
                caught = true;
            }

            if (caught == false)
            {
                Assert.Fail("Did not catch expected exception");
            }
        }

        [TestMethod]
        public void GroupByCountTest_Dynamic_4()
        {
            var items = new int[] {100, 200, 300, 400, 500, 600, 700, 800, 900, 1000};
            var sizes = new int[] {3, 0, 0, 0, 0, 0, 0, 0, 0, 7};
            var grouped_items =
                EnumerableUtil.GroupByCount(items, sizes, size => new List<int>(size),
                                            (list, index, item) => list.Add(item)).ToList();

            Assert.AreEqual(10, grouped_items.Count);
            Assert.AreEqual(10, grouped_items.Select(g => g.Count()).Sum());
            Assert.AreEqual(3, grouped_items[0].Count);
            Assert.AreEqual(0, grouped_items[1].Count);
            Assert.AreEqual(0, grouped_items[2].Count);
            Assert.AreEqual(0, grouped_items[3].Count);
            Assert.AreEqual(0, grouped_items[4].Count);
            Assert.AreEqual(0, grouped_items[5].Count);
            Assert.AreEqual(0, grouped_items[6].Count);
            Assert.AreEqual(0, grouped_items[7].Count);
            Assert.AreEqual(0, grouped_items[8].Count);
            Assert.AreEqual(7, grouped_items[9].Count);
            Assert.AreEqual(100, grouped_items[0][0]);
            Assert.AreEqual(200, grouped_items[0][1]);
            Assert.AreEqual(300, grouped_items[0][2]);
            Assert.AreEqual(400, grouped_items[9][0]);
            Assert.AreEqual(500, grouped_items[9][1]);
            Assert.AreEqual(600, grouped_items[9][2]);
            Assert.AreEqual(700, grouped_items[9][3]);
            Assert.AreEqual(800, grouped_items[9][4]);
            Assert.AreEqual(900, grouped_items[9][5]);
            Assert.AreEqual(1000, grouped_items[9][6]);
        }

        [TestMethod]
        public void BucketizeTest_1()
        {
            int[] a = new int[] {0, 1, 2, 3, 4, 5, 6, 7};
            var dic = EnumerableUtil.Bucketize(a, i => i%3);
            Assert.AreEqual(3, dic.Keys.Count);
            Assert.AreEqual(0, dic.Keys.ElementAt(0));
            Assert.AreEqual(1, dic.Keys.ElementAt(1));
            Assert.AreEqual(2, dic.Keys.ElementAt(2));

            Assert.AreEqual(0, dic[0].ElementAt(0));
            Assert.AreEqual(3, dic[0].ElementAt(1));
            Assert.AreEqual(6, dic[0].ElementAt(2));

            Assert.AreEqual(1, dic[1].ElementAt(0));
            Assert.AreEqual(4, dic[1].ElementAt(1));
            Assert.AreEqual(7, dic[1].ElementAt(2));

            Assert.AreEqual(2, dic[2].ElementAt(0));
            Assert.AreEqual(5, dic[2].ElementAt(1));
        }

        [TestMethod]
        public void Histogram_1()
        {
            var a1 = new string[] { "0", "1", "2", "1" };
            var dic1 = EnumerableUtil.Histogram(a1);
            Assert.AreEqual(3, dic1.Keys.Count);
            Assert.AreEqual(1, dic1["0"]);
            Assert.AreEqual(2, dic1["1"]);
            Assert.AreEqual(1, dic1["2"]);

            var a2 = new [] { 0, 1, 2, 3, 4, 5, 6, 7 };
            var dic2 = EnumerableUtil.Histogram(a2, i=>i%3, null);
            Assert.AreEqual(3, dic2.Keys.Count);
            Assert.AreEqual(3, dic2[0]);
            Assert.AreEqual(3, dic2[1]);
            Assert.AreEqual(2, dic2[2]);
            Assert.AreEqual(a2.Length , dic2.Keys.Select( k=> dic2[k]).Sum());
        }

        [TestMethod]
        public void Chunkify_1()
        {
            var items1 = new[] { 0, 1, 2, 3, 4, 5, 6, 7 };
            var chunks1 = EnumerableUtil.Chunkify(items1, 1);
            Assert.AreEqual(items1.Length, chunks1.Count);
            Assert.AreEqual(items1.Length, chunks1.Select(c => c.Count).Sum());

            var items2 = new[] { 0, 1, 2, 3, 4, 5, 6, 7 };
            var chunks2 = EnumerableUtil.Chunkify(items2, 3);
            Assert.AreEqual(3, chunks2.Count);
            Assert.AreEqual(3, chunks2[0].Count);
            Assert.AreEqual(3, chunks2[1].Count);
            Assert.AreEqual(2, chunks2[2].Count);

            var items3 = new[] { 0, 1, 2, 3, 4, 5  };
            var chunks3 = EnumerableUtil.Chunkify(items3, 3);
            Assert.AreEqual(2, chunks3.Count);
            Assert.AreEqual(3, chunks3[0].Count);
            Assert.AreEqual(3, chunks3[1].Count);

            var items4 = new[] { 0, 1, 2, 3, 4, 5, 6 };
            var chunks4 = EnumerableUtil.Chunkify(items4, 3);
            Assert.AreEqual(3, chunks4.Count);
            Assert.AreEqual(3, chunks4[0].Count);
            Assert.AreEqual(3, chunks4[1].Count);
            Assert.AreEqual(1, chunks4[2].Count);
        }

    }
}