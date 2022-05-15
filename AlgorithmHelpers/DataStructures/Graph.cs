using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmHelpers.DataStructures;

public class Vertex<T>
{
    public T? Data;
    public List<int> Neighbors;

    public Vertex(T? data, List<int> neighbors)
    {
        Data = data;
        Neighbors = neighbors;
    }

    public Vertex(T? data)
    {
        Data = data;
        Neighbors = new List<int>();
    }

    public static Vertex<T> Empty() => new(default, new List<int>());
}

public abstract class Graph<TData>
{
    public Dictionary<int, Vertex<TData>> Vertices { get; }
    public List<(int V1, int V2)> Edges { get; }

    #region Constructors

    protected Graph()
    {
        Vertices = new Dictionary<int, Vertex<TData>>();
        Edges = new List<(int, int)>();
    }

    protected Graph(List<(int, int)> edges)
    {
        Vertices = new Dictionary<int, Vertex<TData>>();
        Edges = edges;
    }

    protected Graph(Dictionary<int, Vertex<TData>> vertices)
    {
        // Make sure the Neighbours lists only contain values that exist in the vertex list
        foreach (var vertex in vertices)
        foreach (var neighbour in vertex.Value.Neighbors)
            if (!vertices.ContainsKey(neighbour))
                throw new ArgumentException("The edge list contains a vertex that is not in the vertex list");

        Vertices = vertices;
        Edges = new List<(int, int)>();
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

    public abstract bool ContainsEdge(int v1, int v2);

    public bool ContainsEdge((int v1, int v2) edge)
    {
        return ContainsEdge(edge.v1, edge.v2);
    }

    public bool ContainsVertex(int v)
    {
        return Vertices.ContainsKey(v);
    }

    public void Clear()
    {
        Vertices.Clear();
        Edges.Clear();
    }

    public int VertexCount()
    {
        return Vertices.Count;
    }

    public int EdgeCount()
    {
        return Edges.Count;
    }

    public List<int> AllNeighbours(int v)
    {
        if (!Vertices.ContainsKey(v))
            throw new ArgumentException("The vertex does not exist");

        return Vertices[v].Neighbors;
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
    protected List<int> AllBackwardsNeighbours(int v)
    {
        // Find all vertices that have a directed edge towards v using VertexList
        return (
            from vertex in Vertices
            where vertex.Value.Neighbors.Contains(v)
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

    protected IEnumerable<int> UnvisitedNeighbours(int vertex, HashSet<int> visitedSet)
    {
        return Vertices[vertex].Neighbors.Where(neighbour => !visitedSet.Contains(neighbour));
    }

    protected IEnumerable<int> UnvisitedNeighbours<T>(int vertex, Dictionary<int, T> visitedDict)
    {
        return Vertices[vertex].Neighbors.Where(neighbour => !visitedDict.ContainsKey(neighbour));
    }

    protected int NeighborCount(int v)
    {
        return Vertices[v].Neighbors.Count;
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

            foreach (var neighbour in UnvisitedNeighbours(current, visited))
            {
                visited.Add(neighbour);
                queue.Enqueue(neighbour);
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
        foreach (var neighbour in UnvisitedNeighbours(v, visited))
        {
            visited.Add(neighbour);
            DfsTraversal(neighbour, visited);
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

            UnvisitedNeighbours(current, visited).ToList().ForEachReversed(neighbour => { stack.Push(neighbour); });
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
        if (Vertices.Any(v => v.Value.Neighbors.HasDuplicate()))
            return false;
        
        // Self-loops
        if (Vertices.Any(v => v.Value.Neighbors.Contains(v.Key)))
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

            // Enqueue neighbors
            foreach (var neighbor in UnvisitedNeighbours(v, visited))
            {
                queue.Enqueue(neighbor);
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

            // Enqueue neighbors
            foreach (var neighbor in UnvisitedNeighbours(v, predecessor))
            {
                queue.Enqueue(neighbor);
                predecessor.Add(neighbor, v);
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

            // Enqueue neighbors
            foreach (var neighbor in AllNeighbours(v))
            {
                if (visited.Contains(neighbor))
                {
                    // Loop found
                    var loop = new List<int> {neighbor};
                    for (int current = v; current != neighbor; current = predecessor[current])
                        loop.Add(current);

                    return loop;
                }

                queue.Enqueue(neighbor);
                predecessor.Add(neighbor, v);
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

            // Enqueue neighbors
            foreach (var neighbor in UnvisitedNeighbours(v, visited))
            {
                queue.Enqueue(neighbor);
                distance.Add(neighbor, distance[v] + 1);
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