using MSCHART = System.Web.UI.DataVisualization.Charting;

namespace WebCharting.Format
{
    public class StackedAreaChart : XYChart
    {
        public StackedAreaChart(ChartFormat fmt) :
            base(fmt)
        {
        }

        public override void ConfigureLegend(MSCHART.Chart chart)
        {
            this.ConfigureMultiSeriesLegend(chart);
        }

        public override void ConfigureColors(MSCHART.Chart chart)
        {
            int i = 0;
            foreach (var series in chart.Series)
            {
                if (this.ChartFormat.ColorStyle == ColorStyle.FlatColor)
                {
                    series.Color = this.ChartFormat.Palette.GetBaseColor(i);
                }
                else
                {
                    series.Color = this.ChartFormat.Palette.GetBaseColor(i);
                    series.BackGradientStyle = MSCHART.GradientStyle.TopBottom;
                    series.BackSecondaryColor = this.ChartFormat.Palette.GetDarkColor(i);
                }
                i++;
            }
        }

        public override void ConfigureSeriesType(MSCHART.Chart chart)
        {
            foreach (var ser in chart.Series)
            {
                ser.ChartType = MSCHART.SeriesChartType.StackedArea;
            }
        }
    }
}