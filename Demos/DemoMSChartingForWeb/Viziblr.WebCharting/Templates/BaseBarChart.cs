using MSCHART = System.Web.UI.DataVisualization.Charting;
using SD=System.Drawing;
using Viziblr.WebCharting.Extensions;

namespace Viziblr.WebCharting.Templates
{
    public abstract class BaseBarChart<T> : XYChart<T> where T : WebCharting.Data.BaseDataSet
    {
        public GradientStyle GradientStyle = GradientStyle.GradientColor;
        public BarStyle BarStyle = BarStyle.Vertical;

        public override void ConfigureSeriesType(MSCHART.Chart chart)
        {
            foreach (var ser in chart.Series)
            {
                ser.ChartType = GetSeriesChartType();
            }
        }

        protected virtual MSCHART.SeriesChartType GetSeriesChartType()
        {
            return this.BarStyle == BarStyle.Horizontal ? MSCHART.SeriesChartType.Bar : MSCHART.SeriesChartType.Column;
        }

        public override void ConfigureColors(MSCHART.Chart chart)
        {
            for (int series_index = 0; series_index < chart.Series.Count; series_index++)
            {
                var series = chart.Series[series_index];
                format_series(series, series_index);
                for (int point_index = 0; point_index < series.Points.Count; point_index++)
                {
                    foreach (var point in series.Points)
                    {
                        format_point(point, series_index, point_index);
                        point_index++;
                    }
                }
            }
        }

        protected abstract void format_series(MSCHART.Series series, int series_index);
        protected abstract void format_point(MSCHART.DataPoint point, int series_index, int point_index);
    }
}