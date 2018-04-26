using System.Linq;
using Deadline24.Core.Algorithms;
using Deadline24._2018_.RocketScience.Graphs;
using NUnit.Framework;

namespace Deadline24.Core.Tests.Algorithms
{
    [TestFixture]
    public class BreadthFirstTraversalAlgorithmTests : GraphAlgorithmTestsBase
    {
        [TestCase("world-A.data", 11)]
        [TestCase("world-B.data", 12)]
        [TestCase("world-C.data", 11)]
        public void ShouldFindShortestPath_WhenItExists(string fileName, int expectedPathLength)
        {
            // given
            var graph = LoadGraph(fileName);
            var bfs = new BreadthFirstTraversalAlgorithm<City, Road>();

            var startNode = graph.First();
            var endNode = graph.Last();

            // when
            var result = bfs.FindShortestPath(startNode, endNode).ToList();

            // then
            Assert.AreEqual(expectedPathLength, result.Count);
            AssertAllNodesAreConnected(result);
        }

        [TestCase("disconnectedGraph.data")]
        public void ShouldReturnEmptyEnumerable_WhenNodesAreNotConnected(string fileName)
        {
            // given
            var graph = LoadGraph(fileName);
            var bfs = new BreadthFirstTraversalAlgorithm<City, Road>();

            var startNode = graph.First();
            var endNode = graph.Last();

            // when
            var result = bfs.FindShortestPath(startNode, endNode).ToList();

            // then
            Assert.AreEqual(0, result.Count);
        }
    }
}
