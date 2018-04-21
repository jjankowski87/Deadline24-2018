using Deadline24.Core;

namespace Deadline24.ConsoleApp.RocketScience.Responses
{
    public class TimeToEndResponse : Response
    {
        public int LaunchedRockets { get; set; }

        public int TurnsTillEnd { get; set; }
    }
}
