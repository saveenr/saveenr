using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoXAMLReporting
{
    class Program
    {
        static void Main(string[] args)
        {


            var items = typeof(string).Assembly.GetTypes().Take(100);
            var items2 = items.Select(i => new { i.Name, i.IsClass, i.IsEnum, i.IsValueType });


            var datatable = Isotope.Data.DataUtil.DataTableFromEnumerable(items2);

            string out_filename = "out.xaml";
            var encoding = System.Text.Encoding.UTF8;

            var x = new System.Xml.XmlTextWriter(out_filename, encoding);

            var repdef = new Isotope.Reporting.ReportDefinition();


            repdef.ReportHeaderDefinition.ReportTitle.TextStyle.FontSize = 18.0;
            repdef.ReportHeaderDefinition.ReportTitle.TextStyle.FontWeight = Isotope.Reporting.FontWeight.Bold;
            repdef.ReportHeaderDefinition.HeaderText.TextStyle.FontSize = 8.0;

            var colstyle1 = new Isotope.Reporting.TableColumnStyle();
            colstyle1.DetailTextStyle.FontWeight = Isotope.Reporting.FontWeight.Bold;
            colstyle1.HorzontalAlignment = Isotope.Reporting.HorzontalAlignment.Right;
            colstyle1.Width = 150;

            repdef.Table.ColumnStyles.Add(colstyle1);

            repdef.Table.TableStyle.CellSpacing = 10;
            repdef.WriteXML(x,datatable);
            x.Close();

        }
    }


}