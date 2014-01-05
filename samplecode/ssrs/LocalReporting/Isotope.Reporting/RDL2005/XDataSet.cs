namespace Isotope.Reporting.RDL2005
{
    public class XDataSet : Node
    {
        public NodeCollection<Field> Fields;
        public string Name;
        public string CommandText;
        public string DataSourceName;

        public XDataSet()
        {
            this.Fields = new NodeCollection<Field>();
        }
        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent)
        {
            var el_dataset= parent.RS_AddElement("DataSet");
            el_dataset.SetAttributeValue("Name", this.Name);

            var query = el_dataset.RS_AddElement("Query");
            query.RS_SetElementValue("CommandText", this.CommandText);
            query.RS_SetElementValue("DataSourceName", this.DataSourceName);

            var el_fields = el_dataset.RS_AddElement("Fields");
            foreach (var field in this.Fields.Items())
            {
                field.write(el_fields);
            }


            return el_dataset;
        }
    }
}