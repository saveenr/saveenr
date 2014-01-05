namespace Isotope.Xml.Extensions
{
    internal static class XmlNodeExtensions
    {
        public static string SelectSingleInnerText(this System.Xml.XmlNode node, string xpath )
        {
            return node.SelectSingleInnerText(xpath, null);
        }

        public static string SelectSingleInnerText(this System.Xml.XmlNode node, string xpath, System.Xml.XmlNamespaceManager nsmgr)
        {
            System.Xml.XmlNode tnode = node.SelectSingleNode(xpath,nsmgr);
            if (tnode == null)
            {
                return null;
            }
            return tnode.InnerText;
        }
    }
}