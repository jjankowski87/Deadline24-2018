using Deadline24.Core.Algorithms.Graphs;

namespace Deadline24._2018_.RocketScience.Graphs
{
    public class Road : IEdgeData
    {
        public Road(int baseCost)
        {
            EdgeCost = baseCost;
        }

        public static Edge<City, Road> Create(Node<City, Road> start, Node<City, Road> end, int baseCost)
        {
            return new Edge<City, Road>(start, end, new Road(baseCost));
        }

        public double EdgeCost { get; }
    }
}