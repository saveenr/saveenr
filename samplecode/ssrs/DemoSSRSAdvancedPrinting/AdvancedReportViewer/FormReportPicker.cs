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
    public partial class FormReportPicker : Form
    {

        public SSRSCommon.ReportExecutionService.ReportExecutionService rep_exec_svc;
        public SSRSCommon.ReportService2005.ReportingService2005 rep_svc;

        public string ReportServerURL;
        public string Path;

        public string Report;

        public FormReportPicker(SSRSCommon.ReportExecutionService.ReportExecutionService rep_exec_svc, SSRSCommon.ReportService2005.ReportingService2005 rep_svc, string surl, string path)
        {
            InitializeComponent();

            this.rep_exec_svc = rep_exec_svc;
            this.rep_svc = rep_svc;
            this.ReportServerURL = surl;
            this.Path = path;

            var children = this.rep_svc.ListChildren(this.Path, false);
            foreach (var child in children)
            {
                this.listView1.Items.Add(child.Name);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {

            this.Report = (string) this.listView1.SelectedItems[0].Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}
