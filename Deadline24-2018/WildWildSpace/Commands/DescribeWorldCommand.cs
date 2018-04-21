using Deadline24.ConsoleApp.WildWildSpace.Responses;
using Deadline24.Core;

namespace Deadline24.ConsoleApp.WildWildSpace.Commands
{
    public class DescribeWorldCommand : Command<DescribeWorld>
    {
        public DescribeWorldCommand(Client client) : base(client)
        {
        }

        public override DescribeWorld SendCommand()
        {
            Client.SendCommand("DESCRIBE_WORLD");

            var response = Client.ReadLine().Split(' ');
            VerifyResponseLength(response, 19);

            return new DescribeWorld
            {
                Dimensions = ParseInt(response, 0),
                BoardSize = ParseInt(response, 1),
                ScoreScaling = ParseDouble(response, 7),
                MapName = response[5]
            };
        }
    }
}