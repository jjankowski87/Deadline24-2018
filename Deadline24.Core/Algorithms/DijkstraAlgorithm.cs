using System.Collections.Generic;
using System.Linq;
using Deadline24.Core.Algorithms.Graphs;

namespace Deadline24.Core.Algorithms
{
    /// <summary>
    /// This class implements Dijkstra shortest path finder algorithm for graph/tree.
    /// It can be used to find shortest path in weighted graph.
    /// </summary>
    public class DijkstraAlgorithm<TNode, TEdge>
        where TNode : class
        where TEdge : IEdgeData
    {
        /// <summary>
        /// Returns shortest path from start node to end.
        /// Returns empty enumerable if path is not found.
        /// </summary>
        public IEnumerable<Node<TNode, TEdge>> FindShortestPath(Graph<TNode, TEdge> graph, Node<TNode, TEdge> start, Node<TNode, TEdge> end)
        {
            var processedNodes = new HashSet<Node<TNode, TEdge>>();
            var distances = graph.ToDictionary(node => node, node => double.MaxValue);
            var parents = new Dictionary<Node<TNode, TEdge>, Node<TNode, TEdge>>();

            distances[start] = 0;

            for (var i = 0; i < graph.Count - 1; i++)
            {
                var node = GetNotProcessedNodeWithLeastDistance(distances, processedNodes);
                processedNodes.Add(node);

                // Shortest path to end node is found
                if (node.Equals(end))
                {
                    break;
                }

                foreach (var edge in node.Edges)
                {
                    var edgeEnd = edge.End;
                    if (!processedNodes.Contains(edgeEnd)
                        && distances[node] < double.MaxValue
                        && distances[node] + edge.EdgeCost < distances[edgeEnd])
                    {
                        parents[edgeEnd] = node;
                        distances[edgeEnd] = distances[node] + edge.EdgeCost;
                    }
                }
            }

            return CreateReversedShortestPath(parents, start, end).Reverse();
        }

        private static IEnumerable<Node<TNode, TEdge>> CreateReversedShortestPath(
            IDictionary<Node<TNode, TEdge>, Node<TNode, TEdge>> parents,
            Node<TNode, TEdge> start,
            Node<TNode, TEdge> end)
        {
            // There are no path to end node
            if (!parents.ContainsKey(end))
            {
                yield break;
            }

            yield return end;
            var node = end;

            do
            {
                node = parents[node];

                yield return node;
            } while (!node.Equals(start));
        }

        private static Node<TNode, TEdge> GetNotProcessedNodeWithLeastDistance(IDictionary<Node<TNode, TEdge>, double> nodeDistances, ICollection<Node<TNode, TEdge>> processedNodes)
        {
            return nodeDistances.Where(node => !processedNodes.Contains(node.Key))
                                .OrderBy(node => node.Value)
                                .First().Key;
        }
    }
}
