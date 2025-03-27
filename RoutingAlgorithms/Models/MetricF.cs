using RoutingAlgorithms.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Xml.Linq;

namespace RoutingAlgorithms.Models
{
    /// <summary>
    /// Модель метрики перехаода между узлами
    /// </summary>
    public readonly struct MetricF : IComparable<MetricF>, IEquatable<MetricF>
    {
        public static MetricF One => new(1f, 1f);
        public static MetricF Zero => new(0f, 0f);
        public static MetricF Random => new(StaticRandom.FloatN, StaticRandom.FloatN);
        public MetricF(float delay, float bandwidth)
        {
            Delay = delay;
            BandWidth = bandwidth;
        }
        public float Delay { get; init; }

        public float BandWidth { get; init; }

        public override readonly string ToString()
        {
            return $"[{BandWidth:F1};{Delay:F1}]";
            //return $"[{Delay:F1};{BandWidth:F1}]";

        }

        public override readonly bool Equals(object obj)
        {
            return obj is MetricF other && Delay == other.Delay && BandWidth == other.BandWidth;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(Delay, BandWidth);
        }

        public int CompareTo(MetricF other)
        {
            return (Delay, BandWidth).CompareTo((other.Delay, other.BandWidth));
        }

        public bool Equals(MetricF other)
        {
            return Delay == other.Delay && BandWidth == other.BandWidth;
        }
    }
}
