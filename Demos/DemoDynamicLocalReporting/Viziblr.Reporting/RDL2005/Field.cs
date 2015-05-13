namespace Viziblr.Reporting.RDL2005
{
    public class Field : Node
    {
        public string Name;
        public string DataField;

        public Field()
        {
        }
        
        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent)
        {
            var el_field = parent.RS_AddElement("Field");
            el_field.SetAttributeValue("Name", this.Name);
            el_field.RS_SetElementValue("DataField", this.DataField);
            return el_field;
        }
    }
}