using System.Collections.Generic;

namespace Isotope.Reporting
{
    public class SimpleTable
    {
        public System.Data.DataTable DataTable;
        public TableStyle TableStyle = new TableStyle();
        public TextStyle HeaderTextStyle = new TextStyle();
        public TextStyle DetailTextStyle = new TextStyle();

        public List<TableColumnStyle> ColumnStyles;

        public SimpleTable()
        {
            this.TableStyle = new TableStyle();
            this.HeaderTextStyle = new TextStyle();
            this.HeaderTextStyle.FontWeight = FontWeight.Bold;
            this.HeaderTextStyle.FontSize = ReportDefinition.default_font_size;
            
            this.DetailTextStyle = new TextStyle();
            this.DetailTextStyle.FontSize = ReportDefinition.default_font_size;

            this.ColumnStyles = new List<TableColumnStyle>();
        }

    }
}