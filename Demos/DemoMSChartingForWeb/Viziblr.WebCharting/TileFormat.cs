using System;
using System.Drawing;
using SD = System.Drawing;
using WEBCONTROLS = System.Web.UI.WebControls;
using MSCHART = System.Web.UI.DataVisualization.Charting;

namespace Viziblr.WebCharting
{
    public class TileFormat
    {
        public TileStyle Style = TileStyle.None;
        public Viziblr.Colorspace.ColorRGB32Bit Color = new Viziblr.Colorspace.ColorRGB32Bit(SD.Color.WhiteSmoke.ToArgb());
        public MSCHART.GradientStyle GradientStyle = MSCHART.GradientStyle.TopBottom;

        public TileFormat()
        {
        }

        public Viziblr.Colorspace.ColorRGB32Bit SecondaryColor
        {
            get
            {
                var c1 = new Viziblr.Colorspace.ColorHSL(this.Color);
                var c2 = c1.Add(+0.05, 0, -0.03);
                var c3 = new Viziblr.Colorspace.ColorRGB32Bit(c2);

                return c3;
            }
        }

        public TileFormat Clone()
        {
            var o = new TileFormat();
            o.Style = this.Style;
            o.Color = this.Color;
            o.GradientStyle = this.GradientStyle;
            return o;
        }
    }

}