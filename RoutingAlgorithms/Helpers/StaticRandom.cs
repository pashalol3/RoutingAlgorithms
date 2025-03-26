using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingAlgorithms.Helpers
{
    public class StaticRandom
    {
        private static readonly Random _gloablRandom = new();

        public static float Float 
        {
            get => _gloablRandom.NextSingle() * 2f - 1f;        
        }
        public static float FloatN
        {
            get => _gloablRandom.NextSingle();        
        }
        public static double DoubleN
        {
            get => _gloablRandom.NextDouble();
        }
        public static int Integer
        {
            get => _gloablRandom.Next(int.MinValue, int.MaxValue);
        }
        public static int MaxInteger(int max) => _gloablRandom.Next(0, max);
    }
}
