namespace Isotope.Xml.Extensions
{
    internal static class XmlWriterExtensions
    {
        public static void WriteAttributeString(this System.Xml.XmlWriter xw, string localName, int value)
        {
            xw.WriteAttributeString(localName, value.ToString(System.Globalization.CultureInfo.InvariantCulture));
        }

        public static void WriteAttributeString(this System.Xml.XmlWriter xw, string localName, double value)
        {
            xw.WriteAttributeString(localName, value.ToString(System.Globalization.CultureInfo.InvariantCulture));
        }

        public static void WriteAttributeStringIfNotNull(this System.Xml.XmlWriter xw, string localName, string value)
        {
            if (value == null)
            {
                return;
            }
            xw.WriteAttributeString(localName, value);
        }

        public static void WriteAttributeStringIfNotNull(this System.Xml.XmlWriter xw, string localName, int? value)
        {
            if (value == null)
            {
                return;
            }
            xw.WriteAttributeString(localName, value.Value);
        }

        public static void WriteAttributeStringIfNotNull(this System.Xml.XmlWriter xw, string localName, double? value)
        {
            if (value == null)
            {
                return;
            }
            xw.WriteAttributeString(localName, value.Value);
        }

        public static void WriteAttributeStringIfHasValue<T>(this System.Xml.XmlWriter xw, string localName, T? value) where T : struct
        {
            if (!value.HasValue)
            {
                return;
            }
            xw.WriteAttributeString(localName, value.Value.ToString());
        }

        public static void WriteAttributeStringIfHasValue<T>(this System.Xml.XmlWriter xw, string localName, T? value, System.Func<T, string> tostring) where T : struct
        {
            if (!value.HasValue)
            {
                return;
            }
            xw.WriteAttributeString(localName, tostring(value.Value));
        }
    }
}