using SD = System.Drawing;
using MSCHART = System.Web.UI.DataVisualization.Charting;
using WEBCONTROLS = System.Web.UI.WebControls;
using Viziblr.WebCharting.Extensions;

namespace Viziblr.WebCharting.Templates
{
    public abstract class BaseTemplate<T> where T : WebCharting.Data.BaseDataSet
    {
        public FontFormat DefaultFont;
        public FontFormat TitleFont;
        public Palette Palette { get; set; }
        public TileFormat TileFormat;
        public string Title { get; set; }
        public string SubTitle { get; set; }

        // Default Content Values
        public string DefaultChartTitle = "Untitled Chart";
        public string DefaultChartSubTitle = "Subtitle";

        public BaseTemplate()
        {
            this.TileFormat = new TileFormat();
            this.DefaultFont = new FontFormat("Segoe UI", 8.0f);
            this.TitleFont = new FontFormat("Segoe UI Semibold", this.DefaultFont.EmSize + 5.0f);
            this.Palette = PaletteBuilder.GetDefaultPalette();
            this.Title = this.DefaultChartTitle;
            this.SubTitle = this.DefaultChartSubTitle;
        }

        protected virtual void InitializeTitle(MSCHART.Chart chart)
        {
            chart.Titles.Clear();
            if (this.Title != null)
            {
                string title_text = this.Title;
                var title = chart.Titles.Add(title_text);
                title.Font = new SD.Font(this.TitleFont.Name, TitleFont.EmSize);
                title.Alignment = SD.ContentAlignment.MiddleLeft;

                if (this.SubTitle != null)
                {
                    var subtitle = chart.Titles.Add(this.SubTitle);
                    subtitle.Font = new SD.Font(this.TitleFont.Name, DefaultFont.EmSize);
                    subtitle.Alignment = SD.ContentAlignment.MiddleLeft;
                }
            }
        }

        public void SetDefaultFonts(MSCHART.Chart chart)
        {
            chart.Font.Name = DefaultFont.Name;
            chart.Font.Size = new WEBCONTROLS.FontUnit(DefaultFont.EmSize.ToString() + " pt");

            var fnt = new SD.Font(this.DefaultFont.Name, DefaultFont.EmSize);
            foreach (var chartarea in chart.ChartAreas)
            {
                var axes = new[] { chartarea.AxisX, chartarea.AxisY };
                foreach (var axis in axes)
                {
                    axis.LabelStyle.Font = fnt;
                }
            }
        }

        public void Apply(MSCHART.Chart chart, T chartdata)
        {
            this.BindSeries(chart, chartdata);
            
            this.ConfigureRendering(chart);
            this.InitializeTitle(chart);
            this.SetDefaultFonts(chart);
            this.ConfigureAxisInterval(chart);
            this.ConfigureSeriesType(chart);
            this.ConfigureSeriesMarkers(chart);
            this.ConfigureColors(chart);
            this.ConfigureXYGrid(chart);
            this.ConfigureLegend(chart);

            this.Customize(chart, chartdata);
        }

        public virtual void Customize(MSCHART.Chart chart, WebCharting.Data.BaseDataSet chartdata)
        {

        }

        public virtual void ConfigureSeriesMarkers(MSCHART.Chart chart)
        {
        }

        public virtual void ConfigureLegend(MSCHART.Chart chart)
        {
            throw new System.NotImplementedException();
        }


        public virtual void ConfigureXYGrid(MSCHART.Chart chart)
        {
        }

        public virtual void ConfigureColors(MSCHART.Chart chart)
        {
            throw new System.NotImplementedException();
        }

        public virtual void ConfigureAxisInterval(MSCHART.Chart chart)
        {
            throw new System.NotImplementedException();
        }

        public virtual void ConfigureSeriesType(MSCHART.Chart chart)
        {
            throw new System.NotImplementedException();
        }

        protected virtual void ConfigureRendering(MSCHART.Chart chart)
        {
            this.InitializeTitle(chart);
            chart.TextAntiAliasingQuality = MSCHART.TextAntiAliasingQuality.High;
            chart.IsSoftShadows = true;
            chart.AntiAliasing = MSCHART.AntiAliasingStyles.All;
            ConfigureTile(chart);
        }

        private void ConfigureTile(MSCHART.Chart chart)
        {
            chart.ChartAreas[0].BackColor = System.Drawing.Color.Transparent;
            if (this.TileFormat.Style == TileStyle.Emboss)
            {
                chart.BackColor = TileFormat.Color.ToSystemColor();
                chart.BackGradientStyle = TileFormat.GradientStyle;
                chart.BackSecondaryColor = TileFormat.SecondaryColor.ToSystemColor();
                chart.BorderSkin.SkinStyle = MSCHART.BorderSkinStyle.Emboss;
            }
            else if (this.TileFormat.Style == TileStyle.Simple)
            {
                chart.BackColor = TileFormat.Color.ToSystemColor();
            }
            else
            {
                // do nothing
            }

        }

        protected virtual void BindSeries(MSCHART.Chart chart, WebCharting.Data.BaseDataSet chartdata)
        {
            chart.Series.Clear();
            for (int i = 0; i < chartdata.SeriesCount; i++)
            {
                var labels = chartdata.XAxisLabels;
                var values = chartdata[i];
                this.AddNewSeries(chart, values, labels);
            }
        }

        protected void AddNewSeries(MSCHART.Chart chart, WebCharting.Data.SeriesDataPoints datapoints, WebCharting.Data.AxisLabels labels)
        {
            var ser = new MSCHART.Series();
            ser.Points.DataBindXY(labels.ToArray(), datapoints.GetDoubleArray());
            if (datapoints.Name != null)
            {
                ser.LegendText = datapoints.Name;
            }
            for (int i = 0; i < datapoints.Count; i++)
            {
                var chart_datapoint = ser.Points[i];
                var v = datapoints[i];

                if (v.ToolTip != null && v.ToolTip.Text != null)
                {
                    chart_datapoint.ToolTip = v.ToolTip.Text;
                }

                if (v.Hyperlink != null)
                {
                    chart_datapoint.Url = v.Hyperlink;
                }
            }


            if (datapoints.Hyperlink != null)
            {
                ser.Url = datapoints.Hyperlink;
            }

            if (datapoints.ToolTip != null && datapoints.ToolTip.Text != null)
            {
                ser.ToolTip = datapoints.ToolTip.Text;
            }
            chart.Series.Add(ser);
        }

        protected MSCHART.Docking GetLegendDockingPosition(LegendPosition pos)
        {
            if (pos == LegendPosition.Bottom)
            {
                return MSCHART.Docking.Bottom;
            }
            else if (pos == LegendPosition.Right)
            {
                return MSCHART.Docking.Right;
            }
            else
            {
                throw new System.ArgumentException();
            }
        }
    }
}