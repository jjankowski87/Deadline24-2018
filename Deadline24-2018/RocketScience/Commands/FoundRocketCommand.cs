using Deadline24.ConsoleApp.RocketScience.Responses;
using Deadline24.Core;

namespace Deadline24.ConsoleApp.RocketScience.Commands
{
    public class FoundRocketCommand : Command<FoundRocketResponse>
    {
        private readonly string _parameters;

        public FoundRocketCommand(Client client, string parameters) : base(client)
        {
            _parameters = parameters;
        }

        public override FoundRocketResponse SendCommand()
        {
            Client.SendCommand($"FOUND_ROCKET {_parameters}");

            return new FoundRocketResponse();
        }
    }
}
