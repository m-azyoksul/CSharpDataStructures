using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.Heap;

namespace DataStructures.Graph;

/// <summary>
/// This class represents a graph.
/// It uses an adjacency list representation of the graph.
/// This approach is found effective in many problems compared to edge list and adjacency matrix representations.
/// 
/// Primary purpose of this class is to include all operations and algorithms related to graph for later reference.
/// Instead of taking a graph as parameter or using global variables, this class uses an object oriented approach.
/// It includes all operations inside the Graph class and its subclasses.
/// The members are grouped in regions.
/// 
/// --- The list of regions ---
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
/// - Breadth-first search (4 variations)
/// - Depth-first search (4 variations)
/// - Iterative depth-first search (4 variations)
/// 
/// Variations of these searches include:
/// - Vertex search (Record every visited vertex in order) / Edge search (record every crossed edge in order including backtracking)
/// - Searching the whole graph / Searching from a vertex
///
/// 3. ShortestPath
/// Contains various implementations of finding the shortest path between two vertices or the shortest path between a vertex and all other vertices.
/// Includes:
/// - Dijkstra's algorithm (2 variations)
/// - Bellman-Ford algorithm (2 variations)
/// - Floyd-Warshall algorithm
/// - Johnson's algorithm
/// - A* algorithm
///
/// </summary>
/// <typeparam name="TData">The type of data to be stored in the vertices.</typeparam>
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

    protected IEnumerable<Connection> UnaccountedConnections(int vertex, HashSet<int> visitedSet)
    {
        return Vertices[vertex].Connections.Where(connection => !visitedSet.Contains(connection.To));
    }

    protected IEnumerable<Connection> UnaccountedConnections<T>(int vertex, Dictionary<int, T> visitedDict)
    {
        return Vertices[vertex].Connections.Where(connection => !visitedDict.ContainsKey(connection.To));
    }

    protected IEnumerable<Connection> AccountedConnections(int vertex, HashSet<int> visitedSet)
    {
        return Vertices[vertex].Connections.Where(connection => visitedSet.Contains(connection.To));
    }

    protected IEnumerable<Connection> AccountedConnections<T>(int vertex, Dictionary<int, T> visitedDict)
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
            throw new ArgumentException("Vertex already exists");


        Vertices.Add(v, Vertex<TData>.Empty());
    }

    public void AddVertices(List<int> vertices)
    {
        foreach (var v in vertices)
            AddVertex(v);
    }

    public abstract void AddVertex(int v, List<Connection> connections);

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
        return Vertices[v].Connections;
    }

    protected int ConnectionCount(int v)
    {
        CheckVertex(v);
        return Vertices[v].Connections.Count;
    }

    public List<int> GetVertices()
    {
        return Vertices.Keys.ToList();
    }

    public bool IsEmpty()
    {
        return Vertices.Count == 0;
    }

    protected void CheckVertex(int v)
    {
        if (!Vertices.ContainsKey(v))
            throw new ArgumentException($"The vertex does not exist. Parameter: {nameof(v)} = {v}");
    }

    protected void CheckVertices(int v1, int v2)
    {
        if (!Vertices.ContainsKey(v1))
            throw new ArgumentException($"The vertex does not exist. Parameter: {nameof(v1)} = {v1}");

        if (!Vertices.ContainsKey(v2))
            throw new ArgumentException($"The vertex does not exist. Parameter: {nameof(v2)} = {v2}");
    }

    #endregion

    #region Ungrouped

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

    #endregion

    // Done
    
    #region Search

    /// <summary>
    /// Iterative breath first search that traverses all vertices reachable from v.
    ///
    /// Time Complexity: O(E)
    /// Space Complexity: O(V)
    /// </summary>
    /// <param name="v">Start vertex</param>
    /// <returns>All visited vertices</returns>
    public List<int> BfsVertexTraversal(int v)
    {
        CheckVertex(v);

        var visited = new HashSet<int> {v};
        var queue = new Queue<int> {v};

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var connection in UnaccountedConnections(current, visited))
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
    public List<int> DfsVertexTraversal(int v)
    {
        CheckVertex(v);

        var visited = new HashSet<int> {v};
        DfsVertexTraversal(v, visited);
        return visited.ToList();
    }

    /// <summary>
    /// Recursive call for DfsVertexTraversal that traverses all vertices reachable from v.
    /// </summary>
    private void DfsVertexTraversal(int v, HashSet<int> visited)
    {
        foreach (var connection in UnaccountedConnections(v, visited))
        {
            visited.Add(connection.To);
            DfsVertexTraversal(connection.To, visited);
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
    public List<int> DfsVertexTraversalIterative(int v)
    {
        CheckVertex(v);

        var visited = new HashSet<int> {v};
        var stack = new Stack<int> {v};

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            visited.Add(current);

            UnaccountedConnections(current, visited).ToList().ForEachReversed(connection => { stack.Push(connection.To); });
        }

        return visited.ToList();
    }


    /// <summary>
    /// Iterative breath first search that traverses all vertices.
    ///
    /// Time Complexity: O(V+E)
    /// Space Complexity: O(V)
    /// </summary>
    /// <returns>All vertices</returns>
    public List<int> BfsVertexTraversal()
    {
        var visited = new HashSet<int>();

        foreach (var v in Vertices.Keys)
        {
            if (visited.Contains(v))
                continue;
            visited.Add(v);

            var queue = new Queue<int> {v};

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                foreach (var connection in UnaccountedConnections(current, visited))
                {
                    visited.Add(connection.To);
                    queue.Enqueue(connection.To);
                }
            }
        }

        return visited.ToList();
    }

    /// <summary>
    /// Recursive depth first search that traverses all vertices.
    ///
    /// Time Complexity: O(V+E)
    /// Space Complexity: O(V)
    /// </summary>
    /// <returns>All vertices</returns>
    public List<int> DfsVertexTraversal()
    {
        var visited = new HashSet<int>();
        foreach (var v in Vertices.Keys)
        {
            if (visited.Contains(v))
                continue;
            visited.Add(v);

            DfsVertexTraversal(v, visited);
        }

        return visited.ToList();
    }

    /// <summary>
    /// Iterative depth first search that traverses all vertices.
    ///
    /// Time Complexity: O(V+E)
    /// Space Complexity: O(V)
    /// </summary>
    /// <returns>All vertices</returns>
    public List<int> DfsVertexTraversalIterative()
    {
        var visited = new HashSet<int>();

        foreach (var v in Vertices.Keys)
        {
            if (visited.Contains(v))
                continue;
            visited.Add(v);

            var stack = new Stack<int> {v};

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                visited.Add(current);

                UnaccountedConnections(current, visited).ToList().ForEachReversed(connection => { stack.Push(connection.To); });
            }
        }

        return visited.ToList();
    }


    /// <summary>
    /// Iterative breath first search that traverses all vertices and all edges reachable from v.
    /// </summary>
    /// <param name="v">Start vertex</param>
    /// <returns>All visited vertices and edges</returns>
    public abstract List<(int From, int To, bool Forwards)> BfsEdgeTraversal(int v);

    /// <summary>
    /// Recursive depth first search that traverses all vertices and all edges reachable from v.
    /// </summary>
    /// <param name="v">Start vertex</param>
    /// <returns>All visited vertices and edges</returns>
    public abstract List<(int From, int To, bool Forward)> DfsEdgeTraversal(int v);

    /// <summary>
    /// Iterative depth first search that traverses all vertices and all edges reachable from v.
    /// </summary>
    /// <param name="v">Start vertex</param>
    /// <returns>All visited vertices and edges</returns>
    public abstract List<(int From, int To, bool Forward)> DfsEdgeTraversalIterative(int v);


    /// <summary>
    /// Iterative breath first search that traverses all vertices and all edges.
    /// </summary>
    /// <returns>All vertices and edges</returns>
    public abstract List<(int From, int To, bool Forwards)> BfsEdgeTraversal();

    /// <summary>
    /// Recursive depth first search that traverses all vertices and all edges.
    /// </summary>
    /// <returns>All vertices and edges</returns>
    public abstract List<(int From, int To, bool Forward)> DfsEdgeTraversal();

    /// <summary>
    /// Iterative depth first search that traverses all vertices and all edges.
    /// </summary>
    /// <returns>All vertices and edges</returns>
    public abstract List<(int From, int To, bool Forward)> DfsEdgeTraversalIterative();

    #endregion

    #region ShortestPath

    /// <summary>
    /// Finds if there is a path between two vertices in the graph.
    /// Uses bfs search.
    ///
    /// Time complexity: O(E)
    /// Space complexity: O(V)
    /// </summary>
    /// <param name="fromVertex">The vertex to start the search from.</param>
    /// <param name="toVertex">The vertex to search for.</param>
    /// <returns>True if there is a path between the two vertices, false otherwise.</returns>
    public bool IsTherePath(int fromVertex, int toVertex)
    {
        CheckVertices(fromVertex, toVertex);

        var visited = new HashSet<int>();
        var queue = new Queue<int> {fromVertex};

        while (queue.Count > 0)
        {
            var v = queue.Dequeue();

            if (v == toVertex)
                return true;

            // Enqueue connections
            foreach (var connection in UnaccountedConnections(v, visited))
            {
                queue.Enqueue(connection.To);
                visited.Add(v);
            }
        }

        return false;
    }

    /// <summary>
    /// Finds one of the the shortest paths between two vertices in the graph.
    /// Uses bfs search.
    ///
    /// Time complexity: O(E)
    /// Space complexity: O(V)
    /// </summary>
    /// <param name="fromVertex">The vertex to start the search from.</param>
    /// <param name="toVertex">The vertex to search for.</param>
    /// <returns>The shortest path between the two vertices, or null if there is no path.</returns>
    public List<int>? ShortestPath(int fromVertex, int toVertex)
    {
        CheckVertices(fromVertex, toVertex);

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
            foreach (var connection in UnaccountedConnections(v, predecessor))
            {
                queue.Enqueue(connection.To);
                predecessor.Add(connection.To, v);
            }
        }

        return null;
    }

    /// <summary>
    /// Implementation of the Dijkstra shortest path algorithm.
    /// Finds the shortest path from the start vertex to all of the connected vertices.
    /// Uses bfs search with a priority queue.
    /// Assumes that all edges have a positive weight. Otherwise, the result will be incorrect.
    ///
    /// Time complexity: O(E*logV)
    /// Space complexity: O(V)
    /// </summary>
    /// <param name="v1">Start vertex</param>
    /// <returns>Shortest distance to each connected vertex</returns>
    public Dictionary<int, double> Dijkstra(int v1)
    {
        CheckVertex(v1);

        var distance = new Dictionary<int, double>();
        var candidates = new IndexedMinHeap<int, double> {{v1, 0}};

        while (candidates.Count > 0)
        {
            var minCandidate = candidates.Pop();
            distance.Add(minCandidate.Key, minCandidate.Value);

            foreach (var connection in UnaccountedConnections(minCandidate.Key, distance))
            {
                var newDistance = minCandidate.Value + connection.Weight;

                // Add new candidate
                if (!candidates.ContainsKey(connection.To))
                    candidates.Add(connection.To, newDistance);

                // Update candidate
                else if (candidates.ValueOfKey(connection.To) > newDistance)
                    candidates.UpdateKey(connection.To, newDistance);
            }
        }

        return distance;
    }

    /// <summary>
    /// Implementation of the Dijkstra shortest path algorithm.
    /// Finds one of the shortest paths from the start vertex to the destination vertex.
    /// Uses bfs search with a priority queue.
    /// Assumes that all edges have a positive weight. Otherwise, the result will be incorrect.
    /// 
    /// Time complexity: O(E*logV)
    /// Space complexity: O(V)
    /// </summary>
    /// <param name="v1">Start vertex</param>
    /// <param name="v2">Destination vertex</param>
    /// <returns>Shortest distance and path to the destination</returns>
    public (double Distance, List<int> Path) Dijkstra(int v1, int v2)
    {
        CheckVertices(v1, v2);

        var distanceFound = new HashSet<int> {v1};
        var candidates = new IndexedMinHeap<int, double> {(v1, 0)};
        var predecessor = new Dictionary<int, int> {{v1, v1}};

        while (candidates.Count > 0)
        {
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

                return (minCandidate.Value, path);
            }

            foreach (var connection in UnaccountedConnections(minCandidate.Key, distanceFound))
            {
                var newDistance = minCandidate.Value + connection.Weight;

                // Add new candidate
                if (!candidates.ContainsKey(connection.To))
                {
                    predecessor.Add(connection.To, minCandidate.Key);
                    candidates.Add((connection.To, newDistance));
                    continue;
                }

                // Update candidate
                if (candidates.ValueOfKey(connection.To) > newDistance)
                {
                    predecessor[connection.To] = minCandidate.Key;
                    candidates.UpdateKey(connection.To, newDistance);
                }
            }
        }

        return (-1, new List<int>());
    }

    /// <summary>
    /// Implementation of the Bellman-Ford shortest path algorithm.
    /// Finds the shortest path from the start vertex to all of the connected vertices.
    /// Goes over ell edges V-1 times.
    /// If a vertex is connected to a negative cycle, its shortest path will be double.NegativeInfinity.
    /// 
    /// Time complexity: O(E*logV)
    /// Space complexity: O(V)
    /// </summary>
    /// <param name="v">Start vertex</param>
    /// <returns>Shortest distance to each connected vertex</returns>
    public Dictionary<int, double> BellmanFord(int v)
    {
        CheckVertex(v);

        var distance = new Dictionary<int, double> {{v, 0}};

        foreach (var _ in Vertices)
        foreach (var vertex in Vertices)
        {
            if (!distance.ContainsKey(vertex.Key))
                continue;

            foreach (var connection in AllConnections(vertex.Key))
            {
                var newDistance = distance[vertex.Key] + connection.Weight;
                if (!distance.ContainsKey(connection.To) || newDistance < distance[connection.To])
                    distance[connection.To] = newDistance;
            }
        }

        // Check for negative cycles
        foreach (var _ in Vertices)
        foreach (var vertex in Vertices)
        {
            if (!distance.ContainsKey(vertex.Key))
                continue;

            foreach (var connection in AllConnections(vertex.Key))
            {
                var newDistance = distance[vertex.Key] + connection.Weight;
                if (newDistance < distance[connection.To])
                    distance[connection.To] = double.NegativeInfinity;
            }
        }

        return distance;
    }

    /// <summary>
    /// Implementation of the Bellman-Ford shortest path algorithm.
    /// Finds the shortest path from the start vertex to the destination vertex.
    /// Goes over all edges V-1 times.
    /// If the destination vertex is unreachable, its shortest path will be double.NaN.
    /// If the destination vertex is connected to a negative cycle, its shortest path will be double.NegativeInfinity.
    /// 
    /// Time complexity: O(E*logV)
    /// Space complexity: O(V)
    /// </summary>
    /// <param name="v1">Start vertex</param>
    /// <param name="v2">Destination vertex</param>
    /// <returns>Shortest distance and path to the destination</returns>
    public (double Distance, List<int> Path) BellmanFord(int v1, int v2)
    {
        CheckVertices(v1, v2);

        var distance = new Dictionary<int, double> {{v1, 0}};
        var predecessor = new Dictionary<int, int> {{v1, v1}};

        // Relax all edges V-1 times
        foreach (var _ in Enumerable.Range(0, Vertices.Count - 1))
        foreach (var vertex in VisitedVertices(distance))
        foreach (var connection in AllConnections(vertex))
        {
            var newDistance = distance[vertex] + connection.Weight;
            if (!distance.ContainsKey(connection.To) || newDistance < distance[connection.To])
            {
                distance[connection.To] = newDistance;
                predecessor[connection.To] = vertex;
            }
        }

        // Check for negative cycles
        foreach (var _ in Enumerable.Range(0, Vertices.Count - 1))
        foreach (var vertex in VisitedVertices(distance))
        foreach (var connection in AllConnections(vertex))
        {
            var newDistance = distance[vertex] + connection.Weight;
            if (newDistance < distance[connection.To])
            {
                distance[connection.To] = double.NegativeInfinity;
                predecessor[connection.To] = vertex;
            }
        }

        if (!distance.ContainsKey(v2))
            return (double.NaN, new List<int>());

        if (double.IsNegativeInfinity(distance[v2]))
            return (double.NegativeInfinity, new List<int>());

        var path = new List<int>();
        for (var current = v2; current != v1; current = predecessor[current])
            path.Add(current);
        path.Add(v1);
        path.Reverse();

        return (distance[v2], path);
    }
    
    // TODO: Floyd-Warshall algorithm

    /// <summary>
    /// Implementation of the A* algorithm.
    /// Finds the shortest path from the start vertex to the destination vertex using an estimation function.
    /// Uses a priority queue to find the next vertex to visit.
    /// Assumes that all edges have a positive weight. Otherwise, the result will be incorrect.
    /// 
    /// Time complexity: O(E*logV)
    /// Space complexity: O(V)
    /// </summary>
    /// <param name="v1">Start vertex</param>
    /// <param name="v2">Destination vertex</param>
    /// <param name="estimator">The function that estimates distance between two vertices</param>
    /// <returns>Shortest distance and path to the destination</returns>
    public (double Distance, List<int> Path) AStar(int v1, int v2, Func<int, int, int> estimator)
    {
        CheckVertices(v1, v2);

        var distanceFound = new HashSet<int> {v1};
        var candidates = new IndexedMinHeap<int, double> {(v1, estimator(v1, v2))};
        var predecessor = new Dictionary<int, int> {{v1, v1}};

        while (candidates.Count > 0)
        {
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

                return (minCandidate.Value, path);
            }

            foreach (var connection in UnaccountedConnections(minCandidate.Key, distanceFound))
            {
                var newDistance = minCandidate.Value - estimator(minCandidate.Key, v2) + connection.Weight + estimator(connection.To, v2);

                // Add new candidate
                if (!candidates.ContainsKey(connection.To))
                {
                    predecessor.Add(connection.To, minCandidate.Key);
                    candidates.Add((connection.To, newDistance));
                    continue;
                }

                // Update candidate
                if (candidates.ValueOfKey(connection.To) > newDistance)
                {
                    predecessor[connection.To] = minCandidate.Key;
                    candidates.UpdateKey(connection.To, newDistance);
                }
            }
        }

        return (-1, new List<int>());
    }

    #endregion

    #region Strongly Connected Components

    public abstract (int SccCount, Dictionary<int, int> SccDictionary) SccMap();

    public abstract List<List<int>> SccList();

    #endregion

    // Done
    #region Bridges and Articulation Points

    public abstract List<(int V1, int V2)> Bridges();

    public abstract HashSet<int> ArticulationPoints();

    #endregion

    #region Minimum Spanning Tree
    
    public abstract (double Weight, List<(int V1, int V2)> Edges) MinimumSpanningTree();

    #endregion

    #region Excentricity

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
            foreach (var connection in UnaccountedConnections(v, visited))
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

    #endregion
}