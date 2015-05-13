namespace Viziblr.Reporting.RDL2005
{
    public class Style : Node
    {

        public BorderColor BorderColor;
        public BorderStyle BorderStyle;
        public BorderWidth BorderWidth;

        public string FontFamily;
        public double? FontSize;
        public string FontWeight;
        public Color Color;

        public string Format;

        public double? PaddingLeft;
        public double? PaddingRight;
        public double? PaddingTop;
        public double? PaddingBottom;

        public string TextDecoration;
        public string TextAlign;
        public string VerticalAlign;

        public Color BackgroundColor;
        public Image BackgroundImage;

        public Style()
        {
            this.BorderStyle = new BorderStyle();
            this.BorderColor = new BorderColor();
            this.BorderWidth = new BorderWidth();
        }

        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent)
        {
            var el_style= parent.RS_AddElement("Style");



            this.BorderColor.write(el_style);
            this.BorderStyle.write(el_style);
            this.BorderWidth.write(el_style);


            el_style.RS_SetElementValueCOND("FontFamily", this.FontFamily);
            el_style.RS_SetElementValueCOND("FontSize", this.FontSize, "pt");
            el_style.RS_SetElementValueCOND("FontWeight", this.FontWeight);

            el_style.RS_SetElementValueCOND("Format", this.Format);

            el_style.RS_SetElementValueCOND("Color", this.Color);
            el_style.RS_SetElementValueCOND("PaddingLeft", this.PaddingLeft, "pt");
            el_style.RS_SetElementValueCOND("PaddingRight", this.PaddingRight, "pt");
            el_style.RS_SetElementValueCOND("PaddingTop", this.PaddingTop, "pt");
            el_style.RS_SetElementValueCOND("PaddingBottom", this.PaddingBottom, "pt");


            el_style.RS_SetElementValueCOND("TextDecoration", this.TextDecoration);
            el_style.RS_SetElementValueCOND("TextAlign", this.TextAlign);
            el_style.RS_SetElementValueCOND("TextAlign", this.VerticalAlign);

            el_style.RS_SetElementValueCOND("BackgroundColor", this.BackgroundColor);

            if (this.BackgroundImage!=null)
            {
                this.BackgroundImage.write(el_style, "BackgroundImage");
            }
 
            return el_style;
        }
    }
}