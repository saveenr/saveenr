namespace Viziblr.WebCharting.Data
{
    public class DataSetSingleSeries : BaseDataSet
    {
        public SeriesDataPoints DataPoints { get; private set; }

        public DataSetSingleSeries()
        {
            this.DataPoints = new SeriesDataPoints();
            this.XAxisLabels = new AxisLabels();
        }

        public void Add(DataPoint point, string category)
        {
            this.DataPoints.Add(point);
            this.XAxisLabels.Add(category);
        }

        public DataSetSingleSeries(SeriesDataPoints values, AxisLabels labels)
        {
            if (values == null)
            {
                throw new System.ArgumentNullException("values");
            }

            if (labels == null)
            {
                throw new System.ArgumentNullException("labels");
            }

            if (values.Count != labels.Count)
            {
                string msg =
                    string.Format("Number of values and labels do not match. {0} values given and {1} labels given",
                                  values.Count, labels.Count);

                throw new System.ArgumentException(msg);
            }
            this.DataPoints = values;
            this.XAxisLabels = labels;
        }

        protected override SeriesDataPoints GetSeries(int index)
        {
            if (index < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            if (index > 1)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            return this.DataPoints;
        }
        public override int RowCount
        {
            get { return this.DataPoints.Count; }
        }

        public override int SeriesCount
        {
            get { return 1; }
        }
    }
}