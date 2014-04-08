using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestFastStringSplit
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestEmptyString()
        {
            string input = "";
            var splitter = new FastStringSplit.Splitter(',');

            var tokens = splitter.Split(input);

            Assert.AreEqual(0,tokens.Length);
        }

        [TestMethod]
        public void TestNullString()
        {
            string input = null;
            var splitter = new FastStringSplit.Splitter(',');

            var tokens = splitter.Split(input);

            Assert.AreEqual(0, tokens.Length);
        }

        [TestMethod]
        public void TestCaseSingleToken1()
        {
            string input = "a";
            var splitter = new FastStringSplit.Splitter(',');

            var tokens = splitter.Split(input);

            Assert.AreEqual(1, tokens.Length);
        }

        [TestMethod]
        public void TestCaseSingleToken2()
        {
            string input = "abc";
            var splitter = new FastStringSplit.Splitter(',');

            var tokens = splitter.Split(input);

            Assert.AreEqual(1, tokens.Length);
        }

        [TestMethod]
        public void TestCaseTwoTokens1()
        {
            string input = "a,b";
            var splitter = new FastStringSplit.Splitter(',');

            var tokens = splitter.Split(input);

            Assert.AreEqual(2, tokens.Length);
        }

        [TestMethod]
        public void TestCaseTwoTokens2()
        {
            string input = ",a,b";
            var splitter = new FastStringSplit.Splitter(',');

            var tokens = splitter.Split(input);

            Assert.AreEqual(3, tokens.Length);
        }

        [TestMethod]
        public void TestCaseTwoTokens3()
        {
            string input = ",a,,b";
            var splitter = new FastStringSplit.Splitter(',');

            var tokens = splitter.Split(input);

            Assert.AreEqual(4, tokens.Length);
        }

        [TestMethod]
        public void TestCaseTwoTokens4()
        {
            string input = ",a,,b,";
            var splitter = new FastStringSplit.Splitter(',');

            var tokens = splitter.Split(input);

            Assert.AreEqual(4, tokens.Length);
            Assert.AreEqual(null,tokens[0]);
            Assert.AreEqual("a", tokens[1]);
            Assert.AreEqual(null, tokens[2]);
        }

    }
}
