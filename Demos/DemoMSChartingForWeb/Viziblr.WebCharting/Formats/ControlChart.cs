using System.Collections.Generic;
using MSCHART = System.Web.UI.DataVisualization.Charting;
using SD = System.Drawing;
using System.Linq;

namespace WebCharting.Format
{
    public class ControlChart : XYChart
    {
        public LineStyle LineStyle { get; set; }

        public ControlChart(ChartFormat fmt) :
            base(fmt)
        {
            this.LineStyle = LineStyle.Jagged;
        }

        public override void ConfigureColors(MSCHART.Chart chart)
        {
            int i = 0;
            foreach (var series in chart.Series)
            {
                if (i == 0)
                {
                    var c = this.ChartFormat.Palette.GetBaseColor(i);
                    series.Color = c;
                }
                else
                {
                    series.Color = System.Drawing.Color.LightBlue;
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

        protected override void BindSeries(MSCHART.Chart chart, Data.DataSetSingleSeries chartdata)
        {
            base.BindSeries(chart, chartdata);
            double avg = chartdata.Values.Items.Select(i => i.Value).Average();
            double stddev = StdDev(chartdata.Values.Items.Select(i => i.Value));
            double top = avg + stddev;
            double bottom = avg - stddev;

            var dp_avg = new WebCharting.Data.SeriesDataPoints(Enumerable.Repeat(avg, chartdata.Values.Count));
            this.AddNewSeries(chart, dp_avg, chartdata.XAxisLabels);

            var dp_top = new WebCharting.Data.SeriesDataPoints(Enumerable.Repeat(top, chartdata.Values.Count));
            this.AddNewSeries(chart, dp_top, chartdata.XAxisLabels);

            var dp_bottom = new WebCharting.Data.SeriesDataPoints(Enumerable.Repeat(bottom, chartdata.Values.Count));
            this.AddNewSeries(chart, dp_bottom, chartdata.XAxisLabels);
        }

        public override void ConfigureSeriesType(MSCHART.Chart chart)
        {
            foreach (var ser in chart.Series)
            {
                ser.ChartType = this.GetSeriesChartType();
                ser.BorderWidth = this.ChartFormat.LineChartLineWidth;
            }
        }

        public override void ConfigureSeriesMarkers(MSCHART.Chart chart)
        {
            var ser = chart.Series[0];
            ser.Color = System.Drawing.Color.FromArgb(this.ChartFormat.Palette[0].Color.ToInt());
            ser.MarkerStyle = this.ChartFormat.GetMSCHARTMarkerStyle();
            ser.MarkerSize = this.ChartFormat.LineMarkerSize;
            ser.MarkerBorderColor = ser.Color;
            ser.MarkerBorderWidth = ser.BorderWidth;
            ser.MarkerColor = this.ChartFormat.LineMarkerColor;
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
            if (this.LineStyle == LineStyle.Jagged)
            {
                return MSCHART.SeriesChartType.Line;
            }
            else
            {
                return MSCHART.SeriesChartType.Spline;
            }
        }
    }
}