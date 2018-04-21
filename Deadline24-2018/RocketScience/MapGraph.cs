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

        public IList<Road> FindBestRoute(Car car, RoadStatusResponse knownRoads)
        {
            var routeFinder = new RouteFinder(GetCityById(car.Entity.CityId));
            var routes = routeFinder.FindRoutes(car.Entity.FuelLevel + _random.Next(-20, 30), knownRoads)
                .OrderByDescending(r => r.TotalCost);
            if (!routes.Any())
            {
                routes = routeFinder.FindRoutes(80, knownRoads).OrderBy(r => r.TotalCost);
                if (!routes.Any())
                {
                    return routeFinder.FindClosestBaseCity(knownRoads);
                }
            }

            return routes.FirstOrDefault();
        }

        private City GetCityById(int cityId)
        {
            return Cities[cityId - 1];
        }

        public double CalculateRouteCost(IList<Road> route, RoadStatusResponse knownRoads)
        {
            var currentCost = 0d;
            foreach (var road in route)
            {
                var routeDetails = knownRoads.RoadStatuses.FirstOrDefault(r =>
                    (r.FromCityId == road.From.Id && r.ToCityId == road.To.Id) ||
                    (r.FromCityId == road.To.Id && r.ToCityId == road.From.Id));

               currentCost += RouteFinder.CalculateRouteCost(road.BaseCost, routeDetails);
            }

            return currentCost;
        }
    }
}
