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
        }

        public int CarId => Entity.CarId;

        public IList<Road> Route { get; set; }

        public CarEntity Entity { get; set; }

        public bool CanMove => Entity.TurnsWhereCarIsOnRoad <= 0;

        public void MoveToNextCity(CommandFactory commandFactory)
        {
            var nextRoute = Route.FirstOrDefault();
            if (nextRoute != null)
            {
                Console.WriteLine($"Car {CarId} goes to city {nextRoute.To.Id}, is base: {nextRoute.To.IsBaseCity}, has {Entity.FuelLevel} fuel.");
                commandFactory.ExecuteCommand<MoveResponse>($"{CarId} {nextRoute.To.Id}");
                Route.RemoveAt(0);
            }
        }
    }
}
