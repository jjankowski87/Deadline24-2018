namespace Deadline24.Core.Algorithms.Graphs
{
    public class Edge<TNode, TEdge>
        where TEdge : IEdgeData
        where TNode : class
    {
        public Edge(Node<TNode, TEdge> start, Node<TNode, TEdge> end, TEdge edgeData)
        {
            Start = start;
            End = end;
            EdgeData = edgeData;
        }

        public Node<TNode, TEdge> Start { get; }

        public Node<TNode, TEdge> End { get; }

        public TEdge EdgeData { get; }

        public double EdgeCost => EdgeData.EdgeCost;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((Edge<TNode, TEdge>) obj);
        }

        public override int GetHashCode()
        {
            return (Start.GetHashCode() * 397) ^ End.GetHashCode();
        }

        protected bool Equals(Edge<TNode, TEdge> other)
        {
            return Start.Equals(other.Start) && End.Equals(other.End);
        }
    }
}