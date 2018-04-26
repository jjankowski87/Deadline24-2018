using System.Collections.Generic;
using System.Linq;
using Deadline24.Core.Algorithms.Graphs;
using Deadline24.Core.Extensions;

namespace Deadline24.Core.Algorithms
{
    /// <summary>
    /// This class implements Depth First Search algorithm for graph/tree.
    /// It can be used to determine whether path exists between two nodes
    /// </summary>
    public class DepthFirstTraversalAlgorithm<TNode, TEdge>
        where TNode : class
        where TEdge : IEdgeData
    {
        /// <summary>
        /// Returns full Depth First Traversal path,
        /// pass 'end' value if algorithm should stop searching after reaching end node.
        /// </summary>
        public IEnumerable<Node<TNode, TEdge>> DepthFirstTraversal(Node<TNode, TEdge> start, Node<TNode, TEdge> end)
        {
            var visitedNodes = new HashSet<Node<TNode, TEdge>>();
            var stack = new Stack<Node<TNode, TEdge>>();

            stack.Push(start);

            while (stack.Any())
            {
                var node = stack.Pop();
                if (visitedNodes.Contains(node))
                {
                    continue;
                }

                if (node.Equals(end))
                {
                    yield return node;
                    yield break;
                }

                visitedNodes.Add(node);

                stack.PushMany(node.Edges.Where(e => !visitedNodes.Contains(e.End)).Select(e => e.End));

                yield return node;
            }
        }

        /// <summary>
        /// Finds path between two nodes, returns empty IEnumerable if nodes are not connected.
        /// </summary>
        public IEnumerable<Node<TNode, TEdge>> FindPath(Node<TNode, TEdge> start, Node<TNode, TEdge> end)
        {
            return FindPathFromEndToStart(start, end).Reverse();
        }

        private IEnumerable<Node<TNode, TEdge>> FindPathFromEndToStart(Node<TNode, TEdge> start, Node<TNode, TEdge> end)
        {
            var dfs = DepthFirstTraversal(start, end).ToList();

            // there are no path from start to end node.
            if (!end.Equals(dfs.Last()))
            {
                yield break;
            }

            yield return end;
            var node = end;
            for (var i = dfs.Count - 1; i > 0; i--)
            {
                var previousNode = dfs[i - 1];
                if (previousNode.Edges.Any(e => node.Equals(e.End)))
                {
                    node = previousNode;
                    yield return previousNode;
                }
            }
        }
    }
}
