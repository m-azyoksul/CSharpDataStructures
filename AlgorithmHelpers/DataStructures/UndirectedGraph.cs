using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmHelpers.DataStructures;

public class UndirectedGraph<TVertexData> : Graph<TVertexData>
{
    #region Constructors

    public UndirectedGraph() : base()
    {
    }

    public UndirectedGraph(List<(int, int)> edges) : base(edges)
    {
        // Find all vertices
        foreach (var edge in edges)
        {
            if (!Vertices.ContainsKey(edge.Item1))
                Vertices.Add(edge.Item1, Vertex<TVertexData>.Empty());

            if (!Vertices.ContainsKey(edge.Item2))
                Vertices.Add(edge.Item2, Vertex<TVertexData>.Empty());

            Vertices[edge.Item1].Neighbors.Add(edge.Item2);
            Vertices[edge.Item2].Neighbors.Add(edge.Item1);
        }
    }

    public UndirectedGraph(Dictionary<int, Vertex<TVertexData>> vertices) : base(vertices)
    {
        // TODO
        // Check if every vertex is the neighbour its neighbours
        // This graph also allows for self-loops and multiple edges between two vertices
        foreach (var vertex in vertices)
        {
            foreach (var neighbour in vertex.Value.Neighbors)
            {
                if (!vertices[neighbour].Neighbors.Contains(vertex.Key))
                    throw new ArgumentException("Graph is not undirected");
            }
        }
    }

    #endregion

    #region Elementary operations

    public override void AddEdge(int v1, int v2)
    {
        if (!Vertices.ContainsKey(v1) && !Vertices.ContainsKey(v2))
            throw new ArgumentException("The vertex does not exist");

        Edges.Add((v1, v2));
        Vertices[v1].Neighbors.Add(v2);
        Vertices[v2].Neighbors.Add(v1);
    }

    public override void RemoveEdge(int v1, int v2)
    {
        if (!Vertices.ContainsKey(v1) && !Vertices.ContainsKey(v2))
            throw new ArgumentException("The vertex does not exist");

        if (Edges.Contains((v1, v2)))
        {
            Edges.Remove((v1, v2));
            Vertices[v1].Neighbors.Remove(v2);
            Vertices[v2].Neighbors.Remove(v1);
        }
        else if (Edges.Contains((v2, v1)))
        {
            Edges.Remove((v2, v1));
            Vertices[v2].Neighbors.Remove(v1);
            Vertices[v1].Neighbors.Remove(v2);
        }
        else
            throw new ArgumentException("The edge does not exist");
    }

    public override void RemoveVertex(int v)
    {
        if (!Vertices.ContainsKey(v))
            throw new ArgumentException("The vertex does not exist");

        foreach (var vertex in Vertices[v].Neighbors)
            Vertices[vertex].Neighbors.Remove(v);

        Vertices.Remove(v);
        Edges.RemoveAll(edge => edge.Item1 == v || edge.Item2 == v);
    }

    public override bool ContainsEdge(int v1, int v2)
    {
        return Edges.Contains((v1, v2)) || Edges.Contains((v2, v1));
    }

    #endregion

    /// <summary>
    /// Time complexity: O(V)
    /// Space complexity: O(1)
    /// </summary>
    public override bool IsEulerian()
    {
        return Vertices.All(vertex => vertex.Value.Neighbors.Count % 2 == 0);
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

        var visited = new HashSet<int>();
        var queue = new Queue<int> {Vertices.Keys.First()};

        while (queue.Count > 0)
        {
            var v = queue.Dequeue();

            // Enqueue neighbors
            foreach (var neighbor in AllNeighbours(v))
            {
                if (visited.Contains(neighbor))
                    return false;

                queue.Enqueue(neighbor);
                visited.Add(v);
            }
        }

        return visited.Count == VertexCount();
    }

    /// <summary>
    /// Time complexity: O(V)
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
            foreach (var neighbor in AllNeighbours(v))
            {
                // If the neighbor is visited before and is not the predecessor, the graph is not a tree
                if (predecessor.ContainsKey(neighbor))
                {
                    if (predecessor[v] != neighbor)
                        return false;
                    continue;
                }

                queue.Enqueue(neighbor);
                predecessor[neighbor] = v;
            }
        }

        return VertexCount() == predecessor.Count;
    }

    /// <summary>
    /// Finds all bridges (critical edges) in the graph.
    /// Uses recursive DFS to find bridges.
    ///
    /// Time complexity: O(V + E)
    /// Space complexity: O(V)
    /// </summary>
    /// <returns></returns>
    public override List<(int V1, int V2)> Bridges()
    {
        var bridges = new List<(int V1, int V2)>();

        var visited = new HashSet<int>();
        var low = new Dictionary<int, int>();
        var timeOfVisit = new Dictionary<int, int>();
        var time = 0;

        foreach (var vertex in Vertices)
        {
            if (visited.Contains(vertex.Key))
                continue;

            BridgesDfs(vertex.Key, visited, low, timeOfVisit, bridges, ref time);
        }

        return bridges;
    }

    private void BridgesDfs(int v, HashSet<int> visited, Dictionary<int, int> low, Dictionary<int, int> timeOfVisit, List<(int V1, int V2)> bridges, ref int time, int parent = -1)
    {
        visited.Add(v);
        low[v] = timeOfVisit[v] = time;
        time++;

        foreach (int to in Vertices[v].Neighbors)
        {
            if (to == parent)
                continue;

            if (visited.Contains(to))
                low[v] = Math.Min(low[v], timeOfVisit[to]);
            else
            {
                BridgesDfs(to, visited, low, timeOfVisit, bridges, ref time, v);
                if (low[to] > timeOfVisit[v])
                    bridges.Add((v, to));
                low[v] = Math.Min(low[v], low[to]);
            }
        }
    }

    /// <summary>
    /// Finds all bridges (critical edges) in the graph.
    /// Uses iterative dfs using stack frame to avoid recursion.
    /// Implementation is based on the following SO answer: https://stackoverflow.com/a/61645529/7279624
    ///
    /// Time complexity: O(V + E)
    /// Space complexity: O(V)
    /// </summary>
    public List<(int V1, int V2)> BridgesIterative()
    {
        var bridges = new List<(int V1, int V2)>();

        var visited = new HashSet<int>();
        var low = new Dictionary<int, int>();
        var timeOfVisit = new Dictionary<int, int>();
        var time = 0;

        foreach (var vertex in Vertices.Keys)
        {
            if (visited.Contains(vertex))
                continue;

            var stack = new Stack<(int vertex, int parent, int visitedNCount)> {(vertex, NonKeyValue(), 0)};

            while (stack.Count > 0)
            {
                var current = stack.Pop();

                // If the vertex is being visited for the first time, set the appropriate values
                if (current.visitedNCount == 0)
                {
                    visited.Add(current.vertex);
                    low[current.vertex] = timeOfVisit[current.vertex] = time;
                    time++;
                }

                // If a neighbours was visited, check if it is a bridge and update the low value
                else
                {
                    int neighbour = Vertices[current.vertex].Neighbors[current.visitedNCount - 1];
                    if (neighbour != current.parent)
                    {
                        if (low[neighbour] > timeOfVisit[current.vertex])
                            bridges.Add((current.vertex, neighbour));

                        low[current.vertex] = Math.Min(low[current.vertex], low[neighbour]);
                    }
                }

                // Push next unvisited neighbour to the stack
                if (current.visitedNCount < Vertices[current.vertex].Neighbors.Count)
                {
                    // Push the current vertex back to the stack
                    stack.Push((current.vertex, current.parent, current.visitedNCount + 1));

                    // If the neighbour is the parent, skip it
                    int neighbour = Vertices[current.vertex].Neighbors[current.visitedNCount];
                    if (neighbour == current.parent)
                        continue;

                    // If the neighbour is already visited, set the low value
                    if (visited.Contains(neighbour))
                        low[current.vertex] = Math.Min(low[current.vertex], timeOfVisit[neighbour]);

                    // If the neighbour is not visited, push it to the stack
                    else
                        stack.Push((neighbour, current.vertex, 0));
                }
            }
        }

        return bridges;
    }

    public (int, Dictionary<int, int>) ConnectedComponentMap()
    {
        var components = new Dictionary<int, int>();
        var componentIndex = 0;

        foreach (var vertex in Vertices.Keys)
        {
            if (components.ContainsKey(vertex))
                continue;

            var queue = new Queue<int> {vertex};

            while (queue.Count > 0)
            {
                var v = queue.Dequeue();

                foreach (var neighbor in UnvisitedNeighbours(v, components))
                {
                    queue.Enqueue(neighbor);
                    components[v] = componentIndex;
                }
            }

            componentIndex++;
        }

        return (componentIndex, components);
    }

    public List<List<int>> ConnectedComponentList()
    {
        var components = new List<List<int>>();

        foreach (var vertex in Vertices.Keys)
        {
            var visited = new HashSet<int>();

            if (visited.Contains(vertex))
                continue;

            var queue = new Queue<int> {vertex};

            while (queue.Count > 0)
            {
                var v = queue.Dequeue();

                foreach (var neighbor in UnvisitedNeighbours(v, visited))
                {
                    queue.Enqueue(neighbor);
                    visited.Add(v);
                }
            }

            components.Add(visited.ToList());
        }

        return components;
    }
}