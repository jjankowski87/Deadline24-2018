using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Deadline24.ConsoleApp.RocketScience.Commands;
using Deadline24.ConsoleApp.RocketScience.Responses;
using Deadline24.Core;
using Deadline24.Core.Exceptions;

namespace Deadline24.ConsoleApp.RocketScience
{
    public class Game : IGame
    {
        private string _worldName = string.Empty;

        private WorldResponse _worldResponse;

        private MapGraph _graph;

        private HomeResponse _homeResponse;

        private IList<Car> _cars;

        public IDictionary<Type, CommandFactoryMethod> CommandFactories => new Dictionary<Type, CommandFactoryMethod>
        {
            { typeof(DescribeWorldResponse), (client, param) => new DescribeWorldCommand(client)},
            { typeof(WorldResponse), (client, param) => new LoadWorldCommand(client, param)},
            { typeof(HomeResponse), (client, param) => new HomeCommand(client)},
            { typeof(CarsResponse), (client, param) => new CarsCommand(client)},
            { typeof(MoveResponse), (client, param) => new MoveCommand(client, param)},
        };

        public void Update(CommandFactory commandFactory, Core.Visualization.IVisualizer visualizer)
        {
            var describeWorld = commandFactory.ExecuteCommand<DescribeWorldResponse>(string.Empty);
            if (_worldName != describeWorld.Name)
            {
                _worldResponse = commandFactory.ExecuteCommand<WorldResponse>(describeWorld.Name);
                _homeResponse = commandFactory.ExecuteCommand<HomeResponse>(string.Empty);
                _graph = new MapGraph(_worldResponse);
                _cars = new List<Car>();

                _worldName = describeWorld.Name;
            }

            UpdateCarState(commandFactory);
            foreach (var car in _cars.Where(c => c.CanMove))
            {
                if (!car.Route.Any())
                {
                    car.Route = _graph.FindBestRoute(car) ?? new List<Road>();
                }

                car.MoveToNextCity(commandFactory);
            }

            Thread.Sleep(100);
        }

        private void UpdateCarState(CommandFactory commandFactory)
        {
            var carsResponse = commandFactory.ExecuteCommand<CarsResponse>(string.Empty);
            foreach (var carEntity in carsResponse.CarList)
            {
                var car = _cars.FirstOrDefault(c => c.CarId == carEntity.CarId);
                if (car == null)
                {
                    _cars.Add(new Car(carEntity));
                }
                else
                {
                    car.Entity = carEntity;
                }
            }
        }

        public void HandleException(ServerExceptionBase exception)
        {
            Console.WriteLine(exception.Message);
            Thread.Sleep(100);
        }

        public void HandleTimeout(double timeout)
        {
            Console.WriteLine($"Command limit reached, waiting {timeout} seconds.");
            Thread.Sleep((int)timeout);
        }
    }
}
