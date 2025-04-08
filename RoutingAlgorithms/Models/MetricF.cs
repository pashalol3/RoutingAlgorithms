using RoutingAlgorithms.Helpers;

namespace RoutingAlgorithms.Models
{
    /// <summary>
    /// Модель метрики перехаода между узлами
    /// </summary>
    public readonly struct MetricF : IComparable<MetricF>, IEquatable<MetricF>
    {
        public static MetricF One => new(1f, 1f);
        public static MetricF Zero => new(0f, 0f);
        public static MetricF RandomN => new(StaticRandom.FloatN, StaticRandom.FloatN);
        public static MetricF Random => new(MathF.Round(StaticRandom.FloatN,1) * 10f, MathF.Round(StaticRandom.FloatN, 1));
        public MetricF(float delay, float bandwidth)
        {
            Delay = delay;
            BandWidth = bandwidth;
        }
        public float Delay { get; init; }

        public float BandWidth { get; init; }

        public override readonly string ToString() => $"[{BandWidth:F1};{Delay:F1}]";

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
