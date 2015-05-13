using MSCHART = System.Web.UI.DataVisualization.Charting;
using SD=System.Drawing;
using Viziblr.WebCharting.Extensions;

namespace Viziblr.WebCharting.Templates
{
    public class StackedAreaChart : XYChart<WebCharting.Data.DataSetMultiSeries>
    {
        public GradientStyle GradientStyle = GradientStyle.GradientColor;

        public StackedAreaChart() :
            base()
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
                if (this.GradientStyle == GradientStyle.FlatColor)
                {
                    var pal_item = this.Palette[i];

                    series.Color = pal_item.Color.ToSystemColor();
                }
                else
                {
                    var pal_item = this.Palette[i];

                    series.Color = pal_item.Color.ToSystemColor();
                    series.BackGradientStyle = MSCHART.GradientStyle.TopBottom;
                    series.BackSecondaryColor = pal_item.SecondaryColor.ToSystemColor();
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