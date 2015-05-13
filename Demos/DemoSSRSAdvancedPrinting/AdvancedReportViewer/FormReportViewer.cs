using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AdvancedReportViewer
{
    public partial class FormReportViewer : Form
    {
        private static SSRSCommon.ReportExecutionService.ReportExecutionService rep_exec_svc = SSRSCommon.RSUtil.ConnectToReportExecutionService();
        private static SSRSCommon.ReportService2005.ReportingService2005 rep_svc = SSRSCommon.RSUtil.ConnectToReportingService();
  
        private System.Xml.Linq.XDocument ReportXML;

        public FormReportViewer()
        {
            InitializeComponent();

            this.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;

            string url = SSRSCommon.RSUtil.GetReportServerUrl(rep_exec_svc);
            this.textBoxReportServer.Text = url;

            this.reportViewer1.Print += reportViewer1_Print;
            this.reportViewer1.ReportExport += reportViewer1_ReportExport;

            //this hides the print button, the print layout button, and the page layout button
            this.reportViewer1.ShowPrintButton = false;
            //this.refreshreport();
        }

        void reportViewer1_ReportExport(object sender, Microsoft.Reporting.WinForms.ReportExportEventArgs e)
        {
            if (e.Extension.Name == "PDF")
            {
                var report = this.reportViewer1.ServerReport;
                var md = SSRSCommon.RDLMetaData.Load(this.ReportXML);
                
                var devinfo = new SSRSCommon.DeviceInfo();

                devinfo.PageHeight = string.Format("{0}in", md.PageSize.Height);
                devinfo.PageWidth= string.Format("{0}in", md.PageSize.Width);
                devinfo.MarginBottom = string.Format("{0}in", md.MarginBottom);
                devinfo.MarginTop = string.Format("{0}in", md.MarginTop);
                devinfo.MarginLeft= string.Format("{0}in", md.MarginLeft);
                devinfo.MarginRight= string.Format("{0}in", md.MarginRight);

                MessageBox.Show(devinfo.ToString());

                e.DeviceInfo = devinfo.ToString();
            }
        }

        void reportViewer1_Print(object sender, Microsoft.Reporting.WinForms.ReportPrintEventArgs e)
        {
            MessageBox.Show("Custom Printing Code");
            //e.Cancel = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.refreshreport();
        }

        private void buttonRenderReport_Click(object sender, EventArgs e)
        {
            refreshreport();
        }

        private void refreshreport()
        {
            var rep = this.reportViewer1.ServerReport;
            var exec_header = new SSRSCommon.ReportExecutionService.ExecutionHeader();
            rep_exec_svc.ExecutionHeaderValue = exec_header;
            string historyid = null;
            var reportserver_url =  new System.Uri(this.textBoxReportServer.Text);
            var report = this.reportViewer1.ServerReport;
            report.ReportServerUrl = reportserver_url;
            report.ReportPath = this.GetFullReportPath();
            this.ReportXML = SSRSCommon.RSUtil.GetRDLXML(rep_svc, rep);
            this.reportViewer1.RefreshReport();
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            this.reportViewer1.PrintDialog();
        }

        private void buttonPrintPreview_Click(object sender, EventArgs e)
        {
            var md = SSRSCommon.RDLMetaData.Load(this.ReportXML);
            
            var form = new SSRSPrintPreview(rep_exec_svc, rep_svc, this.textBoxReportServer.Text, this.GetFullReportPath(), md);
            form.ShowDialog();
        }

        public string GetFullReportPath()
        {
            string s = "/" + URLJoin( new [] { this.textBoxPath.Text, this.textBoxReportPath.Text} );
            return s;
        }

        public string GetPath()
        {
            string s = "/" + this.textBoxPath.Text.Trim(pathseps);
            return s;
        }

        public string ReportServer
        {
            get { return this.textBoxReportServer.Text; }
            set { this.textBoxReportServer.Text = value;  }
        }

        public string ReportPath
        {
            get { return this.textBoxPath.Text; }
            set { this.textBoxPath.Text = value; }
        }

        public string ReportName
        {
            get { return this.textBoxReportPath.Text; }
            set { this.textBoxReportPath.Text = value; }
        }

        private static char[] pathseps = new char[] {'/'};

        public string URLJoin( IEnumerable<string> tokens)
        {
            var sb = new StringBuilder();
            int n = 0;
            foreach (var token in tokens)
            {
                if (n > 0)
                {
                    sb.Append("/");
                }
                sb.Append(token.Trim(pathseps));

                n++;

            }
            return sb.ToString();
        }

        private void buttonSelectReport_Click(object sender, EventArgs e)
        {
            var form = new FormReportPicker(rep_exec_svc, rep_svc, this.textBoxReportServer.Text, this.GetPath());
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.textBoxReportPath.Text = form.Report;
                this.refreshreport();
            }
        }

        private void buttonGetRDL_Click(object sender, EventArgs e)
        {
            var Form = new FormRDLViewer(this.ReportXML);
            Form.ShowDialog();
        }
    }
}
