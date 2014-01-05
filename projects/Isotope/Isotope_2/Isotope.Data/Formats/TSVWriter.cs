namespace Isotope.Data.Formats
{
    public class TSVWriter
    {
        const string  tab = "\t";
        const string  newline= "\n";
        private System.IO.StringWriter sb;
        private int cur_item_count;

        public TSVWriter(System.IO.StringWriter x)
        {
            this.sb = x;
            this.cur_item_count = 0;
        }

        public void AppendItem(string s)
        {
            if (cur_item_count > 0)
            {
                sb.Write(tab);
            }
            sb.Write(s);
            this.cur_item_count++;
        }

        public void NewLine()
        {
            sb.Write(newline);
            this.cur_item_count = 0;
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }
}