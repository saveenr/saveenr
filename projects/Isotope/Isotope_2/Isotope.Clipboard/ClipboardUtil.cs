namespace Isotope.Clipboard
{
    public static class ClipboardUtil
    {
        public static string GetUnicodeText()
        {
            var unicodetext = System.Windows.Forms.DataFormats.UnicodeText;

            if (!System.Windows.Forms.Clipboard.ContainsData(unicodetext))
            {
                return null;
            }

            var dataobject = System.Windows.Forms.Clipboard.GetDataObject();

            if (dataobject == null)
            {
                return null;
            }

            if (!dataobject.GetDataPresent(unicodetext))
            {
                return null;
            }

            string text = System.Windows.Forms.Clipboard.GetText();
            return text;
        }
        
        /// <summary>
        /// Returns CSV data from the clipboard. If there is no CSV data, null is returned
        /// </summary>
        /// <returns>CSV data as a string</returns>
        public static string GetCSV()
        {
            var fmt_csv = System.Windows.Forms.DataFormats.CommaSeparatedValue;

            if (!System.Windows.Forms.Clipboard.ContainsData(fmt_csv))
            {
                return null;
            }

            var dataobject = System.Windows.Forms.Clipboard.GetDataObject();

            if (dataobject == null)
            {
                return null;
            }

            if (!dataobject.GetDataPresent(fmt_csv))
            {
                return null;
            }

            var stream = (System.IO.Stream) dataobject.GetData(fmt_csv);
            var encoding = System.Text.Encoding.Default;
            var reader = new System.IO.StreamReader(stream, encoding);
            string csvdata = reader.ReadToEnd();

            reader.Close();
            return csvdata;
        }

        /// <summary>
        /// Returns Bitmap data from the clipboard. If there is no CSV data, null is returned
        /// </summary>
        /// <returns>The Bitmap object</returns>
        public static System.Drawing.Bitmap GetBitmap()
        {
            var fmt_bitmap = System.Windows.Forms.DataFormats.Bitmap;

            if (!System.Windows.Forms.Clipboard.ContainsData(fmt_bitmap))
            {
                return null;
            }

            var dataobject = System.Windows.Forms.Clipboard.GetDataObject();

            if (dataobject == null)
            {
                return null;
            }

            if (!dataobject.GetDataPresent(fmt_bitmap))
            {
                return null;
            }

            var bmp = (System.Drawing.Bitmap)dataobject.GetData(fmt_bitmap);
            return bmp;
        }

        /// <summary>
        /// Returns HTML from the clipboard. If there is no HTML data, null is returned
        /// </summary>
        /// <returns>the HTML as a string</returns>
        public static string GetHTML()
        {
            var html_format = System.Windows.Forms.TextDataFormat.Html;

            if (!System.Windows.Forms.Clipboard.ContainsText(html_format))
            {
                return null;
            }

            var fragment = Isotope.Clipboard.HTMLCLipboardData.FromClipboard();

            if (fragment.HTMLFragment == null)
            {
                return null;
            }

            // The string returned from HTML Fragment contains UTF-8 text
            // convert it to plain unicode string and return it
            var unicode_string = EncodeToUnicode(fragment.HTMLFragment, System.Text.Encoding.UTF8);
            return unicode_string;
        }

        private static string EncodeToUnicode(string encoded_string, System.Text.Encoding encoding)
        {
            var default_encoding = System.Text.Encoding.Default;
            var encoded_bytes = default_encoding.GetBytes(encoded_string);
            var unicode_string = encoding.GetString(encoded_bytes);
            return unicode_string;
        }

        public static void SetHTML(string html_fragment)
        {
            if (html_fragment == null)
            {
                throw new System.ArgumentNullException("html_fragment");
            }

            string cf_html = Isotope.Clipboard.HTMLCLipboardData.GetCFHTMLString(html_fragment, null, null);

            System.Windows.Forms.Clipboard.SetText(cf_html, System.Windows.Forms.TextDataFormat.Html);
        }
        
        public static void SetDataCSVFromTable(System.Windows.Forms.IDataObject dataobject, System.Data.DataTable datatable)
        {
            var default_encoding = System.Text.Encoding.Default;
            var out_memstream = new System.IO.MemoryStream();
            var streamwriter = new System.IO.StreamWriter(out_memstream, default_encoding);
            var csvwriter = new CSV.CSVWriter(streamwriter);
            ExportToCSV(datatable, csvwriter);
            var bytes = out_memstream.ToArray();
            var in_memstream = new System.IO.MemoryStream(bytes);
            dataobject.SetData(System.Windows.Forms.DataFormats.CommaSeparatedValue, in_memstream);
        }

        private static void ExportToCSV(System.Data.DataTable datatable, CSV.CSVWriter csvwriter)
        {
            foreach (System.Data.DataRow row in datatable.Rows)
            {
                csvwriter.WriteItems(row.ItemArray);
            }

            csvwriter.Close();
        }
    }
}