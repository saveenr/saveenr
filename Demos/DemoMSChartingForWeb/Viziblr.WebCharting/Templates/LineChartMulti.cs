using MSCHART = System.Web.UI.DataVisualization.Charting;
using SD = System.Drawing;
using Viziblr.WebCharting.Extensions;

namespace Viziblr.WebCharting.Templates
{
    public class LineChartMulti : BaseLineChart<WebCharting.Data.DataSetMultiSeries>
    {
        public LineChartMulti() :
            base()
        {
        }

        public override void ConfigureLegend(MSCHART.Chart chart)
        {
            this.ConfigureMultiSeriesLegend(chart);
        }
    }
}