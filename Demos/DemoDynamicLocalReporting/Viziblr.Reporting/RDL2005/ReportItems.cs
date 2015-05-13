using System.Collections.Generic;

namespace Viziblr.Reporting.RDL2005
{
    public class ReportItems : Node
    {
        private readonly NodeCollection<Node> _reportitems;

        public ReportItems()
        {
            this._reportitems = new NodeCollection<Node>();
        }
        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent)
        {

            var el_reportitems = parent.RS_AddElement("ReportItems");

            foreach (var cell in this._reportitems.Items())
            {
                if (cell is TextBox)
                {
                    var celltb = (TextBox)cell;
                    celltb.write(el_reportitems);

                }
                if (cell is Table)
                {
                    var celltb = (Table)cell;
                    celltb.write(el_reportitems);

                }

            }
            return el_reportitems;
        }

        public IEnumerable<Node> Items
        {
            get
            {
                return this._reportitems.Items();
            }
        }

        public Node Add(Node n)
        {
            this._reportitems.Add(n);
            return n;
        }

        
    }
}