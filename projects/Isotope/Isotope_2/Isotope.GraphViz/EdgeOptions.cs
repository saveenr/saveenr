namespace Isotope.GraphViz
{
    public class EdgeOptions
    {
        public string Label { get; set; }
        public string LabelFontName { get; set; }
        public double? LabelFontSize { get; set; }
        public ColorRGB32Bit? LabelFontColor { get; set; }
        public double? Weight { get; set; }
        public ColorRGB32Bit? Color { get; set; }
        public NodeStyle? Style { get; set; }
        public NodeShape? Shape { get; set; }
        public string URL { get; set; }
        public string ToolTip { get; set; }
        public double? Z { get; set; }
    }
}