using Deadline24.ConsoleApp.RocketScience.Responses;
using Deadline24.Core;

namespace Deadline24.ConsoleApp.RocketScience.Commands
{
    public class GiveCommand : Command<GiveResponse>
    {
        private readonly string _parameters;

        public GiveCommand(Client client, string parameters) : base(client)
        {
            _parameters = parameters;
        }

        public override GiveResponse SendCommand()
        {
            Client.SendCommand($"GIVE {_parameters}");

            return new GiveResponse();
        }
    }
}
