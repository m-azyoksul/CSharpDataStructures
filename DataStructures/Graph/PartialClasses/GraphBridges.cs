using System;
using System.Collections.Generic;

namespace DataStructures.Graph;

public partial class DirectedGraph<TData>
{
    public override List<(int V1, int V2)> Bridges()
    {
        // TODO: Academic paper for the algorithm: https://stackoverflow.com/a/17107586/7279624
        throw new NotImplementedException();
    }
}

public partial class UndirectedGraph<TData>
{
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

        foreach (var vertex in UnvisitedVertices(visited))
        {
            if (visited.Contains(vertex))
                continue;

            BridgesDfs(vertex, visited, low, timeOfVisit, bridges, ref time, -1);
        }

        return bridges;
    }

    private void BridgesDfs(int v, HashSet<int> visited, Dictionary<int, int> low, Dictionary<int, int> timeOfVisit, List<(int V1, int V2)> bridges, ref int time, int parent)
    {
        visited.Add(v);
        low[v] = timeOfVisit[v] = time;
        time++;

        foreach (var to in AllConnections(v))
        {
            if (to.To == parent)
                continue;

            if (visited.Contains(to.To))
                low[v] = Math.Min(low[v], timeOfVisit[to.To]);
            else
            {
                BridgesDfs(to.To, visited, low, timeOfVisit, bridges, ref time, v);
                if (low[to.To] > timeOfVisit[v])
                    bridges.Add((v, to.To));
                low[v] = Math.Min(low[v], low[to.To]);
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
                    var connection = Vertices[current.vertex].Connections[current.visitedNCount - 1];
                    if (connection.To != current.parent)
                    {
                        if (low[connection.To] > timeOfVisit[current.vertex])
                            bridges.Add((current.vertex, connection.To));

                        low[current.vertex] = Math.Min(low[current.vertex], low[connection.To]);
                    }
                }

                // Push next unvisited neighbour to the stack
                if (current.visitedNCount < Vertices[current.vertex].Connections.Count)
                {
                    // Push the current vertex back to the stack
                    stack.Push((current.vertex, current.parent, current.visitedNCount + 1));

                    // If the neighbour is the parent, skip it
                    var connection = Vertices[current.vertex].Connections[current.visitedNCount];
                    if (connection.To == current.parent)
                        continue;

                    // If the neighbour is already visited, set the low value
                    if (visited.Contains(connection.To))
                        low[current.vertex] = Math.Min(low[current.vertex], timeOfVisit[connection.To]);

                    // If the neighbour is not visited, push it to the stack
                    else
                        stack.Push((connection.To, current.vertex, 0));
                }
            }
        }

        return bridges;
    }
}