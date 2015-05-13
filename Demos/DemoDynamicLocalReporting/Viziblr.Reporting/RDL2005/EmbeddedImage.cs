namespace Viziblr.Reporting.RDL2005
{
    public class EmbeddedImage: Node
    {


        public string Name;
        public ImageMIMETypeEnum? MIMEType;
        public string ImageData;

        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent)
        {
            var el_field = parent.RS_AddElement("EmbeddedImage");
            el_field.SetAttributeValue("Name", this.Name);


            el_field.RS_SetElementValueCOND("MIMEType", this.MIMEType, i => Image.MIMETypeToString(i));

            el_field.RS_SetElementValue("ImageData", this.ImageData);
            return el_field;
        }
    }
}