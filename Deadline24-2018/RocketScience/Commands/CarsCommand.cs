using System;
using System.Collections.Generic;
using Deadline24.ConsoleApp.RocketScience.Responses;
using Deadline24.Core;

namespace Deadline24.ConsoleApp.RocketScience.Commands
{
    public class CarsCommand : Command<CarsResponse>
    {
        public CarsCommand(Client client) : base(client)
        {
        }

        public override CarsResponse SendCommand()
        {
            Client.SendCommand("CARS");

            var firstLine = Client.ReadLine().Split(' ');
            VerifyResponseLength(firstLine, 1);

            var numberOfCars = ParseInt(firstLine, 0);

            var cars = new List<CarEntity>();
            for (var i = 1; i < numberOfCars + 1; i++)
            {
                var carLine = Client.ReadLine().Split(' ');

                cars.Add(new CarEntity
                {
                    CarId = ParseInt(carLine, 0),
                    CityId = ParseInt(carLine, 1),
                    FuelLevel = ParseInt(carLine, 2),
                    EngineType = carLine[3],
                    CapacityState = carLine[4],
                    UniqueRoads = carLine[5],
                    RoadsVisited = ParseInt(carLine, 6),
                    TurnsWhereCarIsOnRoad = ParseInt(carLine, 7),
                    MoneyInCar = ParseInt(carLine, 8),
                });
            }

            return new CarsResponse
            {
                NumberOfCars = numberOfCars,
                CarList = cars
            };
        }
    }
}
