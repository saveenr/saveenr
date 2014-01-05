
using DGML=Isotope.DGML;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    [TestClass]
    public class DirectedGraphTest
    {
        [TestMethod]
        public void SampleDoc1()
        {

            var dw = new DGML.DGMLWriter(IsotopeTestCommon.Helper.GetTestMethodOutputFilename("dgml-sample2.dgml"));

            var graph_options = new DGML.GraphAttributes();
            graph_options.Title = "FOOBAR";
            graph_options.Background = new Isotope.Colorspace.ColorRGB32Bit(0xc0c0c0);

            var container_node_options = new DGML.NodeAttributes();
            container_node_options.Group = DGML.GroupState.Expanded;

            var contains_link = new DGML.LinkAttributes();
            contains_link.Category = "Contains";

            dw.StartDocument();
            dw.StartDirectedGraph(graph_options);
            dw.StartNodes();

            dw.Node("A");
            dw.Node("B");
            dw.Node("C",container_node_options);

            dw.EndNodes();
            dw.StartLinks();

            dw.Link("A","B");
            dw.Link("C", "A",contains_link);
            dw.EndLinks();
            dw.StartCategories();
            dw.EndCategories();
            dw.StartProperties();
            dw.EndProperties();
            dw.StartStyles();
            dw.EndStyles();
            dw.EndDirectedGraph();
            dw.EndDocument();
            dw.Close();
        }

        [TestMethod]
        public void SampleDoc3()
        {
            var d = new DGML.DGMLBuilder();

            d.AddNode("A");
            d.AddNode("B");
            d.AddNode("C");
            d.AddNode("D");
            d.AddLink("A","B");
            d.AddLink("B", "C");
            d.AddLink("C", "D");
            d.AddLink("D", "B");

            d.Save( IsotopeTestCommon.Helper.GetTestMethodOutputFilename("dgml-sample3.dgml" ) );
        }

        [TestMethod]
        public void SampleDoc4()
        {

            var d = new DGML.DGMLBuilder();

            d.AddNode("D", d.DefaultContainerNodeAttributes);
 
            d.AddLink("A", "B");
            d.AddLink("B", "C");
            d.AddLink("C", "D");
            d.AddLink("D", "B");
            d.AddLink("D", "E", d.DefaultContainerLinkAttributes);

            d.Save(IsotopeTestCommon.Helper.GetTestMethodOutputFilename("dgml-sample4.dgml"));
        }
    }
}