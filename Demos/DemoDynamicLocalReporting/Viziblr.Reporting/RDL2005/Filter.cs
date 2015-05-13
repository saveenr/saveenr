using System.Collections.Generic;

namespace Viziblr.Reporting.RDL2005
{
    public class Filter : Node
    {


        public string FilterExpression;
        public string Operator;
        public List<string> FilterValues = new List<string>();

        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent)
        {
            var el_field = parent.RS_AddElement("Filter");
            el_field.RS_SetElementValue("FilterExpression", this.FilterExpression);
            el_field.RS_SetElementValue("Operator", this.Operator);
            var el_ex =  el_field.RS_AddElement("FilterValues");
            foreach (var fv in this.FilterValues)
            {
                el_ex.RS_AddElement("FilterValue");
                el_ex.Value = fv;
            }
            return el_field;
        }
    }
}