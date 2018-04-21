using System.Collections.Generic;
using System.Linq;

namespace Deadline24.ConsoleApp.RocketScience
{
    public class RouteFinder
    {
        private readonly City _startCity;

        private IList<Route> _bestRoutes;

        public RouteFinder(City startCity)
        {
            _startCity = startCity;
        }

        public IList<Route> FindRoutes(int maxCost)
        {
            _bestRoutes = new List<Route>();

            FindBestRoutes(_startCity, maxCost, 0, new List<Road>());

            return _bestRoutes;
        }

        public Route FindClosestBaseCity()
        {
            return new Route(FindShortestRoad(_startCity), 0);
        }

        private IList<Road> FindShortestRoad(City fromCity)
        {
            var stack = new Stack<List<Road>>();
            var currentCity = fromCity;

            while (true)
            {
                foreach (var currentRoad in currentCity.Roads)
                {
                    if (currentRoad.To.IsBaseCity)
                    {
                        var route = stack.Pop();
                        route.Add(currentRoad);

                        return route;
                    }

                    
                }
            }

            //    foreach (var road in fromCity.Roads.Reverse())
            //    {
            //        if (road.To.IsBaseCity)
            //        {
            //            return new List<Road> { road };
            //        }

            //        var route = new List<Road> { road };
            //        route.AddRange(FindShortestRoad(road.To));

            //        return route;
            //    }

            //    return new List<Road>();
        }

        //private IList<Road> FindShortestRoad(City fromCity)
        //{
        //    foreach (var road in fromCity.Roads.Reverse())
        //    {
        //        if (road.To.IsBaseCity)
        //        {
        //            return new List<Road> { road };
        //        }

        //        var route = new List<Road> { road };
        //        route.AddRange(FindShortestRoad(road.To));

        //        return route;
        //    }

        //    return new List<Road>();
        //}

        private bool FindBestRoutes(City fromCity, int maxCost, int currentCost, IList<Road> roads)
        {
            foreach (var road in fromCity.Roads)
            {
                // Do not return to the same city
                var lastRoad = roads.LastOrDefault();
                if (road.To.Id == lastRoad?.From.Id)
                {
                    continue;
                }

                // if city was already visited
                if (roads.Select(r => r.To.Id).ToList().Contains(road.To.Id))
                {
                    continue;
                }

                var newCost = currentCost + road.BaseCost;
                if (newCost > maxCost)
                {
                    continue;
                }

                if (road.To.IsBaseCity)
                {
                    roads.Add(road);
                    return true;
                }

                var newRoads = new List<Road>(roads) { road };
                currentCost = newCost;

                if (FindBestRoutes(road.To, maxCost, currentCost, newRoads))
                {
                    _bestRoutes.Add(new Route(newRoads, currentCost));
                }
            }

            return false;
        }

        public class Route : List<Road>
        {
            public Route(IEnumerable<Road> roads, int totalCost) : base(roads)
            {
                TotalCost = totalCost;
            }

            public int TotalCost { get; }
        }
    }
}