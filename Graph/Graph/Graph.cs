using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph;

/// <summary>
/// This class represents a graph.
/// It uses an adjacency list representation of the graph.
/// This approach is found effective in many problems compared to edge list and adjacency matrix representations.
/// 
/// Primary purpose of this class is to include all operations and algorithms related to graph for later reference.
/// Instead of taking a graph as parameter or using global variables, this class uses an object oriented approach.
/// It includes all operations inside the Graph class and its subclasses.
/// 
/// The class is structures to make use of partial classes to semantically separate the graph operations.
/// 
/// --- The list of partial classes ---
/// 
/// 1. ElementaryOperations
/// Contains the elementary operations of the graph.
/// Elementary operations include:
/// - AddVertex
/// - AddEdge
/// - RemoveVertex
/// - ...
/// 
/// 2. Search
/// Contains various implementations of breadth-first and depth-first search algorithms.
/// Includes:
/// - Breadth-first search
/// - Depth-first search
/// - Iterative depth-first search
/// 
/// Variations of these searches include:
/// - Vertex search (Record every visited vertex in order) / Edge search (record every crossed edge in order including backtracking)
/// - Searching the whole graph / Searching from a vertex
///
/// 3. ShortestPath
/// Contains various implementations of finding the shortest path between two vertices or the shortest path between a vertex and all other vertices.
/// Includes:
/// - Dijkstra's algorithm
/// - Bellman-Ford algorithm
/// - Floyd-Warshall algorithm
/// - Johnson's algorithm
/// - A* algorithm
///
/// </summary>
/// <typeparam name="TData"></typeparam>
public abstract partial class Graph<TData>
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

    protected IEnumerable<Connection> VisitedConnections(int vertex, HashSet<int> visitedSet)
    {
        return Vertices[vertex].Connections.Where(connection => visitedSet.Contains(connection.To));
    }

    protected IEnumerable<Connection> VisitedConnections<T>(int vertex, Dictionary<int, T> visitedDict)
    {
        return Vertices[vertex].Connections.Where(connection => visitedDict.ContainsKey(connection.To));
    }

    protected IEnumerable<int> UnvisitedVertices(HashSet<int> visitedSet)
    {
        return Vertices.Keys.Where(v => !visitedSet.Contains(v));
    }

    protected IEnumerable<int> UnvisitedVertices<T>(Dictionary<int, T> visitedDict)
    {
        return Vertices.Keys.Where(v => !visitedDict.ContainsKey(v));
    }

    protected IEnumerable<int> VisitedVertices(HashSet<int> visitedSet)
    {
        return Vertices.Keys.Where(v => visitedSet.Contains(v));
    }

    protected IEnumerable<int> VisitedVertices<T>(Dictionary<int, T> visitedDict)
    {
        return Vertices.Keys.Where(v => visitedDict.ContainsKey(v));
    }

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
        CheckVertex(vertex);

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
        CheckVertex(vertex);

        return Excentricity(vertex) == Radius();
    }

    public bool IsBorderVertex(int vertex)
    {
        CheckVertex(vertex);

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