using SD = System.Drawing;
using WEBCONTROLS = System.Web.UI.WebControls;
using MSCHART = System.Web.UI.DataVisualization.Charting;

namespace Viziblr.WebCharting.Extensions
{
    public static class ChartExt
    {

    }

    public static class SDExt
    {
        public static SD.Color ToSystemColor(this Viziblr.Colorspace.ColorRGB32Bit color)
        {
            return SD.Color.FromArgb(color.ToInt());
        }
    }
}