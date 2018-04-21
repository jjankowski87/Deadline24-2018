using System.Threading;
using Deadline24.Core.Commands;
using Deadline24.Core.Exceptions;
using Deadline24.Core.Visualization;

namespace Deadline24.Core
{
    public class GameLoop<T>
    {
        private readonly IGame<T> _game;

        private readonly Client _client;

        private readonly IVisualizer<T> _visualizer;

        private readonly CommandFactory _commandFactory;

        public GameLoop(IGame<T> game, Client client, IVisualizer<T> visualizer)
        {
            _game = game;
            _client = client;
            _visualizer = visualizer;
            _commandFactory = new CommandFactory(_client, game.CommandFactories);
        }

        public bool IsRunning { get; private set; }

        public void Start()
        {
            IsRunning = true;

            new Thread(() =>
            {
                while (IsRunning)
                {
                    try
                    {
                        _game.Update(_commandFactory, _visualizer);
                    }
                    catch (ServerExceptionBase exception)
                    {
                        var commandException = exception as CommandException;
                        if (commandException != null && commandException.Code == 6)
                        {
                            var command = new GetWaitingTimeCommand(_client);
                            var timeout = command.SendCommand();
                            _client.ReadLine();

                            _game.HandleTimeout(timeout.Value);
                        }
                        else
                        {
                            _game.HandleException(exception);
                        }
                    }
                }
            }).Start();
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}