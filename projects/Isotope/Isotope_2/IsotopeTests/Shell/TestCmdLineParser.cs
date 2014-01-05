using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    [TestClass]
    public class TestCmdLineParser
    {
        [TestMethod]
        public void TestCreateEmptyParser()
        {
            var p = new Isotope.CommandLine.CommandLineParser();
        }

        [TestMethod]
        public void TestCreatePositionalArg()
        {
            var p = new Isotope.CommandLine.CommandLineParser();
            p.AddPositionalParameter("a");
        }

        [TestMethod]
        public void TestDuplicatePositionalArgNames()
        {
            bool caught = false;
            var p = new Isotope.CommandLine.CommandLineParser();
            p.AddPositionalParameter("a");
            try{p.AddPositionalParameter("a");}
            catch (Isotope.CommandLine.GrammarException )
            {
                caught = true;
            }

            if (caught == false)
            {
                Assert.Fail("Did not catch expected exception");
            }

        }

        [TestMethod]
        public void TestCreateNamedRequiredArg()
        {
            var p = new Isotope.CommandLine.CommandLineParser();
            p.AddNamedParameter("a", Isotope.CommandLine.ParameterRequirement.Required);
        }

        [TestMethod]
        public void TestDuplicateNamedArgs()
        {
            bool caught = false;
            var p = new Isotope.CommandLine.CommandLineParser();
            p.AddNamedParameter("a", Isotope.CommandLine.ParameterRequirement.Required);
            try{p.AddNamedParameter("a", Isotope.CommandLine.ParameterRequirement.Required);}
            catch (Isotope.CommandLine.GrammarException )
            {
                caught = true;
            }

            if (caught == false)
            {
                Assert.Fail("Did not catch expected exception");
            }
        }

        [TestMethod]
        public void TestPositionalArgAndNamedArgHaveSameName()
        {
            bool caught = false;
            var p = new Isotope.CommandLine.CommandLineParser();
            p.AddNamedParameter("a", Isotope.CommandLine.ParameterRequirement.Required);
            try {p.AddNamedParameter("a", Isotope.CommandLine.ParameterRequirement.NotRequired);}
            catch (Isotope.CommandLine.GrammarException )
            {
                caught = true;
            }

            if (caught == false)
            {
                Assert.Fail("Did not catch expected exception");
            }

        }

        [TestMethod]
        public void TestUnhandlesTokens1()
        {
            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");

            string[] tokens = {"1"};
            p.Parse(tokens);
            Assert.AreEqual(p.FindParameter("a"), a);
            Assert.AreEqual(p.FindParameter("x"), null);

            Assert.AreEqual(a.Text, "1");
        }

        [TestMethod]
        public void TestMissingPositionalArg1()
        {
            bool caught = false;
            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");

            string[] tokens = {};
            try {p.Parse(tokens);}
            catch (Isotope.CommandLine.MissingRequiredParameterException )
            {
                caught = true;
            }

            if (caught == false)
            {
                Assert.Fail("Did not catch expected exception");
            }
        }

        [TestMethod]
        public void TestTwoPositionalArgs()
        {
            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            var b = p.AddPositionalParameter("b");

            string[] tokens = {"1", "2"};
            p.Parse(tokens);

            Assert.AreEqual(a.Text, "1");
            Assert.AreEqual(b.Text, "2");
        }

        [TestMethod]
        public void TestMissingPositionalArg2()
        {
            bool caught = false;
            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            var b = p.AddPositionalParameter("b");

            string[] tokens = {"1"};
            try {p.Parse(tokens);}
            catch (Isotope.CommandLine.MissingRequiredParameterException )
            {
                caught = true;
            }

            if (caught == false)
            {
                Assert.Fail("Did not catch expected exception");
            }
        }

        [TestMethod]
        public void TestMissingRequiredNamedArgThrowsException()
        {
            bool caught = false;
            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            var b = p.AddNamedParameter("b", Isotope.CommandLine.ParameterRequirement.Required);

            string[] tokens = {"1", "2"};
            try {p.Parse(tokens);}
            catch (Isotope.CommandLine.MissingRequiredParameterException )
            {
                caught = true;
            }

            if (caught == false)
            {
                Assert.Fail("Did not catch expected exception");
            }
        }

        [TestMethod]
        public void TestRequiredNamedArg()
        {
            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            var b = p.AddNamedParameter("b", Isotope.CommandLine.ParameterRequirement.Required);

            string[] tokens = {"1", "-b", "2"};
            p.Parse(tokens);
            Assert.AreEqual(a.Text, "1");
            Assert.AreEqual(b.Text, "2");
        }

        [TestMethod]
        public void TestExcess3()
        {
            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            var b = p.AddNamedParameter("b", Isotope.CommandLine.ParameterRequirement.Required);

            string[] tokens = {"1", "x1", "-b", "2", "x2"};
            p.Parse(tokens);
            Assert.AreEqual("1", a.Text);
            Assert.AreEqual("2", b.Text);
        }

        [TestMethod]
        public void TestNamedOptionalWithoutValue()
        {
            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            var b = p.AddNamedParameter("b", Isotope.CommandLine.ParameterRequirement.Required);
            var c = p.AddNamedParameter("c", Isotope.CommandLine.ParameterRequirement.NotRequired);

            string[] tokens = {"1", "x1", "x2", "-b", "2"};
            p.Parse(tokens);
            Assert.AreEqual("1", a.Text);
            Assert.AreEqual("2", b.Text);
            Assert.AreEqual(false, c.HasValue);
        }

        [TestMethod]
        public void TestNamedOptionalWithoutValueNegInt()
        {
            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            var b = p.AddNamedParameter("b", Isotope.CommandLine.ParameterRequirement.Required);
            var c = p.AddNamedParameter("c", Isotope.CommandLine.ParameterRequirement.NotRequired);

            string[] tokens = {"1", "x1", "x2", "-b", "-2"};
            p.Parse(tokens);
            Assert.AreEqual("1", a.Text);
            Assert.AreEqual("-2", b.Text);
            Assert.AreEqual(false, c.HasValue);
        }

        [TestMethod]
        public void TestNamedOptionalWithoutValueNegInX()
        {
            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            var b = p.AddNamedParameter("b", Isotope.CommandLine.ParameterRequirement.Required);
            var c = p.AddNamedParameter("c", Isotope.CommandLine.ParameterRequirement.NotRequired);

            string[] tokens = {"1", "x1", "x2", "-2", "-b", "x3"};
            p.Parse(tokens);
            Assert.AreEqual("1", a.Text);
            Assert.AreEqual("x3", b.Text);
            Assert.AreEqual(false, c.HasValue);
        }

        [TestMethod]
        public void TestNamedOptionalWithValue()
        {
            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            var b = p.AddNamedParameter("b", Isotope.CommandLine.ParameterRequirement.Required);
            var c = p.AddNamedParameter("c", Isotope.CommandLine.ParameterRequirement.NotRequired);

            string[] tokens = {"1", "x1", "-c", "3", "x2", "-b", "2", "x3", "x4"};
            p.Parse(tokens);
            Assert.AreEqual("1", a.Text);
            Assert.AreEqual("2", b.Text);
            Assert.AreEqual("3", c.Text);
        }

        [TestMethod]
        public void TestPostParseCallbacks()
        {
            int a1_v = 0;
            int a2_v = 0;
            int b_v = 0;

            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            var b = p.AddNamedParameter("b", Isotope.CommandLine.ParameterRequirement.Required);
            var c = p.AddNamedParameter("c", Isotope.CommandLine.ParameterRequirement.NotRequired);

            a.OnPostParse += (parser, arg) => { a1_v = 1; };

            a.OnPostParse += (parser, arg) => { a2_v = 2; };

            b.OnPostParse += (parser, arg) => { b_v = 3; };

            string[] tokens = {"1", "x1", "-c", "3", "x2", "-b", "2", "x3", "x4"};
            p.Parse(tokens);
            Assert.AreEqual("1", a.Text);
            Assert.AreEqual("2", b.Text);
            Assert.AreEqual("3", c.Text);
            Assert.AreEqual(1, a1_v);
            Assert.AreEqual(2, a2_v);
            Assert.AreEqual(3, b_v);
        }

        [TestMethod]
        public void TestPositionArgsTakePrecedence()
        {
            // positional args always take precedence in order over named args, even if they are named on the command line

            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            var b = p.AddNamedParameter("b", Isotope.CommandLine.ParameterRequirement.Required);
            var c = p.AddNamedParameter("c", Isotope.CommandLine.ParameterRequirement.NotRequired);

            string[] tokens = {"-c", "t1", "t2", "-b", "bv", "t5", "t6"};
            p.Parse(tokens);
            Assert.AreEqual("-c", a.Text);
            Assert.AreEqual("bv", b.Text);
            Assert.IsFalse(c.HasValue);
            var unnassigned = p.GetUnassignedTokens();
            Assert.AreEqual(4, unnassigned.Count);
            Assert.AreEqual("t1", unnassigned[0]);
            Assert.AreEqual("t2", unnassigned[1]);
            Assert.AreEqual("t5", unnassigned[2]);
            Assert.AreEqual("t6", unnassigned[3]);
        }

        [TestMethod]
        public void TestValuesForKeywordParametersCanBeginWithDash()
        {
            // positional args always take precedence in order over named args, even if they are named on the command line

            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            var b = p.AddNamedParameter("b", Isotope.CommandLine.ParameterRequirement.Required);

            string[] tokens = {"t0", "-b", "-X"};
            p.Parse(tokens);
            Assert.AreEqual("t0", a.Text);
            Assert.AreEqual("-X", b.Text);
        }

        [TestMethod]
        public void TestValuesForEmptyStringParameterValues()
        {
            // positional args always take precedence in order over named args, even if they are named on the command line

            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            var b = p.AddNamedParameter("b", Isotope.CommandLine.ParameterRequirement.Required);

            string[] tokens = {"", "-b", ""};
            p.Parse(tokens);
            Assert.AreEqual("", a.Text);
            Assert.AreEqual("", b.Text);
        }

        [TestMethod]
        public void TestMisingPositionalValueThrowsException()
        {
            // verifies that when the text value is missing, an exception is thrown

            bool caught = false;
            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");

            try {string s = a.Text;}
            catch (Isotope.CommandLine.RuntimeErrorException )
            {
                caught = true;
            }

            if (caught == false)
            {
                Assert.Fail("Did not catch expected exception");
            }
        }

        [TestMethod]
        public void TestCallbacksCalledOnlyOnSuccessfulParsing()
        {
            int a1_v = 0;

            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            var b = p.AddNamedParameter("b", Isotope.CommandLine.ParameterRequirement.Required);

            a.OnPostParse += (parser, arg) => { a1_v = 1; };

            string[] tokens = {"1", "x1"}; // b is missing
            try
            {
                p.Parse(tokens);
            }
            catch (Isotope.CommandLine.MissingRequiredParameterException)
            {
                // this is ok
            }
            catch (System.Exception)
            {
                // this is not
                throw;
            }
            Assert.AreEqual("1", a.Text);
            Assert.AreEqual(0, a1_v);
        }

        [TestMethod]
        public void Test0PossibleValues()
        {
            // verify that adding zero possible values is possible

            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            a.PossibleValues = new string[] {};
            string[] tokens = {"foo"};
            p.Parse(tokens);

            Assert.AreEqual("foo", a.Text);
        }

        [TestMethod]
        public void Test1PossibleValue()
        {
            bool caught = false;
            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            a.PossibleValues = new string[] {"Hello"};
            try {p.Parse(new string[] {"foo"});}
            catch (Isotope.CommandLine.InvalidParameterValueException )
            {
                caught = true;
            }

            if (caught == false)
            {
                Assert.Fail("Did not catch expected exception");
            }
        }

        [TestMethod]
        public void Test2PossibleValue()
        {
            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            a.PossibleValues = new string[] {"Hello", "World"};
            p.Parse(new string[] {"Hello"});
            Assert.AreEqual("Hello", a.Text);
            p.Parse(new string[] {"World"});
            Assert.AreEqual("World", a.Text);
            p.Parse(new string[] {"helLO"});
            Assert.AreEqual("helLO", a.Text);
        }

        [TestMethod]
        public void TestMethodIntValues1()
        {
            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            var b = p.AddPositionalParameter("b");
            var c = p.AddPositionalParameter("c");
            p.Parse(new string[] {"-1", "0", "1"});
            Assert.AreEqual(-1, a.GetInt());
            Assert.AreEqual(0, b.GetInt());
            Assert.AreEqual(1, c.GetInt());
        }

        [TestMethod]
        public void TestMethodIntValues2()
        {
            bool caught = false;
            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            p.Parse(new string[] {"foo"});
            try {var n = a.GetInt();}
            catch (System.FormatException )
            {
                caught = true;
            }

            if (caught == false)
            {
                Assert.Fail("Did not catch expected exception");
            }
        }

        [TestMethod]
        public void TestMethodDoubleValues1()
        {
            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            var b = p.AddPositionalParameter("b");
            var c = p.AddPositionalParameter("c");
            p.Parse(new string[] {"-2.1", "0.0", "1.1"});
            Assert.AreEqual(-2.1, a.GetDouble());
            Assert.AreEqual(0, b.GetDouble());
            Assert.AreEqual(1.1, c.GetDouble());
        }

        [TestMethod]
        public void TestMethodDoubleValues2()
        {
            bool caught = false;
            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            p.Parse(new string[] {"foo"});
            try {var n = a.GetDouble();}
            catch (System.FormatException )
            {
                caught = true;
            }

            if (caught == false)
            {
                Assert.Fail("Did not catch expected exception");
            }
        }

        [TestMethod]
        public void TestMethodYesNoValues1()
        {
            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            var b = p.AddPositionalParameter("b");
            var c = p.AddPositionalParameter("c");
            var d = p.AddPositionalParameter("d");
            p.Parse(new string[] {"y", "yes", "no", "n"});
            Assert.AreEqual(true, a.GetYesNo());
            Assert.AreEqual(true, b.GetYesNo());
            Assert.AreEqual(false, c.GetYesNo());
            Assert.AreEqual(false, d.GetYesNo());
        }

        [TestMethod]
        public void TestMethodYesNoValues2()
        {
            bool caught = false;
            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            p.Parse(new string[] {"foo"});
            try {var n = a.GetYesNo();}
            catch (System.FormatException )
            {
                caught = true;
            }

            if (caught == false)
            {
                Assert.Fail("Did not catch expected exception");
            }
        }

        private enum EFOO
        {
            Choice1,
            Choice2
        }

        [TestMethod]
        public void TestMethodEnumValues1()
        {
            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            var b = p.AddPositionalParameter("b");
            var c = p.AddPositionalParameter("c");
            p.Parse(new string[] {"Choice1", "choICE1", "choice2"});
            Assert.AreEqual(EFOO.Choice1, a.GetEnum<EFOO>());
            Assert.AreEqual(EFOO.Choice1, b.GetEnum<EFOO>());
            Assert.AreEqual(EFOO.Choice2, c.GetEnum<EFOO>());
        }

        [TestMethod]
        public void TestMethodEnumValues2()
        {
            bool caught = false;
            var p = new Isotope.CommandLine.CommandLineParser();
            var a = p.AddPositionalParameter("a");
            p.Parse(new string[] {"foo"});
            try {var n = a.GetEnum<EFOO>();}
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
        public void TestUnknownArg()
        {
            bool caught = false;
            var p = new Isotope.CommandLine.CommandLineParser();

            string[] tokens = {"1", "-foo", "bar"};
            try
            {
                p.Parse(tokens);
            }
            catch (Isotope.CommandLine.UnknownKeywordErrorException )
            {
                caught = true;
            }

            if (caught == false)
            {
                Assert.Fail("Did not catch expected exception");
            }

        }

        [TestMethod]
        public void TestUnknownArgNegativeNumber()
        {
            // Negative numbers should not be interpreted as arguments.
            var p = new Isotope.CommandLine.CommandLineParser();

            string[] tokens = {"1", "-2", "bar"};
            p.Parse(tokens);
        }
    }

    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class TestTokenizer
    {
        public TestTokenizer()
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

        #endregion

        public void check_tokenization(string expected_line, string input_line, string punc)
        {
            var actual_tokens = Isotope.CommandLine.Tokenizer.Tokenize(input_line, punc);
            var actual_line = string.Join("|", actual_tokens.Select(t => t.Text).ToArray());
            Assert.AreEqual(expected_line, actual_line);
        }

        [TestMethod]
        public void TestBasicTokenization()
        {
            string punc1 = "";
            check_tokenization("", "", punc1);
            check_tokenization("a", "a", punc1);
            check_tokenization("a", " a ", punc1);
            check_tokenization("a|b", "a b", punc1);
            check_tokenization("a|b", "\"a\" \"b\"", punc1);
            check_tokenization("|b", "\"\" \"b\"", punc1);
            check_tokenization(" |b", "\" \" \"b\"", punc1);
            check_tokenization(" |b", "\" \" \"b", punc1);
            check_tokenization(" |b   ", "\" \" \"b   ", punc1);
            string punc2 = ".";
            check_tokenization("", ".........", punc2);
            check_tokenization("a|a", "a.a", punc2);
            check_tokenization("a|a", " a....a ", punc2);
            check_tokenization("a|b", "a b", punc2);
            check_tokenization("a|b", "\"a\" \"b\"", punc2);
            check_tokenization("|b", "\"\" \"b\"", punc2);
            check_tokenization(" |b", "\" \" \"b\"", punc2);
            check_tokenization(" |b", "\" \" \"b", punc2);
            check_tokenization(" |b   ", "\" \" \"b   ", punc2);
            check_tokenization("a.b", "\"a.b", punc2);
            check_tokenization("....a.b..", "\"....a.b..\"", punc2);
        }
    }
}