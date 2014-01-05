namespace Isotope.Graph
{
    public class DirectedGraphEdge<TID, TNODEDATA, TEDGEDATA> : Node<TID, TEDGEDATA>
    {
        private GraphNode<TID, TNODEDATA> _from;
        private GraphNode<TID, TNODEDATA> _to;

        public DirectedGraphEdge(TID id, GraphNode<TID, TNODEDATA> from, GraphNode<TID, TNODEDATA> to)
        {
            this._from = from;
            this._to = to;
            this.Id = id;
            this.Data = default(TEDGEDATA);
        }

        public DirectedGraphEdge(TID id, GraphNode<TID, TNODEDATA> from, GraphNode<TID, TNODEDATA> to, TEDGEDATA data)
        {
            this._from = from;
            this._to = to;
            this.Id = id;
            this.Data = data;
        }

        public GraphNode<TID, TNODEDATA> From
        {
            get { return _from; }
            set { _from = value; }
        }

        public GraphNode<TID, TNODEDATA> To
        {
            get { return _to; }
            set { _to = value; }
        }
    }
}