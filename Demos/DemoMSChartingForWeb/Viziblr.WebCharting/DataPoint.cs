namespace WebCharting
{
    public class DataPoint
    {
        public double Value;
        public string Hyperlink;
        public ToolTip ToolTip;
        public string Label;

        public DataPoint(double value)
        {
            this.Value = value;
        }

        public DataPoint(double value, string hyperlink, string tooltip)
        {
            this.Value = value;
            this.Hyperlink = hyperlink;
            this.ToolTip = new ToolTip(tooltip);
            this.Label = null;
        }
    }
}