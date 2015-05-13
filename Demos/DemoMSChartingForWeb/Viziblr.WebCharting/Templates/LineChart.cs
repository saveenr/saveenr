using MSCHART = System.Web.UI.DataVisualization.Charting;
using SD=System.Drawing;
using Viziblr.WebCharting.Extensions;
using Viziblr.WebCharting;

namespace Viziblr.WebCharting.Templates
{
    public class LineChart : BaseLineChart<WebCharting.Data.DataSetSingleSeries>
    {
        public LineChart() :
            base()
        {
        }

        public override void ConfigureLegend(MSCHART.Chart chart)
        {
            //
        }
    }
}