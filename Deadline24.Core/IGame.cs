using System;
using System.Collections.Generic;
using Deadline24.Core.Exceptions;
using Deadline24.Core.Visualization;

namespace Deadline24.Core
{
    public interface IGame<T>
    {
        void Update(CommandFactory commandFactory, IVisualizer<T> visualizer);

        void HandleException(ServerExceptionBase exception);

        void HandleTimeout(double timeout);

        IDictionary<Type, CommandFactoryMethod> CommandFactories { get; }
    }
}