using Deadline24.Core.Algorithms.Graphs;

namespace Deadline24._2018_.RocketScience.Graphs
{
    public class City
    {
        public City(int id, double visitingBonus, bool isBaseCity)
        {
            Id = id;
            VisitingBonus = visitingBonus;
            IsBaseCity = isBaseCity;
        }

        public int Id { get; }

        public double VisitingBonus { get; }

        public bool IsBaseCity { get; }

        public static Node<City, Road> Create(int id, double visitingBonus, bool isBaseCity)
        {
            return new Node<City, Road>(new City(id, visitingBonus, isBaseCity));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((City) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        protected bool Equals(City other)
        {
            return Id == other.Id;
        }
    }
}