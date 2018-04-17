using System.Threading;
using Deadline24.Core.Commands;
using Deadline24.Core.Exceptions;

namespace Deadline24.Core
{
    public class GameLoop
    {
        private readonly IGame _game;

        private readonly Client _client;

        private readonly CommandFactory _commandFactory;

        public GameLoop(IGame game, Client client)
        {
            _game = game;
            _client = client;
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
                        _game.Update(_commandFactory);
                    }
                    catch (ServerExceptionBase exception)
                    {
                        var commandException = exception as CommandException;
                        if (commandException != null && commandException.Code == 6)
                        {
                            var command = new GetWaitingTimeCommand(_client);
                            var timeout = command.SendCommand();

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