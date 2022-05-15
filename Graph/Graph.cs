using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmHelpers.DataStructures;

namespace Graph;

public abstract class Graph<TData>
{
    public Dictionary<int, Vertex<TData>> Vertices { get; }

    #region Constructors

    protected Graph()
    {
        Vertices = new Dictionary<int, Vertex<TData>>();
    }

    protected Graph(List<(int, int)> edges)
    {
        Vertices = new Dictionary<int, Vertex<TData>>();
    }

    protected Graph(Dictionary<int, Vertex<TData>> vertices)
    {
        // Make sure the connections lists only contain values that exist in the vertex list
        foreach (var vertex in vertices)
        foreach (var connection in vertex.Value.Connections)
            if (!vertices.ContainsKey(connection.To))
                throw new ArgumentException("The edge list contains a vertex that is not in the vertex list");

        Vertices = vertices;
    }

    #endregion

    #region Elementary Operations

    public abstract void AddEdge(int v1, int v2);

    public void AddEdge((int v1, int v2) edge)
    {
        AddEdge(edge.v1, edge.v2);
    }

    public void AddEdges(List<(int v1, int v2)> edges)
    {
        foreach (var edge in edges)
            AddEdge(edge.v1, edge.v2);
    }

    public abstract void AddEdge(int v1, int v2, int weight);

    public abstract void RemoveEdge(int v1, int v2);


    public void RemoveEdge((int v1, int v2) edge)
    {
        RemoveEdge(edge.v1, edge.v2);
    }

    public void RemoveEdges(List<(int v1, int v2)> edges)
    {
        foreach (var edge in edges)
            RemoveEdge(edge.v1, edge.v2);
    }

    public void AddVertex(int v)
    {
        if (Vertices.ContainsKey(v))
            throw new ArgumentException("The vertex already exists");

        Vertices.Add(v, Vertex<TData>.Empty());
    }

    public abstract void RemoveVertex(int v);

    protected abstract bool HasEdge(int v1, int v2);

    public bool VerticesConnected((int v1, int v2) edge)
    {
        return HasEdge(edge.v1, edge.v2);
    }

    public bool ContainsVertex(int v)
    {
        return Vertices.ContainsKey(v);
    }

    public void Clear()
    {
        Vertices.Clear();
    }

    protected int VertexCount()
    {
        return Vertices.Count;
    }

    public abstract int EdgeCount();

    protected List<Connection> AllConnections(int v)
    {
        if (!Vertices.ContainsKey(v))
            throw new ArgumentException("The vertex does not exist");

        return Vertices[v].Connections;
    }

    public List<int> GetVertices()
    {
        return Vertices.Keys.ToList();
    }

    public bool IsEmpty()
    {
        return Vertices.Count == 0;
    }

    #endregion

    #region Sementic Operations

    /// <summary>
    /// Finds all the vertices that are connected to the given vertex
    ///
    /// Time Complexity: O(V + E)
    /// Space Complexity: O(V)
    /// </summary>
    protected List<int> AllBackwardsConnections(int v)
    {
        // Find all vertices that have a directed edge towards v using VertexList
        return (
            from vertex in Vertices
            where vertex.Value.Connections.Contains(v)
            select vertex.Key
        ).ToList();
    }

    protected int NonKeyValue()
    {
        var nonKeyValue = int.MinValue;
        while (Vertices.ContainsKey(nonKeyValue))
            nonKeyValue++;
        return nonKeyValue;
    }

    protected IEnumerable<Connection> UnvisitedConnections(int vertex, HashSet<int> visitedSet)
    {
        return Vertices[vertex].Connections.Where(connection => !visitedSet.Contains(connection.To));
    }

    protected IEnumerable<Connection> UnvisitedConnections<T>(int vertex, Dictionary<int, T> visitedDict)
    {
        return Vertices[vertex].Connections.Where(connection => !visitedDict.ContainsKey(connection.To));
    }

    protected IEnumerable<int> UnvisitedVertices(HashSet<int> visitedSet)
    {
        return Vertices.Keys.Where(v => !visitedSet.Contains(v));
    }

    protected IEnumerable<int> UnvisitedVertices<T>(Dictionary<int, T> visitedDict)
    {
        return Vertices.Keys.Where(v => !visitedDict.ContainsKey(v));
    }

    protected int ConnectionCount(int v)
    {
        return Vertices[v].Connections.Count;
    }

    #endregion

    #region Traversal Operations

    /// <summary>
    /// Iterative breath first search that traverses all vertices reachable from v.
    ///
    /// Time Complexity: O(E)
    /// Space Complexity: O(V)
    /// </summary>
    /// <param name="v">Start vertex</param>
    /// <returns>All visited vertices</returns>
    public List<int> BfsTraversal(int v)
    {
        if (!Vertices.ContainsKey(v))
            throw new ArgumentException("The vertex does not exist");

        var visited = new HashSet<int> {v};
        var queue = new Queue<int> {v};

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var connection in UnvisitedConnections(current, visited))
            {
                visited.Add(connection.To);
                queue.Enqueue(connection.To);
            }
        }

        return visited.ToList();
    }

    /// <summary>
    /// Recursive depth first search that traverses all vertices reachable from v.
    ///
    /// Time Complexity: O(E)
    /// Space Complexity: O(V)
    /// </summary>
    /// <param name="v">Start vertex</param>
    /// <returns>All visited vertices</returns>
    public List<int> DfsTraversal(int v)
    {
        if (!Vertices.ContainsKey(v))
            throw new ArgumentException("The vertex does not exist");

        var visited = new HashSet<int> {v};
        DfsTraversal(v, visited);
        return visited.ToList();
    }

    /// <summary>
    /// Recursive call for recursive depth first search that traverses all vertices reachable from v.
    /// </summary>
    private void DfsTraversal(int v, HashSet<int> visited)
    {
        foreach (var connection in UnvisitedConnections(v, visited))
        {
            visited.Add(connection.To);
            DfsTraversal(connection.To, visited);
        }
    }

    /// <summary>
    /// Iterative depth first search that traverses all vertices reachable from v.
    ///
    /// Time Complexity: O(E)
    /// Space Complexity: O(V)
    /// </summary>
    /// <param name="v">Start vertex</param>
    /// <returns>All visited vertices</returns>
    public List<int> DfsTraversalIterative(int v)
    {
        if (!Vertices.ContainsKey(v))
            throw new ArgumentException("The vertex does not exist");

        var visited = new HashSet<int> {v};
        var stack = new Stack<int> {v};

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            visited.Add(current);

            UnvisitedConnections(current, visited).ToList().ForEachReversed(connection => { stack.Push(connection.To); });
        }

        return visited.ToList();
    }

    /// <summary>
    /// Iterative breath first search that traverses all vertices and all edges reachable from v.
    ///
    /// Time Complexity: O(E)
    /// Space Complexity: O(E)
    /// </summary>
    /// <param name="v">Start vertex</param>
    /// <returns>All visited vertices and edges</returns>
    public abstract List<(int From, int To, bool Forwards)> BfsEdgeTraversal(int v);

    /// <summary>
    /// Recursive depth first search that traverses all vertices and all edges reachable from v.
    ///
    /// Time Complexity: O(E)
    /// Space Complexity: O(E)
    /// </summary>
    /// <param name="v">Start vertex</param>
    /// <returns>All visited vertices and edges</returns>
    public abstract List<(int From, int To, bool Forward)> DfsEdgeTraversal(int v);

    /// <summary>
    /// Iterative depth first search that traverses all vertices and all edges reachable from v.
    /// Uses a stack frame data structure to store vertex data in stack.
    ///
    /// Time Complexity: O(E)
    /// Space Complexity: O(E)
    /// </summary>
    /// <param name="v">Start vertex</param>
    /// <returns>All visited vertices and edges</returns>
    public abstract List<(int From, int To, bool Forward)> DfsEdgeTraversalIterative(int v);

    // TODO: Implement traversals without starting vertex

    #endregion

    public bool IsSimple()
    {
        // Duplicate edges
        if (Vertices.Any(v => v.Value.Connections.HasDuplicate()))
            return false;

        // Self-loops
        if (Vertices.Any(v => v.Value.Connections.Contains(v.Key)))
            return false;

        return true;
    }

    public abstract bool IsEulerian();

    public abstract bool IsTree();

    public abstract bool IsConnected();

    public abstract List<(int V1, int V2)> Bridges();

    public bool IsTherePath(int fromVertex, int toVertex)
    {
        if (!Vertices.ContainsKey(fromVertex) || !Vertices.ContainsKey(toVertex))
            throw new ArgumentException("The vertex does not exist");

        var visited = new HashSet<int>();
        var queue = new Queue<int> {fromVertex};

        while (queue.Count > 0)
        {
            var v = queue.Dequeue();

            if (v == toVertex)
                return true;

            // Enqueue connections
            foreach (var connection in UnvisitedConnections(v, visited))
            {
                queue.Enqueue(connection.To);
                visited.Add(v);
            }
        }

        return false;
    }

    public List<int>? ShortestPath(int fromVertex, int toVertex)
    {
        if (!Vertices.ContainsKey(fromVertex) || !Vertices.ContainsKey(toVertex))
            throw new ArgumentException("The vertex does not exist");

        var predecessor = new Dictionary<int, int> {{fromVertex, -1}};
        var queue = new Queue<int> {fromVertex};

        while (queue.Count > 0)
        {
            var v = queue.Dequeue();

            if (v == toVertex)
            {
                var path = new List<int>();
                for (var current = toVertex; current != -1; current = predecessor[current])
                    path.Add(current);

                path.Reverse();
                return path;
            }

            // Enqueue connections
            foreach (var connection in UnvisitedConnections(v, predecessor))
            {
                queue.Enqueue(connection.To);
                predecessor.Add(connection.To, v);
            }
        }

        return null;
    }

    // TODO: Correct implementation
    /// <returns>The shortest loop, null if there are no loops</returns>
    public List<int>? ShortestLoop()
    {
        var firstVertex = Vertices.Keys.First();

        var visited = new HashSet<int>();
        var queue = new Queue<int> {firstVertex};
        var predecessor = new Dictionary<int, int> {{firstVertex, -1}};

        while (queue.Count > 0)
        {
            var v = queue.Dequeue();

            visited.Add(v);

            // Enqueue connections
            foreach (var connection in AllConnections(v))
            {
                if (visited.Contains(connection.To))
                {
                    // Loop found
                    var loop = new List<int> {connection.To};
                    for (int current = v; current != connection.To; current = predecessor[current])
                        loop.Add(current);

                    return loop;
                }

                queue.Enqueue(connection.To);
                predecessor.Add(connection.To, v);
            }
        }

        return null;
    }

    // TODO: Implement
    public List<int>? LongestLoop()
    {
        return null;
    }

    public int Excentricity(int vertex)
    {
        if (!Vertices.ContainsKey(vertex))
            throw new ArgumentException("The vertex does not exist");

        var visited = new HashSet<int>();
        var queue = new Queue<int> {vertex};
        var distance = new Dictionary<int, int>
        {
            {vertex, 0},
        };

        while (queue.Count > 0)
        {
            var v = queue.Dequeue();

            visited.Add(v);

            // Enqueue connections
            foreach (var connection in UnvisitedConnections(v, visited))
            {
                queue.Enqueue(connection.To);
                distance.Add(connection.To, distance[v] + 1);
            }
        }

        return distance.Values.Max();
    }

    // TODO: Maybe improvable
    public int Radius()
    {
        return Vertices.Keys.Min(Excentricity);
    }

    // TODO: Maybe improvable
    public int Diameter()
    {
        return Vertices.Keys.Max(Excentricity);
    }

    public bool IsCentralVertex(int vertex)
    {
        if (!Vertices.ContainsKey(vertex))
            throw new ArgumentException("The vertex does not exist");

        return Excentricity(vertex) == Radius();
    }

    public bool IsBorderVertex(int vertex)
    {
        if (!Vertices.ContainsKey(vertex))
            throw new ArgumentException("The vertex does not exist");

        return Excentricity(vertex) == Diameter();
    }

    // TODO: Maybe improvable
    public int[] GetCentralVertices()
    {
        var radius = Radius();

        return Vertices.Keys
            .Where(v => Excentricity(v) == radius)
            .ToArray();
    }

    /// <summary>
    /// Implementation of the Dijkstra search algorithm.
    /// Finds the shortest path from the source to all of the connected vertices.
    ///
    /// Time complexity: O(E * logE)
    /// </summary>
    /// <param name="v1">Start vertex</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public Dictionary<int, int> Dijkstra(int v1)
    {
        if (!Vertices.ContainsKey(v1))
            throw new ArgumentException("The vertex does not exist");

        var distance = new Dictionary<int, int> {{v1, 0}};
        var distanceFound = new HashSet<int> {v1};
        var candidates = new MinHeap<int, int> {(v1, 0)};

        while (candidates.Count > 0)
        {
            // Find candidate with smallest distance
            var minCandidate = candidates.Pop();
            distanceFound.Add(minCandidate.Key);

            foreach (var connection in UnvisitedConnections(minCandidate.Key, distanceFound))
            {
                var newDistance = distance[minCandidate.Key] + connection.Weight;

                if (distance.ContainsKey(connection.To))
                {
                    if (distance[connection.To] <= newDistance)
                        continue;

                    distance[connection.To] = newDistance;
                    candidates.Update(connection.To, newDistance);
                }
                else
                {
                    distance.Add(connection.To, newDistance);
                    candidates.Add((connection.To, newDistance));
                }
            }
        }

        return distance;
    }

    public (int, List<int>) Dijkstra(int v1, int v2)
    {
        if (!Vertices.ContainsKey(v1) || !Vertices.ContainsKey(v2))
            throw new ArgumentException("The vertex does not exist");

        var distance = new Dictionary<int, int> {{v1, 0}};
        var distanceFound = new HashSet<int> {v1};
        var candidates = new MinHeap<int, int> {(v1, 0)};
        var predecessor = new Dictionary<int, int> {{v1, v1}};

        while (candidates.Count > 0)
        {
            // Find candidate with smallest distance
            var minCandidate = candidates.Pop();
            distanceFound.Add(minCandidate.Key);

            // Check if we found the target
            if (distanceFound.Contains(v2))
            {
                var path = new List<int>();
                for (var current = v2; current != v1; current = predecessor[current])
                    path.Add(current);
                path.Add(v1);
                path.Reverse();

                return (distance[v2], path);
            }

            foreach (var connection in UnvisitedConnections(minCandidate.Key, distanceFound))
            {
                var newDistance = distance[minCandidate.Key] + connection.Weight;

                if (distance.ContainsKey(connection.To))
                {
                    if (distance[connection.To] <= newDistance)
                        continue;

                    distance[connection.To] = newDistance;
                    predecessor[connection.To] = minCandidate.Key;
                }
                else
                {
                    distance.Add(connection.To, newDistance);
                    predecessor.Add(connection.To, minCandidate.Key);
                    candidates.Add((connection.To, newDistance));
                }
            }
        }

        return (-1, new List<int>());
    }
}