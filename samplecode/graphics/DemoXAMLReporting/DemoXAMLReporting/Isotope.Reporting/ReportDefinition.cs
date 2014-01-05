using System.Linq;

namespace Isotope.Reporting
{
    public class ReportDefinition
    {
        public static double default_font_size = 10.0;

        public TextStyle TextStyle;
        public ReportHeaderDefinition ReportHeaderDefinition ;
        public ReportFooterDefinition ReportFooterDefinition;
        
        
        public SimpleTable Table;

        public ReportDefinition( )
        {
            this.ReportHeaderDefinition = new ReportHeaderDefinition();
            this.ReportHeaderDefinition.ReportTitle.TextStyle.FontSize = default_font_size;

            this.ReportFooterDefinition = new ReportFooterDefinition();

            this.Table = new SimpleTable();
            this.TextStyle = new TextStyle();



        }

        public void WriteXML(System.Xml.XmlWriter x, System.Data.DataTable datatable)
        {

            this.Table.DataTable = datatable;
            // ---------------
            
            x.WriteStartDocument();
            x.WriteStartFlowDocument();
            x.WriteTextStyleAttributes(this.TextStyle);


            x.WriteX(this.ReportHeaderDefinition.ReportTitle);

            x.WriteX(this.ReportHeaderDefinition.HeaderText);

            
            x.WriteX(this.Table);

            x.WriteX(this.ReportFooterDefinition.FooterText);

            x.WriteEndFlowDocument();
            x.WriteEndDocument();
            x.Flush();
        }

    }
}