namespace Isotope.Reporting.RDL2005
{
    public class ReportParameter : Node
    {


        public string Name;
        public ParameterDataTypeEnum DataType;
        public bool? Nullable;
        public string DefaultValue;
        public bool? AllowBlank;
        public string Prompt;
        public bool? Hidden;
        //validvalues
        //multivalue
        //usedinquery

        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent)
        {
            var el_parameter = parent.RS_AddElement("ReportParameter");
            el_parameter.SetAttributeValue("Name", this.Name);
            el_parameter.RS_SetElementValue("DataType", this.DataType.ToString());
            el_parameter.RS_SetElementValueCONDBOOL("Nullable", this.Nullable);
            el_parameter.RS_SetElementValueCOND("DefaultValue", this.DefaultValue);
            el_parameter.RS_SetElementValueCONDBOOL("AllowBlank", this.AllowBlank);
            el_parameter.RS_SetElementValueCONDBOOL("Hidden", this.Hidden);

            return el_parameter;
        }
    }
}