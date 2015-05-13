using MSCHART = System.Web.UI.DataVisualization.Charting;
using SD=System.Drawing;
using Viziblr.WebCharting.Extensions;

namespace Viziblr.WebCharting.Templates
{
    public abstract class XYChart<T> : BaseTemplate<T> where T : WebCharting.Data.BaseDataSet
    {
        public Viziblr.Colorspace.ColorRGB32Bit AxisLineColor = new Viziblr.Colorspace.ColorRGB32Bit(SD.Color.Silver.ToArgb());
        public Viziblr.Colorspace.ColorRGB32Bit GridLineColor = new Viziblr.Colorspace.ColorRGB32Bit(SD.Color.LightGray.ToArgb());
        public LegendPosition LegendPosition = LegendPosition.Right;
        public CategoryAxis CategoryAxis = new CategoryAxis();
        public ValueAxis ValueAxis = new ValueAxis();

        public XYChart() :
            base()
        {
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
                axis.LineColor = this.AxisLineColor.ToSystemColor();
                axis.MajorGrid.LineColor = this.GridLineColor.ToSystemColor();
                axis.MajorTickMark.Enabled = false;
                axis.MajorTickMark.LineColor = this.GridLineColor.ToSystemColor();
            }

            var category_axis = chart.ChartAreas[0].AxisX;
            var value_axis = chart.ChartAreas[0].AxisY;

            // configure margin
            category_axis.IsMarginVisible = this.CategoryAxis.ShowAxisMargin;
            value_axis.IsMarginVisible = this.ValueAxis.ShowAxisMargin;

            // Configure if lines show up for each major value on the X axis
            category_axis.MajorGrid.Enabled = this.CategoryAxis.ShowLines;

            // If needed turn the y axis line off by setting its width to zero
            if (!this.ValueAxis.Visible)
            {
                value_axis.LineWidth = 0;
            }

            // Configure Tick Marks on the X & Y Axis
            value_axis.MajorTickMark.Enabled = this.ValueAxis.ShowTickMarks;
            category_axis.MajorTickMark.Enabled = this.CategoryAxis.ShowTickMarks;

            // Configure the Maximum Value used for the Y Axis
            if (this.ValueAxis.MaximumValue.HasValue)
            {
                chart.ChartAreas[0].AxisY.Maximum = this.ValueAxis.MaximumValue.Value;
            }
        }

        public void ConfigureMultiSeriesLegend(MSCHART.Chart chart)
        {
            if (this.LegendPosition == LegendPosition.Hidden)
            {
                
            }
            else
            {
                var legend = new MSCHART.Legend();
                chart.Legends.Add(legend);
                legend.Enabled = true;
                legend.Font = DefaultFont.GetSDFont();
                legend.IsDockedInsideChartArea = true;
                legend.BackColor = System.Drawing.Color.Transparent;
                legend.Docking = this.GetLegendDockingPosition(this.LegendPosition);                
            }
        }
    }
}