using MSCHART=System.Web.UI.DataVisualization.Charting;

namespace WebCharting.Format
{
    public class StackedBarChart : BarChartMulti
    {
        public StackedBarChart(ChartFormat fmt) :
            base(fmt)
        {
        }

        protected override MSCHART.SeriesChartType GetSeriesChartType()
        {
            return this.BarStyle == BarStyle.Horizontal ? MSCHART.SeriesChartType.StackedBar : MSCHART.SeriesChartType.StackedColumn;
        }
    }
}