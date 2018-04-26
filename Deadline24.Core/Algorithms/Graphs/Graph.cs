using System.Collections.Generic;

namespace Deadline24.Core.Algorithms.Graphs
{
    public class Graph<TNode, TEdge> : List<Node<TNode, TEdge>>
        where TNode : class
        where TEdge : IEdgeData
    {
        public Graph(IEnumerable<Node<TNode, TEdge>> nodes) : base(nodes)
        {
        }
    }
}
