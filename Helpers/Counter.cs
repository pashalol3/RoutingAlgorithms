using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingAlgorithms.Helpers
{
    class Counter
    {
        private static int _iter = 0;
        public static int Next
        {
            get => _iter++;
        }
    }
}
