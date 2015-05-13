using System.Collections.Generic;

namespace WebCharting
{
    public class SeriesLabels : SeriesArray<string>
    {
        public SeriesLabels(int capacity) :
            base(capacity)
        {
        }

        public SeriesLabels(IEnumerable<string> labels) :
            base(labels)
        {
            this.CheckForNullLabels();
        }

        private void CheckForNullLabels()
        {
            foreach (string label in this.Array)
            {
                if (label == null)
                {
                    string msg = string.Format("Null labels are not allowed");

                    throw new System.ArgumentException(msg);
                }
            }
        }
    }
}