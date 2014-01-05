namespace SSRSCommon
{
    public class DeviceInfo
    {
        public bool Toolbar { get; set; }
        public string PageWidth { get; set; }
        public string PageHeight { get; set; }
        public string MarginTop { get; set; }
        public string MarginBottom { get; set; }
        public string MarginLeft { get; set; }
        public string MarginRight { get; set; }
        public string OutputFormat { get; set; }
        public int? DpiX { get; set; }
        public int? DpiY { get; set; }
        public int? PrintDpiX { get; set; }
        public int? PrintDpiY { get; set; }

        private string Capitalize(string value)
        {
            return System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(value);
        }

        public System.Xml.Linq.XDocument ToXML()
        {
            var dom = new System.Xml.Linq.XDocument();
            var devinfo_el = new System.Xml.Linq.XElement("DeviceInfo");
            dom.Add(devinfo_el);


            this.WriteStringSafe(devinfo_el, "OutputFormat", this.OutputFormat);

            devinfo_el.SetElementValue("Toolbar", this.Capitalize(this.Toolbar.ToString()));

            this.WriteStringSafe(devinfo_el, "PageWidth", this.PageWidth);
            this.WriteStringSafe(devinfo_el, "PageHeight", this.PageHeight);
            this.WriteStringSafe(devinfo_el, "MarginTop", this.MarginTop);
            this.WriteStringSafe(devinfo_el, "MarginBottom", this.MarginBottom);
            this.WriteStringSafe(devinfo_el, "MarginLeft", this.MarginLeft);
            this.WriteStringSafe(devinfo_el, "MarginRight", this.MarginRight);

            if (this.PrintDpiX.HasValue)
            {
                devinfo_el.SetElementValue("PrintDpiX", this.PrintDpiX.ToString());
            }
            if (this.PrintDpiY.HasValue)
            {
                devinfo_el.SetElementValue("PrintDpiY", this.PrintDpiY.ToString());
            }
            if (this.DpiX.HasValue)
            {
                devinfo_el.SetElementValue("DpiX", this.DpiX.ToString());
            }
            if (this.DpiY.HasValue)
            {
                devinfo_el.SetElementValue("DpiY", this.DpiY.ToString());
            }


            return dom;
        }

        public override string ToString()
        {
            return this.ToXML().ToString();
        }

        private void WriteStringSafe(System.Xml.Linq.XElement el, string name, string s)
        {
            if (s != null)
            {
                el.SetElementValue(name, s);
            }
        }
    }
}