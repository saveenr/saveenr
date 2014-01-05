namespace Isotope.Reporting.RDL2005
{
    public class Body: Node
    {
        public readonly ReportItems ReportItems;
        public double Height;
        public double ColumnSpacing;
        
        public Body()
        {
            this.ReportItems = new ReportItems();
        }

        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent)
        {
            var el_body = parent.RS_AddElement("Body");

            el_body.RS_SetElementValue("Height", this.Height.ToString() + "in");
            el_body.RS_SetElementValue("ColumnSpacing", this.ColumnSpacing.ToString() + "in");

            this.ReportItems.write(el_body);


            return el_body;
        }
    }
}