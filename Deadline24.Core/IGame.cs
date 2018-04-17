using System;
using System.Collections.Generic;
using Deadline24.Core.Exceptions;

namespace Deadline24.Core
{
    public interface IGame
    {
        void Update(CommandFactory commandFactory);

        void HandleException(ServerExceptionBase exception);

        void HandleTimeout(float timeout);

        IDictionary<Type, CommandFactoryMethod> CommandFactories { get; }
    }
}