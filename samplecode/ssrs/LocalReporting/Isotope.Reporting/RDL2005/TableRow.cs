namespace Isotope.Reporting.RDL2005
{
    public class TableRow : Node
    {
        public readonly NodeCollection<TableCell> TableCells;
        public double? Height;

        public TableRow()
        {
            this.TableCells = new NodeCollection<TableCell>();
        }

        public System.Xml.Linq.XElement  write(System.Xml.Linq.XElement parent)
        {
            var el_tablerow = parent.RS_AddElement("TableRow");
            var el_tablecells = el_tablerow.RS_AddElement("TableCells");

            foreach (var cell in this.TableCells.Items())
            {
                if (cell is TableCell)
                {
                    var celltb = (TableCell)cell;
                    celltb.write(el_tablecells);

                }

            }


            if (this.Height.HasValue)
            {
                el_tablerow.RS_SetElementValue("Height", this.Height.ToString() + "in");
                
            }

            return el_tablerow;

        }
    }
}