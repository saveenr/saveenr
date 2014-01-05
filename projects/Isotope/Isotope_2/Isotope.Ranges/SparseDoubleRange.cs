using System.Collections.Generic;
using System.Linq;

namespace Isotope.Ranges
{
    public class SparseDoubleRange
    {
        private List<DoubleRange> ranges;

        public SparseDoubleRange()
        {
            this.clear();
        }

        public IEnumerable<DoubleRange> Ranges
        {
            get
            {
                foreach (var range in this.ranges)
                {
                    yield return range;
                }
            }
        }

        public double Count
        {
            get
            {
                double length = this.ranges.Aggregate(0.0, (old, rng) => old + rng.Length);
                return length;
            }
        }

        private void clear()
        {
            this.ranges = new List<DoubleRange>();
        }

        public int RangeCount
        {
            get { return this.ranges.Count; }
        }

        public void AddInclusive(double lower, double upper)
        {
            var rng = DoubleRange.FromEndpoints(lower, upper);
            this.Add(rng);
        }

        public void Add(DoubleRange range)
        {
            var left = new List<DoubleRange>();
            var right = new List<DoubleRange>();
            foreach (var rng in this.ranges)
            {
                if (range.IntersectsExclusive(rng) || range.Touches(rng))
                {
                    range = range.JoinWith(rng);
                }
                else if (rng.Upper < range.Lower)
                {
                    left.Add(rng);
                }
                else if (range.Upper < rng.Lower)
                {
                    right.Add(rng);
                }
                else
                {
                    throw new System.InvalidOperationException("Internal Error");
                }
            }

            this.ranges = left.Concat(EnumerableUtil.Single(range)).Concat(right).ToList();
        }

        public double Lower
        {
            get
            {
                if (this.ranges.Count < 1)
                {
                    throw new System.InvalidOperationException("empty");
                }

                return this.ranges[0].Lower;
            }
        }

        public double Upper
        {
            get
            {
                if (this.ranges.Count < 1)
                {
                    throw new System.InvalidOperationException("empty");
                }

                return this.ranges[this.ranges.Count - 1].Upper;
            }
        }

        public void RemoveInclusive(int lower, int upper)
        {
            var rng = DoubleRange.FromEndpoints(lower, upper);
            this.RemoveInclusive(rng);
        }

        public void RemoveExclusive(int lower, int upper)
        {
            if ((upper - lower) > (2 * double.Epsilon))
            {
                var rng = DoubleRange.FromEndpoints(lower + double.Epsilon, upper - double.Epsilon);
                this.RemoveInclusive(rng);
            }
        }

        public void RemoveInclusive(DoubleRange range)
        {
            // if the range doesn't intersect this collection do nothing
            if (!range.IntersectsInclusive(DoubleRange.FromEndpoints(this.Lower, this.Upper)))
            {
                return;
            }

            var middle = new List<DoubleRange>();

            foreach (var S in this.ranges)
            {
                if (!range.IntersectsInclusive(S))
                {
                    middle.Add(S);
                    continue;
                }

                if ((range.Lower <= S.Lower) && (range.Upper >= S.Upper))
                {
                    // disregard S completely
                    continue;
                }

                if (range.Lower > S.Lower)
                {
                    if (S.Lower <= (range.Lower - double.Epsilon))
                    {
                        var X = DoubleRange.FromEndpoints(S.Lower, range.Lower);
                        middle.Add(X);
                    }
                }

                if (range.Upper <= S.Upper)
                {
                    if ((range.Upper + double.Epsilon) <= S.Upper)
                    {
                        var X = DoubleRange.FromEndpoints(range.Upper, S.Upper);
                        middle.Add(X);
                    }
                }
                else
                {
                    throw new System.InvalidOperationException("internal error");
                }
            }

            this.ranges = middle;
        }

        public DoubleRange? FindRangeContainingNumber(double n)
        {
            foreach (var rng in this.ranges)
            {
                if (rng.Contains(n))
                {
                    return rng;
                }
            }

            return null;
        }

        public bool Contains(double n)
        {
            var rng = this.FindRangeContainingNumber(n);
            return rng != null ? true : false;
        }

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendFormat("{0}(", this.GetType().Name);
            foreach (var rng in this.ranges)
            {
                sb.Append(rng.ToString());
            }

            sb.Append(")");
            return sb.ToString();
        }

        public IEnumerable<double> Values
        {
            get
            {
                foreach (var rng in this.Ranges)
                {
                    foreach (int i in rng.Values)
                    {
                        yield return i;
                    }
                }
            }
        }

        public IEnumerable<double> GetValues(double step)
        {
            foreach (var rng in this.Ranges)
            {
                foreach (double i in rng.GetValues(step))
                {
                    yield return i;
                }
            }
        }
    }
}