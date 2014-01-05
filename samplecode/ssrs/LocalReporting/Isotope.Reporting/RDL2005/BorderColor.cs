namespace Isotope.Reporting.RDL2005
{
    public class BorderColor : Node
    {
        public Color Default;
        public Color Left;
        public Color Right;
        public Color Top;
        public Color Bottom;


        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent)
        {
            var el_node = parent.RS_AddElement("BorderColor");

            el_node.RS_SetElementValueCOND("Default", this.Default);
            el_node.RS_SetElementValueCOND("Left", this.Left );
            el_node.RS_SetElementValueCOND("Right", this.Right );
            el_node.RS_SetElementValueCOND("Top", this.Top );
            el_node.RS_SetElementValueCOND("Bottom" , this.Bottom );

            return el_node;
        }
    }
}