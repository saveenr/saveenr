using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DemoMSChartDataBindCrossTable
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Setup the data
            var dt = new System.Data.DataTable();
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("BugCount", typeof(int));
            dt.Columns.Add("Day", typeof(int));
            dt.Rows.Add("Kim", 10, 0);
            dt.Rows.Add("Kim", 12, 1);
            dt.Rows.Add("Kim", 18, 2);
            dt.Rows.Add("Kim", 5, 3);
            dt.Rows.Add("Philby", 18, 0);
            dt.Rows.Add("Philby", 25, 1);
            dt.Rows.Add("Philby", 9, 2);
            dt.Rows.Add("Philby", 32, 3);

            // Build the chart
            this.chart1.Series.Clear();
            this.chart1.DataBindCrossTable(dt.Rows, "Name", "Day", "BugCount", "");

        }
    }
}
