namespace Isotope.Trees
{
    /// <summary>
    /// Assists in performing a depth-first traversal of nodes for some Node type T. 
    /// T need not be of any specific type.
    /// </summary>
    public struct WalkEvent<T>
    {
        public readonly WalkEventType Type;
        public readonly T Node;

        public WalkEvent(T node, WalkEventType event_type)
        {
            this.Node = node;
            this.Type = event_type;
        }

        public bool HasEnteredNode
        {
            get { return this.Type == WalkEventType.Enter; }
        }

        public bool HasExitedNode
        {
            get { return this.Type == WalkEventType.Exit; }
        }
    }
}