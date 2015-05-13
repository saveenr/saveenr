using MSCHART = System.Web.UI.DataVisualization.Charting;
using SD = System.Drawing;

namespace WebCharting.Format
{
    public class LineChartMulti : BaseLineChart
    {
        public LineChartMulti(ChartFormat fmt) :
            base(fmt)
        {
        }

        public override void ConfigureColors(MSCHART.Chart chart)
        {
            int i = 0;
            foreach (var series in chart.Series)
            {
                var paletteItem = this.ChartFormat.Palette[i];
                if (this.ChartFormat.ShowAreaUnderLine)
                {
                    var tc = paletteItem.Color;
                    var bc = new Isotope.Colorspace.ColorRGB32Bit(0x20, paletteItem.SecondaryColor);

                    var top_color = System.Drawing.Color.FromArgb(tc.ToInt());
                    var bottom_color = System.Drawing.Color.FromArgb(bc.ToInt());

                    series.Color = top_color;
                    series.BackGradientStyle = MSCHART.GradientStyle.TopBottom;
                    series.BackSecondaryColor = bottom_color;
                }
                else
                {
                    series.Color = System.Drawing.Color.FromArgb(paletteItem.Color.ToInt());
                }
                i++;
            }
        }

        public override void ConfigureLegend(MSCHART.Chart chart)
        {
            this.ConfigureMultiSeriesLegend(chart);
        }

        public override void ConfigureSeriesMarkers(MSCHART.Chart chart)
        {
            this.FormatMarkers(chart);
        }

        public override void ConfigureSeriesType(MSCHART.Chart chart)
        {
            foreach (var ser in chart.Series)
            {
                ser.ChartType = this.GetSeriesChartType();
                ser.BorderWidth = this.ChartFormat.LineChartLineWidth;
            }
        }
    }
}