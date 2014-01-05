using System.Collections.Generic;

namespace Isotope.HTML
{
    public class SimpleHTMLWriter : XmlDocWriterBase
    {
        public SimpleHTMLWriter(System.Xml.XmlWriter xmlwriter) :
            base(xmlwriter)
        {
        }

        public SimpleHTMLWriter(string filename) :
            base(filename)
        {
        }

        public void StartHtml()
        {
            this.start("html");
        }

        public void EndHtml()
        {
            this.end("html");
        }

        public void StartHead()
        {
            this.start("head");
        }

        public void EndHead()
        {
            this.end("head");
        }

        public void Title(string s)
        {
            this.element("title", s);
        }

        public void StartBody()
        {
            this.start("body");
        }

        public void EndBody()
        {
            this.end("body");
        }

        public void StartH(int n)
        {
            this.start("h" + n.ToString());
        }

        public void EndH(int n)
        {
            this.end("h" + n.ToString());
        }

        public void StartH1()
        {
            this.StartH(1);
        }

        public void EndH1()
        {
            this.EndH(1);
        }

        public void StartH2()
        {
            this.StartH(2);
        }

        public void EndH2()
        {
            this.EndH(2);
        }

        public void StartH3()
        {
            this.StartH(3);
        }

        public void EndH3()
        {
            this.EndH(3);
        }

        public void StartHyperlink(string url)
        {
            this.start("a");
            this.attr("href", url);
        }

        public void EndHyperlink()
        {
            this.end("a");
        }

        public void Br()
        {
            this.start("br");
            this.end("br");
        }

        public void Hyperlink(string url, string message)
        {
            this.StartHyperlink(url);
            this.text(message);
            this.EndHyperlink();
        }

        public void StartTable()
        {
            this.start("table");
        }

        public void EndTable()
        {
            this.end("table");
        }

        public void StartTableRow()
        {
            this.start("tr");
        }

        public void EndTableRow()
        {
            this.end("tr");
        }

        public void StartTableHeader()
        {
            this.start("th");
        }

        public void EndTableHeader()
        {
            this.end("th");
        }

        public void StartTableData()
        {
            this.start("td");
        }

        public void EndTableData()
        {
            this.end("td");
        }

        public void TableRow<T>(IEnumerable<T> items)
        {
            this.StartTableRow();
            foreach (var item in items)
            {
                this.TableData(item.ToString());
            }
            this.EndTableRow();
        }

        public void Header(int n, string s)
        {
            this.StartH(n);
            this.Text(s);
            this.EndH(n);
        }

        public void TableHeader(string s)
        {
            this.StartTableHeader();
            this.Text(s);
            this.EndTableHeader();
        }

        public void TableData(string s)
        {
            this.StartTableData();
            this.Text(s);
            this.EndTableData();
        }

        public void Attribute(string name, string value)
        {
            this.attr(name, value);
        }

        public void Text(string s)
        {
            this.text(s);
        }

        public void StartPara()
        {
            this.start("p");
        }

        public void EndPara()
        {
            this.end("p");
        }

        public void Para(string s)
        {
            this.StartPara();
            this.Text(s);
            this.EndPara();
        }

        public void StartDiv()
        {
            this.start("div");
        }

        public void EndDiv()
        {
            this.end("div");
        }

        public void StartSpan()
        {
            this.start("span");
        }

        public void EndSpan()
        {
            this.end("span");
        }

        public void StartStyle(string type)
        {
            this.start("style");
            this.attr_if_not_null("type", type);
        }

        public void EndStyle()
        {
            this.end("style");
        }

        public void StartScript(string type)
        {
            this.start("script");
            this.attr_if_not_null("type", type);
        }

        public void EndScript()
        {
            this.end("script");
        }

        public void StartMeta()
        {
            this.start("meta");
        }

        public void EndMeta()
        {
            this.end("meta");
        }

        public void Link(string rel, string href, string type)
        {
            this.start("link");
            this.attr_if_not_null("rel", rel);
            this.attr_if_not_null("href", href);
            this.attr_if_not_null("type", type);
            this.end("link");
        }

        public void Flush()
        {
            this.xmlwriter.Flush();
        }

        public void Close()
        {
            this.xmlwriter.Flush();
            this.xmlwriter.Close();
        }
    }
}