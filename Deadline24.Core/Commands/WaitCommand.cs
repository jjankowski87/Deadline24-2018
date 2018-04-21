using System;
using Deadline24.Core.Responses;

namespace Deadline24.Core.Commands
{
    public class WaitCommand : Command<WaitResponse>
    {
        public WaitCommand(Client client) : base(client)
        {
        }

        public override WaitResponse SendCommand()
        {
            Client.SendCommand("WAIT");

            var response = Client.ReadLine();
            response = Client.ReadLine();

            return new WaitResponse();
        }
    }
}
