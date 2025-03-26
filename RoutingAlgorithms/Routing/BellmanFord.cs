using RoutingAlgorithms.Models;

namespace RoutingAlgorithms.Routing
{
    internal static class BellmanFord
    {
        public static PathFindingResult FindRoute(IList<Node> nodes, Node source, Node destination)
        {
            var totalCost = new Dictionary<Node, float>();
            var totalTime = new Dictionary<Node, float>();
            var predecessors = new Dictionary<Node, Node>();

            // Инициализация
            foreach (var node in nodes)
            {
                totalCost[node] = float.PositiveInfinity;
                predecessors[node] = null;
            }
            totalCost[source] = 0;
            totalTime[source] = 0;

            // Релаксация
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                foreach (var node in nodes)
                {
                    foreach (var neighbor in node.Neighbors)
                    {
                        var edgeCost = neighbor.Value.BandWidth; // Цена перехода
                        var edgeTime = neighbor.Value.Delay; // Время перехода
                        if (totalCost[node] + edgeCost < totalCost[neighbor.Key]) //Учитываем только цену перехода
                        {
                            totalCost[neighbor.Key] = totalCost[node] + edgeCost;
                            totalTime[neighbor.Key] = totalTime[node] + edgeTime;
                            predecessors[neighbor.Key] = node;
                        }
                    }
                }
            }

            // Проверка на наличие отрицательных циклов
            foreach (var node in nodes)
            {
                foreach (var neighbor in node.Neighbors)
                {
                    var edgeWeight = neighbor.Value.BandWidth;
                    if (totalCost[node] + edgeWeight < totalCost[neighbor.Key])
                    {
                        throw new InvalidOperationException("Граф содержит отрицательный цикл.");
                    }
                }
            }

            // Восстановление пути
            var path = new List<Node>();
            Node currentNode = destination;

            while (currentNode != null)
            {
                path.Add(currentNode);
                currentNode = predecessors[currentNode];
            }

            path.Reverse();

            // Проверка, что путь начинается с начального узла
            if (path.Count == 0 || path[0] != source)
            {
                return PathFindingResult.Empty; // Путь не найден
            }

            return new PathFindingResult(path, totalCost[destination], totalTime[destination], RoutingMethod.BellmanFord); // Время можно рассчитать аналогично, если нужно
        }

    }
}