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

        public IList<Road> FindBestRoute(Car car)
        {
            var routeFinder = new RouteFinder(GetCityById(car.Entity.CityId));
            var routes = routeFinder.FindRoutes(car.Entity.FuelLevel + _random.Next(0, 50))
                .OrderByDescending(r => r.TotalCost);
            if (!routes.Any())
            {
                return routeFinder.FindClosestBaseCity();
            }

            return routes.FirstOrDefault();
        }

        private City GetCityById(int cityId)
        {
            return Cities[cityId - 1];
        }
    }
}
