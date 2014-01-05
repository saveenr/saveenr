namespace Isotope.Atom
{
    public sealed class AtomLink
    {
        private string _type;
        private string _rel;
        private string _href;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Rel
        {
            get { return _rel; }
            set { _rel = value; }
        }

        public string Href
        {
            get { return _href; }
            set { _href = value; }
        }
    }
}