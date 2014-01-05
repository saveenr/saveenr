using System.Collections.Generic;
using System.Xml.Linq;
using SXL = System.Xml.Linq;

namespace Isotope.Xml.Linq
{
    public static class LinqXmlUtil
    {
        public static string GetAttributeValue(SXL.XElement el, SXL.XName name, string defval)
        {
            var attr = el.Attribute(name);
            if (attr == null)
            {
                return defval;
            }

            return attr.Value ?? defval;
        }

        public static T GetAttributeValue<T>(SXL.XElement el, SXL.XName name, System.Func<string, T> converter)
        {
            var a = el.Attribute(name);
            if (a == null)
            {
                string msg = string.Format(System.Globalization.CultureInfo.InvariantCulture, "Missing value for attribute \"{0}\"", name);
                throw new System.ArgumentException(msg);
            }
            string v = el.Attribute(name).Value;
            return converter(v);
        }

        public static T GetAttributeValue<T>(SXL.XElement el, SXL.XName name, T defval, System.Func<string, T> converter)
        {
            var a = el.Attribute(name);
            if (a == null)
            {
                return defval;
            }
            string v = el.Attribute(name).Value;
            return converter(v);
        }
    }
}