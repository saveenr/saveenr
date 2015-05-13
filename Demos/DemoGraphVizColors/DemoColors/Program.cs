using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoColors
{
    class Program
    {

        public class ColorInfo
        {
            public string Category;
            public string Name;
            public int rgb;

            public static ColorInfo FromXml(System.Xml.Linq.XElement el)
            {
                var c = new ColorInfo();
                c.Category = el.Attribute("Category").Value;
                c.Name= el.Attribute("Name").Value;
                c.rgb = int.Parse(el.Attribute("RGB").Value.Substring(1),System.Globalization.NumberStyles.HexNumber);
                return c;
            }
        }

        static void get_rgb_from_int(int rgb0, out byte r0, out byte g0, out byte b0)
        {
            r0 = (byte) ( rgb0 & (0x0000ff) >> 0 );
            g0 = (byte) ( rgb0 & (0x00ff00) >> 8) ;
            b0 = (byte ) ( rgb0 & (0xff0000) >> 16);
        }

        static double compute_distance(int rgb0, int rgb1)
        {
            byte r0, g0, b0;
            byte r1, g1, b1;
            get_rgb_from_int(rgb0, out r0, out g0, out b0);
            get_rgb_from_int(rgb1, out r1, out g1, out b1);
                                                                                                                                                                                                                                                                                                                                                                                            
            int rx = r0 - r1;
            int gx = g0 - g1;
            int bx = b0 - b1;

            double distance = System.Math.Sqrt((rx * rx) + (gx * gx) + (bx * bx));
            return distance;
        }

        public static double[,] get_distances(ICollection<ColorInfo> colordata)
        {
            var distances = new double[colordata.Count, colordata.Count];

            for (int row = 0; row < colordata.Count; row++)
            {

                for (int col = 0; col < colordata.Count; col++)
                {
                    var rgb0 = colordata.ElementAt(row).rgb;
                    var rgb1 = colordata.ElementAt(col).rgb;

                    double distance = compute_distance(rgb0, rgb1);

                    distances[row, col] = distance;
                }

            }
            return distances;
        }

        static void Main(string[] args)
        {
            string filename = "D:\\SystemColors.xml";
            var xdoc = System.Xml.Linq.XDocument.Load( filename );
            var root = xdoc.Root;
            var xcolors = root.Element("Colors").Elements("Color");

            var colordata = xcolors.Select(el => ColorInfo.FromXml(el)).ToList();


            var distances = get_distances(colordata);


            var fp = System.IO.File.CreateText("D:\\y.txt");
            var gw = new Isotope.GraphViz.GraphVizWriter(fp);

            var go = new Isotope.GraphViz.GraphVizWriter.GraphOptions();
            go.Overlap = false;
            gw.WriteStartGraph( "so" , go);


            var nodeoptions = new Isotope.GraphViz.GraphVizWriter.NodeOptions();
            var edgeoptions = new Isotope.GraphViz.GraphVizWriter.EdgeOptions();
            int row = 0;

            var cur_distances = new double[colordata.Count];

            foreach (var color0 in colordata)
            {
                nodeoptions.Label = color0.Name.ToUpper();
                nodeoptions.FillColor = color0.rgb;
                nodeoptions.Style = Isotope.GraphViz.GraphVizWriter.NodeStyle.Filled;
                gw.WriteStartNode(color0.Name, nodeoptions);
                gw.WriteEndNode();


                for (int col = 0; col < colordata.Count; col++)
                {
                    cur_distances[col] = distances[row, col];
                }

                var pairs = cur_distances.Select((d, i) => new { color = i, distance = d }).Where( i => i.distance>0.0) ;
                var unique_ordered_distances = cur_distances.Where(d=>d > 0.0).Distinct().OrderBy(d=>d).ToList();

                foreach (var d in unique_ordered_distances.Take(1))
                {
                    foreach (var p in pairs.Where( pair => pair.distance==d))
                    {
                        var color1 = colordata[p.color];
                        gw.WriteStartEdge(color0.Name, color1.Name, edgeoptions);
                        gw.WriteEndEdge();

                    }

                }
 

                
                row++;
            }
            gw.WriteEndGraph();
            fp.Flush();
            fp.Close();

            
        }
    }

}

