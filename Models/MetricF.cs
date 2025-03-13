using RoutingAlgorithms.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RoutingAlgorithms.Models
{
    /// <summary>
    /// Модель метрики перехаода между узлами
    /// </summary>
    internal readonly struct MetricF
    {
        public static MetricF One => new(1f, 1f);
        public static MetricF Zero => new(0f, 0f);
        public static MetricF Random => new(StaticRandom.FloatN, StaticRandom.FloatN);
        public MetricF(float time, float cost)
        {
            Time = time;
            Cost = cost;
        }
        public float Time { get; init; }

        public float Cost { get; init; }

        public override readonly string ToString()
        {
            return $"[{Cost:F2}:{Time:F2}]";
        }

        public override readonly bool Equals(object obj)
        {
            if (obj is MetricF other)
            {
                return Time == other.Time && Cost == other.Cost;
            }
            return false;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(Time,Cost);
        }
    }
}
