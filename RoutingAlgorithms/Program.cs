using RoutingAlgorithms;
using RoutingAlgorithms.Helpers;
using RoutingAlgorithms.Models;
using RoutingAlgorithms.Routing;

internal class Program
{
    private static void Main(string[] args)
    {
#if false
        if (args.Length != 4) 
        {
            Console.WriteLine("ОШИБКА: Количество параметров должно быть 4. [количество узлов] [вероятность  (0,3) ИМЕННО ЗАПЯТАЯ] [ограничение по цене 2,0 ИМЕННО ЗАПЯТАЯ] [ограничение по времени 2,0 ИМЕННО ЗАПЯТАЯ]");
            return;
        }
        var numNodes = int.Parse(args[0]);
        var probability = float.Parse(args[1],System.Globalization.NumberStyles.Float);
        var maxCost = float.Parse(args[2], System.Globalization.NumberStyles.Float);
        var maxTime = float.Parse(args[3], System.Globalization.NumberStyles.Float);
#endif
        //var nodes = GraphGenerator.RandomTree(numNodes: 9, connectionProbability: 0.4f);
        var nodes = GraphGenerator.StaticGraph();
        var source = nodes.First();
        var dest = nodes.Last();

        PathFindingResult samcraResult = PathFindingResult.Empty;
        PathFindingResult dijkstraResult = PathFindingResult.Empty;
        PathFindingResult bellmanFordResult = PathFindingResult.Empty;
        float maxTime = 0.1f;
        do
        {
            Console.WriteLine($"MaxTime = {maxTime}");
            samcraResult = SAMCRA.FindRoute(source: source, destination: dest, routeMaxBandwidth: 11f, routeMaxDelay: maxTime);

            if(PathFindingResult.IsEmpty(dijkstraResult))
                dijkstraResult = Dijkstra.FindRoute(source: source, destination: dest);

            if (PathFindingResult.IsEmpty(bellmanFordResult))
                bellmanFordResult = BellmanFord.FindRoute(nodes: nodes, source: source, destination: dest);

            maxTime += 0.01f;

        } while (PathFindingResult.IsEmpty(samcraResult));


        Render.Graph(nodes, [samcraResult, dijkstraResult, bellmanFordResult], source, dest);

        PrintResult(samcraResult, nameof(SAMCRA));
        PrintResult(dijkstraResult, nameof(Dijkstra));
        PrintResult(bellmanFordResult, nameof(BellmanFord));


    }

    private static void PrintResult(PathFindingResult result, string name)
    {
        if (!PathFindingResult.IsEmpty(result))
        {
            Console.WriteLine($"{name} Найденный путь:");
            foreach (var node in result.Path)
            {
                Console.Write($"{node.NodeName} ");
            }
            Console.WriteLine();
            Console.WriteLine($"Total cost: {result.TotalDelay}");
            Console.WriteLine($"Total time: {result.TotalBandwidth}");
            Console.WriteLine($"Total sum: {result.TotalBandwidth + result.TotalDelay}");
        }
        else
        {
            Console.WriteLine($"{name} Путь не найден.");
        }
    }

}