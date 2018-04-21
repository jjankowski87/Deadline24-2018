using System.Collections.Generic;
using Deadline24.Core;

namespace Deadline24.ConsoleApp.RocketScience.Responses
{
    public class RoadStatusResponse : Response
    {
        public int RoadCount { get; set; }

        public IList<RoadStatusEntity> RoadStatuses { get; set; }
    }

    public class RoadStatusEntity
    {
        public int FromCityId { get; set; }

        public int ToCityId { get; set; }

        public double TravelBonus { get; set; }

        public double TotalCost { get; set; }
    }
}
