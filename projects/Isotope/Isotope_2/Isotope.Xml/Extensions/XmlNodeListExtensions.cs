using System.Collections.Generic;

namespace Isotope.Xml.Extensions
{
    public static class XmlNodeListExtensions
    {
        public static IEnumerable<System.Xml.XmlNode> AsEnumerable(this System.Xml.XmlNodeList nodelist)
        {
            foreach (System.Xml.XmlNode node in nodelist)
            {
                yield return node;
            }
        }
    }
}