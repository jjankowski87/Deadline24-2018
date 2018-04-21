using System;
using System.Collections.Generic;
using System.Linq;
using Deadline24.ConsoleApp.RocketScience.Responses;
using Deadline24.Core;

namespace Deadline24.ConsoleApp.RocketScience
{
    public class Car
    {
        public Car(CarEntity entity)
        {
            Entity = entity;
            Route = new List<Road>();
            VisitedCities = new List<int>();
        }

        public int CarId => Entity.CarId;

        public IList<int> VisitedCities { get; private set; }

        public IList<Road> Route { get; private set; }

        public CarEntity Entity { get; set; }

        public bool CanMove => Entity.TurnsWhereCarIsOnRoad <= 0;

        public void SetNewRoute(IList<Road> newRoute)
        {
            Route = newRoute;
            VisitedCities = new List<int>();
        }

        public int MoveToNextCity(CommandFactory commandFactory)
        {
            var nextRoute = Route.FirstOrDefault();
            if (nextRoute != null)
            {
                Console.WriteLine($"Car {CarId} goes to city {nextRoute.To.Id}, is base: {nextRoute.To.IsBaseCity}, has {Entity.FuelLevel} fuel.");
                commandFactory.ExecuteCommand<MoveResponse>($"{CarId} {nextRoute.To.Id}");
                Route.RemoveAt(0);

                VisitedCities.Add(nextRoute.From.Id);
                return nextRoute.From.Id;
            }

            return 0;
        }
    }
}
