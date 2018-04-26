using System.Collections.Generic;
using Deadline24.Core.Algorithms.Graphs;

namespace Deadline24._2018_.RocketScience.Graphs
{
    public class RocketScienceGraph : Graph<City, Road>
    {
        public RocketScienceGraph(IList<Node<City, Road>> nodes) : base(nodes)
        {
        }
    }

}
