using System.Collections.Generic;
using System.Linq;

namespace Isotope.CSV
{
    public class CSVWriter
    {
        private readonly System.IO.StreamWriter _stream;
        private static readonly char[] charstoquote = "\",\x0A\x0D".ToCharArray();
        private const string sep = ",";
        private const string newline = "\n";

        public CSVWriter(string filename)
        {
            this._stream = System.IO.File.CreateText(filename);
        }

        public CSVWriter(System.IO.StreamWriter tw)
        {
            this._stream = tw;
        }

        public System.IO.StreamWriter Stream
        {
            get { return _stream; }
        }

        public void WriteItems(IEnumerable<string> strings)
        {
            int str_index = 0;
            foreach (var str in strings)
            {
                if (str_index > 0)
                {
                    this._stream.Write(sep);
                }
                WriteItem(str);
                str_index++;
            }
            this._stream.Write(newline);
        }

        public void WriteItems(IEnumerable<object> items)
        {
            var a = items.Select(i => i.ToString()).ToList();
            this.WriteItems(a);
        }

        private void WriteItem(string s)
        {
            s = s ?? string.Empty;
            string q = "\"";
            string dq = "\"\"";

            bool force_quoting = true;

            if (force_quoting || s.IndexOfAny(charstoquote) > -1)
            {
                this._stream.Write("\"{0}\"", s.Replace(q, dq));
            }
            else
            {
                this._stream.Write(s);
            }
        }

        public void Close()
        {
            this._stream.Close();
        }
    }
}