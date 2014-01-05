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
            string failure = null;
            var thread = new System.Threading.Thread(() =>
                                                         {
                                                             System.Windows.Forms.Clipboard.Clear();
                                                             var in_html = "<html><body>Aąłä</body></html>";
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
    }
}