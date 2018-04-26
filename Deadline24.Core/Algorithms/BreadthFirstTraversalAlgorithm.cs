using System;
using System.Collections.Generic;
using System.Linq;
using Deadline24.Core.Algorithms.Graphs;

namespace Deadline24.Core.Algorithms
{
    /// <summary>
    /// This class implements Breadth First Search algorithm for graph/tree.
    /// It can be used to find shortest path in unweighted graph.
    /// </summary>
    public class BreadthFirstTraversalAlgorithm<TNode, TEdge>
        where TNode : class
        where TEdge : IEdgeData
    {
        /// <summary>
        /// Returns full Breadth First Traversal path,
        /// pass 'end' value if algorithm should stop searching after reaching end node.
        /// </summary>
        public IEnumerable<Node<TNode, TEdge>> BreadthFirstTraversal(Node<TNode, TEdge> start, Node<TNode, TEdge> end)
        {
            var visitedNodes = new HashSet<Node<TNode, TEdge>>();
            var queue = new Queue<Node<TNode, TEdge>>();

            queue.Enqueue(start);

            while (queue.Any())
            {
                var node = queue.Dequeue();
                yield return node;

                if (node.Equals(end))
                {
                    yield break;
                }

                foreach (var edge in node.Edges.Where(edge => !visitedNodes.Contains(edge.End)))
                {
                    visitedNodes.Add(edge.End);
                    queue.Enqueue(edge.End);
                }
            }
        }

        /// <summary>
        /// Finds shortest path between two nodes, returns empty IEnumerable if nodes are not connected.
        /// </summary>
        public IEnumerable<Node<TNode, TEdge>> FindShortestPath(Node<TNode, TEdge> start, Node<TNode, TEdge> end)
        {
            return FindPathFromEndToStart(start, end).Reverse();
        }

        private IEnumerable<Node<TNode, TEdge>> FindPathFromEndToStart(Node<TNode, TEdge> start, Node<TNode, TEdge> end)
        {
            var bfs = BreadthFirstTraversal(start, end).ToList();

            // there are no path from start to end node.
            if (!end.Equals(bfs.Last()))
            {
                yield break;
            }

            yield return end;
            var node = end;
            var nodeIndex = bfs.Count - 1;

            while (!node.Equals(start))
            {
                var firstNodeIndex = nodeIndex;
                Node<TNode, TEdge> firstNode = null;

                for (var i = nodeIndex - 1; i >= 0; i--)
                {
                    if (bfs[i].Edges.Any(e => node.Equals(e.End)))
                    {
                        firstNodeIndex = i;
                        firstNode = bfs[i];
                    }
                }

                if (firstNode == null)
                {
                    throw new Exception("Invalid Breadth First Search path.");
                }

                node = firstNode;
                nodeIndex = firstNodeIndex;

                yield return node;
            }
        }
    }
}
