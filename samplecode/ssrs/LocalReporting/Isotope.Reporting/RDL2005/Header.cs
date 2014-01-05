namespace Isotope.Reporting.RDL2005
{
    public class Header : Node
    {
        public readonly NodeCollection<TableRow> TableRows;
        public double? Height;

        public bool RepeatOnNewPage=true;

        public Header()
        {
            this.TableRows = new NodeCollection<TableRow>();
        }

        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent)
        {

            var el_header = parent.RS_AddElement("Header");
            el_header.RS_SetElementValue("RepeatOnNewPage", this.RepeatOnNewPage.ToString().ToLower() );
            var el_tablerows = el_header.RS_AddElement("TableRows");
            foreach (var t in this.TableRows.Items())
            {
                t.write(el_tablerows);
            }

            return el_header;

        }
    }

    public class Details : Node
    {
        public readonly NodeCollection<TableRow> TableRows;
        public double? Height;

        public bool RepeatOnNewPage;

        public Details()
        {
            this.TableRows = new NodeCollection<TableRow>();
        }

        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent)
        {

            var el_header = parent.RS_AddElement("Details");
            var el_tablerows = el_header.RS_AddElement("TableRows");
            foreach (var t in this.TableRows.Items())
            {
                t.write(el_tablerows);
            }

            return el_header;

        }
    }
}