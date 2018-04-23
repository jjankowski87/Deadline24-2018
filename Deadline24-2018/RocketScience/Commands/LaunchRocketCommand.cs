using System;
using Deadline24.ConsoleApp.RocketScience.Responses;
using Deadline24.Core;

namespace Deadline24.ConsoleApp.RocketScience.Commands
{
    public class LaunchRocketCommand : Command<LaunchRocketResponse>
    {
        private readonly string _parameters;

        public LaunchRocketCommand(Client client, string parameters) : base(client)
        {
            _parameters = parameters;
        }

        public override LaunchRocketResponse SendCommand()
        {
            Client.SendCommand($"LAUNCH_ROCKET {_parameters}");

            return new LaunchRocketResponse();
        }
    }
}
