using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    [TestClass]
    public class ClipboardUtilTest
    {
        [TestMethod]
        public void XGetUnicodeTextTest()
        {
            string failure = null;
            var thread = new System.Threading.Thread(() =>

                                                         {
                                                             string src = "Aąłä";
                                                             string expected = src.Substring(0);
                                                             System.Windows.Forms.Clipboard.Clear();
                                                             System.Windows.Forms.Clipboard.SetText(src);
                                                             string actual = System.Windows.Forms.Clipboard.GetText();
                                                             if (expected != actual)
                                                             {
                                                                 failure = string.Format("Expected ={0} Actual={1}",
                                                                                         expected, actual);
                                                             }
                                                         });
            thread.SetApartmentState(System.Threading.ApartmentState.STA);
            thread.Start();
            thread.Join();
            if (failure != null)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        ///A test for GetUnicodeText
        ///</summary>
        [TestMethod]
        public void GetUnicodeHTMLTest()
        {
            string failure = null;
            var thread = new System.Threading.Thread(() =>
                                                         {
                                                             string src = "<html><body><p>Aąłä</p></body></html>";
                                                             string expected = src.Substring(0);
                                                             System.Windows.Forms.Clipboard.Clear();
                                                             Isotope.Clipboard.ClipboardUtil.SetHTML(src);
                                                             string actual = Isotope.Clipboard.ClipboardUtil.GetHTML();
                                                             if (expected != actual)
                                                             {
                                                                 failure = string.Format("Expected ={0} Actual={1}",
                                                                                         expected, actual);
                                                             }
                                                         });
            thread.SetApartmentState(System.Threading.ApartmentState.STA);
            thread.Start();
            thread.Join();
            if (failure != null)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        ///A test for GetUnicodeText
        ///</summary>
        [TestMethod]
        public void GetCSVTest()
        {
            var datatable = new System.Data.DataTable();
            datatable.Columns.Add("Col1", typeof (string));
            datatable.Columns.Add("Co12", typeof (string));
            datatable.Rows.Add("A", "ł");
            datatable.Rows.Add("ą", "ä");

            string failure = null;
            var thread = new System.Threading.Thread(() =>
                                                         {
                                                             var dataobject =
                                                                 System.Windows.Forms.Clipboard.GetDataObject();
                                                             Isotope.Clipboard.ClipboardUtil.SetDataCSVFromTable(
                                                                 dataobject, datatable);
                                                             System.Windows.Forms.Clipboard.SetDataObject(dataobject);

                                                             var out_csv = Isotope.Clipboard.ClipboardUtil.GetCSV();
                                                             System.Windows.Forms.Clipboard.Clear();
                                                         });
            thread.SetApartmentState(System.Threading.ApartmentState.STA);
            thread.Start();
            thread.Join();
            if (failure != null)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        ///A test for GetUnicodeText
        ///</summary>
        [TestMethod]
        public void GetHTMLTest()
        {
            var datatable = new System.Data.DataTable();
            datatable.Columns.Add("Col1", typeof (string));
            datatable.Columns.Add("Co12", typeof (string));
            datatable.Rows.Add("A", "ł");
            datatable.Rows.Add("ą", "ä");

            string failure = null;
            var thread = new System.Threading.Thread(() =>
                                                         {
                                                             System.Windows.Forms.Clipboard.Clear();
                                                             var in_html =
                                                                 Isotope.Data.DataExporter.ToHTMLString(datatable, true);
                                                             Isotope.Clipboard.ClipboardUtil.SetHTML(in_html);
                                                             var out_html = Isotope.Clipboard.ClipboardUtil.GetHTML();
                                                             if (out_html != in_html)
                                                             {
                                                                 failure = "failed";
                                                             }
                                                         });
            thread.SetApartmentState(System.Threading.ApartmentState.STA);
            thread.Start();
            thread.Join();
            if (failure != null)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        ///A test for GetUnicodeText
        ///</summary>
        [TestMethod]
        public void GetHTMLTest2()
        {
            var datatable = new System.Data.DataTable();
            datatable.Columns.Add("Col1", typeof (string));
            datatable.Columns.Add("Co12", typeof (string));
            datatable.Rows.Add("A", "ł");
            datatable.Rows.Add("ą", "ä");

            bool write_col_headers = true;
            string tsv = Isotope.Data.DataExporter.ToTSVString(datatable, write_col_headers);
            string in_html = Isotope.Data.DataExporter.ToHTMLString(datatable, write_col_headers);

            string failure = null;
            var thread = new System.Threading.Thread(() =>
                                                         {
                                                             System.Windows.Forms.Clipboard.Clear();
                                                             string cf_html =
                                                                 Isotope.Clipboard.HTMLCLipboardData.GetCFHTMLString(
                                                                     in_html, null, null);
                                                             var dataobject = new System.Windows.Forms.DataObject();
                                                             // System.Windows.Forms.Clipboard.GetDataObject();
                                                             dataobject.SetData(System.Windows.Forms.DataFormats.Text,
                                                                                tsv);
                                                             dataobject.SetData(System.Windows.Forms.DataFormats.Html,
                                                                                cf_html);
                                                             System.Windows.Forms.Clipboard.SetDataObject(dataobject,
                                                                                                          true);
                                                             var out_html = Isotope.Clipboard.ClipboardUtil.GetHTML();
                                                             if (out_html != in_html)
                                                             {
                                                                 failure = "fail";
                                                             }
                                                         });
            thread.SetApartmentState(System.Threading.ApartmentState.STA);
            thread.Start();
            thread.Join();
            if (failure != null)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        ///A test for GetUnicodeText
        ///</summary>
        [TestMethod]
        public void DataTableToHTMLTest()
        {
            var datatable = new System.Data.DataTable();
            datatable.Columns.Add("Col1", typeof (string));
            datatable.Columns.Add("Co12", typeof (string));
            datatable.Rows.Add("A", "ł");
            datatable.Rows.Add("ą", "ä");

            var actual_html = Isotope.Data.DataExporter.ToHTMLString(datatable, false);
            var expected_html =
                "<table><tr><td>A</td><td>ł</td></tr><tr><td>ą</td><td>ä</td></tr></table>";
            if (actual_html != expected_html)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        ///A test for GetUnicodeText
        ///</summary>
        [TestMethod]
        public void DataTableToHTMLTest2()
        {
            var datatable = new System.Data.DataTable();
            datatable.Columns.Add("Col1", typeof (string));
            datatable.Columns.Add("Col2", typeof (string));
            datatable.Rows.Add("A", "ł");
            datatable.Rows.Add("ą", "ä");

            var actual_tsv = Isotope.Data.DataExporter.ToTSVString(datatable, true);

            var actual_html1 = Isotope.Data.DataExporter.ToHTMLString(datatable, true);
            var expected_html1 =
                "<table><tr><th>Col1</th><th>Col2</th></tr><tr><td>A</td><td>ł</td></tr><tr><td>ą</td><td>ä</td></tr></table>";
            if (actual_html1 != expected_html1)
            {
                Assert.Fail();
            }

            var actual_html2 = Isotope.Data.DataExporter.ToHTMLString(datatable, false);
            var expected_html2 =
                "<table><tr><td>A</td><td>ł</td></tr><tr><td>ą</td><td>ä</td></tr></table>";
            if (actual_html2 != expected_html2)
            {
                Assert.Fail();
            }
        }
    }
}