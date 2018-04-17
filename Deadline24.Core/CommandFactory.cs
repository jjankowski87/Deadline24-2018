using System;
using System.Collections.Generic;
using Deadline24.Core.Exceptions;

namespace Deadline24.Core
{
    public delegate ICommand<Response> CommandFactoryMethod(Client client, string parameters);

    public class CommandFactory
    {
        private readonly Client _client;

        private readonly IDictionary<Type, CommandFactoryMethod> _factories;

        public CommandFactory(Client client, IDictionary<Type, CommandFactoryMethod> factories)
        {
            _client = client;
            _factories = factories;
        }

        public TResponse ExecuteCommand<TResponse>(string parameters) where TResponse: Response
        {
            CommandFactoryMethod factoryMethod;
            if (_factories.TryGetValue(typeof(TResponse), out factoryMethod))
            {
                var command = factoryMethod(_client, parameters);
                return command.SendCommand().As<TResponse>();
            }

            throw new ServerExceptionBase($"Could not find {typeof(TResponse)} command.");
        }
    }
}