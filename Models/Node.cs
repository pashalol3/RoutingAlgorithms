using RoutingAlgorithms.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingAlgorithms.Models
{
    /// <summary>
    /// Модель узла графа
    /// </summary>
    internal class Node
    {
        
        public int Id { get; set; }
        public float X { get; init; }
        public float Y { get; init; }
        public Dictionary<Node, MetricF> Neighbors { get; set; } = [];

        public Node()
        {
            Id = Counter.Next;
            X = StaticRandom.Float;
            Y = StaticRandom.Float;
        }

        public override string ToString()
        {
            return $"{Id}";
        }
        public override bool Equals(object? obj)
        {
            if (obj is Node other)
            {
                return Id == other.Id;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
