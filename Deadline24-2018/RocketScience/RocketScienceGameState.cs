using System.Collections.Generic;
using System.Linq;
using Deadline24.ConsoleApp.RocketScience.Responses;

namespace Deadline24.ConsoleApp.RocketScience
{
    public class RocketScienceGameState
    {
        public RocketScienceGameState()
        {
            Cars = new List<CarState>();
        }

        public TimeToEndResponse TimeToEnd { get; set; }

        public HomeResponse HomeStats { get; set; }

        public DescribeWorldResponse DescribeWorld { get; set; }

        public RocketInfoEntity RocketInfo { get; set; }

        public IList<CarState> Cars { get; }

        internal void UpdateCarData(CarEntity car)
        {
            var carToUpdate = Cars.FirstOrDefault(c => c.Id == car.CarId);
            if (carToUpdate == null)
            {
                carToUpdate = new CarState(car.CarId);
                Cars.Add(carToUpdate);
            }

            carToUpdate.Fuel = car.FuelLevel;

            var upgrades = string.Empty;
            if (car.EngineType == "FAST")
            {
                upgrades += "E";
            }

            if (car.CapacityState == "BIG")
            {
                upgrades += "T";
            }

            carToUpdate.Upgrades = upgrades;
        }

        public void AddVisitedCity(int carId, int cityId)
        {
            var car = Cars.FirstOrDefault(c => c.Id == carId);
            if (car != null)
            {
                car.VisitedCities.Enqueue(cityId);
            }
        }

        public void ResetRoute(int carId)
        {
            var car = Cars.FirstOrDefault(c => c.Id == carId);
            if (car != null)
            {
                car.VisitedCities.Clear();
            }
        }

        public void AddRouteLength(int carId, int routeLength)
        {
            var car = Cars.FirstOrDefault(c => c.Id == carId);
            if (car != null)
            {
                car.LengthToBase = routeLength;
            }
        }
    }

    public class CarState
    {
        public CarState(int id)
        {
            Id = id;
            VisitedCities = new Queue<int>();
        }

        public int Id { get; set; }

        public int Fuel { get; set; }

        public string Upgrades { get; set;}

        public int LengthToBase { get; set; }

        public Queue<int> VisitedCities { get; }
    }

    public class VisitedCitiesQueue : Queue<int>
    {
        private readonly int _capacity;

        public VisitedCitiesQueue(int capacity)
        {
            _capacity = capacity;
        }

        public void LimitedEnqueue(int item)
        {
            if (Count >= _capacity)
            {
                Dequeue();
            }

            Enqueue(item);
        }
    }
}
