using System.Linq;
using Deadline24.Core.Algorithms;
using Deadline24._2018_.RocketScience.Graphs;
using NUnit.Framework;

namespace Deadline24.Core.Tests.Algorithms
{
    [TestFixture]
    public class DepthFirstTraversalAlgorithmTests : GraphAlgorithmTestsBase
    {
        [TestCase("world-A.data")]
        public void ShouldFindPathWithAllNodes(string fileName)
        {
            // given
            var graph = LoadGraph("world-A.data");
            var dfs = new DepthFirstTraversalAlgorithm<City, Road>();

            var startNode = graph.First();
            var endNode = graph.Last();

            // when
            var result = dfs.FindPath(startNode, endNode).ToList();

            // then
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
