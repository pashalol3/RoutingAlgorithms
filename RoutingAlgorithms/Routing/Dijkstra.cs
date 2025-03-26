using RoutingAlgorithms.Models;

namespace RoutingAlgorithms.Routing
{
    public static class Dijkstra
    {
        public static PathFindingResult FindRoute(Node source, Node destination)
        {
            // Словарь для хранения количества переходов от начального узла
            var shortestPaths = new Dictionary<Node, (int Transitions, MetricF Metrics)>();
            // Словарь для хранения предшественников узлов
            var predecessors = new Dictionary<Node, Node>();
            // Приоритетная очередь для обработки узлов
            var priorityQueue = new PriorityQueue<Node, int>();

            // Инициализация начального узла
            shortestPaths[source] = (0, MetricF.Zero);
            priorityQueue.Enqueue(source, 0);

            while (priorityQueue.Count > 0)
            {
                var currentNode = priorityQueue.Dequeue();

                // Если достигли целевого узла, выходим из цикла
                if (currentNode.Equals(destination))
                {
                    break;
                }

                // Обход соседей текущего узла
                foreach (var neighbor in currentNode.Neighbors)
                {
                    var (neighborNode, metric) = neighbor;

                    // Вычисление нового количества переходов и обновление метрик
                    var newTransitions = shortestPaths[currentNode].Transitions + 1; // Увеличиваем количество переходов на 1
                    var newMetrics = new MetricF(
                        shortestPaths[currentNode].Metrics.Delay + metric.Delay,
                        shortestPaths[currentNode].Metrics.BandWidth + metric.BandWidth
                    );

                    // Если новый путь имеет меньшее количество переходов, обновляем и добавляем в очередь
                    if (!shortestPaths.TryGetValue(neighborNode, out var value) || newTransitions < value.Transitions)
                    {
                        shortestPaths[neighborNode] = (newTransitions, newMetrics);
                        predecessors[neighborNode] = currentNode; // Запоминаем предшественника
                        priorityQueue.Enqueue(neighborNode, newTransitions);
                    }
                }
            }

            // Восстановление пути
            var path = new List<Node>();
            if (shortestPaths.ContainsKey(destination))
            {
                var current = destination;
                while (current != null)
                {
                    path.Add(current);
                    predecessors.TryGetValue(current, out current);
                }
                path.Reverse(); // Обратим порядок, чтобы получить путь от начального до целевого узла
            }

            var totalBandwidth = float.PositiveInfinity;
            var totalDelay = float.PositiveInfinity;
            var totalTransitions = int.MaxValue;

            if (shortestPaths.TryGetValue(destination, out var targetMetrics))
            {
                totalTransitions = targetMetrics.Transitions;
                totalBandwidth = targetMetrics.Metrics.BandWidth;
                totalDelay = targetMetrics.Metrics.Delay;
            }

            return new PathFindingResult(path, totalBandwidth, totalDelay, RoutingMethod.Dijkstra);
        }
    }
}
