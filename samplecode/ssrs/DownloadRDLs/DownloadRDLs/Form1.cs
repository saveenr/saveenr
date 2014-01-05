using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DownloadRDLs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var s = new RS2005.ReportingService2005();
            s.Credentials = System.Net.CredentialCache.DefaultCredentials;
            var items = s.ListChildren("/", true);

            foreach (var item in items)
            {
                if ( item.Type == RS2005.ItemTypeEnum.Report)
                {
                    string rep = item.Path;
                    var defi = s.GetReportDefinition(rep);

                    string path = @"D:\outrdl";
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    string fname = System.IO.Path.Combine(path, item.Name) + ".rdl";
                    System.IO.File.WriteAllBytes(fname,defi);
                }
            }


            int n = 1;
        }
    }
}
