using RoutingAlgorithms.Helpers;
using System.Drawing;

namespace RoutingAlgorithms.Models
{
    /// <summary>
    /// Модель узла графа
    /// </summary>
    public class Node : IEquatable<Node>
    {
        public NodeStatus Status { get; set; } = NodeStatus.Default;
        public string NodeName { get; init; }
        public PointF PositionN { get; set; }
        public Dictionary<Node, MetricF> Neighbors { get; set; } = [];

        public Node(string? letter = null, PointF? pos = null)
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
            return NodeName == other.NodeName;
        }
    }
}
