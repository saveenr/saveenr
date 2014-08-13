using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FilenameTimestamp;
using UT = Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilenameTimestampTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
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



        public bool check_stamp_parsing(ParseResult pr, string expected_remainder, string expected_year, string expected_month, string expected_day)
        {
            UT.Assert.IsNotNull(pr);
            UT.Assert.IsNotNull(pr);
            UT.Assert.IsNotNull(pr.Stamp);
            UT.Assert.AreEqual(expected_remainder, pr.Remainder);
            UT.Assert.AreEqual(expected_year, pr.Stamp.Year);
            UT.Assert.AreEqual(expected_month, pr.Stamp.Month);
            UT.Assert.AreEqual(expected_day, pr.Stamp.Day);
            return true;
        }

        public bool check_file_renaming( string input_file_name, System.DateTime input_file_lastmod, string expected_out_name, System.DateTime expected_out_lastmod)
        {
            UT.Assert.IsNotNull(input_file_name);
            Stamper stamper = new Stamper();

            string actual_out_name = null;
            System.DateTime actual_out_lastmod;
            stamper.GetRenameInfo(input_file_name, input_file_lastmod, out actual_out_name, out actual_out_lastmod);

            UT.Assert.AreEqual(expected_out_name, actual_out_name);
            UT.Assert.AreEqual(expected_out_lastmod, actual_out_lastmod);

            return true;
        }


        [TestMethod]
        public void TestParsingWithFilesHaveValidStamps()
        {
            check_stamp_parsing2("foo 2001-02-03", "foo ", "2001", "02", "03");
            check_stamp_parsing2("foo (2001-02-03)", "foo ", "2001", "02", "03");
            check_stamp_parsing2("foo [2001-02-03]", "foo ", "2001", "02", "03");
            check_stamp_parsing2("foo (2001-02-03]", "foo ", "2001", "02", "03");
            check_stamp_parsing2("foo [2001-02-03)", "foo ", "2001", "02", "03");

            check_stamp_parsing2("foo [[2001-02-03)", "foo ", "2001", "02", "03");
            check_stamp_parsing2("foo [(([([2001-02-03)", "foo ", "2001", "02", "03");
            check_stamp_parsing2("foo (2001-02-03)])]", "foo ", "2001", "02", "03");

            check_stamp_parsing2("foo 2001-02-03", "foo ", "2001", "02", "03");

            check_stamp_parsing2("foo (0001-02-03)", "foo ", "0001", "02", "03");
            
            check_stamp_parsing2("0x01-xx-x", "", "0x01", "xx", "x");
            
            check_stamp_parsing2("bar [2001-02-03].jpg", "bar .jpg", "2001", "02", "03");
            
            check_stamp_parsing2("[2001-02-03].jpg", ".jpg", "2001", "02", "03");
            
            check_stamp_parsing2("[2001-02-03].foo.jpg", ".foo.jpg", "2001", "02", "03");
            
            check_stamp_parsing2(".foo[2001-02-03].jpg", ".foo.jpg", "2001", "02", "03");
            
            check_stamp_parsing2(" . [2001-02-03] . ", " .  . ", "2001", "02", "03");

            check_stamp_parsing2("foo (2001-2-03)", "foo ", "2001", "2", "03");
            check_stamp_parsing2("foo (2001-02-3)", "foo ", "2001", "02", "3");
            check_stamp_parsing2("foo (2001-2-3)", "foo ", "2001", "2", "3");

            check_stamp_parsing2("foo (2-3-2001)", "foo ", "2001", "2", "3");

        }

        public void check_no_stamp(string input_file_name)
        {
            Stamper stamper = new Stamper();
            ParseResult res = stamper.parse(input_file_name);
            UT.Assert.IsNotNull(res);
            UT.Assert.IsNull(res.Stamp);

        }

        [TestMethod]
        public void TestParseFailures()
        {
            check_no_stamp("");
            check_no_stamp("    ");
            check_no_stamp(".");
            check_no_stamp("foo (0x01)");
            check_no_stamp("foo (99-01-01)");
            check_no_stamp("foo (999-01-01)");
            check_no_stamp("foo (01-9999-01)");
            check_no_stamp("foo (01-9999)");
            check_no_stamp("foo.jpg");
            check_no_stamp("foo (1).jpg");
            check_no_stamp("foo (001).jpg");

        }

        public void check_stamp_parsing2(string input_file_name, string expected_remainder, string expected_year, string expected_month, string expected_day)
        {
            Stamper stamper = new Stamper();
            ParseResult res = stamper.parse(input_file_name);
            check_stamp_parsing(res, expected_remainder, expected_year, expected_month, expected_day);
        }


        [TestMethod]
        public void TestRenamingWithFullStamps()
        {


            // case: filestamp present and more recent than the filelastmod
            // expected: use filestamp
            check_file_renaming(
                "foo[2001-02-03].jpg",
                new DateTime(2001, 1, 2),
                "foo-(2001-02-03).jpg",
                new DateTime(2001, 2, 3));

            // case: filestamp present and older than the filelastmod
            // expected: use filestamp
            check_file_renaming(
                "foo[2001-01-01].jpg",
                new DateTime(2001, 1, 2),
                "foo-(2001-01-01).jpg",
                new DateTime(2001, 1, 1));

            // case: filestamp present and equal to the filelastmod
            // expected: no change
            check_file_renaming(
                "foo[2001-01-02].jpg",
                new DateTime(2001, 1, 2),
                "foo-(2001-01-02).jpg",
                new DateTime(2001, 1, 2));

            // fix excessive numbers of dashes between the name and the stamp
            check_file_renaming(
                "foo---------------[2001-01-02].jpg",
                new DateTime(2001, 1, 2),
                "foo-(2001-01-02).jpg",
                new DateTime(2001, 1, 2));

        }

        [TestMethod]
        public void TestRenamingWithNoStamps()
        {

            check_file_renaming(
                "foo.jpg",
                new DateTime(2001, 1, 2),
                "foo-(2001-01-02).jpg",
                new DateTime(2001, 1, 2));


            check_file_renaming(
                "foo (1).jpg",
                new DateTime(2001, 1, 2),
                "foo (1)-(2001-01-02).jpg",
                new DateTime(2001, 1, 2));


            check_file_renaming(
                "foo (1999).jpg",
                new DateTime(2001, 1, 2),
                "foo (1999)-(2001-01-02).jpg",
                new DateTime(2001, 1, 2));


            check_file_renaming(
                "foo (1999-11).jpg",
                new DateTime(2001, 1, 2),
                "foo (1999-11)-(2001-01-02).jpg",
                new DateTime(2001, 1, 2));


        }


        [TestMethod]
        public void TestRenamingWithPartialStamps()
        {
            // Basically if it is partial we just give up on using a date, but we do try to normalize the format

            check_file_renaming(
                "foo-[2000-xx-xx].jpg",
                new DateTime(2001, 6, 7),
                "foo-(2000-xx-xx).jpg",
                new DateTime(2001, 6, 7));

            check_file_renaming(
                "foo-[xxxx-xx-xx].jpg",
                new DateTime(2001, 6, 7),
                "foo-(xxxx-xx-xx).jpg",
                new DateTime(2001, 6, 7));


            check_file_renaming(
                "foo-[xxxx-4-5].jpg",
                new DateTime(2001, 6, 7),
                "foo-(xxxx-4-5).jpg",
                new DateTime(2001, 6, 7));



            check_file_renaming(
                "foo-[xx-xx-1995].jpg",
                new DateTime(2001, 6, 7),
                "foo-(1995-xx-xx).jpg",
                new DateTime(2001, 6, 7));


        }
  

    }
}
