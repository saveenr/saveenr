namespace Viziblr.Reporting.RDL2005
{
    public class TableColumn : Node
    {

        public double Width;

        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent)
        {

            var el_tablecolumn = parent.RS_AddElement("TableColumn");
            el_tablecolumn.RS_SetElementValue("Width", this.Width.ToString() + "in");
            return el_tablecolumn;

        }

    }
}