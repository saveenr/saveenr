using System.Collections.Generic;

namespace Viziblr.WebCharting.Data
{
    public class DataSetMultiSeries : BaseDataSet
    {
        public List<SeriesDataPoints> DataPointsCollection { get; private set; }

        public DataSetMultiSeries(List<SeriesDataPoints> datapoints_col, AxisLabels labels)
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
                int num_rows = series.Count;
                if (num_rows != labels.Count)
                {
                    string msg =
                        string.Format("Number of values and labels do not match. {0} values given and {1} labels given",
                                      series.Count, labels.Count);

                    throw new System.ArgumentException(msg);
                }
            }

            this.DataPointsCollection = datapoints_col;
            this.XAxisLabels = labels;
        }

        protected override SeriesDataPoints GetSeries(int index)
        {
            return this.DataPointsCollection[index];
        }

        public override int SeriesCount
        {
            get { return this.DataPointsCollection.Count; }
        }

        public override int RowCount
        {
            get { return this.DataPointsCollection[0].Count; }
        }
    }
}