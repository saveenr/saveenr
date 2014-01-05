namespace Isotope.CommandLine
{
    public class Token
    {
        private readonly string _text;

        public Token(string t)
        {
            this._text = t;
        }

        public string Text
        {
            get { return _text; }
        }
    }
}