using System;

namespace Viziblr.Reporting.RDL2005
{
    public class Image : Node
    {


        public ImageSourceEnum Source;
        public string Value;
        public ImageMIMETypeEnum? MIMEType;
        public ImageSizingEnum? Sizing;
        
        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent)
        {
            return write(parent, "Image");
        }

        public System.Xml.Linq.XElement write(System.Xml.Linq.XElement parent,string s)
        {
            var el_image = parent.RS_AddElement(s);
            el_image.RS_SetElementValue("Source", this.Source.ToString() );
            el_image.RS_SetElementValue("Value", this.Value);

            el_image.RS_SetElementValueCOND("MIMEType", this.MIMEType, i => MIMETypeToString(i));

            if (this.Sizing.HasValue)
            {
                el_image.SetAttributeValue("Sizing", this.Sizing.ToString());                
            }
            return el_image;
        }

        public static string MIMETypeToString(ImageMIMETypeEnum mt)
        {
            if (mt==ImageMIMETypeEnum.GIF)
            {
                return "image/gif";
            }
            else if (mt == ImageMIMETypeEnum.PNG)
            {
                return "image/png";
            }
            else if (mt == ImageMIMETypeEnum.JPEG)
            {
                return "image/jpeg";
            }
            else
            {
                throw new ArgumentOutOfRangeException("mt");
            }
            
        }

        public static Image FromEmbeddedImage(EmbeddedImage embedded_image)
        {
            var img = new Image();
            img.Source = ImageSourceEnum.Embedded;
            img.Value = embedded_image.Name;
            img.MIMEType = embedded_image.MIMEType;
            img.Sizing = ImageSizingEnum.AutoSize;
            return img;
        }
    }
}