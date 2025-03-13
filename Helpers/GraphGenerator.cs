using RoutingAlgorithms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingAlgorithms.Helpers
{
    internal class GraphGenerator
    {
        /// <summary>
        /// Тестовый граф
        /// </summary>
        /// <returns></returns>
        public static List<Node> TestGraph() 
        {
            var source = new Node();
            var node1 = new Node();
            var node2 = new Node();
            var node3 = new Node();
            var node4 = new Node();
            var dest = new Node();

            source.Neighbors[node1] = MetricF.One;
            node1.Neighbors[source] = MetricF.One;

            source.Neighbors[node2] = new(0.5f, 0.5f);
            node2.Neighbors[node2] = new(0.5f, 0.5f);

            source.Neighbors[node3] = new(1f, 0.25f);
            node3.Neighbors[source] = new(1f, 0.25f);

            source.Neighbors[node4] = new(0.25f, 0.25f);
            node4.Neighbors[source] = new(0.25f, 0.25f);

            node1.Neighbors[dest] = MetricF.One;
            dest.Neighbors[node1] = MetricF.One;

            dest.Neighbors[node2] = new(0.5f, 0.5f);
            node2.Neighbors[dest] = new(0.5f, 0.5f);

            dest.Neighbors[node3] = new(0.25f, 0.25f);
            node3.Neighbors[dest] = new(0.25f, 0.25f);

            dest.Neighbors[node4] = new(0.25f, 0.25f);
            node4.Neighbors[dest] = new(0.25f, 0.25f);

            return [source, node1, node2, node3, node4, dest];

        }
        /// <summary>
        /// Генерирует рандомный граф, каждый узел которого связан (нет висячих узлов)
        /// </summary>
        /// <param name="numNodes"></param>
        /// <param name="connectionProbability"></param>
        /// <returns></returns>
        public static List<Node> RandomTree(int numNodes, float connectionProbability)
        {
            List<Node> nodes = [];

            for (int i = 0; i < numNodes; i++)
            {
                nodes.Add(new Node());
            }

            for (int i = 1; i < numNodes; i++)
            {
                int parentIndex = StaticRandom.MaxInteger(i);
                var metric = MetricF.Random;
                nodes[i].Neighbors[nodes[parentIndex]] = metric;
                nodes[parentIndex].Neighbors[nodes[i]] = metric;
            }

            for (int i = 0; i < numNodes; i++)
            {
                for (int j = i + 1; j < numNodes; j++)
                {
                    if (StaticRandom.FloatN < connectionProbability)
                    {
                        var metric = MetricF.Random;
                        nodes[i].Neighbors[nodes[j]] = metric;
                        nodes[j].Neighbors[nodes[i]] = metric;
                    }
                }
            }

            return nodes;
        }

        /// <summary>
        /// Возвращает граф, узлы которого могут имеять связность равную нулю
        /// </summary>
        /// <param name="numNodes"></param>
        /// <param name="connectionProbability"></param>
        /// <returns></returns>
        public static List<Node> RandomNodes(int numNodes, float connectionProbability)
        {
            List<Node> nodes = [];
            for (int i = 0; i < numNodes; i++)
            {
                nodes.Add(new Node());
            }

            for (int i = 0; i < numNodes; i++)
            {
                for (int j = i + 1; j < numNodes; j++)
                {
                    if (StaticRandom.FloatN < connectionProbability)
                    {
                        var metric = MetricF.Random;
                        nodes[i].Neighbors[nodes[j]] = metric;
                        nodes[j].Neighbors[nodes[i]] = metric;
                    }
                }
            }
            return nodes;
        }
    }
}
