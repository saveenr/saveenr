using System.Collections.Generic;

namespace Viziblr.WebCharting.Data
{
    public class AxisLabels : SeriesList<string>
    {

        public AxisLabels() :
            base()
        {
        }

        public AxisLabels(int capacity) :
            base(capacity)
        {
        }

        public AxisLabels(IEnumerable<string> labels) :
            base(labels)
        {
            this.CheckForNullLabels();
        }

        private void CheckForNullLabels()
        {
            foreach (string label in this)
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