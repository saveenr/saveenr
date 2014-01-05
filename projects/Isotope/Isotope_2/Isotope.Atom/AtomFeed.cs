using System.Collections.Generic;
using Isotope.Xml.Extensions;
using System.Linq;

namespace Isotope.Atom
{
    public class AtomFeed
    {
        public readonly static string NamespaceURI = "http://www.w3.org/2005/Atom";
        public readonly static string NamespaceShortName = "atom";

        private string URL;
        private IList<AtomEntry> _entries;

        private string _title;
        private string _subTitle;
        private string ID;

        private List<AtomLink> _links;

        public AtomFeed()
        {
            this._entries = new List<AtomEntry>();
            this._links = new List<AtomLink>(0);

        }

        public string Url
        {
            get { return URL; }
            set { URL = value; }
        }

        public IList<AtomEntry> Entries
        {
            get { return _entries; }
            set { _entries = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string SubTitle
        {
            get { return _subTitle; }
            set { _subTitle = value; }
        }

        public string Id
        {
            get { return ID; }
            set { ID = value; }
        }

        public List<AtomLink> Links
        {
            get { return _links; }
            set { _links = value; }
        }

        public static AtomFeed LoadURL(string url)
        {
            var dom = new System.Xml.XmlDocument();
            var nsmgr = new System.Xml.XmlNamespaceManager(dom.NameTable);
            nsmgr.AddNamespace(NamespaceShortName, NamespaceURI);
            dom.Load(url);

            var feed = load_from_dom(dom, nsmgr);
            return feed;

        }

        public static AtomFeed LoadXML(string xml)
        {
            var dom = new System.Xml.XmlDocument();
            var nsmgr = new System.Xml.XmlNamespaceManager(dom.NameTable);
            nsmgr.AddNamespace(NamespaceShortName, NamespaceURI);
            dom.LoadXml(xml);

            var feed = load_from_dom(dom, nsmgr);
            return feed;

        }

        public static AtomFeed load_from_dom(System.Xml.XmlDocument dom, System.Xml.XmlNamespaceManager nsmgr)
        {


            var root = dom.DocumentElement;

            var afeed = new AtomFeed();
            afeed.ID = root.SelectSingleInnerText("atom:id", nsmgr);
            afeed._title = root.SelectSingleInnerText("atom:title", nsmgr);
            afeed._subTitle = root.SelectSingleInnerText("atom:subtitle", nsmgr);


            afeed._links = new List<AtomLink>();
            afeed._links.AddRange(get_links(root, nsmgr));

            var entry_nodes = root.SelectNodes("atom:entry", nsmgr);
            foreach (var entry_node in entry_nodes.AsEnumerable())
            {
                var aentry = new AtomEntry();
                aentry.AuthorName = entry_node.SelectSingleInnerText("atom:author/atom:name", nsmgr);
                aentry.AuthorEmail = entry_node.SelectSingleInnerText("atom:author/atom:email", nsmgr);
                aentry.AuthorURI = entry_node.SelectSingleInnerText("atom:author/atom:uri", nsmgr);
                aentry.Updated = System.DateTimeOffset.Parse(entry_node.SelectSingleInnerText("atom:updated", nsmgr), System.Globalization.CultureInfo.InvariantCulture);

                aentry.Content = entry_node.SelectSingleInnerText("atom:content", nsmgr);
                var content_node = (System.Xml.XmlElement)entry_node.SelectSingleNode("atom:content", nsmgr);
                if ( content_node!=null)
                {
                    aentry.ContentType = content_node.GetAttribute("type");
                }

                aentry.Link = entry_node.SelectSingleInnerText("atom:link", nsmgr);
                aentry.Id = entry_node.SelectSingleInnerText("atom:id", nsmgr);
                aentry.Title = entry_node.SelectSingleInnerText("atom:title", nsmgr);
                aentry.Summary = entry_node.SelectSingleInnerText("atom:summary", nsmgr);
                aentry.Links = new List<AtomLink>();
                aentry.Links.AddRange(get_links(entry_node, nsmgr));
                afeed._entries.Add(aentry);
            }

            return afeed;

        }


        private static IEnumerable<AtomLink> get_links( System.Xml.XmlNode parent , System.Xml.XmlNamespaceManager nsmgr)
        {
            var link_nodes = parent.SelectNodes("atom:link",nsmgr).AsEnumerable().Cast<System.Xml.XmlElement>();
            foreach (var linknode in link_nodes)
            {
                var alink = new AtomLink();
                alink.Type = linknode.GetAttribute("type");
                alink.Rel = linknode.GetAttribute("rel");
                alink.Href= linknode.GetAttribute("href");
                yield return alink;
            }

            
        }
    }
}