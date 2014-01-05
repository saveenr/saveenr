namespace Isotope.GraphViz
{
    public class GraphOptions
    {
        public bool? Overlap { get; set; }
        public string FontName { get; set; }
        public ColorRGB32Bit? FontColor { get; set; }
        public double? FontSize { get; set; }
        public double? LevelsGap { get; set; }
        public double? LayerSep { get; set; }
        public double? NodeSep { get; set; }
        public double? RankSep { get; set; }
    }
}