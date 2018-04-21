using System.Collections.Generic;
using Deadline24.Core;

namespace Deadline24.ConsoleApp.RocketScience.Responses
{
    public class DescribeWorldResponse : Response
    {
        public int M_NumberOfCities { get; set; }

        public int Sr_AmountToDepositBeforeLaunch { get; set; }

        public int Sf_AmountForTaxes { get; set; }

        public int Sue_CarUpgradeCost { get; set; }

        public int Sut_CarTankUpgradeCost { get; set; }

        public int Nmax_MaximumMoneyThatCarCanCarry { get; set; }

        public int Umax_MaximumMoneyThatCarCanCarryWithUpgradedTrunk { get; set; }

        public int Sg_FuelCost { get; set; }

        public double R_BaseRoadRevenue { get; set; }

        public int Q_ProfitMultiplier { get; set; }

        public int Slmin_MinLawyerFee { get; set; }

        public int Slmax_MaxLawyerFee { get; set; }

        public int T_TurnDuration { get; set; }

        public int L_MaxNumberOfCommands { get; set; }

        public double K_ScalingCoefficient { get; set; }

        public string Name { get; set; }

        public int Mh_NumberOfHomeBases { get; set; }

        public List<int> Mh_HomeBases { get; set; }
    }
}
