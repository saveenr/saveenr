namespace Viziblr.WebCharting.Data
{
    public abstract class BaseDataSet
    {
        public AxisLabels XAxisLabels { get; protected set; }

        protected abstract SeriesDataPoints GetSeries(int index);
        public abstract int RowCount { get; }
        public abstract int SeriesCount { get; }

        public SeriesDataPoints this[int index]
        {
            get { return this.GetSeries(index); }
        }
    }

}