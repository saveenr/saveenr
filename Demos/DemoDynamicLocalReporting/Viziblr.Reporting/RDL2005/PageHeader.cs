namespace Viziblr.Reporting.RDL2005
{
    public class PageHeader : _BaseHeaderFooter
    {


        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent)
        {
            return base.write(parent, "PageHeader");
        }
    }
}