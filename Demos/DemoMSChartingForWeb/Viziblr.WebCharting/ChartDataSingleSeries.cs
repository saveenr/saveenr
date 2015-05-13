namespace WebCharting
{
    public class ChartDataSingleSeries
    {
        public SeriesDataPoints Values { get; private set; }
        public SeriesLabels XAxisLabels { get; private set; }

        public ChartDataSingleSeries(SeriesDataPoints values, SeriesLabels labels)
        {
            if (values == null)
            {
                throw new System.ArgumentNullException("values");
            }

            if (labels == null)
            {
                throw new System.ArgumentNullException("labels");
            }

            if (values.Length != labels.Length)
            {
                string msg =
                    string.Format("Number of values and labels do not match. {0} values given and {1} labels given",
                                  values.Length, labels.Length);

                throw new System.ArgumentException(msg);
            }
            this.Values = values;
            this.XAxisLabels = labels;
        }
    }
}