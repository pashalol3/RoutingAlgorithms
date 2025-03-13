using RoutingAlgorithms.Models;

namespace RoutingAlgorithms
{
    internal static class SAMCRA
    {
        public static PathFindingResult FindRoute(Node source, Node destination, float routeMaxCost, float routeMaxTime)
        {
            // Приоритетная очередь для хранения путей
            var queue = new PriorityQueue<List<Node>, float>();
            var visitedNodes = new HashSet<Node>();

            // Начальный путь: только source
            var initialPath = new List<Node> { source };
            queue.Enqueue(initialPath, 0);

            while (queue.Count > 0)
            {
                var currentPath = queue.Dequeue();
                var currentNode = currentPath.Last();

                // Если достигли целевого узла, возвращаем результат
                if (currentNode == destination)
                {
                    var totalCost = CalculateTotalCost(currentPath);
                    var totalTime = CalculateTotalTime(currentPath);
                    return new PathFindingResult(currentPath, totalCost, totalTime);
                }

                // Если узел уже посещен, пропускаем его
                if (visitedNodes.Contains(currentNode))
                {
                    continue;
                }

                visitedNodes.Add(currentNode);

                // Перебираем всех соседей текущего узла
                foreach (var edge in currentNode.Neighbors)
                {
                    var neighbor = edge.Key;
                    var timeToNextNode = edge.Value.Time;
                    var nextNodeTransitionCost = edge.Value.Cost;

                    // Проверяем, не превышают ли новые значения ограничения
                    var newTime = CalculateTotalTime(currentPath) + timeToNextNode;
                    var newCost = CalculateTotalCost(currentPath) + nextNodeTransitionCost;

                    if (newTime <= routeMaxTime && newCost <= routeMaxCost)
                    {
                        // Создаем новый путь
                        var newPath = new List<Node>(currentPath) { neighbor };

                        /* Добавляем новый путь в очередь с приоритетом по стоимости и времени
                         * Приоритет нормализуется:
                         *  priority = cost / maxCost + time / maxTime
                         * Возможно, можно добавить веса:
                         *  priority = alphaCost * (cost / maxCost) + alphaTime * (time / maxTime)
                         */
                        var priority = nextNodeTransitionCost / routeMaxCost + timeToNextNode / routeMaxTime;
                        queue.Enqueue(newPath, priority);
                    }
                }
            }

            // Если путь не найден, возвращаем пустой результат
            return PathFindingResult.Empty;
        }
        private static float CalculateTotalCost(List<Node> path)
        {
            float totalCost = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                totalCost += path[i].Neighbors[path[i + 1]].Cost;
            }
            return totalCost;
        }

        private static float CalculateTotalTime(List<Node> path)
        {
            float totalTime = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                totalTime += path[i].Neighbors[path[i + 1]].Time;
            }
            return totalTime;
        }
    }
}
