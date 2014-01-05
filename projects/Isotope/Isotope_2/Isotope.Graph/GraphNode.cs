namespace Isotope.Graph
{
    public class GraphNode<TID, TNODEDATA> : Node<TID, TNODEDATA>
    {
        public GraphNode(TID tid)
        {
            this.Id = tid;
            this.Data = default(TNODEDATA);
        }
        public GraphNode(TID tid, TNODEDATA data)
        {
            this.Id = tid;
            this.Data = data;
        }
    }
}