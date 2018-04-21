using Deadline24.ConsoleApp.RocketScience.Responses;
using Deadline24.Core;

namespace Deadline24.ConsoleApp.RocketScience.Commands
{
    public class TakeCommand : Command<TakeResponse>
    {
        private readonly string _parameters;

        public TakeCommand(Client client, string parameters) : base(client)
        {
            _parameters = parameters;
        }

        public override TakeResponse SendCommand()
        {
            Client.SendCommand($"TAKE {_parameters}");

            return new TakeResponse();
        }
    }
}
