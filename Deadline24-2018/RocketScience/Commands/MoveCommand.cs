using System;
using Deadline24.ConsoleApp.RocketScience.Responses;
using Deadline24.Core;

namespace Deadline24.ConsoleApp.RocketScience.Commands
{
    public class MoveCommand : Command<MoveResponse>
    {
        private readonly string _parameters;

        public MoveCommand(Client client, string parameters) : base(client)
        {
            _parameters = parameters;
        }

        public override MoveResponse SendCommand()
        {
            Client.SendCommand("MOVE " + _parameters);

            return new MoveResponse();
        }
    }
}
