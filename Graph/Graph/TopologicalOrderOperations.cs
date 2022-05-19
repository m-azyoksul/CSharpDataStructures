using System.Collections.Generic;
using System.Linq;

namespace Graph;

public partial class DirectedGraph<TData>
{
    public List<int> TopologicalOrder(int v)
    {
        CheckVertex(v);
        var visited = new HashSet<int>();
        var stack = new Stack<int>();

        TopologicalOrderDfs(v, visited, stack);

        return stack.ToList();
    }

    public List<int> TopologicalOrder()
    {
        var visited = new HashSet<int>();
        var stack = new Stack<int>();

        foreach (var vertex in Vertices.Keys.Where(vertex => !visited.Contains(vertex)))
            TopologicalOrderDfs(vertex, visited, stack);

        return stack.ToList();
    }

    private void TopologicalOrderDfs(int v, HashSet<int> visited, Stack<int> stack)
    {
        visited.Add(v);
        foreach (var connection in UnaccountedConnections(v, visited))
            TopologicalOrderDfs(connection.To, visited, stack);

        stack.Push(v);
    }

    public List<int> BackwardsTopologicalOrder(int v)
    {
        var transposed = Transpose();
        var ordering = transposed.TopologicalOrder(v);
        ordering.Reverse();
        return ordering;
    }

    public List<int> BackwardsTopologicalOrder()
    {
        var transposed = Transpose();
        var ordering = transposed.TopologicalOrder();
        ordering.Reverse();
        return ordering;
    }

    public Dictionary<int, double> TopologicalOrderSingleSourceShortestPath(int v)
    {
        var topologicalOrder = TopologicalOrder(v);
        if (topologicalOrder.Count == 0)
            return new Dictionary<int, double>();

        var remainingVertices = new HashSet<int>(topologicalOrder);
        var distances = new Dictionary<int, double> {[topologicalOrder[0]] = 0};

        foreach (var vertex in topologicalOrder)
        {
            foreach (var connection in AccountedConnections(vertex, remainingVertices))
            {
                // Relax edge
                var newDistance = distances[vertex] + connection.Weight;
                if (!distances.TryGetValue(connection.To, out var currentDistance) || currentDistance > newDistance)
                    distances[connection.To] = newDistance;
            }
        }

        return distances;
    }

    public Dictionary<int, double> TopologicalOrderSingleSourceShortestPath()
    {
        var topologicalOrder = TopologicalOrder();

        var remainingVertices = new HashSet<int>(topologicalOrder);
        var distances = new Dictionary<int, double>();

        foreach (var vertex in topologicalOrder)
        {
            if (!distances.ContainsKey(vertex))
                distances[vertex] = 0;

            foreach (var connection in AccountedConnections(vertex, remainingVertices))
            {
                // Relax edge
                var newDistance = distances[vertex] + connection.Weight;
                if (!distances.TryGetValue(connection.To, out var currentDistance) || currentDistance > newDistance)
                    distances[connection.To] = newDistance;
            }
        }

        return distances;
    }
}