using System;
using System.Collections.Generic;

namespace Viziblr.Reporting.RDL2005
{
    public class TableGroup : Node
    {

        public List<string> GroupingExpressions = new List<string>();
        public string SortingExpressions;
        public Header Header;

        public string Name;

        public TableGroup()
        {
            this.Header = new Header();
        }

        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent)
        {
            var el_tablegroup = parent.RS_AddElement("TableGroup");

            var el_grouping = el_tablegroup.RS_AddElement("Grouping");
            el_grouping.SetAttributeValue("Name",this.Name);
            var el_ges = el_grouping.RS_AddElement("GroupExpressions");
            foreach (var expr in this.GroupingExpressions)
            {
                var el_ge = el_ges.RS_AddElement("GroupExpression");
                el_ge.Value = expr;
            }

            if (this.SortingExpressions!=null)
            {
                var el_Sorting = el_tablegroup.RS_AddElement("Sorting");
                var el_sb = el_Sorting.RS_AddElement("SortBy");
                var el_sbe = el_sb.RS_AddElement("SortExpresson");
                el_sbe.Value = this.SortingExpressions;
                
            }

            this.Header.write(el_tablegroup);

            return el_tablegroup;

        }
 
    }
}