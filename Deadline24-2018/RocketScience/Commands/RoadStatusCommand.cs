using System.Collections.Generic;
using Deadline24.ConsoleApp.RocketScience.Responses;
using Deadline24.Core;

namespace Deadline24.ConsoleApp.RocketScience.Commands
{
    public class RoadStatusCommand : Command<RoadStatusResponse>
    {
        public RoadStatusCommand(Client client) : base(client)
        {
        }

        public override RoadStatusResponse SendCommand()
        {
            Client.SendCommand("ROADS_STATUS");

            var firstLine = Client.ReadLine().Split(' ');
            VerifyResponseLength(firstLine, 1);

            var numberOfRoads = ParseInt(firstLine, 0);

            var roads = new List<RoadStatusEntity>();
            for (var i = 1; i < numberOfRoads + 1; i++)
            {
                var roadLine = Client.ReadLine().Split(' ');

                roads.Add(new RoadStatusEntity
                {
                    FromCityId = ParseInt(roadLine, 0),
                    ToCityId = ParseInt(roadLine, 1),
                    TravelBonus = ParseDouble(roadLine, 2),
                    TotalCost = ParseDouble(roadLine, 3)
                });
            }

            return new RoadStatusResponse
            {
                RoadCount = numberOfRoads,
                RoadStatuses = roads
            };
        }
    }
}
