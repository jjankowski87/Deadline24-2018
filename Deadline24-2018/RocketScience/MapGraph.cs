using System;
using System.Collections.Generic;
using System.Linq;
using Deadline24.ConsoleApp.RocketScience.Responses;

namespace Deadline24.ConsoleApp.RocketScience
{
    public class MapGraph
    {
        private readonly Random _random = new Random();

        public MapGraph(WorldResponse worldResponse)
        {
            Cities = worldResponse.VisitingBonuses.Select((vb, i) => new City(i + 1, vb, worldResponse.BaseLocations.Contains(i + 1))).ToList();

            foreach (var road in worldResponse.Roads)
            {
                var fromCity = GetCityById(road.FromCityId);
                var toCity = GetCityById(road.ToCityId);

                fromCity.Roads.Add(new Road(fromCity, toCity, road.BaseCost));
                toCity.Roads.Add(new Road(toCity, fromCity, road.BaseCost));
            }
        }

        public IList<City> Cities { get; }

        public IList<Road> FindBestRoute(Car car, IList<RoadStatusEntity> knownRoads, IList<int> citiesToAvoid, int homeCityId)
        {
            var routeFinder = new RouteFinder(GetCityById(car.Entity.CityId));
            var routes = routeFinder.FindRoutes(car.Entity.FuelLevel + _random.Next(25, 50), knownRoads, citiesToAvoid)
                .OrderBy(r => r.TotalIncome).ToList();
            if (routes.Any())
            {
                //return routes.First();
                var i = _random.Next(0, routes.Count);
                return routes[i];
            }

            Console.WriteLine("Could not find route avoiding some cities.");
            routes = routeFinder.FindRoutes(car.Entity.FuelLevel + _random.Next(25, 50), knownRoads, new List<int>())
                .OrderBy(r => r.TotalIncome).ToList();
            if (routes.Any())
            {
                //return routes.First();
                var i = _random.Next(0, routes.Count);
                return routes[i];
            }

            Console.WriteLine("Could not find best route, searching wider.");
            //routes = routeFinder.FindRoutes(60, knownRoads, new List<int>(), false).OrderBy(r => r.Count).ToList();
            //if (!routes.Any())
            //{
                Console.WriteLine("Could not find best route, searching closes base city.");
                var route = routeFinder.FindRouteToCity(GetCityById(homeCityId), knownRoads);
                return route.FirstOrDefault() ?? new List<Road>();

                //return routeFinder.FindClosestBaseCity(knownRoads);
            //}

            //return routes.FirstOrDefault();
        }

        private City GetCityById(int cityId)
        {
            return Cities[cityId - 1];
        }

        public double CalculateRouteCost(IList<Road> route, IList<RoadStatusEntity> knownRoads)
        {
            var currentCost = 0d;
            foreach (var road in route)
            {
                var routeDetails = knownRoads.FirstOrDefault(r =>
                    (r.FromCityId == road.From.Id && r.ToCityId == road.To.Id) ||
                    (r.FromCityId == road.To.Id && r.ToCityId == road.From.Id));

               currentCost += RouteFinder.CalculateRouteCost(road.BaseCost, routeDetails);
            }

            return currentCost;
        }
    }
}
