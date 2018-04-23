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

        public IList<Route> FindRoutes(double maxCost, IList<RoadStatusEntity> knownRoads, IList<int> citiesToAvoid, bool lookAtCost = true)
        {
            _bestRoutes = new List<Route>();

            FindBestRoutes(_startCity, maxCost, 0, new List<Road>(), knownRoads, citiesToAvoid, lookAtCost);

            return _bestRoutes;
        }

        public Route FindClosestBaseCity(IList<RoadStatusEntity> knownRoads)
        {
            return new Route(FindShortestRoad(_startCity, knownRoads, 0), 0);
        }

        private IList<Road> FindShortestRoad(City fromCity, IList<RoadStatusEntity> knownRoads, int iteration)
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

        private bool FindBestRoutes(City fromCity, double maxCost, double currentCost, IList<Road> roads, IList<RoadStatusEntity> knownRoads, IList<int> citiesToAvoid, bool lookAtCost)
        {
            foreach (var road in fromCity.Roads)
            {
                if (_bestRoutes.Count >= 5)
                {
                    return false;
                }

                // Do not return to the same city
                var lastRoad = roads.LastOrDefault();
                if (road.To.Id == lastRoad?.From.Id)
                {
                    continue;
                }

                // we re trying to avoid this city
                if (citiesToAvoid.Contains(road.To.Id))
                {
                    continue;
                }

                // if city was already visited
                if (roads.Select(r => r.To.Id).ToList().Contains(road.To.Id))
                {
                    continue;
                }

                var routeDetails = knownRoads.FirstOrDefault(r =>
                            (r.FromCityId == road.From.Id && r.ToCityId == road.To.Id) ||
                            (r.FromCityId == road.To.Id && r.ToCityId == road.From.Id));

                var newCost = currentCost + CalculateRouteCost(road.BaseCost, routeDetails);
                if (lookAtCost && newCost > maxCost)
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

                if (FindBestRoutes(road.To, maxCost, currentCost, newRoads, knownRoads, citiesToAvoid, lookAtCost))
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

        public IList<Route> FindRouteToCity(City toCity, IList<RoadStatusEntity> knownRoads)
        {
            const int maxLength = 120;
            const int routesToFind = 1;

            var routes = new List<Route>();
            var cityStack = new Stack<City>();
            var routeStack = new Stack<List<Road>>();

            cityStack.Push(_startCity);
            routeStack.Push(new List<Road>());

            while (true)
            {
                if (!cityStack.Any())
                {
                    return routes;
                }

                var currentCity = cityStack.Pop();
                var currentRoute = routeStack.Pop();

                foreach (var road in currentCity.Roads)
                {
                    if (road.To.Id == toCity.Id)
                    {
                        var route = new List<Road>(currentRoute) {road};

                        routes.Add(new Route(route, knownRoads));
                        if (routes.Count >= routesToFind)
                        {
                            return routes;
                        }
                    }

                    if (currentRoute.Any(r => r.To.Id == road.To.Id))
                    {
                        break;
                    }

                    if (currentRoute.Count >= maxLength)
                    {
                        break;
                    }

                    // this road was already considered
                    if (currentRoute.Any(r => r.IsTheSame(road)))
                    {
                        continue;
                    }

                    cityStack.Push(road.To);
                    var newRoute = new List<Road>(currentRoute) {road};
                    routeStack.Push(newRoute);
                }
            }
        }

        public class Route : List<Road>
        {
            public Route(IList<Road> roads, IList<RoadStatusEntity> knownRoads) : base(roads)
            {
                TotalCost = 0d;
                foreach (var road in roads)
                {
                    var routeDetails = knownRoads.FirstOrDefault(r =>
                        (r.FromCityId == road.From.Id && r.ToCityId == road.To.Id) ||
                        (r.FromCityId == road.To.Id && r.ToCityId == road.From.Id));

                    TotalCost += CalculateRouteCost(road.BaseCost, routeDetails);
                }

                TotalRevenue = CalculateRevenue(roads);
            }

            public Route(IList<Road> roads, double totalCost) : base(roads)
            {
                TotalCost = totalCost;
                TotalRevenue = CalculateRevenue(roads);
            }

            public double TotalCost { get; }

            public int TotalRevenue { get; }

            public double TotalIncome => TotalRevenue - TotalCost;

            private int CalculateRevenue(IList<Road> roads)
            {
                var n = roads.Count;
                var nmax = roads.FirstOrDefault()?.From.VisitingBonus ?? 0d;
                var isAnyDuplicated = false;

                foreach (var road in roads)
                {
                    if (roads.Any(r => r.IsTheSame(road)))
                    {
                        isAnyDuplicated = true;
                        break;
                    }

                    if (road.To.VisitingBonus > nmax)
                    {
                        nmax = road.To.VisitingBonus;
                    }
                }

                if (isAnyDuplicated)
                {
                    return n;
                }

                return (int)Math.Ceiling(n * Math.Pow(1.04, n) * nmax);
            }
        }
    }
}