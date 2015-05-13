using MSCHART = System.Web.UI.DataVisualization.Charting;

namespace WebCharting.Format
{
    public class LineChart : BaseLineChart
    {
        public LineChart(ChartFormat fmt) :
            base(fmt)
        {
        }

        public override void ConfigureColors(MSCHART.Chart chart)
        {
            foreach (var series in chart.Series)
            {
                if (this.ChartFormat.ShowAreaUnderLine)
                {
                    var c = this.ChartFormat.Palette.GetBaseColor(0);
                    var top_color = System.Drawing.Color.FromArgb(0xff, c.R, c.G, c.B);
                    var bottom_color = System.Drawing.Color.FromArgb(0x20, c.R, c.G, c.B);

                    series.Color = top_color;
                    series.BackGradientStyle = MSCHART.GradientStyle.TopBottom;
                    series.BackSecondaryColor = bottom_color;
                }
                else
                {
                    series.Color = this.ChartFormat.Palette.GetBaseColor(0);
                }
            }
        }

        public override void ConfigureSeriesMarkers(MSCHART.Chart chart)
        {
            this.FormatMarkers(chart);
        }

        public override void ConfigureLegend(MSCHART.Chart chart)
        {
            //
        }

        public override void ConfigureSeriesType(MSCHART.Chart chart)
        {
            chart.Series[0].ChartType = this.GetSeriesChartType();
            chart.Series[0].BorderWidth = this.ChartFormat.LineChartLineWidth;
        }
    }
}