using System;
using System.Collections.Generic;

namespace DijkstraAlgorithmExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var nodeA = new Node("A");
            var nodeB = new Node("B");
            var nodeC = new Node("C");
            var nodeD = new Node("D");
            var nodeE = new Node("E");

            var edges = new List<Edge>
            {
                new Edge(nodeA, nodeB, 4),
                new Edge(nodeA, nodeC, 2),
                new Edge(nodeB, nodeC, 5),
                new Edge(nodeB, nodeD, 10),
                new Edge(nodeC, nodeD, 3),
                new Edge(nodeD, nodeE, 7),
                new Edge(nodeE, nodeA, 1)
            };

            // Dijkstra algoritmasını uygula
            var result = Dijkstra(nodeA, nodeE, edges);

            if (result != null)
            {
                Console.WriteLine("En kısa yol:");
                foreach (var node in result)
                {
                    Console.Write(node.Name + " ");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Yol bulunamadı.");
            }
        }

        static List<Node> Dijkstra(Node start, Node end, List<Edge> edges)
        {
            var queue = new PriorityQueue<Node>();
            var distance = new Dictionary<Node, int>();
            var previous = new Dictionary<Node, Node>();

            foreach (var item in edges)
            {
                distance[item.From] = int.MaxValue;
                previous[item.From] = null;
                queue.Enqueue(item.From, int.MaxValue);
            }

            distance[start] = 0;
            queue.Enqueue(start, 0);

            while (!queue.IsEmpty())
            {
                var current = queue.Dequeue();

                if (current == end)
                    break;

                foreach (var edge in edges)
                {
                    if (edge.From == current)
                    {
                        var newDist = distance[current] + edge.Weight;

                        if (newDist < distance[edge.To])
                        {
                            distance[edge.To] = newDist;
                            previous[edge.To] = current;
                            queue.Enqueue(edge.To, newDist);
                        }
                    }
                }
            }

            if (!previous.ContainsKey(end))
                return null;

            var path = new List<Node>();
            var node = end;

            while (node != null)
            {
                path.Insert(0, node);
                node = previous[node];
            }

            return path;
        }
    }

    class Node : IComparable<Node>
    {
        public string Name { get; }
        public int CompareTo(Node other) => 0; // Not used in this example

        public Node(string name)
        {
            Name = name;
        }
    }

    class Edge
    {
        public Node From { get; }
        public Node To { get; }
        public int Weight { get; }

        public Edge(Node from, Node to, int weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }
    }

    class PriorityQueue<T> where T : IComparable<T>
    {
        private List<Tuple<T, int>> elements = new List<Tuple<T, int>>();

        public int Count => elements.Count;

        public bool IsEmpty() => elements.Count == 0;

        public void Enqueue(T item, int priority)
        {
            elements.Add(new Tuple<T, int>(item, priority));
            int i = elements.Count - 1;

            while (i > 0)
            {
                int parent = (i - 1) / 2;

                if (elements[parent].Item2 <= elements[i].Item2)
                    break;

                var temp = elements[i];
                elements[i] = elements[parent];
                elements[parent] = temp;

                i = parent;
            }
        }

        public T Dequeue()
        {
            var result = elements[0];
            elements[0] = elements[elements.Count - 1];
            elements.RemoveAt(elements.Count - 1);

            int i = 0;

            while (true)
            {
                int left = 2 * i + 1;
                int right = 2 * i + 2;
                int smallest = i;

                if (left < elements.Count && elements[left].Item2 < elements[smallest].Item2)
                    smallest = left;

                if (right < elements.Count && elements[right].Item2 < elements[smallest].Item2)
                    smallest = right;

                if (smallest == i)
                    break;

                var temp = elements[i];
                elements[i] = elements[smallest];
                elements[smallest] = temp;

                i = smallest;
            }

            return result.Item1;
        }
    }
}
