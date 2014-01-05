namespace Isotope.Reporting.RDL2005
{
    public class ReportItem: Node
    {

        public string Name;
        public double Height;
        public double? Width;
        public Style Style;
        public bool? Visibility;
        

        public ReportItem(string name)
        {
            this.Name = name;
            this.Style = new Style();
            
        }

        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent,string name)
        {

            var el = parent.RS_AddElement(name);
            el.SetAttributeValue("Name", this.Name);
            el.RS_SetAttributeValueCONDBOOL("Visibility",this.Visibility);
            el.RS_SetElementValue("Height", this.Height.ToString() + "in");
            if (this.Width.HasValue)
            {
                el.RS_SetElementValue("Width", this.Width.ToString() + "in");
            }
            if (this.Style != null)
            {
                this.Style.write(el);
            }

            return el;
        }
    }
}