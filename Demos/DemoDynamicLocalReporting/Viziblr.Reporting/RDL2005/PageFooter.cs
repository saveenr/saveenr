namespace Viziblr.Reporting.RDL2005
{
    public class PageFooter : _BaseHeaderFooter
    {


        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent)
        {
            return base.write(parent,"PageFooter");
        }
    }
}