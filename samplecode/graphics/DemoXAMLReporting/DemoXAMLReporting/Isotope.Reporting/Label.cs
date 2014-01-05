namespace Isotope.Reporting
{
    public class Label
    {
        public string Text;
        public TextStyle TextStyle = new TextStyle();

        public Label(string s)
        {
            this.Text= s;
        }
    }
}