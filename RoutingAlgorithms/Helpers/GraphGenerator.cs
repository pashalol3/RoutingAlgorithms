using RoutingAlgorithms.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
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
        public static List<Node> MainGraph()
        {
            var nodeS = new Node("S", new(0.2f, 0.5f));

            var nodeA = new Node("A", new(0.4f, 0.2f));
            var nodeB = new Node("B", new(0.4f, 0.5f));
            var nodeC = new Node("C", new(0.4f, 0.8f));

            var nodeE = new Node("E", new(0.6f, 0.2f));
            var nodeF = new Node("F", new(0.6f, 0.5f));
            var nodeG = new Node("G", new(0.6f, 0.8f));

            var nodeD = new Node("D", new(0.8f, 0.5f));


            nodeS.Neighbors[nodeA] = new(2f, 1f);
            nodeA.Neighbors[nodeS] = new(2f, 1f);

            nodeS.Neighbors[nodeB] = new(1f, 3f);
            nodeB.Neighbors[nodeS] = new(1f, 3f);

            nodeS.Neighbors[nodeC] = new(8f, 1f);
            nodeC.Neighbors[nodeS] = new(8f, 1f);

            nodeA.Neighbors[nodeE] = new(6f, 10f);
            nodeE.Neighbors[nodeA] = new(6f, 10f);

            nodeA.Neighbors[nodeB] = new(3f, 1f);
            nodeB.Neighbors[nodeA] = new(3f, 1f);

            nodeB.Neighbors[nodeF] = new(4f, 8f);
            nodeF.Neighbors[nodeB] = new(4f, 8f);

            nodeB.Neighbors[nodeG] = new(12f, 5f);
            nodeG.Neighbors[nodeB] = new(12f, 5f);

            nodeB.Neighbors[nodeC] = new(2f, 1f);
            nodeC.Neighbors[nodeB] = new(2f, 1f);

            nodeC.Neighbors[nodeG] = new(2f, 3f);
            nodeG.Neighbors[nodeC] = new(2f, 3f);

            nodeE.Neighbors[nodeF] = new(1f, 3f);
            nodeF.Neighbors[nodeE] = new(1f, 3f);

            nodeF.Neighbors[nodeD] = new(2f, 5f);
            nodeD.Neighbors[nodeF] = new(2f, 5f);

            nodeE.Neighbors[nodeD] = new(4f, 5f);
            nodeD.Neighbors[nodeE] = new(4f, 5f);

            nodeG.Neighbors[nodeD] = new(2f, 2f);
            nodeD.Neighbors[nodeG] = new(2f, 2f);

            return [nodeS, nodeA, nodeB, nodeC, nodeE, nodeF, nodeG, nodeD];
        }

        public static List<Node> Graph4()
        {
            var adjacencyMatrix = new int[,]
            {
                {0,1,1,0},
                {1,0,1,1},
                {1,1,0,1},
                {0,1,1,0},
            };
            return GenerateGraphMatrix(adjacencyMatrix);

        }

        public static List<Node> GenerateGraphMatrix(int[,] matrix)
        {
            var width = matrix.GetLength(0);
            var height = matrix.GetLength(1);
            if (width != height)
                throw new ArgumentOutOfRangeException($"The {nameof(matrix)} should be square");

            var nodeStartName = 'A';
            var nodeNames = Enumerable.Range(0, width)
                                     .Select(s => (char)(nodeStartName + s))
                                     .ToArray();
            List<Node> nodes = [];

            int numPairs = (width + 1) / 2; // Округляем вверх, чтобы учесть нечётные случаи

            for (int i = 0; i < width; i++)
            {
                int pairIndex = i / 2;
                bool isTopNode = (i % 2) == 0;
                float x = (pairIndex + 0.5f) / numPairs;
                float y = isTopNode ? 0.75f : 0.25f;

                // Ограничиваем координаты в пределах [0, 1]
                x = Math.Clamp(x, 0f, 1f);
                y = Math.Clamp(y, 0f, 1f);

                nodes.Add(new Node(
                    nodeNames[i].ToString(),
                    new PointF(x, y)));
            }


            for (int i = 0; i < width; i++)
            {
                for (int j = i + 1; j < width; j++) // Начинаем с j = i + 1
                {
                    if (matrix[i, j] == 1)
                    {
                        // Генерируем метрику ОДИН раз для обоих направлений
                        var metric = MetricF.Random;
                        nodes[i].Neighbors[nodes[j]] = metric;
                        nodes[j].Neighbors[nodes[i]] = metric;
                    }
                }
            }

            return nodes;
        }



        public static List<Node> Graph5()
        {
            var adjacencyMatrix = new int[,]
            {
                {0, 1, 1, 0, 0},
                {1, 0, 1, 0, 1},
                {1, 1, 0, 1, 1},
                {0, 0, 1, 0, 1},
                {0, 1, 1, 1, 0}
            };

            return GenerateGraphMatrix(adjacencyMatrix);
        }
        public static List<Node> Graph6()
        {
            var adjacencyMatrix = new int[,]
            {
                {0, 1, 1, 0, 0, 0},
                {1, 0, 1, 1, 1, 0},
                {1, 1, 0, 0, 1, 0},
                {0, 1, 0, 0, 0, 1},
                {0, 1, 1, 0, 0, 1},
                {0, 0, 0, 1, 1, 0}
            };

            return GenerateGraphMatrix(adjacencyMatrix);
        }
        public static List<Node> Graph7()
        {
            var adjacencyMatrix = new int[,]
            {
                {0, 1, 1, 0, 0, 0, 0},
                {1, 0, 0, 1, 0, 0, 0},
                {1, 0, 0, 1, 1, 0, 0},
                {0, 1, 1, 0, 0, 1, 0},
                {0, 0, 1, 0, 0, 1, 1},
                {0, 0, 0, 1, 1, 0, 1},
                {0, 0, 0, 0, 1, 1, 0}
            };

            return GenerateGraphMatrix(adjacencyMatrix);
        }
        public static List<Node> Graph8()
        {
            var adjacencyMatrix = new int[,]
            {
                {0,1,1,0,0,0,0,0},
                {1,0,1,1,0,0,0,0},
                {1,1,0,0,1,0,0,0},
                {0,1,0,0,0,1,0,0},
                {0,0,1,0,0,1,1,0},
                {0,0,0,1,1,0,1,1},
                {0,0,0,0,1,1,0,1},
                {0,0,0,0,0,1,1,0 }
            };

            return GenerateGraphMatrix(adjacencyMatrix);
        }
        public static List<Node> Graph9()
        {
            var adjacencyMatrix = new int[,]
            {
                {0,1,1,0,0,0,0,0,0},
                {1,0,1,1,0,0,0,0,0},
                {1,1,0,1,1,0,0,0,0},
                {0,1,1,0,1,1,0,0,0},
                {0,0,1,1,0,1,1,0,0},
                {0,0,0,1,1,0,1,1,0},
                {0,0,0,0,1,1,0,1,1},
                {0,0,0,0,0,1,1,0,1},
                {0,0,0,0,0,0,1,1,0}
            };

            return GenerateGraphMatrix(adjacencyMatrix);
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
                var metric = MetricF.RandomN;
                nodes[i].Neighbors[nodes[parentIndex]] = metric;
                nodes[parentIndex].Neighbors[nodes[i]] = metric;
            }

            for (int i = 0; i < numNodes; i++)
            {
                for (int j = i + 1; j < numNodes; j++)
                {
                    if (StaticRandom.FloatN < connectionProbability)
                    {
                        var metric = MetricF.RandomN;
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
                        var metric = MetricF.RandomN;
                        nodes[i].Neighbors[nodes[j]] = metric;
                        nodes[j].Neighbors[nodes[i]] = metric;
                    }
                }
            }
            return nodes;
        }
    }
}
