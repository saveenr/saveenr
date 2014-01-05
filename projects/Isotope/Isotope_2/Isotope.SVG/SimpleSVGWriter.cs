using Isotope.Xml.Extensions;
using System.Linq;

namespace Isotope.SVG
{
    public class SimpleSVGWriter
    {
        System.Xml.XmlTextWriter xw;
        int docwidth = -1;
        int docheight = -1;

        public SimpleSVGWriter(System.Xml.XmlTextWriter xw)
        {
            if (xw == null)
            {
                throw new System.ArgumentNullException();
            }
            this.xw = xw;

        }

        public void StartDocument()
        {
            this.xw.WriteStartDocument();            
        }

        public void EndDocument()
        {
            this.xw.WriteEndDocument();            
        }
               
        public void Flush()
        {
            this.xw.Flush();
        }

        public void Close()
        {
            this.xw.Close();
        }

        public void StartSVG(int width, int height)
        {
            if (width<0)
            {
                throw new System.ArgumentOutOfRangeException("width");
            }

            if (height < 0)
            {
                throw new System.ArgumentOutOfRangeException("height");
            }          

            this.docwidth = width;
            this.docheight = height;
            this.xw.WriteStartElement("svg");
            this.xw.WriteAttributeString("width", this.docwidth);
            this.xw.WriteAttributeString("height", this.docheight);
            this.xw.WriteAttributeString("version", "1.1");
            this.xw.WriteAttributeString("xmlns", "http://www.w3.org/2000/svg");
        }

        public void EndSVG()
        {
            this.xw.WriteEndElement();
        }

        public void StartDefs()
        {
            this.xw.WriteStartElement("defs");
        }
        
        public void EndDefs()
        {
            this.xw.WriteEndElement();
        }

        private void check_id(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new System.ArgumentException("id");
            }
        }
        public void StartGroup(string id)
        {
            this.check_id(id);
            this.xw.WriteStartElement("g");
            this.xw.WriteAttributeString("id", id);
        }

        public void EndGroup()
        {
            this.xw.WriteEndElement();
        }

        public void StartFilter(string id)
        {
            this.check_id(id);
            this.xw.WriteStartElement("filter");
            this.xw.WriteAttributeString("id", id);
        }

        public void EndFilter()
        {
            this.xw.WriteEndElement();
        }

        public void FilterGaussianBlur(int stddev)
        {
            this.xw.WriteStartElement("feGaussianBlur");
            this.xw.WriteAttributeString("in", "SourceGraphic");
            this.xw.WriteAttributeString("stdDeviation", stddev);
            this.xw.WriteEndElement();
        }

        public void write_formatting_attributes(Formatting f)
        {
            if (f!=null)
            {
                f.Write(this.xw);
            }
        }

        public void Rect(int x, int y, int w, int h, Formatting f)
        {
            this.xw.WriteStartElement("rect");
            this.xw.WriteAttributeString("x", x);
            this.xw.WriteAttributeString("y", y);
            this.xw.WriteAttributeString("width", w);
            this.xw.WriteAttributeString("height", h);
            this.write_formatting_attributes(f);
            this.xw.WriteEndElement();
        }

        public void Circle(int cx, int cy, int r, Formatting f)
        {
            this.xw.WriteStartElement("circle");
            this.xw.WriteAttributeString("cx", cx);
            this.xw.WriteAttributeString("cy", cy);
            this.xw.WriteAttributeString("r", r);
            this.write_formatting_attributes(f);
            this.xw.WriteEndElement();
        }

        public void Ellipse(int cx, int cy, int rx, int ry, Formatting f)
        {
            this.xw.WriteStartElement("ellipse");
            this.xw.WriteAttributeString("cx", cx);
            this.xw.WriteAttributeString("cy", cy);
            this.xw.WriteAttributeString("rx", rx);
            this.xw.WriteAttributeString("ry", ry);
            this.write_formatting_attributes(f);
            this.xw.WriteEndElement();
        }

        public void Line(int x1, int y1, int x2, int y2, Formatting f)
        {
            this.xw.WriteStartElement("line");
            this.xw.WriteAttributeString("x1", x1);
            this.xw.WriteAttributeString("y1", y1);
            this.xw.WriteAttributeString("x2", x2);
            this.xw.WriteAttributeString("y2", y2);
            this.write_formatting_attributes(f);
            this.xw.WriteEndElement();
        }

        public void Polygon(int[] points, Formatting f)
        {
            this.xw.WriteStartElement("polygon");
            this.xw.WriteAttributeString("points", get_points_attr_string(points));
            this.write_formatting_attributes(f);
            this.xw.WriteEndElement();
        }

        public void Polyline(int[] points, Formatting f)
        {
            this.xw.WriteStartElement("polyline");
            this.xw.WriteAttributeString("points", get_points_attr_string(points));
            this.write_formatting_attributes(f);
            this.xw.WriteEndElement();
        }

        public void Text(int x, int y, string text, Formatting f)
        {
            this.xw.WriteStartElement("text");
            this.xw.WriteAttributeString("x", x);
            this.xw.WriteAttributeString("y", y);
            this.write_formatting_attributes(f);
            this.xw.WriteString(text);
            this.xw.WriteEndElement();
        }

        private string get_points_attr_string(int[] points)
        {
            var pairs = EnumerableUtil.SelectPairs(points);
            var pairs_array = pairs.Select(i => string.Format("{0},{1}", i.Item1, i.Item2)).ToArray();
            return string.Join(" ", pairs_array);
        }
    }
}