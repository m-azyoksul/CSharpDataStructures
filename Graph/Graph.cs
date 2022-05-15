using System;
using System.Collections.Generic;
using System.Linq;

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
            if (!vertices.ContainsKey(connection))
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

    protected List<int> AllConnections(int v)
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

    protected IEnumerable<int> UnvisitedConnections(int vertex, HashSet<int> visitedSet)
    {
        return Vertices[vertex].Connections.Where(connection => !visitedSet.Contains(connection));
    }

    protected IEnumerable<int> UnvisitedConnections<T>(int vertex, Dictionary<int, T> visitedDict)
    {
        return Vertices[vertex].Connections.Where(connection => !visitedDict.ContainsKey(connection));
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
                visited.Add(connection);
                queue.Enqueue(connection);
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
            visited.Add(connection);
            DfsTraversal(connection, visited);
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

            UnvisitedConnections(current, visited).ToList().ForEachReversed(connection => { stack.Push(connection); });
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
                queue.Enqueue(connection);
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
                for (int current = toVertex; current != -1; current = predecessor[current])
                    path.Add(current);

                path.Reverse();
                return path;
            }

            // Enqueue connections
            foreach (var connection in UnvisitedConnections(v, predecessor))
            {
                queue.Enqueue(connection);
                predecessor.Add(connection, v);
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
                if (visited.Contains(connection))
                {
                    // Loop found
                    var loop = new List<int> {connection};
                    for (int current = v; current != connection; current = predecessor[current])
                        loop.Add(current);

                    return loop;
                }

                queue.Enqueue(connection);
                predecessor.Add(connection, v);
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
                queue.Enqueue(connection);
                distance.Add(connection, distance[v] + 1);
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
}