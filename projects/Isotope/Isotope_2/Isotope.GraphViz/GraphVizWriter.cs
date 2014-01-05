namespace Isotope.GraphViz
{
    public class GraphVizWriter
    {
        private System.IO.StreamWriter streamwriter;

        private WriterState state;

        public GraphVizWriter(System.IO.StreamWriter streamwriter)
        {
            if (streamwriter == null)
            {
                throw new System.ArgumentNullException("streamwriter");
            }
            this.streamwriter = streamwriter;
            this.state = WriterState.Begin;
        }

        public void WriteStartGraph(string name, GraphType graphtype, GraphOptions options)
        {
            if (name == null)
            {
                throw new System.ArgumentNullException(name);
            }
            this.checkstate(WriterState.Begin);

            string gt_str = get_str_from_graph_type(graphtype);
            this.writeline(string.Format("{0} {1}", gt_str, name));
            this.writeline("{{");
            this.writeline("");
            if (options != null)
            {
                if (options.Overlap.HasValue)
                {
                    this.WriteNodeOption("overlap", options.Overlap.Value.ToString().ToLower());
                }


                if (options.FontName != null)
                {
                    this.WriteNodeOption("fontname", options.FontName);
                }

                if (options.FontColor != null)
                {
                    this.WriteNodeOption("fontcolor", options.FontColor.Value.ToWebColorString());
                }

                if (options.FontSize.HasValue)
                {
                    this.WriteNodeOption("fontsize", options.FontSize.Value);
                }


                if (options.LevelsGap.HasValue)
                {
                    this.WriteNodeOption("levelsgap", options.LevelsGap.Value);
                }

                if (options.LayerSep.HasValue)
                {
                    this.WriteNodeOption("layersep", options.LayerSep.Value);
                }


                if (options.NodeSep.HasValue)
                {
                    this.WriteNodeOption("nodesep", options.NodeSep.Value);
                }


                if (options.RankSep.HasValue)
                {
                    this.WriteNodeOption("ranksep", options.RankSep.Value);
                }
            }

            this.writeline("");
            this.state = WriterState.Graph;
        }

        private string get_str_from_graph_type(GraphType graphtype)
        {
            string gt_str = "Error";
            if (graphtype == GraphType.DirectedGraph)
            {
                gt_str = "digraph";
            }
            else
            {
                throw new System.ArgumentOutOfRangeException("graphtype");
            }
            return gt_str;
        }

        public void WriteStartNode(string id, NodeOptions options)
        {
            if (id == null)
            {
                throw new System.ArgumentNullException("id");
            }
            this.checkstate(WriterState.Graph);
            this.state = WriterState.Node;

            this.write(id);
            if (options != null)
            {
                this.write(" [ ");

                if (options.Label != null)
                {
                    this.WriteNodeOption("label", options.Label);
                }
                if (options.FontName != null)
                {
                    this.WriteNodeOption("fontname", options.FontName);
                }

                if (options.FillColor != null)
                {
                    this.WriteNodeOption("fillcolor", options.FillColor.Value.ToWebColorString());
                }


                if (options.FontColor != null)
                {
                    this.WriteNodeOption("fontcolor", options.FontColor.Value.ToWebColorString());
                }

                if (options.FontSize.HasValue)
                {
                    this.WriteNodeOption("fontsize", options.FontSize.Value);
                }

                if (options.Shape != null)
                {
                    this.WriteNodeOption("shape", options.Shape.Value.ToString().ToLower());
                }

                if (options.Style != null)
                {
                    this.WriteNodeOption("style", options.Style.Value.ToString().ToLower());
                }

                if (options.Width.HasValue)
                {
                    this.WriteNodeOption("width", options.Width.Value);
                }

                if (options.Height.HasValue)
                {
                    this.WriteNodeOption("height", options.Height.Value);
                }

                if (options.URL != null)
                {
                    this.WriteNodeOption("url", options.URL);
                }

                if (options.ToolTip != null)
                {
                    this.WriteNodeOption("tooltip", options.ToolTip);
                }

                if (options.Z != null)
                {
                    this.WriteNodeOption("z", options.Z.Value);
                }

                this.write(" ]");
            }

            this.writeline("");
        }

        public void WriteNodeOption(string name, string value)
        {
            this.write("{0}=\"{1}\" ", name, value);
        }

        public void WriteNodeOption(string name, int value)
        {
            this.write("{0}=\"{1}\" ", name, value);
        }

        public void WriteNodeOption(string name, double value)
        {
            this.write("{0}=\"{1}\" ", name, value);
        }

        public void WriteNodeOption(string name, ColorRGB32Bit color)
        {
            this.write("{0}=\"{1}\" ", name, color.ToWebColorString());
        }

        public void WriteStartEdge(string id0, string id1, EdgeOptions options)
        {
            if (id0 == null)
            {
                throw new System.ArgumentNullException("id0");
            }
            if (id1 == null)
            {
                throw new System.ArgumentNullException("id1");
            }
            this.checkstate(WriterState.Graph);
            this.state = WriterState.Edge;

            this.write(id0);
            this.write("->");
            this.write(id1);
            if (options != null)
            {
                this.write(" [ ");

                if (options.Label != null)
                {
                    this.WriteNodeOption("label", options.Label);
                }
                if (options.LabelFontName != null)
                {
                    this.WriteNodeOption("fontname", options.LabelFontName);
                }

                if (options.Weight.HasValue)
                {
                    this.WriteNodeOption("weight", options.Weight.Value);
                }

                if (options.LabelFontColor != null)
                {
                    this.WriteNodeOption("fontcolor", options.LabelFontColor.Value.ToWebColorString());
                }

                if (options.LabelFontSize.HasValue)
                {
                    this.WriteNodeOption("fontsize", options.LabelFontSize.Value);
                }

                if (options.Shape != null)
                {
                    this.WriteNodeOption("shape", options.Shape.Value.ToString().ToLower());
                }

                if (options.Style != null)
                {
                    this.WriteNodeOption("style", options.Style.Value.ToString().ToLower());
                }

                if (options.Color != null)
                {
                    this.WriteNodeOption("color", options.Color.Value.ToWebColorString());
                }

                if (options.URL != null)
                {
                    this.WriteNodeOption("url", options.URL);
                }

                if (options.ToolTip != null)
                {
                    this.WriteNodeOption("tooltip", options.ToolTip);
                }

                if (options.Z != null)
                {
                    this.WriteNodeOption("z", options.Z.Value);
                }

                this.write(" ]");
            }

            this.writeline("");
        }

        public void WriteEndNode()
        {
            this.checkstate(WriterState.Node);
            this.state = WriterState.Graph;
        }

        public void WriteEndEdge()
        {
            this.checkstate(WriterState.Edge);
            this.state = WriterState.Graph;
        }

        public void write(string fmt, params object[] items)
        {
            string s = string.Format(fmt, items);
            this.streamwriter.Write(s);
        }

        public void writeline(string fmt, params object[] items)
        {
            string s = string.Format(fmt, items);
            this.streamwriter.WriteLine(s);
        }

        public void WriteEndGraph()
        {
            this.checkstate(WriterState.Graph);
            this.writeline("}}");

            this.state = WriterState.End;
        }

        private void checkstate(WriterState es)
        {
            if (this.state != es)
            {
                throw new System.InvalidOperationException("Wrong state");
            }
        }
    }
}