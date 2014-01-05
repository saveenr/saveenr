using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Isotope.Reporting
{
    public partial class ReportViewerForm : Form
    {
        public string RDL;

        public ReportViewerForm()
        {
            InitializeComponent();
        }

        public Microsoft.Reporting.WinForms.ReportViewer ReportViewer
        {
            get { return this.reportViewer1; }
        }

        private void ReportViewerForm_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }


        public static void SaveToPDF(Microsoft.Reporting.WinForms.ReportViewer reportviewer, string outputfilename)
        {
            string deviceinfo = null;
            string mimetype = null;
            string encoding = null;
            string filenamextension = null;
            string[] streams;
            Microsoft.Reporting.WinForms.Warning[] warnings;
            
            string format = "PDF";

            var bytes = reportviewer.LocalReport.Render(
                format,
                deviceinfo,
                out mimetype,
                out encoding,
                out filenamextension,
                out streams,
                out warnings);


            using (var fs = new System.IO.FileStream(outputfilename, System.IO.FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
            }
        }


        public static void SaveToExcel(Microsoft.Reporting.WinForms.ReportViewer reportviewer, string outputfilename)
        {
            string deviceinfo = null;
            string mimetype = null;
            string encoding = null;
            string filenamextension = null;
            string[] streams;
            Microsoft.Reporting.WinForms.Warning[] warnings;

            string format = "Excel";

            var bytes = reportviewer.LocalReport.Render(
                format,
                deviceinfo,
                out mimetype,
                out encoding,
                out filenamextension,
                out streams,
                out warnings);


            using (var fs = new System.IO.FileStream(outputfilename, System.IO.FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
            }
        }

        private void buttonExportPDF_Click(object sender, EventArgs e)
        {
            string outputfilename = @"d:\output.pdf";


            SaveToPDF(this.reportViewer1, outputfilename);
        }

        private void buttonExportExcel_Click(object sender, EventArgs e)
        {
            string outputfilename = @"d:\output.xls";
            SaveToExcel(this.reportViewer1, outputfilename);
        }

        private void buttonExportRDL_Click(object sender, EventArgs e)
        {
            if (this.RDL != null)
            {
                string outputfilename = @"d:\output.rdl";
                var fp = System.IO.File.CreateText(outputfilename);
                fp.Write(this.RDL);
                fp.Close();

            }
        }
    }
}