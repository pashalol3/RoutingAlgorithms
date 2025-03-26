using RoutingAlgorithms.Helpers;
using SkiaSharp;

namespace RoutingAlgorithms.Models
{
    /// <summary>
    /// Модель узла графа
    /// </summary>
    public class Node : IEquatable<Node>
    {
        public NodeStatus Status { get; set; } = NodeStatus.Default;
        public SKColor NodeColor
        {
            get => _nodeColors[Status];
        }
        public string NodeName { get; init; }
        public SKPoint PositionN { get; init; }
        public Dictionary<Node, MetricF> Neighbors { get; set; } = [];

        private readonly Dictionary<NodeStatus, SKColor> _nodeColors = new()
        {
            {NodeStatus.Start, SKColors.Green},
            {NodeStatus.Destination, SKColors.Blue},
            {NodeStatus.Hovered, SKColors.Red},
            {NodeStatus.Default, new(235, 150, 50) }
        };

        public Node(string? letter = null, SKPoint? pos = null)
        {
            NodeName = string.IsNullOrEmpty(letter) ? Counter.Next.ToString() : letter;
            PositionN = pos is null ? new(StaticRandom.Float, StaticRandom.Float) : pos.Value;
        }

        public override string ToString()
        {
            return $"{NodeName}";
        }
        public override bool Equals(object? obj)
        {
            if (obj is Node other)
            {
                return this.Equals(other);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(NodeName);
        }

        public bool Equals(Node other)
        {
            return PositionN.Equals(other.PositionN) &&NodeName == other.NodeName;
        }
    }
}
