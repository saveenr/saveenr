using Isotope.Formats;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using Formatting = Isotope.Formats.SVG.Formatting;

namespace IsotopeTests
{

    [TestClass]
    public class SimpleSVGWriterTest
    {
        [TestMethod]
        public void SimpleSVGWriterConstructorTest()
        {
            string filename = IsotopeTestCommon.Helper.GetTestMethodOutputFilename("out1.svg");
            var xw = new System.Xml.XmlTextWriter(filename,System.Text.Encoding.UTF8);
            xw.Formatting = System.Xml.Formatting.Indented;

            var sw = new Isotope.Formats.SVG.SimpleSVGWriter(xw);

            sw.StartSVG(800, 800);

            sw.StartDefs();
            sw.StartFilter("gb1");
            sw.FilterGaussianBlur(3);
            sw.EndFilter();
            sw.EndDefs();

            var fmt1 = new Formatting();           
            fmt1.Style = "fill:red;stroke:black;stroke-width:5;opacity:0.5";

            sw.Rect(0, 0, 100, 100,fmt1);
            sw.Rect(50, 50, 100, 100,fmt1);

            var fmt2 = new Formatting();
            fmt2.Fill = "blue";
            fmt2.StrokeWidth = 3;
            fmt2.Stroke = "black";
            fmt2.Opacity = 0.3;

            sw.Circle(50,50,35,fmt2);
            sw.Ellipse(200,100,50,20,fmt2);           
            sw.Line(10,10,500,100,fmt2);

            fmt2.Style = "filter:url(#gb1)";
            sw.Polygon( new int[] { 220,100,300,210,170,250 }, fmt2);

            var fmt3 = new Formatting();
            fmt3.Fill = "none";
            fmt3.StrokeWidth = 2;
            fmt3.Stroke = "black";

            sw.Polyline(new int[] { 0,0,0,20,20,20,20,40,40,40,40,60 }, fmt3);


            var fmt4 = new Formatting();
            fmt4.FontFamily = "Segoe UI";
            fmt4.FontSize = 30;
            sw.Text(300,100,"Hello World",fmt4);
            sw.EndSVG();


            xw.Close();
        }
    }
}
