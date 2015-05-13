namespace Isotope.Reporting
{
    public class TableColumnStyle
    {
        public TextStyle HeaderTextStyle = new TextStyle();
        public TextStyle DetailTextStyle = new TextStyle();
        public double Width = 50;
        public HorzontalAlignment HorzontalAlignment = HorzontalAlignment.Left;

        public TableColumnStyle()
        {
            this.HeaderTextStyle.FontWeight = FontWeight.Bold;
        }

    }
}