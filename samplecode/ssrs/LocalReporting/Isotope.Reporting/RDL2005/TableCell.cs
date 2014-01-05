namespace Isotope.Reporting.RDL2005
{
    public class TableCell: Node
    {
        public readonly NodeCollection<Node> ReportItems;

        public TableCell()
        {
            this.ReportItems = new NodeCollection<Node>();
        }

        public void write( System.Xml.Linq.XElement parent)
        {
            var el_tablecell = parent.RS_AddElement("TableCell");

            var el_reportitems = el_tablecell.RS_AddElement("ReportItems");

            foreach (var cell in this.ReportItems.Items())
            {
                if ( cell is TextBox)
                {
                    var celltb = (TextBox) cell;
                    celltb.write(el_reportitems);
                   
                }

            }

        }
    }
}