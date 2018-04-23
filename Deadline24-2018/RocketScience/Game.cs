using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.ModelBinding;
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

        private IList<RoadStatusEntity> _knownRoads;

        private IDictionary<int, int> _visitedCities;

        private int? _myLaunchpadId = null;

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
            { typeof(LaunchRocketResponse), (client, param) => new LaunchRocketCommand(client, param) },
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
                _knownRoads = new List<RoadStatusEntity>();
                _visitedCities = new Dictionary<int, int>();
            }

            UpdateKnownRoads(commandFactory.ExecuteCommand<RoadStatusResponse>(string.Empty).RoadStatuses);
            var homeResponse = commandFactory.ExecuteCommand<HomeResponse>(string.Empty);
            var myLaunchpadId = GetMyLaunchpadId(commandFactory);

            _gameState.TimeToEnd = timeTillEnd;
            _gameState.HomeStats = homeResponse;

            var shouldCreateLaunchpad = false;
            // Check if we could launch
            if (myLaunchpadId == null &&
                (_cars.All(c => c.Entity.EngineType == "FAST") || !ShouldUpgradeEngine()) &&
                ShouldBuildLaunchpad())
            {
                myLaunchpadId = GetCityForLaunch();
                shouldCreateLaunchpad = true;
            }

            UpdateCarState(commandFactory);
            foreach (var car in _cars)
            {
                _gameState.UpdateCarData(car.Entity);

                if (ShouldUpgradeEngine() &&
                    car.Entity.EngineType == "NORMAL" && car.Entity.CityId == homeResponse.IDh_HomeCityId
                    && _describeWorld.Sue_CarUpgradeCost + _cars.Count * 99 * _describeWorld.Sg_FuelCost <= homeResponse.Ch_Amount)
                {
                    commandFactory.ExecuteCommand<UpgradeCarResponse>($"{car.CarId} ENGINE");
                }

                if (car.Entity.CapacityState == "NORMAL" && car.Entity.CityId == homeResponse.IDh_HomeCityId && _myLaunchpadId.HasValue &&
                    _describeWorld.Sue_CarUpgradeCost + _cars.Count * 99 * _describeWorld.Sg_FuelCost <= homeResponse.Ch_Amount)
                {
                    commandFactory.ExecuteCommand<UpgradeCarResponse>($"{car.CarId} TRUNK");
                }
            }

            foreach (var car in _cars.Where(c => c.CanMove))
            {
                if (shouldCreateLaunchpad && car.Entity.CityId == myLaunchpadId.Value && _describeWorld.Sf_AmountForTaxes < homeResponse.Ch_Amount)
                {
                    Console.WriteLine($"FOUNDING ROCKET LAUNCH AT CITY {car.Entity.CityId}");
                    try
                    {
                        commandFactory.ExecuteCommand<FoundRocketResponse>($"{car.CarId} 50 20");
                        _myLaunchpadId = myLaunchpadId;
                        shouldCreateLaunchpad = false;
                    }
                    catch (CommandException ce)
                    {
                        Console.WriteLine($"LAUNCHPAD NOT CREATED {ce.Code}: {ce.Message}.");
                    }
                }

                // Transfer money to launchpad
                if (!shouldCreateLaunchpad && myLaunchpadId.HasValue)
                {
                    if (_describeWorld.Mh_HomeBases.Contains(car.Entity.CityId))
                    {
                        var amountToTake = Math.Min(_describeWorld.Nmax_MaximumMoneyThatCarCanCarry - car.Entity.MoneyInCar, homeResponse.Ch_Amount);

                        if (amountToTake > 0)
                        {
                            Console.WriteLine($"Car {car.CarId} take {amountToTake}$ to launchpad.");
                            commandFactory.ExecuteCommand<TakeResponse>($"{car.CarId} {amountToTake}");
                        }
                    }

                    if (car.Entity.CityId == myLaunchpadId.Value && car.Entity.MoneyInCar > 0)
                    {
                        Console.WriteLine($"Car {car.CarId} put {car.Entity.MoneyInCar}$ to launchpad.");
                        commandFactory.ExecuteCommand<GiveResponse>($"{car.CarId} {car.Entity.MoneyInCar}");
                    }
                }

                var previousDestinationId = car.Route.LastOrDefault()?.To.Id;
                if (!car.Route.Any() || car.Entity.FuelLevel < _graph.CalculateRouteCost(car.Route, _knownRoads))
                {
                    var citiesToAvoid = GetCitiesToAvoid(_cars.Select(c => c.Route).ToList());
                    var newRoute = _graph.FindBestRoute(car, _knownRoads, citiesToAvoid, homeResponse.IDh_HomeCityId) ?? new List<Road>();
                    if (!car.Route.Any() || _graph.CalculateRouteCost(newRoute, _knownRoads) < car.Entity.FuelLevel)
                    {
                        car.SetNewRoute(newRoute);
                    }
                }

                if (_myLaunchpadId.HasValue &&
                    _gameState.RocketInfo?.Dt_TotalDeposit >= _describeWorld.Sr_AmountToDepositBeforeLaunch)
                {
                    commandFactory.ExecuteCommand<LaunchRocketResponse>($"{_myLaunchpadId.Value}");
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
                    if (!_visitedCities.ContainsKey(cityId))
                    {
                        _visitedCities[cityId] = 1;
                    }
                    else
                    {
                        _visitedCities[cityId]++;
                    }
                }
            }

            visualizer.UpdateGameState(_gameState);

            commandFactory.ExecuteCommand<WaitResponse>(string.Empty);
        }

        private bool ShouldUpgradeEngine()
        {
            return _describeWorld.Sue_CarUpgradeCost <= 20000;
        }

        private bool ShouldBuildLaunchpad()
        {
            return _describeWorld.Sr_AmountToDepositBeforeLaunch < 150000;
        }

        private int? GetMyLaunchpadId(CommandFactory commandFactory)
        {
            if (_myLaunchpadId.HasValue)
            {
                return _myLaunchpadId;
            }

            var rocketInfo = commandFactory.ExecuteCommand<RocketInfoResponse>(string.Empty);
            foreach (var info in rocketInfo.RocketInfos)
            {
                if (info.Do_TeamDeposit > 0)
                {
                    _gameState.RocketInfo = info;

                    _myLaunchpadId = info.CityId;
                    return _myLaunchpadId;
                }
            }

            return null;
        }

        private void UpdateKnownRoads(IList<RoadStatusEntity> knownRoads)
        {
            foreach (var knownRoad in knownRoads)
            {
                var road = _knownRoads.FirstOrDefault(kr => kr.FromCityId == knownRoad.FromCityId && kr.ToCityId == knownRoad.ToCityId);
                if (road != null)
                {
                    road.TotalCost = knownRoad.TotalCost;
                    road.TravelBonus = knownRoad.TravelBonus;
                }
                else
                {
                    _knownRoads.Add(knownRoad);
                }
            }
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

        private IList<int> GetCitiesToAvoid(IList<IList<Road>> carRoads)
        {
            var citiesToAvoid = new List<int>();

            Random rand = new Random();
            foreach (var carRoad in carRoads)
            {
                if (carRoad.Count > 2)
                {
                    var someCities = carRoad.Skip(1).Take(carRoad.Count - 2).ToList();
                    var citiesIds = someCities.Select(c => c.To.Id);

                    foreach (var cityId in citiesIds)
                    {
                        if (rand.Next(0, 10) > 3)
                        {
                            citiesToAvoid.Add(cityId);
                        }
                    }
                }
            }

            return citiesToAvoid;
        }

        private int GetCityForLaunch()
        {
            var maxVisits = 0;
            var launchCityId = 0;

            foreach (var visitedCity in _visitedCities)
            {
                if (visitedCity.Value > maxVisits && !_describeWorld.Mh_HomeBases.Contains(visitedCity.Key))
                {
                    maxVisits = visitedCity.Value;
                    launchCityId = visitedCity.Key;
                }
            }

            return launchCityId;
        }

        public void HandleException(ServerExceptionBase exception)
        {
            Console.WriteLine(exception.Message);

            _lastTimeToEnd = 0;

            Thread.Sleep(100);
        }

        public void HandleTimeout(double timeout)
        {
            Console.WriteLine($"Command limit reached, waiting {timeout} seconds.");
            Thread.Sleep((int)timeout);
        }
    }
}
