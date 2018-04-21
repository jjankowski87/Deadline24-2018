using System.Collections.Generic;

namespace Deadline24.ConsoleApp.RocketScience
{
    public class City
    {
        public City(int id, double visitingBonus, bool isBaseCity)
        {
            Id = id;
            VisitingBonus = visitingBonus;
            IsBaseCity = isBaseCity;
            Roads = new List<Road>();
        }

        public int Id { get; }

        public double VisitingBonus { get; }

        public bool IsBaseCity { get; }

        public IList<Road> Roads { get; }
    }
}