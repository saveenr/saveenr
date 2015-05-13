using MSCHART = System.Web.UI.DataVisualization.Charting;

namespace WebCharting.Format
{
    public class BarChart : XYChart
    {
        public BarStyle BarStyle { get; set; }
        public bool UseMultipleColors { get; set; }

        public BarChart(ChartFormat fmt) :
            base(fmt)
        {
            this.BarStyle = BarStyle.Vertical;
            this.UseMultipleColors = false;
        }

        public override void ConfigureLegend(MSCHART.Chart chart)
        {
            // do nothing
        }

        public override void ConfigureColors(MSCHART.Chart chart)
        {
            foreach (var series in chart.Series)
            {
                int i = 0;
                foreach (var point in series.Points)
                {
                    int color_index = this.UseMultipleColors ? i : 0;
                    if (this.ChartFormat.ColorStyle == ColorStyle.FlatColor)
                    {
                        point.Color = this.ChartFormat.Palette.GetBaseColor(color_index);
                    }
                    else
                    {
                        if (this.BarStyle == BarStyle.Horizontal)
                        {
                            point.Color = this.ChartFormat.Palette.GetDarkColor(color_index);
                            point.BackGradientStyle = MSCHART.GradientStyle.LeftRight;
                            point.BackSecondaryColor = this.ChartFormat.Palette.GetBaseColor(color_index);
                        }
                        else
                        {
                            point.Color = this.ChartFormat.Palette.GetBaseColor(color_index);
                            point.BackGradientStyle = MSCHART.GradientStyle.TopBottom;
                            point.BackSecondaryColor = this.ChartFormat.Palette.GetDarkColor(color_index);
                        }
                    }
                    i++;
                }
            }
        }
        public override void ConfigureSeriesType(MSCHART.Chart chart)
        {
            chart.Series[0].ChartType = GetSeriesChartType();
        }

        private MSCHART.SeriesChartType GetSeriesChartType()
        {
            return this.BarStyle == BarStyle.Horizontal ? MSCHART.SeriesChartType.Bar : MSCHART.SeriesChartType.Column;
        }

    }
}