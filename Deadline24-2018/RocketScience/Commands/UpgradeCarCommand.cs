using Deadline24.ConsoleApp.RocketScience.Responses;
using Deadline24.Core;

namespace Deadline24.ConsoleApp.RocketScience.Commands
{
    public class UpgradeCarCommand : Command<UpgradeCarResponse>
    {
        private readonly string _parameters;

        public UpgradeCarCommand(Client client, string parameters) : base(client)
        {
            _parameters = parameters;
        }

        public override UpgradeCarResponse SendCommand()
        {
            Client.SendCommand("UPGRADE_CAR " + _parameters);

            return new UpgradeCarResponse();
        }
    }
}
