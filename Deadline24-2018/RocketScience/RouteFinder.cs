using System;
using System.Collections.Generic;
using System.Linq;
using Deadline24.ConsoleApp.RocketScience.Responses;

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

        public IList<Route> FindRoutes(double maxCost, RoadStatusResponse knownRoads)
        {
            _bestRoutes = new List<Route>();

            FindBestRoutes(_startCity, maxCost, 0, new List<Road>(), knownRoads);

            return _bestRoutes;
        }

        public Route FindClosestBaseCity(RoadStatusResponse knownRoads)
        {
            return new Route(FindShortestRoad(_startCity, knownRoads, 0), 0);
        }

        private IList<Road> FindShortestRoad(City fromCity, RoadStatusResponse knownRoads, int iteration)
        {
            if (iteration > 4)
            {
                return new List<Road>();
            }

            foreach (var road in fromCity.Roads.Reverse())
            {
                if (road.To.IsBaseCity)
                {
                    return new List<Road> { road };
                }

                var route = new List<Road> { road };
                var foundRoute = FindShortestRoad(road.To, knownRoads, iteration + 1);
                if (!foundRoute.Any())
                {
                    continue;
                }

                route.AddRange(foundRoute);

                return route;
            }

            return new List<Road>();
        }

        private bool FindBestRoutes(City fromCity, double maxCost, double currentCost, IList<Road> roads, RoadStatusResponse knownRoads)
        {
            foreach (var road in fromCity.Roads)
            {

                if (_bestRoutes.Count > 10)
                {
                    return false;
                }

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

                var routeDetails = knownRoads.RoadStatuses.FirstOrDefault( r =>
                            (r.FromCityId == road.From.Id && r.ToCityId == road.To.Id) ||
                            (r.FromCityId == road.To.Id && r.ToCityId == road.From.Id));

                var newCost = currentCost + CalculateRouteCost(road.BaseCost, routeDetails);
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

                if (FindBestRoutes(road.To, maxCost, currentCost, newRoads, knownRoads))
                {
                    _bestRoutes.Add(new Route(newRoads, currentCost));
                }
            }

            return false;
        }

        public static double CalculateRouteCost(int routeCost, RoadStatusEntity roadDetails)
        {
            return Math.Max(3, roadDetails == null ? routeCost : roadDetails.TotalCost - roadDetails.TravelBonus);
        }

        public class Route : List<Road>
        {
            public Route(IEnumerable<Road> roads, double totalCost) : base(roads)
            {
                TotalCost = totalCost;
            }

            public double TotalCost { get; }
        }
    }
}