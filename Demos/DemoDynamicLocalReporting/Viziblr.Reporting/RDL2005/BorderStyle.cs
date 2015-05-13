namespace Viziblr.Reporting.RDL2005
{
    public class BorderStyle : Node
    {
        public BorderStyleEnum? Default;
        public BorderStyleEnum? Left;
        public BorderStyleEnum? Right;
        public BorderStyleEnum? Top;
        public BorderStyleEnum? Bottom;

        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent)
        {
            var el_node = parent.RS_AddElement("BorderStyle");

            el_node.RS_SetElementValueCOND("Default", this.Default, v=>v.ToString() );
            el_node.RS_SetElementValueCOND("Left", this.Left, v => v.ToString());
            el_node.RS_SetElementValueCOND("Right", this.Right, v => v.ToString());
            el_node.RS_SetElementValueCOND("Top", this.Top, v => v.ToString());
            el_node.RS_SetElementValueCOND("Bottom", this.Bottom, v => v.ToString());
            
            return el_node;
        }


    }
}