using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Graph;

public partial class DirectedGraph<TData> : Graph<TData>
{
    #region Constructors

    public DirectedGraph()
    {
    }

    public DirectedGraph(List<(int From, int To)> edges) : base(edges)
    {
        // Find all vertices
        foreach (var edge in edges)
        {
            if (!Vertices.ContainsKey(edge.From))
                Vertices.Add(edge.From, Vertex<TData>.Empty());

            if (!Vertices.ContainsKey(edge.To))
                Vertices.Add(edge.To, Vertex<TData>.Empty());

            Vertices[edge.From].Connections.Add(edge.To);
        }
    }

    public DirectedGraph(Dictionary<int, Vertex<TData>> vertices) : base(vertices)
    {
    }

    #endregion

    /// <summary>
    /// Takes the transpose of the graph. In other words, swaps the direction of all edges in the graph.
    ///
    /// Time Complexity: O(V + E)
    /// Space Complexity: O(V + E)
    /// </summary>
    public DirectedGraph<TData> Transpose()
    {
        // Create a copy of the graph
        var vertexDict = Vertices.ToDictionary(
            vertex => vertex.Key,
            vertex => new Vertex<TData>(vertex.Value.Data)
        );
        var transpose = new DirectedGraph<TData>(vertexDict);

        // Add reversed edges from the copy to the original graph
        foreach (var vertex in Vertices)
        foreach (var connection in vertex.Value.Connections)
            transpose.Vertices[connection.To].Connections.Add(vertex.Key);

        return transpose;
    }

    /// <summary>
    /// Time complexity: O(E)
    /// Space complexity: O(V) and O(E)
    /// </summary>
    public override bool IsEulerian()
    {
        if (VertexCount() <= 1)
            return true;

        var degree = new Dictionary<int, int>();

        foreach (var vertex in Vertices)
        {
            foreach (var connection in vertex.Value.Connections)
            {
                if (degree.ContainsKey(connection.To))
                    degree[connection.To]++;
                else
                    degree.Add(connection.To, 1);
            }
        }

        return degree.Values.All(v => v == Vertices[v].Connections.Count);
    }

    /// <summary>
    /// Creates an undirected graph from the directed graph and calls IsConnected on it.
    ///
    /// Time complexity: O(V + E)
    /// Space complexity: O(V + E)
    /// </summary>
    public override bool IsConnected()
    {
        // Create an undirected vertex list from the directed vertex list
        var undirectedVertexDict = Vertices.ToDictionary(
            vertex => vertex.Key,
            vertex => new Vertex<TData>(vertex.Value.Data)
        );

        var undirectedGraph = new UndirectedGraph<TData>(undirectedVertexDict);
        foreach (var vertex in Vertices)
        foreach (var connection in vertex.Value.Connections)
            undirectedGraph.AddEdge(vertex.Key, connection.To);

        return undirectedGraph.IsConnected();
    }

    /// <summary>
    /// Picks a vertex and runs search from it once forwards and once backwards.
    /// If all vertices are visited, then the graph is connected.
    /// TODO: This algorithm should have less complexity. Maybe investigate
    ///
    /// Time complexity: O((V + E)^2)
    /// Space complexity: O((V + E) * V)
    /// </summary>
    public bool IsConnected2()
    {
        var firstVertex = Vertices.First().Key;

        var successors = new HashSet<int> {firstVertex};
        var predecessors = new HashSet<int> {firstVertex};

        // Find successors
        var queue = new Queue<int> {firstVertex};

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var connection in UnaccountedConnections(current, successors))
            {
                queue.Enqueue(connection.To);
                successors.Add(connection.To);
            }
        }

        // Find predecessors
        queue.Enqueue(firstVertex);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var connection in AllBackwardsConnections(current))
            {
                if (predecessors.Contains(connection))
                    continue;

                queue.Enqueue(connection);
                predecessors.Add(connection);
            }
        }

        successors.UnionWith(predecessors);
        return successors.Overlaps(Vertices.Keys);
    }

    /// <summary>
    /// Time complexity: O(V + E)
    /// Space complexity: O(V)
    /// </summary>
    public override bool IsTree()
    {
        if (VertexCount() <= 1)
            return true;

        // Find the root vertex
        var incomingEdges = Vertices.Keys.ToHashSet();

        foreach (var vertex in Vertices)
        foreach (var connection in vertex.Value.Connections)
            incomingEdges.Remove(connection.To);

        // If there is more than one root vertex, it is not a tree
        if (incomingEdges.Count != 1)
            return false;

        // Do a breadth first search from the root
        var visited = new HashSet<int>();
        var queue = new Queue<int> {incomingEdges.Single()};

        while (queue.Count > 0)
        {
            var v = queue.Dequeue();

            // Enqueue connections
            foreach (var connection in AllConnections(v))
            {
                if (visited.Contains(connection.To))
                    return false;

                queue.Enqueue(connection.To);
                visited.Add(connection.To);
            }
        }

        return true;
    }

    public override List<(int V1, int V2)> Bridges()
    {
        // TODO: Academic paper for the algorithm: https://stackoverflow.com/a/17107586/7279624
        throw new NotImplementedException();
    }
}