using System.Drawing;
using SD = System.Drawing;
using WEBCONTROLS = System.Web.UI.WebControls;
using MSCHART = System.Web.UI.DataVisualization.Charting;


namespace WebCharting
{
    public class FormattingOptions
    {
        public FontDesc DefaultFont;
        public FontDesc ChartTitleFont;
        public Palette Palette { get; set; }
        public MSCHART.GradientStyle RadialGradientStyle { get; set; }
        public SD.Color XYChartAxisLineColor = SD.Color.Silver;
        public SD.Color XYChartMajorGridLineColor = SD.Color.LightGray;
        public string DefaultChartTitle = "Untitled Chart";
        public string DefaultChartSubTitle = "Subtitle";
        public SD.Color EmbossBackColor = SD.Color.WhiteSmoke;
        public MSCHART.GradientStyle EmbossBackGradientStyle = MSCHART.GradientStyle.TopBottom;
        public TileStyle TileStyle = TileStyle.Emboss;

        public Color EmbossBackSecondaryColor
        {
            get
            {
                var c0 = new Isotope.Colorspace.ColorRGB(this.EmbossBackColor.ToArgb());
                var c1 = new Isotope.Colorspace.ColorHSL(c0);
                var c2 = c1.Add(+0.05, 0, -0.03);
                var c3 = new Isotope.Colorspace.ColorRGB24Bit(c2);

                return SD.Color.FromArgb(c3.ToARGBInt(0xff)); 
            }
        }


        public FormattingOptions()
        {
            this.DefaultFont = new FontDesc("Segoe UI",8.0f);
            float TitleFontSizeEmSize = this.DefaultFont.EmSize + 5.0f;
            this.ChartTitleFont = new FontDesc("Segoe UI Semibold", TitleFontSizeEmSize);

            if (this.Palette == null)
            {
                this.Palette = PaletteBuilder.GetDefaultPalette();
            }

            this.RadialGradientStyle = MSCHART.GradientStyle.LeftRight;
        }

        private static FormattingOptions default_Format;

        public static FormattingOptions GetDefaultFormattingOptions()
        {
            if (default_Format == null)
            {
                default_Format = new FormattingOptions();
            }
            return default_Format;
        }
    }
}