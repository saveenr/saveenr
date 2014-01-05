using System;
using System.IO;
using System.Xml.Linq;

namespace Isotope.Reporting.RDL2005
{
    public class Report : Node
    {


        public string Author;
        //AutoRefresh
        //ReportParameters
        //Custom
        public readonly NodeCollection<DataSource> DataSources;
        public double Width;
        public Body Body;

        public readonly NodeCollection<XDataSet> DataSets;
        public readonly NodeCollection<EmbeddedImage> EmbeddedImages;
        public readonly NodeCollection<ReportParameter> ReportParameters;


        public double PageWidth = 8.5;
        public double PageHeight = 11.0;

        public double LeftMargin = 1.0;
        public double RightMargin = 1.0;
        public double TopMargin = 1.0;
        public double BottomMargin = 1.0;
        public string Language = "en-us";


        public PageHeader PageHeader;
        public PageFooter PageFooter;
        //PageHeader
        //PageFooter
        //InteractiveHeight
        //InteractiveWidth
        //EmbeddedImages
        //CodeModules
        //Classes
        //DataTransform

        public Report()
        {

            this.DataSources = new NodeCollection<DataSource>();
            this.DataSets = new NodeCollection<XDataSet>();
            this.EmbeddedImages = new NodeCollection<EmbeddedImage>();
            this.ReportParameters = new NodeCollection<ReportParameter>();
        }

        public System.Xml.Linq.XElement CreateReportXML()
        {
            var el_report =
                new System.Xml.Linq.XElement(RDLINFO.RS_Namespace.GetName("Report"));

            el_report.RS_SetElementValueCOND("Author", this.Author);

            var el_datasources = el_report.RS_AddElement("DataSources");
            foreach (var ds in this.DataSources.Items())
            {
                ds.write(el_datasources);
            }

            el_report.RS_SetElementValue("Width", this.Width.ToString() + "in");

            
            if (this.PageHeader!=null)
            {
                //this.PageHeader.write(el_report);
            }
            this.Body.write(el_report);
            if (this.PageFooter!= null)
            {
               // this.PageFooter.write(el_report);
            }




            el_report.RS_SetElementValue("Language", this.Language);

            el_report.RS_SetElementValue("LeftMargin", this.LeftMargin.ToString() + "in");
            el_report.RS_SetElementValue("RightMargin", this.RightMargin.ToString() + "in");
            el_report.RS_SetElementValue("TopMargin", this.TopMargin.ToString() + "in");
            el_report.RS_SetElementValue("BottomMargin", this.BottomMargin.ToString() + "in");
            el_report.RS_SetElementValue("PageWidth", this.PageWidth.ToString() + "in");
            el_report.RS_SetElementValue("PageHeight", this.PageHeight.ToString() + "in");


            var el_datasets = el_report.RS_AddElement("DataSets");
            foreach (var ds in this.DataSets.Items())
            {
                ds.write(el_datasets);
            }

            if (this.EmbeddedImages.Count>0)
            {
                var el_embedded_images = el_report.RS_AddElement("EmbeddedImages");
                foreach (var ei in this.EmbeddedImages.Items())
                {
                    ei.write(el_embedded_images);
                }
            }

            if (this.ReportParameters.Count > 0)
            {
                var el_report_parameters = el_report.RS_AddElement("ReportParameters");
                foreach (var rp in this.ReportParameters.Items())
                {
                    rp.write(el_report_parameters);
                }
            }
            return el_report;

        }

        public XDocument ReportToXMLDOM()
        {
            var el_report = CreateReportXML();

            var doc = new System.Xml.Linq.XDocument();
            doc.Add(el_report);
            return doc;
        }

        public Isotope.Reporting.RDL2005.EmbeddedImage  AddEmbeddedImage(string filename)
        {
            var embeddedimage = new Isotope.Reporting.RDL2005.EmbeddedImage();
            embeddedimage.Name = "Image1";

            string ext = Path.GetExtension(filename).Trim();
            if ( ext == ".jpg")
            {
                embeddedimage.MIMEType = ImageMIMETypeEnum.JPEG;
            }
            else if (ext == ".png")
            {
                embeddedimage.MIMEType = ImageMIMETypeEnum.PNG;
            }
            else if (ext == ".gif")
            {
                embeddedimage.MIMEType = ImageMIMETypeEnum.GIF;
            }
            else
            {
                throw new ArgumentOutOfRangeException("filename");
            }

            embeddedimage.ImageData = Convert.ToBase64String( File.ReadAllBytes(filename));
            EmbeddedImages.Add(embeddedimage);
            return embeddedimage;
        }
    }
}