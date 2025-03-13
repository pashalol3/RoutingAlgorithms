using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingAlgorithms.Models
{
    internal class PathFindingResult
    {
        public static bool IsEmpty(PathFindingResult pathFindingResult) =>
            pathFindingResult.Path.Count == 0 ||
            pathFindingResult.TotalCost == float.PositiveInfinity ||
            pathFindingResult.TotalTime == float.PositiveInfinity;
        public static PathFindingResult Empty => new(Enumerable.Empty<Node>(), float.PositiveInfinity, float.PositiveInfinity);
        public List<Node> Path { get; set; }
        public float TotalCost { get; set; }
        public float TotalTime { get; set; }

        public PathFindingResult(IEnumerable<Node> path, float totalCost, float totalTime)
        {
            Path = path.ToList();
            TotalCost = totalCost;
            TotalTime = totalTime;
        }
    }
}
