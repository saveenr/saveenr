using Isotope.Xml.Extensions;

namespace Isotope.SVG
{
    public class Formatting
    {
        private string _style;
        private int? _strokeWidth;
        private string _fill;
        private string _stroke;
        private double? _opacity;
        private int? _fontSize;
        private string _fontFamily;

        public string Style
        {
            get { return _style; }
            set { _style = value; }
        }

        public int? StrokeWidth
        {
            get { return _strokeWidth; }
            set { _strokeWidth = value; }
        }

        public string Fill
        {
            get { return _fill; }
            set { _fill = value; }
        }

        public string Stroke
        {
            get { return _stroke; }
            set { _stroke = value; }
        }

        public double? Opacity
        {
            get { return _opacity; }
            set { _opacity = value; }
        }

        public int? FontSize
        {
            get { return _fontSize; }
            set { _fontSize = value; }
        }

        public string FontFamily
        {
            get { return _fontFamily; }
            set { _fontFamily = value; }
        }

        public void Clear()
        {
            this._style = null;
            this._strokeWidth = null;
            this._fill = null;
            this._stroke = null;
            this._opacity = null;
            this._fontSize = null;
        }

        public void Write(System.Xml.XmlTextWriter xw)
        {
            xw.WriteAttributeStringIfNotNull("style", this._style);
            xw.WriteAttributeStringIfNotNull("fill", this._fill);
            xw.WriteAttributeStringIfNotNull("stroke", this._stroke);
            xw.WriteAttributeStringIfNotNull("stroke-width", this._strokeWidth);
            xw.WriteAttributeStringIfNotNull("opacity", this._opacity);
            xw.WriteAttributeStringIfNotNull("font-size", this._fontSize);
            xw.WriteAttributeStringIfNotNull("font-family", this._fontFamily);
        }
            
    }
}