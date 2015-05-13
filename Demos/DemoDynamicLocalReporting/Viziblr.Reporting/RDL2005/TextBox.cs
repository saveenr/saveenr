using System.Collections.Generic;

namespace Viziblr.Reporting.RDL2005
{
    public class TextBox : ReportItem
    {
        public string Value;
        public bool CanGrow;
        public TextBox(string name, string value) :
            base(name)
        {
            this.Value = value;
        }

        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent)
        {

            var el_textbox = base.write(parent,"Textbox");
            el_textbox.RS_SetElementValue("Value", this.Value);
            el_textbox.RS_SetElementValue("CanGrow", this.CanGrow.ToString().ToLower());

            return el_textbox;
        }

    }
}