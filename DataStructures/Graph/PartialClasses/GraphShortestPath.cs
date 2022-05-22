using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.Heap;

namespace DataStructures.Graph;

public abstract partial class Graph<TData>
{
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
    public (double, List<int>) Dijkstra(int v1, int v2)
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
    public (double, List<int>) BellmanFord(int v1, int v2)
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
    public (double, List<int>) AStar(int v1, int v2, Func<int, int, int> estimator)
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
}