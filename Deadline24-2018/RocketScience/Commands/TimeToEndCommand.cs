using Deadline24.ConsoleApp.RocketScience.Responses;
using Deadline24.Core;

namespace Deadline24.ConsoleApp.RocketScience.Commands
{
    public class TimeToEndCommand : Command<TimeToEndResponse>
    {
        public TimeToEndCommand(Client client) : base(client)
        {
        }

        public override TimeToEndResponse SendCommand()
        {
            Client.SendCommand("TIME_TO_END");

            var firstLine = Client.ReadLine().Split(' ');
            VerifyResponseLength(firstLine, 2);

            return new TimeToEndResponse
            {
                LaunchedRockets = ParseInt(firstLine, 0),
                TurnsTillEnd = ParseInt(firstLine, 1)
            };

        }
    }
}
