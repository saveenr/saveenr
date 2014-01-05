using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IR=Isotope.Reporting;

namespace Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var dt = new System.Data.DataTable();
            dt.TableName = "Untitled";
            dt.Columns.Add("Name", typeof (string));
            dt.Columns.Add("Public", typeof (bool));
            dt.BeginLoadData();
            foreach (var type in typeof(string).Assembly.GetExportedTypes().Take(100))
            {
                dt.Rows.Add(type.Name, type.IsPublic);
            }
            dt.EndLoadData();

            var model = new IR.Modeling.Report();
            model.DataTable = dt;
            
            //NormalizeDataTableColumnNames();
            var repdef = CreateSimpleReportStyle1();
            
            repdef.ReportTitle = dt.TableName;
            repdef.DataTable = dt;
            repdef.DatasetName = dt.TableName;
            repdef.DefaultFontFamily = "Segoe UI";

            //repdef.GroupOnColumns.AddRange( new int [] { 0 });

            var viewingoptions = new Isotope.Reporting.ViewingOptions();

            //if (this.OutputRDLFilename != null)
            //{
            //    viewingoptions.OutputRDLFilename = this.OutputRDLFilename;
            //    viewingoptions.SaveRDLFile = true;
            //}
            

            Isotope.Reporting.Util.ShowReport(repdef,viewingoptions,null);
        }

        private static IR.Modeling.Report CreateSimpleReportStyle1()
        {
            var repdef = new IR.Modeling.Report();
            repdef.TableCellBorderStyle = Isotope.Reporting.RDL2005.BorderStyleEnum.Solid;
            repdef.TableCellBorderColor = "#e0e0e0";
            repdef.DefaultFontFamily = "Calibri";
            repdef.DefaultDetailRowFontSize = 10;
            repdef.DefaultHeaderRowFontSize= 10;
            repdef.DefautlReportTitleFontSize = 18;
            repdef.DefaultHeaderRowFontWeight = "Bold";
            repdef.DefaultDetailRowBackgroundColor = "#f7f7f7";
            repdef.DefaultDetailRowBackgroundColorAlternate = "#f0f0f0";
            //repdef.TableCellBorderColor = "#00ff00";
            return repdef;
        }

        private void NormalizeDataTableColumnNames(System.Data.DataTable dt)
        {
            foreach (System.Data.DataColumn col in dt.Columns)
            {
                var new_name = IR.Util.NormalizeColumnName(col.ColumnName);
                col.ColumnName = new_name;
                col.Caption = new_name;
            }
        }


    }
}
