namespace Viziblr.WebCharting
{
    public class PaletteItem
    {
        public Viziblr.Colorspace.ColorRGB32Bit Color { get; set; }
        public Viziblr.Colorspace.ColorRGB32Bit SecondaryColor { get; set; }

        public PaletteItem(uint basecolor)
        {
            this.Color = new Viziblr.Colorspace.ColorRGB32Bit(basecolor);
            this.SecondaryColor = new Viziblr.Colorspace.ColorRGB32Bit(this.calc_sec_color().ToUInt());
        }

        public PaletteItem(uint basecolor, uint seccolor)
        {
            this.Color = new Viziblr.Colorspace.ColorRGB32Bit(basecolor);
            this.SecondaryColor = new Viziblr.Colorspace.ColorRGB32Bit(seccolor);
        }

        private Viziblr.Colorspace.ColorRGB32Bit calc_sec_color()
        {
            var hueDelta = -0.05;
            var satDelta = +0.15;
            var lumDelta = -0.2; var c0 = new Viziblr.Colorspace.ColorHSL(new Viziblr.Colorspace.ColorRGB(this.Color));
            var c1 = c0.Add(hueDelta, satDelta, lumDelta);
            var c2 = new Viziblr.Colorspace.ColorRGB(c1);
            var c3 = new Viziblr.Colorspace.ColorRGB32Bit(c2);
            return c3;
        }

        public PaletteItem Clone()
        {
            var o = new PaletteItem(this.Color.ToUInt(), this.SecondaryColor.ToUInt());
            return o;
        }
    }
}