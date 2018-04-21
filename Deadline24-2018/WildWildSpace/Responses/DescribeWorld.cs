using Deadline24.Core;

namespace Deadline24.ConsoleApp.WildWildSpace.Responses
{
    public class DescribeWorld : Response
    {
        public int Dimensions { get; set; }

        public int BoardSize { get; set; }

        public double ScoreScaling { get; set; }

        public string MapName { get; set; }
    }
}