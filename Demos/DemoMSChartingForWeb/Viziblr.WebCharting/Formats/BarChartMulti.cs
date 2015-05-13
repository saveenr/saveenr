using MSCHART = System.Web.UI.DataVisualization.Charting;

namespace WebCharting.Format
{
    public class BarChartMulti : XYChart
    {
        public BarStyle BarStyle { get; set; }

        public BarChartMulti(ChartFormat fmt) :
            base(fmt)
        {
            this.BarStyle = BarStyle.Vertical;
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
                    if (this.BarStyle == BarStyle.Horizontal)
                    {
                        series.Color = this.ChartFormat.Palette.GetDarkColor(i);
                        series.BackGradientStyle = MSCHART.GradientStyle.LeftRight;
                        series.BackSecondaryColor = this.ChartFormat.Palette.GetBaseColor(i);
                    }
                    else
                    {
                        series.Color = this.ChartFormat.Palette.GetBaseColor(i);
                        series.BackGradientStyle = MSCHART.GradientStyle.TopBottom;
                        series.BackSecondaryColor = this.ChartFormat.Palette.GetDarkColor(i);
                    }
                }
                i++;
            }
        }

        public override void ConfigureLegend(MSCHART.Chart chart)
        {
            this.ConfigureMultiSeriesLegend(chart);
        }

        public override void ConfigureSeriesType(MSCHART.Chart chart)
        {
            foreach (var ser in chart.Series)
            {
                ser.ChartType = GetSeriesChartType();
            }
        }

        protected virtual MSCHART.SeriesChartType GetSeriesChartType()
        {
            return this.BarStyle == BarStyle.Horizontal ? MSCHART.SeriesChartType.Bar : MSCHART.SeriesChartType.Column;
        }
    }
}