using Isotope.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace IsotopeTests
{
    [TestClass]
    public class EnumerableUtilTest2
    {
        [TestMethod]
        public void EnumPairsTest()
        {
            var a_in = new int[] {};
            var a_out = EnumerableUtil.SelectPairs(a_in).ToArray();
            Assert.AreEqual(0, a_out.Length);

            var b_in = new int[] {0};
            var b_out = EnumerableUtil.SelectPairs(b_in).ToArray();
            Assert.AreEqual(0, b_out.Length);

            var c_in = new int[] {0, 1};
            var c_out = EnumerableUtil.SelectPairs(c_in).ToArray();
            Assert.AreEqual(1, c_out.Length);

            var d_in = new int[] {0, 1, 2};
            var d_out = EnumerableUtil.SelectPairs(d_in).ToArray();
            Assert.AreEqual(1, d_out.Length);

            Assert.AreEqual(0, d_out[0].Item1);
            Assert.AreEqual(1, d_out[0].Item2);

            var e_in = new int[] {0, 1, 2, 3};
            var e_out = EnumerableUtil.SelectPairs(e_in).ToArray();
            Assert.AreEqual(2, e_out.Length);

            Assert.AreEqual(0, e_out[0].Item1);
            Assert.AreEqual(1, e_out[0].Item2);
            Assert.AreEqual(2, e_out[1].Item1);
            Assert.AreEqual(3, e_out[1].Item2);
        }

        [TestMethod]
        public void EnumPairsOverlappedTest()
        {
            var a_in = new int[] {};
            var a_out = EnumerableUtil.SelectPairsOverlapped(a_in).ToArray();
            Assert.AreEqual(0, a_out.Length);

            var b_in = new int[] {0};
            var b_out = EnumerableUtil.SelectPairsOverlapped(b_in).ToArray();
            Assert.AreEqual(0, b_out.Length);

            var c_in = new int[] {0, 1};
            var c_out = EnumerableUtil.SelectPairsOverlapped(c_in).ToArray();
            Assert.AreEqual(1, c_out.Length);

            var d_in = new int[] {0, 1, 2};
            var d_out = EnumerableUtil.SelectPairsOverlapped(d_in).ToArray();
            Assert.AreEqual(2, d_out.Length);
            Assert.AreEqual(0, d_out[0].Item1);
            Assert.AreEqual(1, d_out[0].Item2);
            Assert.AreEqual(1, d_out[1].Item1);
            Assert.AreEqual(2, d_out[1].Item2);

            var e_in = new int[] {0, 1, 2, 3};
            var e_out = EnumerableUtil.SelectPairsOverlapped(e_in).ToArray();
            Assert.AreEqual(3, e_out.Length);
            Assert.AreEqual(0, e_out[0].Item1);
            Assert.AreEqual(1, e_out[0].Item2);
            Assert.AreEqual(1, e_out[1].Item1);
            Assert.AreEqual(2, e_out[1].Item2);
            Assert.AreEqual(2, e_out[2].Item1);
            Assert.AreEqual(3, e_out[2].Item2);
        }

        [TestMethod]
        public void ZipRepeatSecond_1()
        {
            var a = new[] {1, 2};
            var b = new[] {3, 4};

            var c = Isotope.Collections.EnumerableUtil.ZipRepeatSecond(a, b).ToList();
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual(1, c[0].Item1);
            Assert.AreEqual(3, c[0].Item2);
            Assert.AreEqual(2, c[1].Item1);
            Assert.AreEqual(4, c[1].Item2);
        }

        [TestMethod]
        public void ZipRepeatSecond_3()
        {
            var a = new[] {1, 2, 3};
            var b = new[] {8, 9};

            var c = Isotope.Collections.EnumerableUtil.ZipRepeatSecond(a, b).ToList();
            Assert.AreEqual(3, c.Count);
            Assert.AreEqual(1, c[0].Item1);
            Assert.AreEqual(8, c[0].Item2);
            Assert.AreEqual(2, c[1].Item1);
            Assert.AreEqual(9, c[1].Item2);
            Assert.AreEqual(3, c[2].Item1);
            Assert.AreEqual(8, c[2].Item2);
        }

        [TestMethod]
        public void ZipRepeatSecond_4()
        {
            var a = new[] {1, 2, 3, 4};
            var b = new[] {8, 9};

            var c = Isotope.Collections.EnumerableUtil.ZipRepeatSecond(a, b).ToList();
            Assert.AreEqual(4, c.Count);
            Assert.AreEqual(1, c[0].Item1);
            Assert.AreEqual(8, c[0].Item2);
            Assert.AreEqual(2, c[1].Item1);
            Assert.AreEqual(9, c[1].Item2);
            Assert.AreEqual(3, c[2].Item1);
            Assert.AreEqual(8, c[2].Item2);
            Assert.AreEqual(4, c[3].Item1);
            Assert.AreEqual(9, c[3].Item2);
        }

        [TestMethod]
        public void ZipRepeatSecond_5()
        {
            var a = new[] {1, 2};
            var b = new[] {8, 9, 10};

            var c = Isotope.Collections.EnumerableUtil.ZipRepeatSecond(a, b).ToList();
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual(1, c[0].Item1);
            Assert.AreEqual(8, c[0].Item2);
            Assert.AreEqual(2, c[1].Item1);
            Assert.AreEqual(9, c[1].Item2);
        }

        [TestMethod]
        public void Zip1()
        {
            var a = new[] {1, 2};
            var b = new[] {8, 9};
            var c = Isotope.Collections.EnumerableUtil.Zip(a, b, (A, B) => new {A, B}).ToList();
        }
    }
}