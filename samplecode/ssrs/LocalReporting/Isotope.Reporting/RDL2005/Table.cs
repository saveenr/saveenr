namespace Isotope.Reporting.RDL2005
{
    public class Table : DataRegion
    {
        public Header Header;
        public Details Details ;
        public string Name;
        public double Top;

        public Style Style;

        public NodeCollection<TableColumn> TableColumns;
        public NodeCollection<TableGroup> TableGroups;

        public Table()
        {
            this.Header = new Header();
            this.Details = new Details();
            this.TableColumns = new NodeCollection<TableColumn>();
            this.TableGroups = new NodeCollection<TableGroup>(); 
            this.Style = new Style();
        }

        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent)
        {
            var el_table = parent.RS_AddElement("Table");
            el_table.SetAttributeValue("Name", this.Name);
            el_table.RS_SetElementValue("Top", this.Top.ToString() + "in");

            this.Style.write(el_table);
            this.Header.write(el_table);
            this.Details.write(el_table);



            el_table.RS_SetElementValue("DataSetName", this.DatasetName);
            var el_tablecolumns = el_table.RS_AddElement("TableColumns");
            foreach (var x in this.TableColumns.Items())
            {
                x.write(el_tablecolumns);
            }

            if (this.TableGroups.Count>0)
            {
                var el_tablegrops = el_table.RS_AddElement("TableGroups");
                foreach (var tg in this.TableGroups.Items())
                {
                    tg.write(el_tablegrops);
                }
            }

            if (this.Filters.Count>0)
            {
                var el_filters = el_table.RS_AddElement("Filters");
                foreach (var filter in this.Filters.Items())
                {
                    filter.write(el_filters);
                }
                
            }

            return el_table;

        }
    }
}