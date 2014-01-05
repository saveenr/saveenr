namespace Isotope.Reporting.RDL2005
{
    public class _BaseHeaderFooter: Node
    {
        public double Height;
        public bool? PrintOnFirstPage;
        public bool? PrintOnLastPage;
        public readonly ReportItems ReportItems;
        public Style Style;

        public _BaseHeaderFooter()
        {
            this.ReportItems = new ReportItems();
            this.Style = new Style();
        }

        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent,string name)
        {
            var el = parent.RS_AddElement(name);

            el.RS_SetElementValue("Height", this.Height.ToString() + "in");
            el.RS_SetElementValueCOND("PrintOnFirstPage", PrintOnFirstPage);
            el.RS_SetElementValueCOND("PrintOnLastPage", PrintOnLastPage);
            this.ReportItems.write(el);
            this.Style.write(el);
            return el;

        }
    }
}