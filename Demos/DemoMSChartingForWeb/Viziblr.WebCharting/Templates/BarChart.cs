using MSCHART = System.Web.UI.DataVisualization.Charting;
using SD=System.Drawing;
using Viziblr.WebCharting.Extensions;

namespace Viziblr.WebCharting.Templates
{
    public class BarChart : BaseBarChart<WebCharting.Data.DataSetSingleSeries>
    {
        public bool UseMultipleColors = false;

        public BarChart() :
            base()
        {
        }

        public override void ConfigureLegend(MSCHART.Chart chart)
        {
            // do nothing
        }


        protected override void format_series(MSCHART.Series series, int series_index)
        {
            // do nothing
        }

        protected override void format_point(MSCHART.DataPoint point, int series_index, int point_index)
        {
            int point_color_index = this.UseMultipleColors ? point_index : 0;
            var point_pal_item = this.Palette[point_color_index];
            if (this.GradientStyle == GradientStyle.FlatColor)
            {
                point.Color = point_pal_item.Color.ToSystemColor();
            }
            else
            {
                if (this.BarStyle == BarStyle.Horizontal)
                {
                    point.Color = point_pal_item.Color.ToSystemColor();
                    point.BackGradientStyle = MSCHART.GradientStyle.LeftRight;
                    point.BackSecondaryColor = point_pal_item.SecondaryColor.ToSystemColor();
                }
                else
                {
                    point.Color = point_pal_item.Color.ToSystemColor();
                    point.BackGradientStyle = MSCHART.GradientStyle.TopBottom;
                    point.BackSecondaryColor = point_pal_item.SecondaryColor.ToSystemColor();
                }
            }
        }
    }
}