namespace Viziblr.WebCharting
{
    public class ToolTip
    {
        public string Text { get; set; }

        public ToolTip()
        {
            this.Text = null;
        }

        public ToolTip(string s)
        {
            this.Text = s;
        }
    }
}