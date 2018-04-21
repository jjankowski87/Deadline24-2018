using System.Collections.Generic;
using Deadline24.Core;

namespace Deadline24.ConsoleApp.RocketScience.Responses
{
    public class CarsResponse : Response
    {
        public int NumberOfCars { get; set; }

        public List<CarEntity> CarList { get; set; }
    }

    public class CarEntity
    {
        public int CarId { get; set; }

        public int CityId { get; set; }

        public int FuelLevel { get; set; }

        public string EngineType { get; set; }

        public string CapacityState { get; set; }

        public string UniqueRoads { get; set; }

        public int RoadsVisited { get; set; }

        public int TurnsWhereCarIsOnRoad { get; set; }

        public int MoneyInCar { get; set; }
    }
}
