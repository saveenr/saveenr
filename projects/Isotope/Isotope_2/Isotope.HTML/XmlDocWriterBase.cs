using System.Collections.Generic;

namespace Isotope.HTML
{
    public class XmlDocWriterBase
    {
        protected System.Xml.XmlWriter _xw;
        private Stack<string> stack;

        protected System.Xml.XmlWriter xmlwriter
        {
            get { return this._xw; }
        }

        public XmlDocWriterBase(System.Xml.XmlWriter xmlwriter)
        {
            this.stack = new Stack<string>();
            if (xmlwriter == null)
            {
                throw new System.ArgumentNullException("xmlwriter");
            }

            this._xw = xmlwriter;
        }

        public XmlDocWriterBase(string filename)
        {
            this.stack = new Stack<string>();
            if (filename == null)
            {
                throw new System.ArgumentNullException("filename");
            }

            var settings = new System.Xml.XmlWriterSettings();
            settings.Indent = true;
            this._xw = System.Xml.XmlWriter.Create(filename, settings);
        }

        protected void start(string s)
        {
            this.xmlwriter.WriteStartElement(s);
            this.stack.Push(s);
        }

        protected void end(string s)
        {
            if (stack.Count < 1)
            {
                string msg = string.Format("No matching starting element for <{0}>", s);
                throw new System.ArgumentException(msg, "s");
            }

            string ontop = stack.Pop();
            if (ontop != s)
            {
                string msg = string.Format("Cannot end element <{0}>, expected to end <{1}>", s, ontop);
                throw new System.ArgumentException(msg);
            }

            this.xmlwriter.WriteEndElement();
        }

        protected void element(string name, string s)
        {
            this.xmlwriter.WriteElementString(name, s);
        }

        protected void attr(string name, string s)
        {
            this.xmlwriter.WriteAttributeString(name, s);
        }

        protected void text(string s)
        {
            this.xmlwriter.WriteString(s);
        }

        protected void attr_if_not_null(string name, string s)
        {
            if (s != null)
            {
                this.xmlwriter.WriteAttributeString(name, s);
            }
        }
    }
}