using System.Collections.Generic;
using System.Linq;

namespace Isotope.Ranges
{
    public class SparseIntRange
    {
        private List<IntRange> ranges;

        public SparseIntRange()
        {
            this.clear();
        }

        public IEnumerable<IntRange> Ranges
        {
            get
            {
                foreach (var range in this.ranges)
                {
                    yield return range;
                }
            }
        }

        public IEnumerable<int> Values
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

        private void clear()
        {
            this.ranges = new List<IntRange>();
        }

        public int RangeCount
        {
            get { return this.ranges.Count; }
        }

        public void Add(int n)
        {
            var rng = IntRange.FromInteger(n);
            this.Add(rng);
        }

        public void AddInclusive(int lower, int upper)
        {
            var rng = IntRange.FromEndpoints(lower, upper);
            this.Add(rng);
        }

        public void Add(IntRange range)
        {
            var left = new List<IntRange>();
            var right = new List<IntRange>();
            foreach (var rng in this.ranges)
            {
                if (range.Intersects(rng) || range.Touches(rng))
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

        public int Lower
        {
            get
            {
                if (this.ranges.Count < 1)
                {
                    throw new System.InvalidOperationException("There are no ranges");
                }

                return this.ranges[0].Lower;
            }
        }

        public int Upper
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

        public int Count
        {
            get
            {
                int length = this.ranges.Aggregate(0, (old, rng) => old + rng.Length);
                return length;
            }
        }

        public void Remove(int value)
        {
            var rng = IntRange.FromInteger(value);
            this.Remove(rng);
        }

        public void RemoveInclusive(int lower, int upper)
        {
            var rng = IntRange.FromEndpoints(lower, upper);
            this.Remove(rng);
        }

        public void Remove(IntRange range)
        {
            // if the range doesn't intersect this collection do nothing
            if (!range.Intersects(IntRange.FromEndpoints(this.Lower, this.Upper)))
            {
                return;
            }

            var middle = new List<IntRange>();

            foreach (var S in this.ranges)
            {
                if (!range.Intersects(S))
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
                    if (S.Lower <= (range.Lower - 1))
                    {
                        var X = IntRange.FromEndpoints(S.Lower, range.Lower - 1);
                        middle.Add(X);
                    }
                }

                if (range.Upper <= S.Upper)
                {
                    if ((range.Upper + 1) <= S.Upper)
                    {
                        var X = IntRange.FromEndpoints(range.Upper + 1, S.Upper);
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

        public IntRange? FindRangeContainingNumber(int n)
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

        public bool Contains(int n)
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
    }

    internal static class EnumerableUtil
    {
        public static IEnumerable<T> Single<T>(T item)
        {
            yield return item;
        }
    }
}