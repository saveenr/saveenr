using System.Collections.Generic;
using System.Xml.Linq;

namespace Isotope.Xml.Linq.Extensions
{
    public static class XElementExtensions
    {
        public static string AttributeValue(this System.Xml.Linq.XElement el, System.Xml.Linq.XName name, string defval)
        {
            return LinqXmlUtil.GetAttributeValue(el, name, defval);
        }

        public static T AttributeValue<T>(this System.Xml.Linq.XElement el, System.Xml.Linq.XName name,
                                             System.Func<string, T> converter)
        {
            return LinqXmlUtil.GetAttributeValue(el, name, converter);
        }

        public static T AttributeValue<T>(this System.Xml.Linq.XElement el, System.Xml.Linq.XName name, T defval,
                                             System.Func<string, T> converter)
        {
            return LinqXmlUtil.GetAttributeValue(el, name, defval, converter);
        }

        public static void AddElementValue(this System.Xml.Linq.XElement el,
                                   System.Xml.Linq.XName name,
                                   string value)
        {
            var new_el = new System.Xml.Linq.XElement(name);
            el.Add(new_el);
            if (!string.IsNullOrEmpty(value))
            {
                new_el.AddText(value);
            }
        }

        public static void AddText(this System.Xml.Linq.XElement el, string s)
        {
            var new_text = new System.Xml.Linq.XText(s);
            el.Add(new_text);
        }
    }
}