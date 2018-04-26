using System.Linq;
using Deadline24.Core.Algorithms;
using Deadline24._2018_.RocketScience.Graphs;
using NUnit.Framework;

namespace Deadline24.Core.Tests.Algorithms
{
    public class DijkstraAlgorithmTests : GraphAlgorithmTestsBase
    {
        [TestCase("simpleGraph.data", 4, 3)]
        [TestCase("world-A.data", 12, 103)]
        [TestCase("world-M.data", 12, 101)]
        public void ShouldFindShortestPath_WhenItExists(string fileName, int expectedPathLength, double expectedPathCost)
        {
            // given
            var graph = LoadGraph(fileName);
            var dijkstra = new DijkstraAlgorithm<City, Road>();

            var startNode = graph.First();
            var endNode = graph.Last();

            // when
            var result = dijkstra.FindShortestPath(graph, startNode, endNode).ToList();

            // then
            Assert.AreEqual(expectedPathLength, result.Count);
            Assert.AreEqual(expectedPathCost, CalculatePathCost(result));
            AssertAllNodesAreConnected(result);
        }

        [TestCase("disconnectedGraph.data")]
        public void ShouldReturnEmptyEnumerable_WhenNodesAreNotConnected(string fileName)
        {
            // given
            var graph = LoadGraph(fileName);
            var dijkstra = new DijkstraAlgorithm<City, Road>();

            var startNode = graph.First();
            var endNode = graph.Last();

            // when
            var result = dijkstra.FindShortestPath(graph, startNode, endNode).ToList();

            // then
            Assert.AreEqual(0, result.Count);
        }
    }
}
