using System;
using System.Collections.Generic;
using System.Threading;
using Deadline24.ConsoleApp.WildWildSpace.Commands;
using Deadline24.ConsoleApp.WildWildSpace.Responses;
using Deadline24.Core;
using Deadline24.Core.Exceptions;
using Deadline24.Core.Visualization;

namespace Deadline24.ConsoleApp.WildWildSpace
{
    public class Game : IGame
    {
        public IDictionary<Type, CommandFactoryMethod> CommandFactories => new Dictionary<Type, CommandFactoryMethod>
        {
            { typeof(DescribeWorld), (client, param) => new DescribeWorldCommand(client)}
        };

        public void Update(CommandFactory commandFactory, IVisualizer visualizer)
        {
            var describeWorld = commandFactory.ExecuteCommand<DescribeWorld>(string.Empty);

            var world = new World(200, 200, 4);

            var rand = new Random();
            for (int i = 0; i < 100; i++)
            {
                int x = rand.Next(200);
                int y = rand.Next(200);
                int type = rand.Next(0, 11);

                world.AddItem(x, y, type);
            }

            visualizer.DisplayWorld(world);

            Console.WriteLine($"executed function DescribeWorld.");
            Thread.Sleep(2000);
        }

        public void HandleException(ServerExceptionBase exception)
        {
            Console.WriteLine(exception.Message);
        }

        public void HandleTimeout(double timeout)
        {
            Console.WriteLine($"Command limit reached, waiting {timeout} seconds.");
            Thread.Sleep((int)timeout * 1000);
        }
    }
}
