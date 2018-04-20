using System;
using System.Collections.Generic;
using Deadline24.Core.Exceptions;
using Deadline24.Core.Visualization;

namespace Deadline24.Core
{
    public interface IGame
    {
        void Update(CommandFactory commandFactory, IVisualizer visualizer);

        void HandleException(ServerExceptionBase exception);

        void HandleTimeout(float timeout);

        IDictionary<Type, CommandFactoryMethod> CommandFactories { get; }
    }
}