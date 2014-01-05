using System.Collections.Generic;

namespace Isotope.Atom
{
    public sealed class AtomEntry
    {
        private string _authorName;
        private string _authorEmail;
        private string _authorURI;
        private System.DateTimeOffset? _updated;
        private string _content;
        private string _contentType;
        private string _link;
        private string ID;
        private string _title;
        private string _summary;
        private List<AtomLink> _links;

        public string AuthorName
        {
            get { return _authorName; }
            set { _authorName = value; }
        }

        public string AuthorEmail
        {
            get { return _authorEmail; }
            set { _authorEmail = value; }
        }

        public string AuthorURI
        {
            get { return _authorURI; }
            set { _authorURI = value; }
        }

        public System.DateTimeOffset? Updated
        {
            get { return _updated; }
            set { _updated = value; }
        }

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        public string ContentType
        {
            get { return _contentType; }
            set { _contentType = value; }
        }

        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        public string Id
        {
            get { return ID; }
            set { ID = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Summary
        {
            get { return _summary; }
            set { _summary = value; }
        }

        public List<AtomLink> Links
        {
            get { return _links; }
            set { _links = value; }
        }
    }
}