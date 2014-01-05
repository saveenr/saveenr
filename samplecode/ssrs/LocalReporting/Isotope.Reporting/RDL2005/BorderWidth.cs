namespace Isotope.Reporting.RDL2005
{
    public class BorderWidth : Node
    {
        public double? Default;
        public double? Left;
        public double? Right;
        public double? Top;
        public double? Bottom;


        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent)
        {
            var el_node = parent.RS_AddElement("BorderWidth");

            el_node.RS_SetElementValueCOND("Default", this.Default, "pt");
            el_node.RS_SetElementValueCOND("Left", this.Left, "pt");
            el_node.RS_SetElementValueCOND("Right", this.Right, "pt");
            el_node.RS_SetElementValueCOND("Top", this.Top, "pt");
            el_node.RS_SetElementValueCOND("Bottom", this.Bottom, "pt");

            return el_node;
        }

    }
}