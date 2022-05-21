using System.Collections.Generic;
using System.Linq;

namespace Graph;

public partial class DirectedGraph<TData>
{
    /// <summary>
    /// Calculates all reachable vertices from every vertex in the graph.
    ///
    /// Time complexity: O(V^2)
    /// Space complexity: O(V^2)
    /// </summary>
    /// <returns>A dictionary of all reachable vertices from every vertex in the graph.</returns>
    public Dictionary<int, List<int>> AllReachableVertices()
    {
        var allReachable = new Dictionary<int, List<int>>();
        var visited = new HashSet<int>();

        // Dfs
        foreach (var vertex in Vertices.Keys)
        {
            if (visited.Contains(vertex))
                continue;

            var currentNodes = new List<int>();

            Dfs(vertex, visited, currentNodes, allReachable);
        }

        return allReachable;
    }

    /// <summary>
    /// Recursive call for <see cref="AllReachableVertices"/>
    /// </summary>
    private void Dfs(int v, HashSet<int> visited, List<int> currentNodes, Dictionary<int, List<int>> allReachable)
    {
        visited.Add(v);
        currentNodes.Add(v);

        foreach (var connection in AllConnections(v))
        {
            // If this vertex has been completely discovered
            if (allReachable.ContainsKey(connection.To))
                currentNodes.AddRange(allReachable[connection.To]);

            // If this vertex has not been visited
            else if (!visited.Contains(connection.To))
                Dfs(connection.To, visited, currentNodes, allReachable);
        }

        allReachable[v] = currentNodes.Skip(currentNodes.IndexOf(v)).Distinct().ToList();
    }
}