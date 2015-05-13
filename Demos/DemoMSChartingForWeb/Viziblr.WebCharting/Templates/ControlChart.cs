using System.Collections.Generic;
using Viziblr.WebCharting.Data;
using MSCHART = System.Web.UI.DataVisualization.Charting;
using System.Linq;
using SD=System.Drawing;
using Viziblr.WebCharting.Extensions;

namespace Viziblr.WebCharting.Templates
{
    public class ControlChart : XYChart<WebCharting.Data.DataSetSingleSeries>
    {
        // For reference: http://en.wikipedia.org/wiki/Control_chart

        public LineFormat LineFormat = new LineFormat();
        public MarkerFormat MarkerFormat = new MarkerFormat();
        public double MeanValue { get; set; }
        public double StandardDeviation { get; set; }

        public ControlChart(double mean, double stddev) :
            base()
        {
            this.MeanValue = mean;

            if (stddev < 0)
            {
                throw new System.ArgumentOutOfRangeException("stddev");
            }
            this.StandardDeviation = stddev;
            this.CategoryAxis.ShowAxisMargin = true;
        }

        public override void ConfigureColors(MSCHART.Chart chart)
        {
            int i = 0;
            foreach (var series in chart.Series)
            {
                if (i == 0)
                {
                    var pal_item = this.Palette[i];

                    series.Color = pal_item.Color.ToSystemColor();
                }
                else
                {
                    var pal_item = this.Palette[i];

                    series.Color = pal_item.Color.ToSystemColor();
                    series.BorderWidth = 2;
                    series.BorderDashStyle = MSCHART.ChartDashStyle.Dash;
                }
                i++;
            }
        }

        public override void ConfigureLegend(MSCHART.Chart chart)
        {
            // do nothing
        }

        protected override void BindSeries(MSCHART.Chart chart, Data.BaseDataSet chartdata)
        {
            base.BindSeries(chart, chartdata);
            var points = chartdata[0];
            double upper_control_limit = this.MeanValue + (3.0 * this.StandardDeviation);
            double lower_control_limit = this.MeanValue - (3.0 * this.StandardDeviation);

            var datapoints_mean = new WebCharting.Data.SeriesDataPoints(Enumerable.Repeat(this.MeanValue, points.Count));
            this.AddNewSeriesHorizLine(chart, datapoints_mean, chartdata.XAxisLabels);

            var datapoints_ucl = new WebCharting.Data.SeriesDataPoints(Enumerable.Repeat(upper_control_limit, points.Count));
            this.AddNewSeriesHorizLine(chart, datapoints_ucl, chartdata.XAxisLabels);

            var datapoints_lcl = new WebCharting.Data.SeriesDataPoints(Enumerable.Repeat(lower_control_limit, points.Count));
            this.AddNewSeriesHorizLine(chart, datapoints_lcl, chartdata.XAxisLabels);
        }

        public override void ConfigureSeriesType(MSCHART.Chart chart)
        {
            foreach (var ser in chart.Series)
            {
                ser.ChartType = this.GetSeriesChartType();
                ser.BorderWidth = this.LineFormat.LineWidth;
            }
        }

        protected void AddNewSeriesHorizLine(MSCHART.Chart chart, WebCharting.Data.SeriesDataPoints datapoints, WebCharting.Data.AxisLabels labels)
        {
            var ser = new MSCHART.Series();
            ser.Points.DataBindXY(labels.ToArray(), datapoints.GetDoubleArray());
            if (datapoints.Name != null)
            {
                ser.LegendText = datapoints.Name;
            }
            for (int i = 0; i < datapoints.Count; i++)
            {
                var chart_datapoint = ser.Points[i];
                var v = datapoints[i];
            }
            chart.Series.Add(ser);
        }

        public override void ConfigureSeriesMarkers(MSCHART.Chart chart)
        {
            var ser = chart.Series[0];
            ser.Color = this.Palette[0].Color.ToSystemColor();
            ser.MarkerStyle = this.GetMSCHARTMarkerStyle();
            ser.MarkerSize = this.MarkerFormat.MarkerSize;
            ser.MarkerBorderColor = ser.Color;
            ser.MarkerBorderWidth = ser.BorderWidth;
            ser.MarkerColor = this.MarkerFormat.MarkerColor.ToSystemColor();
        }

        public double StdDev(IEnumerable<double> values)
        {
            double ret = 0;
            int count = values.Count();
            if (count > 1)
            {
                //Compute the Average          
                double avg = values.Average();
                //Perform the Sum of (value-avg)^2          
                double sum = values.Sum(d => (d - avg)*(d - avg));
                //Put it all together          
                ret = System.Math.Sqrt(sum/(count - 1));
            }
            return ret;
        }

        protected MSCHART.SeriesChartType GetSeriesChartType()
        {
            if (this.LineFormat.LineStyle == LineStyle.Jagged)
            {
                return MSCHART.SeriesChartType.Line;
            }
            else
            {
                return MSCHART.SeriesChartType.Spline;
            }
        }

        internal MSCHART.MarkerStyle GetMSCHARTMarkerStyle()
        {
            if (this.MarkerFormat.MarkerStyle == MarkerStyle.Circle)
            {
                return MSCHART.MarkerStyle.Circle;
            }
            else if (this.MarkerFormat.MarkerStyle == MarkerStyle.Circle)
            {
                return MSCHART.MarkerStyle.None;
            }
            else
            {
                throw new System.ArgumentOutOfRangeException();
            }
        }
    }
}