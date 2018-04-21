using System;
using System.Collections.Generic;
using System.Linq;
using Deadline24.ConsoleApp.RocketScience.Responses;
using Deadline24.Core;

namespace Deadline24.ConsoleApp.RocketScience.Commands
{
    public class DescribeWorldCommand : Command<DescribeWorldResponse>
    {
        public DescribeWorldCommand(Client client) : base(client)
        {
        }

        public override DescribeWorldResponse SendCommand()
        {
            Client.SendCommand("DESCRIBE_WORLD");

            var firstLine = Client.ReadLine().Split(' ');
            VerifyResponseLength(firstLine, 16);

            var secondLine = Client.ReadLine().Split(' ');
            VerifyResponseLength(secondLine, 1);

            var mh = ParseInt(secondLine, 0);

            var thirdLine = Client.ReadLine().Split(new [] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            VerifyResponseLength(thirdLine, mh);

            return new DescribeWorldResponse
            {
                M_NumberOfCities = ParseInt(firstLine, 0),
                Sr_AmountToDepositBeforeLaunch = ParseInt(firstLine, 1),
                Sf_AmountForTaxes = ParseInt(firstLine, 2),
                Sue_CarUpgradeCost = ParseInt(firstLine, 3),
                Sut_CarTankUpgradeCost = ParseInt(firstLine, 4),
                Nmax_MaximumMoneyThatCarCanCarry = ParseInt(firstLine, 5),
                Umax_MaximumMoneyThatCarCanCarryWithUpgradedTrunk = ParseInt(firstLine, 6),
                Sg_FuelCost = ParseInt(firstLine, 7),
                R_BaseRoadRevenue = ParseDouble(firstLine, 8),
                Q_ProfitMultiplier= ParseInt(firstLine, 9),
                Slmin_MinLawyerFee = ParseInt(firstLine, 10),
                Slmax_MaxLawyerFee = ParseInt(firstLine, 11),
                T_TurnDuration = ParseInt(firstLine, 12),
                L_MaxNumberOfCommands = ParseInt(firstLine, 13),
                K_ScalingCoefficient = ParseDouble(firstLine, 14),
                Name = firstLine[15],
                Mh_HomeBases = new List<int>(thirdLine.Select((m, i) => ParseInt(thirdLine, i)))
            };
        }
    }
}
