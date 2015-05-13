using System;
using System.Drawing;
using SD = System.Drawing;
using WEBCONTROLS = System.Web.UI.WebControls;
using MSCHART = System.Web.UI.DataVisualization.Charting;


namespace WebCharting
{
    public class TileFormat
    {
        public TileStyle Style = TileStyle.Emboss;
        public SD.Color Color = SD.Color.WhiteSmoke;
        public MSCHART.GradientStyle GradientStyle = MSCHART.GradientStyle.TopBottom;

        public TileFormat()
        {
        }

        public Color SecondaryColor
        {
            get
            {
                var c0 = new Isotope.Colorspace.ColorRGB(this.Color.ToArgb());
                var c1 = new Isotope.Colorspace.ColorHSL(c0);
                var c2 = c1.Add(+0.05, 0, -0.03);
                var c3 = new Isotope.Colorspace.ColorRGB32Bit(c2);

                return SD.Color.FromArgb(c3.ToInt());
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

    public class ChartFormat
    {
        /*
        // Font Info
        public FontFormat DefaultFont;
        public FontFormat ChartTitleFont;
        public Palette Palette { get; set; }
        public TileFormat TileFormat;

        // static
        private static ChartFormat default_Format;

        public ChartFormat()
        {
            this.TileFormat = new TileFormat();
            this.DefaultFont = new FontFormat("Segoe UI", 8.0f);
            this.ChartTitleFont = new FontFormat("Segoe UI Semibold", this.DefaultFont.EmSize + 5.0f);
            this.Palette = PaletteBuilder.GetDefaultPalette();
        }

        public static ChartFormat DefaultFormatting
        {
            get
            {
                if (default_Format == null)
                {
                    default_Format = new ChartFormat();
                }
                return default_Format;
            }
        }

        public ChartFormat Clone()
        {
            var o = new ChartFormat();
            o.DefaultFont = this.DefaultFont.Clone();
            o.ChartTitleFont = this.ChartTitleFont.Clone();
            o.Palette = this.Palette;
            o.TileFormat = this.TileFormat.Clone();
            return o;
        }

*/
    }
}