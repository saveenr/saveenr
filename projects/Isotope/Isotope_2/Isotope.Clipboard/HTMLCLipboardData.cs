namespace Isotope.Clipboard
{
    /// <summary>
    /// Almost entirely based on this code: http://blogs.msdn.com/jmstall/archive/2007/01/21/html-clipboard.aspx for details.
    /// </summary>
    public class HTMLCLipboardData
    {
        private string _HTMLFragment;
        private System.Uri _source_url;
        private int _start_selection;
        private int _end_selection;
        private int _start_html;
        private int _end_html;
        private int _start_fragment;
        private int _end_fragment;

        private string m_version;
        private string m_fulltext;

        public string HTMLFragment
        {
            get { return _HTMLFragment; }
            //set { _HTMLFragment = value; }
        }

        public System.Uri SourceURL
        {
            get { return _source_url; }
            //set { _source_url = value; }
        }

        public int StartSelection
        {
            get { return _start_selection; }
            //set { _start_selection = value; }
        }

        public int EndSelection
        {
            get { return _end_selection; }
            //set { _end_selection = value; }
        }

        public int StartHTML
        {
            get { return _start_html; }
            //set { _start_html = value; }
        }

        public int EndHTML
        {
            get { return _end_html; }
            //set { _end_html = value; }
        }

        public int StartFragment
        {
            get { return _start_fragment; }
            //set { _start_fragment = value; }
        }

        public int EndFragment
        {
            get { return _end_fragment; }
            //set { _end_fragment = value; }
        }

        /// <summary>
        /// Get a HTML fragment from the clipboard.
        /// </summary>    
        public static HTMLCLipboardData FromClipboard()
        {
            var format = System.Windows.Forms.TextDataFormat.Html;
            string cf_html_text = System.Windows.Forms.Clipboard.GetText(format);
            var html = new HTMLCLipboardData(cf_html_text);
            return html;
        }

        /// <summary>
        /// Create an HTML fragment decoder around raw HTML text from the clipboard. 
        /// This text should have the header.
        /// </summary>
        /// <param name="cf_html_text">raw html text, with header.</param>
        public HTMLCLipboardData(string cf_html_text)
        {
            if (cf_html_text == null)
            {
                throw new System.ArgumentNullException("cf_html_text");
            }

            // This decodes CF_HTML, which is an entirely text format using UTF-8.
            // Format of this header is described at:
            // http://msdn.microsoft.com/en-us/library/aa767917(VS.85).aspx

            // Note the counters are byte counts in the original string, which may be Ansi. So byte counts
            // may be the same as character counts (since sizeof(char) == 1).
            // But System.String is unicode, and so byte couns are no longer the same as character counts,
            // (since sizeof(wchar) == 2). 
            var regex_options = System.Text.RegularExpressions.RegexOptions.IgnoreCase |
                                System.Text.RegularExpressions.RegexOptions.Compiled;
            var r = new System.Text.RegularExpressions.Regex("([a-zA-Z]+):(.+?)[\r\n]", regex_options);
            var invariant_culture = System.Globalization.CultureInfo.InvariantCulture;
            System.Text.RegularExpressions.Match m;

            for (m = r.Match(cf_html_text); m.Success; m = m.NextMatch())
            {
                string key = m.Groups[1].Value.ToLower(invariant_culture);
                string val = m.Groups[2].Value;

                switch (key)
                {
                    case "version":
                        {
                            // Version number of the clipboard. Starting version is 0.9. 
                            m_version = val;
                            break;
                        }

                    case "starthtml":
                        {
                            // Byte count from the beginning of the clipboard to the start of the context, or -1 if no context
                            if (_start_html != 0)
                            {
                                throw new System.FormatException("StartHtml is already declared");
                            }

                            _start_html = int.Parse(val, invariant_culture);
                            break;
                        }

                    case "endhtml":
                        {
                            // Byte count from the beginning of the clipboard to the end of the context, or -1 if no context.
                            if (_start_html == 0)
                            {
                                throw new System.FormatException("StartHTML must be declared before endHTML");
                            }

                            _end_html = int.Parse(val, invariant_culture);

                            m_fulltext = cf_html_text.Substring(_start_html, _end_html - _start_html);
                            break;
                        }

                    case "startfragment":
                        {
                            // Byte count from the beginning of the clipboard to the start of the fragment.
                            if (_start_fragment != 0)
                            {
                                throw new System.FormatException("StartFragment is already declared");
                            }

                            _start_fragment = int.Parse(val, invariant_culture);
                            break;
                        }

                    case "endfragment":
                        {
                            // Byte count from the beginning of the clipboard to the end of the fragment.
                            if (_start_fragment == 0)
                            {
                                throw new System.FormatException("StartFragment must be declared before EndFragment");
                            }

                            _end_fragment = int.Parse(val, invariant_culture);
                            var length = _end_fragment - _start_fragment;
                            _HTMLFragment = cf_html_text.Substring(_start_fragment, length);
                            break;
                        }

                    case "sourceurl":
                        {
                            // Optional Source URL, used for resolving relative links.
                            _source_url = new System.Uri(val);
                            break;
                        }

                    case "startselection":
                        {
                            this._start_selection = int.Parse(val, invariant_culture);
                            break;
                        }

                    case "endselection":
                        {
                            this._end_selection = int.Parse(val, invariant_culture);
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
            }

            if (m_fulltext == null && _HTMLFragment == null)
            {
                throw new System.FormatException("No data specified");
            }
        }

        // Helper to convert an integer into an 8 digit string.
        // String must be 8 characters, because it will be used to replace an 8 character string within a larger string.    
        private static string To8DigitString(int x)
        {
            var invariant_culture = System.Globalization.CultureInfo.InvariantCulture;
            return string.Format(invariant_culture, "{0,8}", x);
        }

        /// <summary>
        /// </summary>
        /// <param name="html_fragment">a html fragment</param>
        /// <param name="title">optional title of the HTML document (can be null)</param>
        /// <param name="source_url">optional Source URL of the HTML document, for resolving relative links (can be null)</param>
        public static string GetCFHTMLString(string html_fragment, string title, System.Uri source_url)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(html_fragment);
            html_fragment = System.Text.Encoding.GetEncoding(0).GetString(bytes);

            if (title == null)
            {
                title = "From Clipboard";
            }

            var sb = new System.Text.StringBuilder();

            // Builds the CF_HTML header. See format specification here:
            // http://msdn.microsoft.com/library/default.asp?url=/workshop/networking/clipboard/htmlclipboard.asp

            // The string contains index references to other spots in the string, so we need placeholders so we can compute the offsets. 
            // The <<<<<<<_ strings are just placeholders. We'll backpatch them actual values afterwards.
            // The string layout (<<<) also ensures that it can't appear in the body of the html because the <
            // character must be escaped.
            const string header = "Format:HTML Format\r\nVersion:1.0\r\nStartHTML:<<<<<<<1\r\nEndHTML:<<<<<<<2\r\nStartFragment:<<<<<<<3\r\nEndFragment:<<<<<<<4\r\nStartSelection:<<<<<<<3\r\nEndSelection:<<<<<<<3\r\n";
            string pre = string.Format(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN""><HTML><HEAD><TITLE>{0}</TITLE></HEAD><BODY><!--StartFragment-->", title);
            const string post = @"<!--EndFragment--></BODY></HTML>";

            sb.Append(header);
            if (source_url != null)
            {
                sb.AppendFormat("SourceURL:{0}", source_url);
            }

            int start_html = sb.Length;

            sb.Append(pre);
            int fragment_start = sb.Length;

            sb.Append(html_fragment);
            int fragment_end = sb.Length;

            sb.Append(post);
            int end_html = sb.Length;

            // Backpatch offsets
            sb.Replace("<<<<<<<1", To8DigitString(start_html));
            sb.Replace("<<<<<<<2", To8DigitString(end_html));
            sb.Replace("<<<<<<<3", To8DigitString(fragment_start));
            sb.Replace("<<<<<<<4", To8DigitString(fragment_end));

            string cf_html = sb.ToString();
            return cf_html;
        }
    }
}