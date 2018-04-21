namespace Deadline24.ConsoleApp.RocketScience
{
    public class Road
    {
        public Road(City @from, City to, int baseCost)
        {
            From = @from;
            To = to;
            BaseCost = baseCost;
        }

        public City From { get; }

        public City To { get; }

        public int BaseCost { get; }
    }
}