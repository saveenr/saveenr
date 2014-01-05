namespace Isotope.GraphViz
{
    public class NodeOptions
    {
        public string Label { get; set; }
        public ColorRGB32Bit? FillColor { get; set; }
        public NodeStyle? Style { get; set; }
        public NodeShape? Shape { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public ColorRGB32Bit? FontColor { get; set; }
        public string FontName { get; set; }
        public double? FontSize { get; set; }
        public string URL { get; set; }
        public string ToolTip { get; set; }
        public double? Z { get; set; }
    }
}