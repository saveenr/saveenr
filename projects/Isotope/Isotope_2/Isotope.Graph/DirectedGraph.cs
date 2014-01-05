using System.Collections.Generic;

namespace Isotope.Graph
{
    public class DirectedGraph<TID, TNODEDATA, TEDGEDATA>
    {
        private List<GraphNode<TID, TNODEDATA>> _nodes;
        private List<DirectedGraphEdge<TID, TNODEDATA, TEDGEDATA>> _edges;

        public DirectedGraph()
        {
            this._nodes = new List<GraphNode<TID, TNODEDATA>>();
            this._edges = new List<DirectedGraphEdge<TID, TNODEDATA, TEDGEDATA>>();
        }

        public GraphNode<TID, TNODEDATA> AddNode(GraphNode<TID, TNODEDATA> node)
        {
            this._nodes.Add(node);
            return node;
        }

        public GraphNode<TID, TNODEDATA> AddNode(TID id)
        {
            var node = new GraphNode<TID, TNODEDATA>(id);
            return this.AddNode(node);
        }

        public GraphNode<TID, TNODEDATA> AddNode(TID id, TNODEDATA data)
        {
            var node = new GraphNode<TID, TNODEDATA>(id);
            return this.AddNode(node);
        }

        public DirectedGraphEdge<TID, TNODEDATA, TEDGEDATA> AddEdge(DirectedGraphEdge<TID, TNODEDATA, TEDGEDATA> edge)
        {
            this._edges.Add(edge);
            return edge;
        }

        public DirectedGraphEdge<TID, TNODEDATA, TEDGEDATA> AddEdge(TID id, GraphNode<TID, TNODEDATA> from,
                                                                    GraphNode<TID, TNODEDATA> to)
        {
            var edge = new DirectedGraphEdge<TID, TNODEDATA, TEDGEDATA>(id, from, to);
            return this.AddEdge(edge);
        }

        public DirectedGraphEdge<TID, TNODEDATA, TEDGEDATA> AddEdge(TID id, GraphNode<TID, TNODEDATA> from,
                                                                    GraphNode<TID, TNODEDATA> to, TEDGEDATA data)
        {
            var edge = new DirectedGraphEdge<TID, TNODEDATA, TEDGEDATA>(id, from, to, data);
            return this.AddEdge(edge);
        }

        public IEnumerable<DirectedGraphEdge<TID, TNODEDATA, TEDGEDATA>> Edges
        {
            get { return this._edges; }
        }

        public IEnumerable<GraphNode<TID, TNODEDATA>> Nodes
        {
            get { return this._nodes; }
        }
    }
}