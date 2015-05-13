using MSCHART = System.Web.UI.DataVisualization.Charting;

namespace Viziblr.WebCharting
{
    internal static class MSChartUtil
    {
        public static MSCHART.MarkerStyle GetMSCHARTMarkerStyle(MarkerFormat m)
        {
            if (m.MarkerStyle == MarkerStyle.Circle)
            {
                return MSCHART.MarkerStyle.Circle;
            }
            else if (m.MarkerStyle == MarkerStyle.Circle)
            {
                return MSCHART.MarkerStyle.None;
            }
            else
            {
                throw new System.ArgumentOutOfRangeException();
            }
        }

        public static MSCHART.SeriesChartType GetLineChartSeriesChartType(LineFormat f, bool area)
        {
            if (f.LineStyle == LineStyle.Jagged)
            {
                return area ? MSCHART.SeriesChartType.Area : MSCHART.SeriesChartType.Line;
            }
            else
            {
                return area ? MSCHART.SeriesChartType.SplineArea : MSCHART.SeriesChartType.Spline;
            }
        }
    }
}
