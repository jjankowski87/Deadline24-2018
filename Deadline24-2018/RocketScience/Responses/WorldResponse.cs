using System.Collections.Generic;
using System.Security.Permissions;
using Deadline24.Core;

namespace Deadline24.ConsoleApp.RocketScience.Responses
{
    public class WorldResponse : Response
    {
        public int NumberOfCities { get; set; }

        public int NumberOfRoads { get; set; }

        public int NumberOfHomeBases { get; set; }

        public List<int> BaseLocations { get; set; }

        public List<double> VisitingBonuses { get; set; }

        public List<RoadEntity> Roads { get; set; }
    }

    public class RoadEntity
    {
        public int FromCityId { get; set; }

        public int ToCityId { get; set; }

        public int BaseCost { get; set; }
    }
}
