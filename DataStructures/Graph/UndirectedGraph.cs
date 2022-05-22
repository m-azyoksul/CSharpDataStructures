using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Graph;

public partial class UndirectedGraph<TData> : Graph<TData>
{
    #region Constructors

    public UndirectedGraph()
    {
    }

    public UndirectedGraph(List<(int, int)> edges) : base(edges)
    {
        // Find all vertices
        foreach (var edge in edges)
        {
            if (!Vertices.ContainsKey(edge.Item1))
                Vertices.Add(edge.Item1, Vertex<TData>.Empty());

            if (!Vertices.ContainsKey(edge.Item2))
                Vertices.Add(edge.Item2, Vertex<TData>.Empty());

            Vertices[edge.Item1].Connections.Add(edge.Item2);
            Vertices[edge.Item2].Connections.Add(edge.Item1);
        }
    }

    public UndirectedGraph(Dictionary<int, Vertex<TData>> vertices) : base(vertices)
    {
        // TODO
        // Check if every vertex is the neighbour its neighbours
        // This graph also allows for self-loops and multiple edges between two vertices
        foreach (var vertex in vertices)
        {
            foreach (var connection in vertex.Value.Connections)
            {
                if (!vertices[connection.To].Connections.Contains(vertex.Key))
                    throw new ArgumentException("Graph is not undirected");
            }
        }
    }

    #endregion

    /// <summary>
    /// Time complexity: O(V)
    /// Space complexity: O(1)
    /// </summary>
    public override bool IsEulerian()
    {
        return Vertices.All(vertex => vertex.Value.Connections.Count % 2 == 0);
    }

    /// <summary>
    /// Search from a vertex.
    ///
    /// Time complexity: O(V + E)
    /// Space complexity: O(V)
    /// </summary>
    /// <returns></returns>
    public override bool IsConnected()
    {
        if (VertexCount() <= 1)
            return true;

        var firstVertex = Vertices.First().Key;

        var visited = new HashSet<int> {firstVertex};
        var queue = new Queue<int> {firstVertex};

        while (queue.Count > 0)
        {
            var v = queue.Dequeue();

            // Enqueue neighbors
            foreach (var neighbor in UnaccountedConnections(v, visited))
            {
                queue.Enqueue(neighbor.To);
                visited.Add(neighbor.To);
            }
        }

        return visited.Count == Vertices.Count;
    }

    /// <summary>
    /// Time complexity: O(V + E)
    /// Space complexity: O(V)
    /// </summary>
    public override bool IsTree()
    {
        if (VertexCount() <= 1)
            return true;

        var firstVertex = Vertices.First().Key;

        var predecessor = new Dictionary<int, int> {{firstVertex, -1}};
        var queue = new Queue<int> {firstVertex};

        while (queue.Count > 0)
        {
            var v = queue.Dequeue();

            // Enqueue neighbors
            foreach (var neighbor in AllConnections(v))
            {
                // If the neighbor is visited before and is not the predecessor, the graph is not a tree
                if (predecessor.ContainsKey(neighbor.To))
                {
                    if (predecessor[v] != neighbor.To)
                        return false;
                    continue;
                }

                queue.Enqueue(neighbor.To);
                predecessor[neighbor.To] = v;
            }
        }

        return VertexCount() == predecessor.Count;
    }
}