using MSCHART = System.Web.UI.DataVisualization.Charting;
using SD=System.Drawing;
using Viziblr.WebCharting.Extensions;
using Viziblr.WebCharting;

namespace Viziblr.WebCharting.Templates
{
    public abstract class BaseLineChart<T> : XYChart<T> where T : WebCharting.Data.BaseDataSet
    {
        public LineFormat LineFormat = new LineFormat();
        public MarkerFormat MarkerFormat = new MarkerFormat();
        public bool ShowAreaUnderLine = false;

        public BaseLineChart():
            base()
        {
            
        }

        public override void ConfigureColors(MSCHART.Chart chart)
        {
            int i = 0;
            foreach (var series in chart.Series)
            {
                var paletteItem = this.Palette[i];
                if (this.ShowAreaUnderLine)
                {
                    var tc = paletteItem.Color;
                    var bc = new Viziblr.Colorspace.ColorRGB32Bit(0x20, paletteItem.SecondaryColor);

                    var top_color = tc.ToSystemColor();
                    var bottom_color = bc.ToSystemColor();

                    series.Color = top_color;
                    series.BackGradientStyle = MSCHART.GradientStyle.TopBottom;
                    series.BackSecondaryColor = bottom_color;
                }
                else
                {
                    series.Color = paletteItem.Color.ToSystemColor();
                }
                i++;
            }
        }

        public override void ConfigureSeriesMarkers(MSCHART.Chart chart)
        {
            int i = 0;
            foreach (var ser in chart.Series)
            {
                var pal_item = this.Palette[i];

                ser.MarkerStyle = MSChartUtil.GetMSCHARTMarkerStyle(this.MarkerFormat);
                ser.MarkerSize = this.MarkerFormat.MarkerSize;
                ser.MarkerBorderColor = pal_item.Color.ToSystemColor();
                ser.MarkerBorderWidth = this.LineFormat.LineWidth;
                ser.MarkerColor = this.MarkerFormat.MarkerColor.ToSystemColor();

                i++;
            }
        }

        public override void ConfigureSeriesType(MSCHART.Chart chart)
        {
            foreach (var ser in chart.Series)
            {
                ser.ChartType = WebCharting.MSChartUtil.GetLineChartSeriesChartType(this.LineFormat, this.ShowAreaUnderLine);
                ser.BorderWidth = this.LineFormat.LineWidth;
            }
        }
    }
}