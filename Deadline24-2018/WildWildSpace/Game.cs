using System;
using System.Collections.Generic;
using System.Threading;
using Deadline24.ConsoleApp.WildWildSpace.Commands;
using Deadline24.ConsoleApp.WildWildSpace.Responses;
using Deadline24.Core;
using Deadline24.Core.Exceptions;

namespace Deadline24.ConsoleApp.WildWildSpace
{
    public class Game : IGame
    {
        public IDictionary<Type, CommandFactoryMethod> CommandFactories => new Dictionary<Type, CommandFactoryMethod>
        {
            { typeof(DescribeWorld), (client, param) => new DescribeWorldCommand(client)}
        };

        public void Update(CommandFactory commandFactory)
        {
            var world = commandFactory.ExecuteCommand<DescribeWorld>(string.Empty);

            Console.WriteLine($"executed function DescribeWorld.");
            Thread.Sleep(500);
        }

        public void HandleException(ServerExceptionBase exception)
        {
            Console.WriteLine(exception.Message);
        }

        public void HandleTimeout(float timeout)
        {
            Console.WriteLine($"Command limit reached, waiting {timeout} seconds.");
            Thread.Sleep((int)timeout * 1000);
        }
    }
}
