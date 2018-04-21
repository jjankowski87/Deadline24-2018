using System.Collections.Generic;
using Deadline24.Core;

namespace Deadline24.ConsoleApp.RocketScience.Responses
{
    public class RocketInfoResponse : Response
    {
        public int NumberOfLaunchSites { get; set; }

        public IList<RocketInfoEntity> RocketInfos { get; set; }
    }

    public class RocketInfoEntity
    {
        public int CityId { get; set; }

        public int Sg_ShareholderGrain { get; set; }

        public int Do_TeamDeposit { get; set; }

        public int Dt_TotalDeposit { get; set; }

        public bool Fd_IsLaunched { get; set; }

        public bool Fc_HasControl { get; set; }

        public int Fl_TurnsTillLawsuit { get; set; }

        public int? Fo_SuingCityId { get; set; }

        public int Ss_NumberOfShareholders { get; set; }

        public int St_SharePercentageThreshold { get; set; }

        public int? Lc_LawyersProtecting { get; set; }

        public int? Sc_NumberOfControlingShareholders { get; set; }

        public int P_LegalProtectionLevel { get; set; }

        public int? Lm_HighestLawyerMotivation { get; set; }
    }
}
