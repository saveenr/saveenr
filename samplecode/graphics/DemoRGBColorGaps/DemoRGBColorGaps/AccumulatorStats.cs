namespace DemoRGBColorGaps
{
    public class AccumulatorStats
    {
        public int Found;
        public int NotFound;
        public int Exact;
        public int Multiple;

        public string ToCSV()
        {
            string msg = string.Format("{0},{1},{2},{3}", this.NotFound, this.Found, this.Multiple,
                           this.Exact);

            return msg;

        }
    }
}