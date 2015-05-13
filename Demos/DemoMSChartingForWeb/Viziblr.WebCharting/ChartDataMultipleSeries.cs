using System.Collections.Generic;

namespace WebCharting
{
    public class ChartDataMultipleSeries
    {
        public List<SeriesDataPoints> DataPointsCollection { get; private set; }
        public SeriesLabels XAxisLabels { get; private set; }

        public ChartDataMultipleSeries(List<SeriesDataPoints> datapoints_col, SeriesLabels labels)
        {
            if (datapoints_col == null)
            {
                throw new System.ArgumentNullException("datapoints_col");
            }

            if (labels == null)
            {
                throw new System.ArgumentNullException("labels");
            }

            foreach (var series in datapoints_col)
            {
                int num_rows = series.Length;
                if (num_rows != labels.Length)
                {
                    string msg =
                        string.Format("Number of values and labels do not match. {0} values given and {1} labels given",
                                      series.Length, labels.Length);

                    throw new System.ArgumentException(msg);
                }
            }

            this.DataPointsCollection = datapoints_col;
            this.XAxisLabels = labels;
        }

        public SeriesDataPoints GetSeries(int index)
        {
            return this.DataPointsCollection[index];
        }

        public int SeriesCount
        {
            get { return this.DataPointsCollection.Count; }
        }

        public int RowCount
        {
            get { return this.DataPointsCollection[0].Length; }
        }
    }
}