using System;

namespace BellmanFordAlgorithmExample
{
    class Program
    {
        static void Main(string[] args)
        {
            int numVertices = 5;
            int numEdges = 8;

            int[,] edges = {
                {0, 1, 4},
                {0, 2, 2},
                {1, 2, 5},
                {1, 3, 10},
                {2, 3, 3},
                {3, 4, 7},
                {4, 0, 1},
                {2, 4, 8}
            };

            int[] distances = BellmanFord(numVertices, numEdges, edges, 0);

            Console.WriteLine("Düğümler arası en kısa mesafeler:");
            for (int i = 0; i < numVertices; ++i)
            {
                Console.WriteLine($"0 -> {i}: {distances[i]}");
            }
        }

        static int[] BellmanFord(int numVertices, int numEdges, int[,] edges, int source)
        {
            int[] distances = new int[numVertices];

            for (int i = 0; i < numVertices; ++i)
            {
                distances[i] = int.MaxValue;
            }

            distances[source] = 0;

            for (int i = 0; i < numVertices - 1; ++i)
            {
                for (int j = 0; j < numEdges; ++j)
                {
                    int u = edges[j, 0];
                    int v = edges[j, 1];
                    int weight = edges[j, 2];

                    if (distances[u] != int.MaxValue && distances[u] + weight < distances[v])
                    {
                        distances[v] = distances[u] + weight;
                    }
                }
            }

            return distances;
        }
    }
}
