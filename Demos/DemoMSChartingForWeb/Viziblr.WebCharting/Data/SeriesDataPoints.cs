using System.Collections.Generic;
using System.Linq;

namespace Viziblr.WebCharting.Data
{
    public class SeriesDataPoints : SeriesList<DataPoint>
    {
        public string Hyperlink;
        public ToolTip ToolTip;
        public string Name;

        public SeriesDataPoints() :
            base()
        {
        }
        public SeriesDataPoints(int capacity) :
            base(capacity)
        {
        }

        public SeriesDataPoints(IEnumerable<DataPoint> values) :
            base(values)
        {
        }

        public SeriesDataPoints(IEnumerable<double> values) :
            base(values.Select(d => new DataPoint(d)))
        {
        }

        public double[] GetDoubleArray()
        {
            return this.Select(d => d.Value).ToArray();
        }
    }
}