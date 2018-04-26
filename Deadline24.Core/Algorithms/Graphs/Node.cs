using System.Collections.Generic;

namespace Deadline24.Core.Algorithms.Graphs
{
    public class Node<TNode, TEdge>
        where TNode : class
        where TEdge : IEdgeData
    {
        public Node(TNode nodeData)
        {
            NodeData = nodeData;
            Edges = new List<Edge<TNode, TEdge>>();
        }

        public IList<Edge<TNode, TEdge>> Edges { get; }

        public TNode NodeData { get; }

        public void AddEdge(Edge<TNode, TEdge> edge)
        {
            Edges.Add(edge);
        }

        protected bool Equals(Node<TNode, TEdge> other)
        {
            return NodeData.Equals(other.NodeData);
        }

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

            return Equals((Node<TNode, TEdge>) obj);
        }

        public override int GetHashCode()
        {
            return NodeData.GetHashCode();
        }
    }
}