using System;
using System.Collections.Generic;

class PrimKruskal
{
    public static List<(int, int, int)> PrimKruskalAlgorithm(int[,] graph)
    {
        int n = graph.GetLength(0);
        bool[] selected = new bool[n];
        List<(int, int, int)> mstEdges = new List<(int, int, int)>();
        PriorityQueue<(int weight, int u, int v), int> edgeHeap = new PriorityQueue<(int weight, int u, int v), int>();

        // Начинаем с первой вершины
        selected[0] = true;
        for (int j = 1; j < n; j++)
        {
            if (graph[0, j] != 0)
            {
                edgeHeap.Enqueue((graph[0, j], 0, j), graph[0, j]);
            }
        }

        while (mstEdges.Count < n - 1 && edgeHeap.Count > 0)
        {
            var edge = edgeHeap.Dequeue();
            int weight = edge.weight;
            int u = edge.u;
            int v = edge.v;

            if (!selected[v])
            {
                selected[v] = true;
                mstEdges.Add((u, v, weight));

                for (int j = 0; j < n; j++)
                {
                    if (!selected[j] && graph[v, j] != 0)
                    {
                        edgeHeap.Enqueue((graph[v, j], v, j), graph[v, j]);
                    }
                }
            }
        }

        // Добавляем недостающее ребро для завершения цикла
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (graph[i, j] != 0 && !mstEdges.Exists(e => (e.Item1 == i && e.Item2 == j) || (e.Item1 == j && e.Item2 == i)))
                {
                    mstEdges.Add((i, j, graph[i, j]));
                    return mstEdges;
                }
            }
        }

        return mstEdges;
    }

    public static void Main()
    {
        int[,] graph = {
            { 0, 1, 0, 1, 0, 0 }, // Вершина 1 соединена с вершинами 2 и 4
            { 1, 0, 0, 0, 1, 0 }, // Вершина 2 соединена с вершинами 1 и 5
            { 0, 0, 0, 0, 0, 0 }, // Вершина 3 (не соединена)
            { 1, 0, 0, 0, 0, 1 }, // Вершина 4 соединена с вершинами 1 и 6
            { 0, 1, 0, 0, 0, 1 }, // Вершина 5 соединена с вершинами 2 и 6
            { 0, 0, 0, 1, 1, 0 }  // Вершина 6 соединена с вершинами 4 и 5
        };

        List<(int, int, int)> mstEdges = PrimKruskalAlgorithm(graph);
        Console.WriteLine("Ребра, входящие в минимальное остовное дерево:");
        foreach (var edge in mstEdges)
        {
            Console.WriteLine($"Ребро ({edge.Item1 + 1}, {edge.Item2 + 1}) с весом {edge.Item3}");
        }
    }
}
