using MSCHART = System.Web.UI.DataVisualization.Charting;

namespace WebCharting.Format
{
    public abstract class BaseLineChart : XYChart
    {
        public BaseLineChart(ChartFormat fmt):
            base(fmt)
        {
            
        }

        protected MSCHART.SeriesChartType GetSeriesChartType()
        {
            if (this.ChartFormat.LineStyle == LineStyle.Jagged)
            {
                return this.ChartFormat.ShowAreaUnderLine ? MSCHART.SeriesChartType.Area : MSCHART.SeriesChartType.Line;
            }
            else
            {
                return this.ChartFormat.ShowAreaUnderLine ? MSCHART.SeriesChartType.SplineArea : MSCHART.SeriesChartType.Spline;
            }
        }

        protected void FormatMarkers(MSCHART.Chart chart)
        {
            int i = 0;
            foreach (var ser in chart.Series)
            {

                ser.MarkerStyle = this.ChartFormat.GetMSCHARTMarkerStyle();
                ser.MarkerSize = this.ChartFormat.LineMarkerSize;
                ser.MarkerBorderColor = System.Drawing.Color.FromArgb( this.ChartFormat.Palette[i].Color.ToInt());
                ser.MarkerBorderWidth = this.ChartFormat.LineChartLineWidth;
                ser.MarkerColor = this.ChartFormat.LineMarkerColor;
                i++;
            }
        }
    }
}