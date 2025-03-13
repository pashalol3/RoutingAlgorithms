using RoutingAlgorithms;
using RoutingAlgorithms.Helpers;
using RoutingAlgorithms.Models;
using SkiaSharp;

internal class Program
{
    private static void Main(string[] args)
    {
        var nodes = GraphGenerator.RandomTree(numNodes:6, connectionProbability: 0.3f);
        //var nodes = GraphGenerator.TestGraph();
        var source = nodes.First();
        var dest = nodes.Last();


        var result = SAMCRA.FindRoute(source: source, 
                                      destination: dest, 
                                      routeMaxCost:2f, 
                                      routeMaxTime:2f);
        Render.Graph(nodes, source, dest, result);
        if (!PathFindingResult.IsEmpty(result))
        {
            Console.WriteLine("Найденный путь:");
            foreach (var node in result.Path)
            {
                Console.Write($"{node.Id} ");
            }
            Console.WriteLine();
            Console.WriteLine($"Total cost: {result.TotalCost}");
            Console.WriteLine($"Total time: {result.TotalTime}");
        }
        else
        {
            Console.WriteLine("Путь не найден.");
        }

    }

    
}