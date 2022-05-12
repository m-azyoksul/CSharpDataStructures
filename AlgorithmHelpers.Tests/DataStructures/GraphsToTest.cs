using System.Collections.Generic;
using AlgorithmHelpers.DataStructures;

namespace AlgorithmHelpers.Tests.DataStructures;

public class GraphsToTest
{
    public static DirectedGraph<string> Directed5By5Matrix()
    {
        var edges = new List<(int, int)>();

        // Add horizontal edges
        for (int i = 0; i <= 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                var edge = (i * 4 + j, i * 4 + j + 1);
                edges.Add(edge);
            }
        }

        // Add vertical edges
        for (int j = 0; j <= 3; j++)
        {
            for (int i = 0; i < 3; i++)
            {
                var edge = (i * 4 + j, i * 4 + j + 4);
                edges.Add(edge);
            }
        }

        return new DirectedGraph<string>(edges);
    }

    public static DirectedGraph<string> DirectedBalancedTree()
    {
        var edges = new List<(int, int)>();

        for (int i = 0; i <= 5; i++)
        {
            edges.Add((i, 2 * i + 1));
            edges.Add((i, 2 * i + 2));
        }

        return new DirectedGraph<string>(edges);
    }

    public static DirectedGraph<string> DirectedCollar()
    {
        var edges = new List<(int, int)>
        {
            (0, 1),
            (0, 2),
            (0, 3),
            (1, 3),
            (1, 4),
            (1, 6),
            (2, 5),
            (3, 6),
            (5, 0),
            (5, 2),
            (5, 3),
            (6, 1),
            (6, 4),
        };

        return new DirectedGraph<string>(edges);
    }

    public static UndirectedGraph<string> Undirected5By5Matrix()
    {
        var edges = new List<(int, int)>();

        // Add horizontal edges
        for (int i = 0; i <= 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                var edge = (i * 4 + j, i * 4 + j + 1);
                edges.Add(edge);
            }
        }

        // Add vertical edges
        for (int j = 0; j <= 3; j++)
        {
            for (int i = 0; i < 3; i++)
            {
                var edge = (i * 4 + j, i * 4 + j + 4);
                edges.Add(edge);
            }
        }

        return new UndirectedGraph<string>(edges);
    }

    public static UndirectedGraph<string> UndirectedBalancedTree()
    {
        var edges = new List<(int, int)>();

        for (int i = 0; i <= 5; i++)
        {
            edges.Add((i, 2 * i + 1));
            edges.Add((i, 2 * i + 2));
        }

        return new UndirectedGraph<string>(edges);
    }

    public static UndirectedGraph<string> UndirectedCollar()
    {
        var edges = new List<(int, int)>
        {
            (0, 1),
            (0, 2),
            (0, 3),
            (1, 3),
            (1, 4),
            (1, 6),
            (2, 5),
            (3, 6),
            (5, 0),
            (5, 2),
            (5, 3),
            (6, 1),
            (6, 4),
        };

        return new UndirectedGraph<string>(edges);
    }
}