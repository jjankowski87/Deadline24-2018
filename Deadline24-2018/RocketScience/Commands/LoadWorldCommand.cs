using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Deadline24.ConsoleApp.RocketScience.Responses;
using Deadline24.Core;

namespace Deadline24.ConsoleApp.RocketScience.Commands
{
    public class LoadWorldCommand : Command<WorldResponse>
    {
        private readonly string _worldName;

        public LoadWorldCommand(Client client, string worldName) : base(client)
        {
            _worldName = worldName;
        }

        public override WorldResponse SendCommand()
        {
            var fileContent = File.ReadAllLines($"rocket-maps\\{_worldName}");

            var firstLine = fileContent[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            VerifyResponseLength(firstLine, 3);

            var cityCount = ParseInt(firstLine, 0);
            var roadCount = ParseInt(firstLine, 1);
            var baseCount = ParseInt(firstLine, 2);

            var secondLine = fileContent[1].Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            VerifyResponseLength(secondLine, baseCount);

            var thirdLine = fileContent[2].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            VerifyResponseLength(thirdLine, cityCount);

            var roads = new List<RoadEntity>();
            for (var i = 3; i < roadCount + 3; i++)
            {
                var roadLine = fileContent[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                VerifyResponseLength(roadLine, 3);

                roads.Add(new RoadEntity
                {
                    FromCityId = ParseInt(roadLine, 0),
                    ToCityId = ParseInt(roadLine, 1),
                    BaseCost = ParseInt(roadLine, 2)
                });
            }

            return new WorldResponse
            {
                NumberOfCities = cityCount,
                NumberOfRoads = roadCount,
                NumberOfHomeBases = baseCount,
                BaseLocations = secondLine.Select((s, i) => ParseInt(secondLine, i)).ToList(),
                VisitingBonuses = thirdLine.Select((s, i) => ParseDouble(thirdLine, i)).ToList(),
                Roads = roads
            };
        }
    }
}
