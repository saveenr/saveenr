using System.Collections.Generic;
using RDL=Isotope.Reporting.RDL2005;

namespace Isotope.Reporting.Modeling
{
    public class Report
    {
        public string DatasetName;
        public System.Data.DataTable DataTable;

        public PageLayout PageLayout;
        public DocumentProperties DocumentProperties;
        
        public double DefaultColumnWidth = 1.0;
        public double HeaderHeight = 0.5;
        public double DetailRowHeight = 0.25;

        public double BodyColumnSpacing = 0.5;


        public double TitleHeight = 0.5;
        public double DataRegionTop = 0.5;

        public string ReportTitle = "Untitled Report";
        
        public string DefaultFontFamily = "Arial";
        public double DefautlReportTitleFontSize = 12;
        public double DefaultDetailRowFontSize = 12;
        public string DefaultDetailRowBackgroundColor;
        public string DefaultDetailRowBackgroundColorAlternate;
        public double DefaultHeaderRowFontSize = 12;

        public string DefaultHeaderRowFontWeight;

        public RDL.BorderStyleEnum TableCellBorderStyle =
            RDL.BorderStyleEnum.None;

        public string TableCellBorderColor = null;
        public double? TableCellBorderWidth ;
        public List<int> GroupOnColumns = new List<int>();
        public List<int> HideColumns = new List<int>();
        public List<RDL.ColumnDef> ColumnDefinitions = new List<RDL.ColumnDef>();
        
        public Report()
        {
            this.PageLayout = new PageLayout();
            this.DocumentProperties = new DocumentProperties();
        }
    }
}