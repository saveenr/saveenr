using MSCHART = System.Web.UI.DataVisualization.Charting;
using SD=System.Drawing;
using Viziblr.WebCharting.Extensions;

namespace Viziblr.WebCharting.Templates
{
    public class BarChartMulti : BaseBarChart<WebCharting.Data.DataSetMultiSeries>
    {

        public BarChartMulti() :
            base()
        {
        }

        public override void ConfigureLegend(MSCHART.Chart chart)
        {
            this.ConfigureMultiSeriesLegend(chart);
        }

        protected override void format_series(MSCHART.Series series, int series_index)
        {
            var series_pal_item = this.Palette[series_index];
            if (this.GradientStyle == GradientStyle.FlatColor)
            {
                series.Color = series_pal_item.Color.ToSystemColor();
            }
            else
            {
                if (this.BarStyle == BarStyle.Horizontal)
                {
                    series.Color = series_pal_item.Color.ToSystemColor();
                    series.BackGradientStyle = MSCHART.GradientStyle.LeftRight;
                    series.BackSecondaryColor = series_pal_item.SecondaryColor.ToSystemColor();
                }
                else
                {
                    series.Color = series_pal_item.Color.ToSystemColor();
                    series.BackGradientStyle = MSCHART.GradientStyle.TopBottom;
                    series.BackSecondaryColor = series_pal_item.SecondaryColor.ToSystemColor();
                }
            }
        }

        protected override void format_point(MSCHART.DataPoint point, int series_index, int point_index)
        {
            // do nothing
        }
    }
}