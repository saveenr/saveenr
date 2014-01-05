using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Isotope.GraphViz
{

    public class GraphVizWriter
    {

        public class GraphOptions
        {
            public bool? Overlap;
        }

        public class NodeOptions
        {
            public string Label;
            public string FontName;
            public int? FillColor;
            public NodeStyle? Style;
            public NodeShape? Shape;
        }

        public class EdgeOptions
        {
            public string Label;
            public string FontName;
            public int? FillColor;
            public NodeStyle? Style;
            public NodeShape? Shape;
        }


        public enum NodeStyle
        {
            Filled
        }

        public enum NodeShape
        {
            Box
        }

        private System.IO.StreamWriter sw;
        enum State
        {
            Begin,
            End,
            Graph,
            Node,
            Edge
        }
        State state;


        public GraphVizWriter(System.IO.StreamWriter sw)
        {
            if (sw == null)
            {
                throw new ArgumentNullException("sw");
            }
            this.sw = sw;
            this.state = State.Begin;
        }


        public void WriteStartGraph(string name, GraphOptions options)
        {
            if (name == null)
            {
                throw new ArgumentNullException(name);
            }
            this.checkstate(State.Begin);
            this.writeline("digraph {0}", name);
            this.writeline("{{");
            this.writeline("");
            if (options != null)
            {
                if (options.Overlap.HasValue)
                {
                    this.writeline("\toverlap=\"{0}\"",options.Overlap.Value.ToString().ToLower());
                }
            }
            this.state = State.Graph;
        }


        public void WriteStartNode(string id, NodeOptions options)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }
            this.checkstate(State.Graph);
            this.state = State.Node;

            this.write(id);
            if (options != null)
            {
                this.write(" [ ");
                if (options.Label != null)
                {
                    this.write("label=\"{0}\" ", options.Label);
                }
                if (options.FontName!= null)
                {
                    this.write("fontname=\"{0}\" ", options.FontName);
                }

                if (options.FillColor!= null)
                {
                    this.write("fillcolor=\"#{0}\" ", options.FillColor.Value.ToString("X6"));
                }

                if (options.Shape!= null)
                {
                    this.write("shape=\"{0}\" ", options.Shape.Value.ToString().ToLower());
                }

                if (options.Style!= null)
                {
                    this.write("style=\"{0}\" ", options.Style.Value.ToString().ToLower());
                }


                this.write(" ]");

            }

            this.writeline("");

        }


        public void WriteStartEdge(string id0, string id1, EdgeOptions options)
        {
            if (id0 == null)
            {
                throw new ArgumentNullException("id0");
            }
            if (id1 == null)
            {
                throw new ArgumentNullException("id1");
            }
            this.checkstate(State.Graph);
            this.state = State.Edge;

            this.write( id0);
            this.write( "->" );
            this.write(id1);
            if (options != null)
            {
                this.write(" [ ");
                if (options.Label != null)
                {
                    this.write("label=\"{0}\" ", options.Label);
                }
                if (options.FontName != null)
                {
                    this.write("fontname=\"{0}\" ", options.FontName);
                }

                if (options.FillColor != null)
                {
                    this.write("fillcolor=\"#{0}\" ", options.FillColor.Value.ToString("X6"));
                }

                if (options.Shape != null)
                {
                    this.write("shape=\"{0}\" ", options.Shape.Value.ToString());
                }

                if (options.Style != null)
                {
                    this.write("style=\"{0}\" ", options.Style.Value.ToString());
                }


                this.write(" ]");

            }

            this.writeline("");

        }


        public void WriteEndNode()
        {
            this.checkstate(State.Node);
            this.state = State.Graph;

        }

        public void WriteEndEdge()
        {
            this.checkstate(State.Edge);
            this.state = State.Graph;

        }


        public void write(string fmt, params object[] items)
        {
            string s = string.Format(fmt, items);
            this.sw.Write(s);
        }

        public void writeline(string fmt, params object[] items)
        {
            string s = string.Format(fmt, items);
            this.sw.WriteLine(s);
        }


        public void WriteEndGraph()
        {
            this.checkstate(State.Graph);
            this.writeline("}}");

            this.state = State.End;

        }



        private void checkstate(State es)
        {
            if (this.state != es)
            {
                throw new InvalidOperationException("Wrong state");
            }

        }



        
    }
}
