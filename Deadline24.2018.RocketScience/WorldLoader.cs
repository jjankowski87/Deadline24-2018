using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Deadline24.Core.Algorithms.Graphs;
using Deadline24._2018_.RocketScience.Graphs;

namespace Deadline24._2018_.RocketScience
{
    public class WorldLoader
    {
        public static RocketScienceGraph LoadWorldFromFile(string filePath)
        {
            var fileContent = File.ReadAllLines(filePath);

            var firstLine = fileContent[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var secondLine = fileContent[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var thirdLine = fileContent[2].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var cityCount = int.Parse(firstLine[0]);
            var roadCount = int.Parse(firstLine[1]);
            var baseCount = int.Parse(firstLine[2]);

            var roads = new List<Road>();
            for (var i = 3; i < roadCount + 3; i++)
            {
                var roadLine = fileContent[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                roads.Add(new Road
                {
                    FromCityId = int.Parse(roadLine[0]),
                    ToCityId = int.Parse(roadLine[1]),
                    BaseCost = int.Parse(roadLine[2])
                });
            }

            var baseLocations = secondLine.Select((s, i) => int.Parse(secondLine[i])).ToList();
            var visitingBonuses = thirdLine.Select((s, i) => double.Parse(thirdLine[i])).ToList();

            var cities = visitingBonuses.Select((vb, i) => City.Create(i + 1, vb, baseLocations.Contains(i + 1))).ToList();

            foreach (var road in roads)
            {
                var fromCity = cities.First(c => c.NodeData.Id == road.FromCityId);
                var toCity = cities.First(c => c.NodeData.Id == road.ToCityId);

                fromCity.AddEdge(Graphs.Road.Create(fromCity, toCity, road.BaseCost));
                toCity.AddEdge(Graphs.Road.Create(toCity, fromCity, road.BaseCost));
            }

            return new RocketScienceGraph(cities);
        }

        private struct Road
        {
            public int FromCityId { get; set; }

            public int ToCityId { get; set; }

            public int BaseCost { get; set; }
        }
    }
}
