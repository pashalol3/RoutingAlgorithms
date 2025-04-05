using RoutingAlgorithms.Models;


namespace RoutingAlgorithms.Routing
{
    public static class SAMCRA
    {
        public static PathFindingResult FindRoute(
            Node source,
            Node destination,
            float routeMaxBandwidth,
            float routeMaxDelay
        )
        {
            // Приоритетная очередь для хранения состояний путей
            var queue = new PriorityQueue<PathState, float>();

            // Словарь для хранения недоминируемых состояний каждого узла
            var nodeStates = new Dictionary<Node, List<PathState>>();

            // Инициализация начального состояния
            var initialState = new PathState(
                path: [source],
                totalCost: 0f,
                totalTime: 0f
            );

            queue.Enqueue(
                initialState,
                CalculatePriority(initialState, routeMaxBandwidth, routeMaxDelay)
            );
            nodeStates[source] = [initialState];

            while (queue.Count > 0)
            {
                var currentState = queue.Dequeue();
                var currentNode = currentState.Path.Last();

                // Проверка достижения цели
                if (currentNode.Equals(destination))
                {
                    return new PathFindingResult(
                        currentState.Path,
                        currentState.TotalBandwidth,
                        currentState.TotalDelay,
                        RoutingMethod.SAMCRA
                    );
                }

                // Перебор всех соседей текущего узла
                foreach (var neighborEdge in currentNode.Neighbors)
                {
                    var neighbor = neighborEdge.Key;
                    var edgeMetric = neighborEdge.Value;

                    var newCost = currentState.TotalBandwidth + edgeMetric.BandWidth;
                    var newTime = currentState.TotalDelay + edgeMetric.Delay;

                    // Проверка ограничений
                    if (newCost > routeMaxBandwidth || newTime > routeMaxDelay)
                        continue;

                    // Создание нового пути
                    var newPath = new List<Node>(currentState.Path) { neighbor };
                    var newState = new PathState(newPath, newCost, newTime);

                    // Проверка на доминирование
                    if (IsDominated(newState, nodeStates, neighbor, routeMaxBandwidth, routeMaxDelay))
                        continue;

                    // Добавление нового состояния
                    if (!nodeStates.TryGetValue(neighbor, out var states))
                    {
                        states = [];
                        nodeStates[neighbor] = states;
                    }
                    states.Add(newState);

                    // Добавление в очередь с приоритетом
                    queue.Enqueue(
                        newState,
                        CalculatePriority(newState, routeMaxBandwidth, routeMaxDelay)
                    );
                }
            }

            // Если путь не найден
            return PathFindingResult.Empty;
        }

        private static float CalculatePriority(PathState state, float maxBandwidth, float maxTime)
        {
            // Приоритет - максимальная нормализованная метрика
            return MathF.Max(state.TotalBandwidth / maxBandwidth , state.TotalDelay / maxTime);
        }

        private static bool IsDominated(
            PathState newState,
            Dictionary<Node, List<PathState>> nodeStates,
            Node targetNode,
            float maxBandwidth,
            float maxDelay
        )
        {
            if (!nodeStates.TryGetValue(targetNode, out var existingStates))
                return false;


            foreach (var existingState in existingStates)
            {
                // Проверка полного доминирования
                if (existingState.TotalBandwidth <= newState.TotalBandwidth &&
                    existingState.TotalDelay <= newState.TotalDelay)
                    return true;

                // Проверка частичного доминирования через приоритет
                var existingPriority = CalculatePriority(existingState, maxBandwidth, maxDelay);
                var newPriority = CalculatePriority(newState, maxBandwidth, maxDelay);

                if (existingPriority <= newPriority &&
                    existingState.TotalBandwidth <= newState.TotalBandwidth &&
                    existingState.TotalDelay <= newState.TotalDelay)
                    return true;

            }
            return false;
        }

        private class PathState(List<Node> path, float totalCost, float totalTime)
        {
            public List<Node> Path { get; init; } = path;
            public float TotalBandwidth { get; init; } = totalCost;
            public float TotalDelay { get; init; } = totalTime;
        }
    }
}
