using System.Collections.Generic;
using Deadline24.ConsoleApp.RocketScience.Responses;
using Deadline24.Core;

namespace Deadline24.ConsoleApp.RocketScience.Commands
{
    public class RocketInfoCommand : Command<RocketInfoResponse>
    {
        public RocketInfoCommand(Client client) : base(client)
        {
        }

        public override RocketInfoResponse SendCommand()
        {
            Client.SendCommand("ROCKET_INFO");

            var firstLine = Client.ReadLine().Split(' ');
            VerifyResponseLength(firstLine, 1);

            var numberOfRockets = ParseInt(firstLine, 0);

            var rockets = new List<RocketInfoEntity>();
            for (var i = 1; i < numberOfRockets + 1; i++)
            {
                var rocketLine = Client.ReadLine().Split(' ');

                rockets.Add(new RocketInfoEntity
                {
                    CityId = ParseInt(rocketLine, 0),
                    Sg_ShareholderGrain = ParseInt(rocketLine, 1),
                    Do_TeamDeposit = ParseInt(rocketLine, 2),
                    Dt_TotalDeposit = ParseInt(rocketLine, 3),
                    Fd_IsLaunched = ParseBool(rocketLine, 4),
                    Fc_HasControl = ParseBool(rocketLine, 5),
                    Fl_TurnsTillLawsuit = ParseInt(rocketLine, 6),
                    Fo_SuingCityId = ParseNullableInt(rocketLine, 7),
                    Ss_NumberOfShareholders = ParseInt(rocketLine, 8),
                    St_SharePercentageThreshold = ParseInt(rocketLine, 9),
                    Lc_LawyersProtecting = ParseNullableInt(rocketLine, 10),
                    Sc_NumberOfControlingShareholders = ParseNullableInt(rocketLine, 11),
                    P_LegalProtectionLevel = ParseInt(rocketLine, 12),
                    Lm_HighestLawyerMotivation = ParseInt(rocketLine, 13)
                });
            }

            return new RocketInfoResponse
            {
                NumberOfLaunchSites = numberOfRockets,
                RocketInfos = rockets
            };
        }
    }
}
