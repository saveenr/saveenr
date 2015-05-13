using MSCHART = System.Web.UI.DataVisualization.Charting;
using System.Linq;
using SD = System.Drawing;
using Viziblr.WebCharting.Extensions;

namespace Viziblr.WebCharting.Templates
{
    public class RadialChart : BaseTemplate<WebCharting.Data.DataSetSingleSeries>
    {
        public LegendPosition LegendPosition = LegendPosition.Right;
        public GradientStyle GradientStyle = GradientStyle.GradientColor;
        public RadialStyle RadialStyle;

        public RadialChart() :
            base()
        {
        }

        public override void ConfigureAxisInterval(MSCHART.Chart chart)
        {
            // do nothigng
        }

        public override void ConfigureColors(MSCHART.Chart chart)
        {
            var font = this.DefaultFont.GetSDFont();
            foreach (var series in chart.Series)
            {
                series.Font = font;
                int i = 0;
                foreach (var point in series.Points)
                {
                    if (this.GradientStyle == GradientStyle.FlatColor)
                    {
                        point.Color = this.Palette[i].Color.ToSystemColor();
                    }
                    else
                    {
                        point.Color = this.Palette[i].Color.ToSystemColor();
                        point.BackGradientStyle = MSCHART.GradientStyle.LeftRight;
                        point.BackSecondaryColor = this.Palette[i].SecondaryColor.ToSystemColor();
                    }
                    i++;
                }
            }
        }

        public override void ConfigureSeriesType(MSCHART.Chart chart)
        {
            chart.Series[0].ChartType = GetSeriesChartType();
        }

        public override void ConfigureLegend(MSCHART.Chart chart)
        {
            var legend = new MSCHART.Legend();
            chart.Legends.Add(legend);
            if (this.LegendPosition == LegendPosition.Hidden)
            {
                legend.Enabled = false;
            }
            else
            {
                legend.Docking = this.GetLegendDockingPosition(this.LegendPosition);
                legend.Enabled = true;
                legend.Font = this.DefaultFont.GetSDFont();
                legend.IsDockedInsideChartArea = true;
                legend.BorderWidth = 5;
                legend.BackColor = System.Drawing.Color.Transparent;

                var series0 = chart.Series[0];
                series0.Legend = legend.Name;
                series0.IsVisibleInLegend = true;
                foreach (var datapoint in series0.Points)
                {
                    datapoint.LegendText = datapoint.AxisLabel;
                    datapoint.ToolTip = datapoint.AxisLabel;
                }

                series0["PieLabelStyle"] = "Outside";
            }

        }

        private MSCHART.SeriesChartType GetSeriesChartType()
        {
            return this.RadialStyle == RadialStyle.Pie ? MSCHART.SeriesChartType.Pie : MSCHART.SeriesChartType.Doughnut;
        }

        public override void Customize(MSCHART.Chart chart, Data.BaseDataSet chartdata)
        {
            base.Customize(chart, chartdata);

            var series0 = chart.Series[0];
            int n = 0;
            for (int i = 0; i < chartdata[0].Count; i++)
            {
                var dp = series0.Points[i];
                var cd = chartdata[0][i];
                if (cd.Label != null)
                {
                    dp.Label = cd.Label;
                    n++;
                }
                else
                {
                    dp.Label = " ";
                }
            }

            if (n > 0)
            {
                series0["PieLabelStyle"] = "Outside";
                series0["PieLineColor"] = "#c0c0c0";
            }
            else
            {
                series0["PieLabelStyle"] = "Disabled";
            }
        }
    }
}