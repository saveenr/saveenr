using MSCHART=System.Web.UI.DataVisualization.Charting;

namespace Viziblr.WebCharting.Templates
{
    public class StackedBarChart : BarChartMulti
    {
        public StackedBarChart() :
            base()
        {
        }

        protected override MSCHART.SeriesChartType GetSeriesChartType()
        {
            return this.BarStyle == BarStyle.Horizontal ? MSCHART.SeriesChartType.StackedBar : MSCHART.SeriesChartType.StackedColumn;
        }
    }
}