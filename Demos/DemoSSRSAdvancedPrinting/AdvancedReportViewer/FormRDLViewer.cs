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
    public partial class FormRDLViewer : Form
    {
        System.Xml.Linq.XDocument xml;
        public System.Xml.Linq.XDocument RDLXML
        {
            set { this.xml = value; }
            get { return this.xml;  }
        }

        public string RDLText
        {
            set { this.textBox1.Text = value; }

        }
        public FormRDLViewer(System.Xml.Linq.XDocument rdlxml)
        {
            InitializeComponent();

            this.xml = rdlxml;
            this.RDLText = xml.ToString();


            var md = SSRSCommon.RDLMetaData.Load(this.RDLXML);

            this.labelXmlNamespace.Text = md.Namespace.ToString();

            this.labelPageSettings.Text = string.Format("{0} x {1} ( {2}, {3}, {4}, {5} )", md.PageSize.Width,
                                                        md.PageSize.Height, md.MarginTop, md.MarginBottom, md.MarginLeft,
                                                        md.MarginRight);


        }


        void selectall(TextBox tb)
        {
            if (!String.IsNullOrEmpty(tb.Text))
            {
                tb.SelectionStart = 0;
                tb.SelectionLength = tb.Text.Length;
            }
        }

        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            this.selectall(this.textBox1);
        }
    }
}
