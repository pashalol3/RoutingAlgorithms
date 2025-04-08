using RoutingAlgorithms.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingAlgorithms.Models
{
    public class PathFindingResult
    {
        private static readonly float _invalidValue = float.PositiveInfinity;
        public static bool IsEmpty(PathFindingResult pathFindingResult) =>
            pathFindingResult.Path.Count == 0 ||
            pathFindingResult.TotalDelay == _invalidValue ||
            pathFindingResult.TotalBandwidth == _invalidValue ||
            pathFindingResult.RoutingMethod == RoutingMethod.Invalid;
        public static PathFindingResult Empty => new([], _invalidValue, _invalidValue, RoutingMethod.Invalid);
        public List<Node> Path { get; init; }
        public float TotalDelay { get; init; }
        public float TotalBandwidth { get; init; }

        public RoutingMethod RoutingMethod { get; init; }

        public PathFindingResult(IEnumerable<Node> path, float totalBandwidth, float totalDelay, RoutingMethod routingMethod)
        {
            Path = [.. path];
            TotalDelay = totalDelay;
            TotalBandwidth = totalBandwidth;
            RoutingMethod = routingMethod;
        }
        public override string ToString() => string.Join(' ',Path.Select(s => s.NodeName));
        public bool Dominates(PathFindingResult other)
        {
            bool betterOrEqualBandwidth = TotalBandwidth <= other.TotalBandwidth;
            bool betterOrEqualDelay = TotalDelay <= other.TotalDelay;
            bool strictlyBetter = TotalBandwidth < other.TotalBandwidth || TotalDelay < other.TotalDelay;

            return betterOrEqualBandwidth && betterOrEqualDelay && strictlyBetter;
        }
    }
}
