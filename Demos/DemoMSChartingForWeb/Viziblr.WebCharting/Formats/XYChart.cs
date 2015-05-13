using MSCHART = System.Web.UI.DataVisualization.Charting;

namespace WebCharting.Format
{
    public abstract class XYChart : BaseChart
    {
        public double? MaximumValue { get; set; }
        public bool ShowAxisMargin { get; set; }
        public bool ShowXAxisLines { get; set; }
        public LegendPosition LegendPosition { get; set; }
        public bool XAxisTickMarks = false;
        public bool YAxisTickMarks = false;

        public XYChart(ChartFormat fmt) :
            base(fmt)

        {
            this.ShowAxisMargin = false;
            this.ShowXAxisLines = true;
            this.LegendPosition = LegendPosition.Right;
        }

        public override void ConfigureAxisInterval(MSCHART.Chart chart)
        {
            chart.ChartAreas[0].AxisX.Interval = 1;
        }

        public override void ConfigureXYGrid(MSCHART.Chart chart)
        {
            // Set Axis Colors
            foreach (var axis in chart.ChartAreas[0].Axes)
            {
                axis.LineColor = this.ChartFormat.XYChartAxisLineColor;
                axis.MajorGrid.LineColor = this.ChartFormat.XYChartMajorGridLineColor;
                axis.IsMarginVisible = this.ShowAxisMargin;
                axis.MajorTickMark.Enabled = false;
                axis.MajorTickMark.LineColor = this.ChartFormat.XYChartMajorGridLineColor;
            }

            var xaxis = chart.ChartAreas[0].AxisX;
            var yaxis = chart.ChartAreas[0].AxisY;

            // Configure if lines show up for each major value on the X axis
            xaxis.MajorGrid.Enabled = this.ShowXAxisLines;

            // If needed turn the y axis line off by setting its width to zero
            if (!this.ChartFormat.YAxisLineVisible)
            {
                yaxis.LineWidth = 0;
            }

            // Configure Tick Marks on the X & Y Axis
            yaxis.MajorTickMark.Enabled = this.YAxisTickMarks;
            xaxis.MajorTickMark.Enabled = this.XAxisTickMarks;

            // Configure the Maximum Value used for the Y Axis
            if (this.MaximumValue.HasValue)
            {
                chart.ChartAreas[0].AxisY.Maximum = this.MaximumValue.Value;
            }
        }

        public void ConfigureMultiSeriesLegend(MSCHART.Chart chart)
        {
            var fmt = ChartFormat.DefaultFormatting;


            if (this.LegendPosition == LegendPosition.Hidden)
            {
                
            }
            else
            {
                var legend = new MSCHART.Legend();
                chart.Legends.Add(legend);
                legend.Enabled = true;
                legend.Font = fmt.DefaultFont.GetSDFont();
                legend.IsDockedInsideChartArea = true;
                legend.BackColor = System.Drawing.Color.Transparent;
                legend.Docking = this.GetLegendDockingPosition(this.LegendPosition);                
            }
        }
    }
}