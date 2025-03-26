using RoutingAlgorithms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingAlgorithms.Helpers
{
    public class GraphGenerator
    {
        /// <summary>
        /// Граф с рисунка
        /// </summary>
        /// <returns></returns>
        public static List<Node> StaticGraph()
        {
            var nodeS = new Node("S", new(0.2f, 0.5f));

            var nodeA = new Node("A", new(0.4f, 0.2f));
            var nodeB = new Node("B", new(0.4f, 0.5f));
            var nodeC = new Node("C", new(0.4f, 0.8f));

            var nodeE = new Node("E", new(0.6f, 0.2f));
            var nodeF = new Node("F", new(0.6f, 0.5f));
            var nodeG = new Node("G", new(0.6f, 0.8f));

            var nodeD = new Node("D", new(0.8f, 0.5f));

            nodeS.Neighbors[nodeA] = new(1f, 2f);
            nodeA.Neighbors[nodeS] = new(1f, 2f);

            nodeS.Neighbors[nodeB] = new(3f, 1f);
            nodeB.Neighbors[nodeS] = new(3f, 1f);

            nodeS.Neighbors[nodeC] = new(1f, 8f);
            nodeC.Neighbors[nodeS] = new(1f, 8f);

            nodeA.Neighbors[nodeE] = new(10f, 6f);
            nodeE.Neighbors[nodeA] = new(10f, 6f);

            nodeA.Neighbors[nodeB] = new(1f, 3f);
            nodeB.Neighbors[nodeA] = new(1f, 3f);

            nodeB.Neighbors[nodeF] = new(8f, 4f);
            nodeF.Neighbors[nodeB] = new(8f, 4f);

            nodeB.Neighbors[nodeG] = new(5f, 12f);
            nodeG.Neighbors[nodeB] = new(5f, 12f);

            nodeB.Neighbors[nodeC] = new(1f, 2f);
            nodeC.Neighbors[nodeB] = new(1f, 2f);

            nodeC.Neighbors[nodeG] = new(3f, 2f);
            nodeG.Neighbors[nodeC] = new(3f, 2f);

            nodeE.Neighbors[nodeF] = new(3f, 1f);
            nodeF.Neighbors[nodeE] = new(3f, 1f);

            nodeF.Neighbors[nodeD] = new(5f, 2f);
            nodeD.Neighbors[nodeF] = new(5f, 2f);

            nodeE.Neighbors[nodeD] = new(5f, 4f);
            nodeD.Neighbors[nodeE] = new(5f, 4f);

            nodeG.Neighbors[nodeD] = new(2f, 2f);
            nodeD.Neighbors[nodeG] = new(2f, 2f);

            return [nodeS, nodeA, nodeB, nodeC, nodeE, nodeF, nodeG, nodeD];

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
