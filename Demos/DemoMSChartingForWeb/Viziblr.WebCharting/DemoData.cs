using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Viziblr.WebCharting
{
    public static class DemoData
    {
        public static WebCharting.Data.DataSetSingleSeries GetSingleSeriesPalette()
        {
            var pal = PaletteBuilder.GetDefaultPalette();
            var items = pal.Select( i=> new WebCharting.Data.DataPoint(1.0));
            
            var values = new WebCharting.Data.SeriesDataPoints(items);
            var axislabels = new 
                WebCharting.Data.AxisLabels(
                    pal.Select(i => i.Color.ToWebColorString()));
 
            var chartdata = new WebCharting.Data.DataSetSingleSeries(values, axislabels);

            return chartdata;
        }

        public static WebCharting.Data.DataSetSingleSeries DomainLengthVsFrequency()
        {
            var data = new [] 
            {
                new { bucket = 5	, count = 40178.57143 },
                new {bucket = 6, count = 125000.0},
                new {bucket = 7, count = 263392.8571},
                new {bucket = 8, count = 312500.0},
                new {bucket = 9, count = 602678.5714},
                new {bucket = 10, count = 776785.7143},
                new {bucket = 11, count = 991071.4286},
                new {bucket = 12, count = 1232142.857},
                new {bucket = 13, count = 531250.0},
                new {bucket = 14, count = 486607.1429},
                new {bucket = 15, count = 428571.4286},
                new {bucket = 16, count = 383928.5714},
                new {bucket = 17, count = 263392.8571},
                new {bucket = 18, count = 232142.8571},
                new {bucket = 19, count = 187500.0},
                new {bucket = 20, count = 160714.2857},
                new {bucket = 21, count = 125000.0},
                new {bucket = 22, count = 98214.28571},
                new {bucket = 23, count = 84821.42857},
                new {bucket = 24, count = 71428.57143},
                new {bucket = 25, count = 62500.0},
                new {bucket = 26, count = 44642.85714}
        };
            var chartdata = new WebCharting.Data.DataSetSingleSeries();

            foreach (var item in data)
            {
                var dataPoint = new WebCharting.Data.DataPoint(item.count, null, null, null);
                chartdata.Add(dataPoint, item.bucket.ToString());
            }
            return chartdata;

    }

        public static WebCharting.Data.DataSetSingleSeries DomainsInAddressBar()
        {
            var data = new[]
                           {
                               new { val=63.0, text=".com"},
                               new { val=25.0, text=".other"},
                               new { val=4.0, text=".de"},
                               new { val=4.0, text=".net"},
                               new { val=4.0, text=".org"}
                           };

            var chartdata = new WebCharting.Data.DataSetSingleSeries();

            foreach (var item in data)
            {
                string callout = string.Format("{0:p1} {1}", item.val/100.0, item.text.ToUpper());
                var dataPoint = new WebCharting.Data.DataPoint(item.val, null, null, callout);
                chartdata.Add(dataPoint, item.text.ToUpper());
            }
            return chartdata;
        }

        public static WebCharting.Data.DataSetSingleSeries WidgetsByDate()
        {
            var values_raw = new double[] {32, 25, 19, 31, 43, 36, 51, 38, 29, 31, 57, 29, 38, 40, 48};
            var values = DemoData.DoublesToDataPoints(values_raw);
            var days = GetLastNDays(values.Count).Select(i => i.ToString("M/d"));
            var labels = new WebCharting.Data.AxisLabels(days);
            var chartdata = new WebCharting.Data.DataSetSingleSeries(values, labels);
            return chartdata;
        }

        public static WebCharting.Data.DataSetSingleSeries WidgetsByMonth()
        {
            var values_raw = new double[] { 32, 25, 19, 31, 43, 36, 51, 38, 29, 59, 68, 29, 38, 40, 48 };
            var values = DemoData.DoublesToDataPoints(values_raw);
            var months = GetLastNMonths(values.Count).Select(i => i.ToString("M/y"));
            var labels = new WebCharting.Data.AxisLabels(months);
            var chartdata = new WebCharting.Data.DataSetSingleSeries(values, labels);

            return chartdata;
        }

        public static IEnumerable<System.DateTime> GetLastNDays(int n)
        {
            var today = System.DateTime.Now;
            return Enumerable.Range(0, n)
                .Select(i => today.AddDays(-n-1).AddDays(i));
        }
        public static IEnumerable<System.DateTime> GetLastNMonths(int n)
        {
            var today = System.DateTime.Now;
            return Enumerable.Range(0, n)
                .Select(i => today.AddMonths(-n-1).AddMonths(i));
        }

        public static WebCharting.Data.DataSetSingleSeries GetSingleSeries1n(int c)
        {
            var values_raw = new double[] { 32, 25, 19, 31, 43, 36, 51, 38, 29, 31, 57, 29, 38, 40, 48 };
            values_raw = values_raw.Take(c).ToArray();

            var values = DemoData.DoublesToDataPoints(values_raw);

            var today = System.DateTime.Now;
            var start = today.AddDays(-values.Count);
            var labels =
                new WebCharting.Data.AxisLabels(
                    Enumerable.Range(0, values.Count).Select(i => start.AddDays(i).ToString("M/d")));

            var chartdata = new WebCharting.Data.DataSetSingleSeries(values, labels);

            return chartdata;
        }

        public static WebCharting.Data.DataSetSingleSeries GetSingleSeries3()
        {
            var values_raw = new double[] { 32, 25, 19, 31, 43, 36};
            var values = DemoData.DoublesToDataPoints(values_raw);

            var today = System.DateTime.Now;
            var start = today.AddDays(-values.Count);
            var labels =
                new WebCharting.Data.AxisLabels(
                    Enumerable.Range(0, values.Count).Select(i => start.AddDays(i).ToString("M/d")));

            var chartdata = new WebCharting.Data.DataSetSingleSeries(values, labels);

            return chartdata;
        }

        public static WebCharting.Data.DataSetSingleSeries GetSingleSeries2()
        {
            var types = typeof (System.String).Assembly.GetExportedTypes()
                .Where(t => t.IsClass)
                .Where(t => t.IsPublic);

            var data =
                types.Select(
                    t =>
                    new
                        {
                            typename = t.Name,
                            typefullname = t.FullName,
                            nummethods = t.GetMethods().Length,
                            numprops = t.GetProperties().Length
                        });

            int topn = 5;
            var final_data = (from p in data
                              orderby p.nummethods descending
                              select p).Take(topn).ToList();

            var datapoints = new WebCharting.Data.SeriesDataPoints();
            var labels = new WebCharting.Data.AxisLabels();
            for (int i = 0; i < final_data.Count; i++)
            {
                var datum = final_data[i];
                string dp_hlink = string.Format("http://msdn.microsoft.com/en-us/library/{0}.aspx", datum.typefullname);

                var dp_label = datum.nummethods.ToString();
                var dp_value = datum.nummethods;
                var dp = new WebCharting.Data.DataPoint(dp_value, dp_hlink, dp_label);
                dp.Label = datum.nummethods.ToString();

                datapoints.Add(dp);
                labels.Add(datum.typename);
            }

            var chartdata = new WebCharting.Data.DataSetSingleSeries(datapoints, labels);

            return chartdata;
        }

        public static WebCharting.Data.SeriesDataPoints DoublesToDataPoints(double[] values_raw)
        {
            var datapoints = new WebCharting.Data.SeriesDataPoints();
            foreach (var val in values_raw)
            {
                var ts = string.Format("{0}", val.ToString());
                var hl = string.Format("http://www.microsoft.com/{0}", val.ToString());
                var label = val.ToString();
                var p = new WebCharting.Data.DataPoint(val, hl, ts);
                p.Label = label;

                datapoints.Add(p);
            }

            return datapoints;
        }

        public static WebCharting.Data.DataSetMultiSeries GetMultiSeries1()
        {
            var datapoints0 = new WebCharting.Data.SeriesDataPoints(new double[]
                                                                      {
                                                                          32,
                                                                          25,
                                                                          19,
                                                                          31,
                                                                          43,
                                                                          36,
                                                                          51,
                                                                          38,
                                                                          29,
                                                                          31,
                                                                          57,
                                                                          29,
                                                                          38,
                                                                          40,
                                                                          48
                                                                      });

            datapoints0.Hyperlink = "http://google.com";
            datapoints0.ToolTip = new ToolTip("Google");
            datapoints0.Name = "Google";

            var datapoints1 = new WebCharting.Data.SeriesDataPoints(new double[]
                                                                      {
                                                                          56,
                                                                          45,
                                                                          70,
                                                                          75,
                                                                          65,
                                                                          59,
                                                                          55,
                                                                          51,
                                                                          40,
                                                                          34,
                                                                          45,
                                                                          39,
                                                                          33,
                                                                          31,
                                                                          19
                                                                      });
            datapoints1.Hyperlink = "http://yahoo.com";
            datapoints1.ToolTip = new ToolTip("Yahoo");
            datapoints1.Name = "Yahoo";

            var values = new List<WebCharting.Data.SeriesDataPoints> { datapoints0, datapoints1 };


            var today = System.DateTime.Now;
            var start = today.AddDays(-datapoints0.Count);
            var labels =
                new WebCharting.Data.AxisLabels(
                    Enumerable.Range(0, datapoints0.Count).Select(i => start.AddDays(i).ToString("M/d")));

            var chartdata = new WebCharting.Data.DataSetMultiSeries(values, labels);

            return chartdata;
        }
    }
}