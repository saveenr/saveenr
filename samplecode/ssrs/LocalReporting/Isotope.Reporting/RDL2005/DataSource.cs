namespace Isotope.Reporting.RDL2005
{
    public class DataSource : Node
    {

        public string DatasetName;

        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent)
        {

            var el_datasource = parent.RS_AddElement("DataSource");
            el_datasource.SetAttributeValue("Name", this.DatasetName);
            var connectionproperties = el_datasource.RS_AddElement("ConnectionProperties");
            connectionproperties.RS_AddElement("DataProvider");
            connectionproperties.RS_AddElement("ConnectString");

            return el_datasource;

        }

    }
}