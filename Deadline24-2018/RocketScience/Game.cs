using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Deadline24.ConsoleApp.RocketScience.Commands;
using Deadline24.ConsoleApp.RocketScience.Responses;
using Deadline24.Core;
using Deadline24.Core.Commands;
using Deadline24.Core.Exceptions;
using Deadline24.Core.Responses;

namespace Deadline24.ConsoleApp.RocketScience
{
    public class Game : IGame<RocketScienceGameState>
    {
        private WorldResponse _worldResponse;

        private MapGraph _graph;

        private IList<Car> _cars;

        private RocketScienceGameState _gameState;

        private DescribeWorldResponse _describeWorld;

        private int _lastTimeToEnd = 0;

        public IDictionary<Type, CommandFactoryMethod> CommandFactories => new Dictionary<Type, CommandFactoryMethod>
        {
            { typeof(DescribeWorldResponse), (client, param) => new DescribeWorldCommand(client) },
            { typeof(WorldResponse), (client, param) => new LoadWorldCommand(client, param) },
            { typeof(HomeResponse), (client, param) => new HomeCommand(client) },
            { typeof(CarsResponse), (client, param) => new CarsCommand(client) },
            { typeof(MoveResponse), (client, param) => new MoveCommand(client, param) },
            { typeof(RoadStatusResponse), (client, param) => new RoadStatusCommand(client) },
            { typeof(TimeToEndResponse), (client, param) => new TimeToEndCommand(client) },
            { typeof(UpgradeCarResponse), (client, param) => new UpgradeCarCommand(client, param) },
            { typeof(WaitResponse), (client, param) => new WaitCommand(client) },
            { typeof(FoundRocketResponse), (client, param) => new FoundRocketCommand(client, param) },
            { typeof(RocketInfoResponse), (client, param) => new RocketInfoCommand(client) },
            { typeof(TakeResponse), (client, param) => new TakeCommand(client, param) },
            { typeof(GiveResponse), (client, param) => new GiveCommand(client, param) },
        };

        public void Update(CommandFactory commandFactory, Core.Visualization.IVisualizer<RocketScienceGameState> visualizer)
        {
            var timeTillEnd = commandFactory.ExecuteCommand<TimeToEndResponse>(string.Empty);
            if (timeTillEnd.TurnsTillEnd > _lastTimeToEnd)
            {
                _lastTimeToEnd = timeTillEnd.TurnsTillEnd;
                _gameState = new RocketScienceGameState();

                _describeWorld = commandFactory.ExecuteCommand<DescribeWorldResponse>(string.Empty);
                _worldResponse = commandFactory.ExecuteCommand<WorldResponse>(_describeWorld.Name);
                _gameState.DescribeWorld = _describeWorld;

                _graph = new MapGraph(_worldResponse);
                _cars = new List<Car>();
            }

            var homeResponse = commandFactory.ExecuteCommand<HomeResponse>(string.Empty);

            _gameState.TimeToEnd = timeTillEnd;
            _gameState.HomeStats = homeResponse;

            UpdateCarState(commandFactory);
            foreach (var car in _cars)
            {
                _gameState.UpdateCarData(car.Entity);

                if (car.Entity.EngineType == "NORMAL" && car.Entity.CityId == homeResponse.IDh_HomeCityId
                    && _describeWorld.Sue_CarUpgradeCost + (_describeWorld.Sg_FuelCost * 500) <= homeResponse.Ch_Amount)
                {
                    commandFactory.ExecuteCommand<UpgradeCarResponse>($"{car.CarId} ENGINE");
                }
            }

            var knownRoads = commandFactory.ExecuteCommand<RoadStatusResponse>(string.Empty);
            foreach (var car in _cars.Where(c => c.CanMove))
            {
                var previousDestinationId = car.Route.LastOrDefault()?.To.Id;

                if (!car.Route.Any() || car.Entity.FuelLevel < _graph.CalculateRouteCost(car.Route, knownRoads))
                {
                    car.SetNewRoute(_graph.FindBestRoute(car, knownRoads) ?? new List<Road>());
                }

                var newDestinationId = car.Route.LastOrDefault()?.To.Id;
                if (previousDestinationId != newDestinationId)
                {
                    _gameState.ResetRoute(car.CarId);
                }

                var cityId = car.MoveToNextCity(commandFactory);
                if (cityId != 0)
                {
                    _gameState.AddVisitedCity(car.CarId, cityId);
                    _gameState.AddRouteLength(car.CarId, car.Route.Count);
                }
            }

            visualizer.UpdateGameState(_gameState);

            commandFactory.ExecuteCommand<WaitResponse>(string.Empty);
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
