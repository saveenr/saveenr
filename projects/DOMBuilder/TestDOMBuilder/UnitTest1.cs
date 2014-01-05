using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestDemoDOM
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var doc = new CustomDOM.Document();
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void TestMethod2()
        {
            var group = new CustomDOM.Group();
            group.Add((CustomDOM.Group)null);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void TestMethod3()
        {
            var group = new CustomDOM.Group();
            group.Add(group);
        }


        [TestMethod]
        public void TestMethod4()
        {
            var doc = new CustomDOM.Document();
            var page = new CustomDOM.Page();
            var group = new CustomDOM.Group();
            doc.Add(page);
            page.Add(group);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void TestMethod5()
        {
            var doc = new CustomDOM.Document();
            var page = new CustomDOM.Page();
            var group = new CustomDOM.Group();
            doc.Add(page);
            page.Add(group);
            page.Add(group);
        }


        [TestMethod]
        public void TestMethod6()
        {
            var doc = new CustomDOM.Document();
            var page = new CustomDOM.Page();
            var group0 = new CustomDOM.Group();
            var group2 = new CustomDOM.Group();
            var rect0 = new CustomDOM.Rectangle();
            var rect1 = new CustomDOM.Rectangle();
            var rect2 = new CustomDOM.Rectangle();
            doc.Add(page);
            page.Add(group0);
            page.Add(group2);
            page.Add(rect0);
            group2.Add(rect1);
            group2.Add(rect2);

            Assert.AreEqual(2,group2.NodeCount);

            var groups = page.Nodes<CustomDOM.Group>().ToList();
            Assert.AreEqual(2,groups.Count);
            var rects = page.Nodes<CustomDOM.Rectangle>().ToList();
            Assert.AreEqual(1, rects.Count);

            var w_groups = page.Walk<CustomDOM.Group>().ToList();
            Assert.AreEqual(2, w_groups.Count);

            var w_rects = page.Walk<CustomDOM.Rectangle>().ToList();
            Assert.AreEqual(3, w_rects.Count);

            Assert.AreEqual(rect0, w_rects[0]);
            Assert.AreEqual(rect1, w_rects[1]);
            Assert.AreEqual(rect2, w_rects[2]);
        } 

    }
}
