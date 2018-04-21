using System;
using Deadline24.ConsoleApp.RocketScience.Responses;
using Deadline24.Core;

namespace Deadline24.ConsoleApp.RocketScience.Commands
{
    public class HomeCommand :Command<HomeResponse>
    {
        public HomeCommand(Client client) : base(client)
        {
        }

        public override HomeResponse SendCommand()
        {
            Client.SendCommand("HOME");

            var firstLine = Client.ReadLine().Split(' ');
            VerifyResponseLength(firstLine, 4);

            return new HomeResponse
            {
                IDh_HomeCityId = ParseInt(firstLine, 0),
                Ch_Amount = ParseInt(firstLine, 1),
                IDl_LawyerProtectingId = firstLine[2] == "NONE" ? null : (int?)ParseInt(firstLine, 2),
                Lm_LawyerMotivation = ParseInt(firstLine, 3)
            };
        }
    }
}
