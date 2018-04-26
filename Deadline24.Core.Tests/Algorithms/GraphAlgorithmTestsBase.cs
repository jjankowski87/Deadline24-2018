using System.Collections.Generic;
using System.IO;
using System.Linq;
using Deadline24.Core.Algorithms.Graphs;
using Deadline24._2018_.RocketScience;
using Deadline24._2018_.RocketScience.Graphs;
using NUnit.Framework;

namespace Deadline24.Core.Tests.Algorithms
{
    public class GraphAlgorithmTestsBase
    {
        protected RocketScienceGraph LoadGraph(string fileName)
        {
            var directory = Path.Combine(TestContext.CurrentContext.TestDirectory, "Data\\graphs", fileName);
            return WorldLoader.LoadWorldFromFile(directory);
        }

        public static void AssertAllNodesAreConnected<TNode, TEdge>(IList<Node<TNode, TEdge>> nodes)
            where TNode : class
            where TEdge : IEdgeData
        {
            for (var i = 1; i < nodes.Count; i++)
            {
                var start = nodes[i - 1];
                var end = nodes[i];

                Assert.IsTrue(start.Edges.Any(e => end.Equals(e.End)));
            }
        }

        public static double CalculatePathCost(List<Node<City, Road>> path)
        {
            var totalCost = 0d;

            for (var i = 1; i < path.Count; i++)
            {
                var start = path[i - 1];
                var end = path[i];

                totalCost += start.Edges.First(e => e.End.Equals(end)).EdgeCost;
            }

            return totalCost;
        }
    }
}