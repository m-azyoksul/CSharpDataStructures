using System.Collections.Generic;
using System.Linq;

namespace GraphQuiz1;

public static class Graph
{
    public static int[] Dijkstra(this List<List<(int To, int Weight)>> vertices, int v)
    {
        var distance = Enumerable.Repeat(-1, vertices.Count).ToArray();
        var candidates = new DataStructureSolution();
        candidates.Add(v, 0);

        while (candidates.Count() > 0) // O(1)
        {
            // Pop candidate
            var (minKey, minDistance) = candidates.PopMinValue(); // O(log n)
            distance[minKey] = minDistance;

            foreach (var connection in vertices[minKey])
            {
                if (distance[connection.To] != -1)
                    continue;

                var newDistance = minDistance + connection.Weight;

                // Add new candidate
                if (!candidates.ContainsKey(connection.To)) // O(1)
                    candidates.Add(connection.To, newDistance); // O(log n)

                // Update candidate
                else if (candidates.GetValue(connection.To) > newDistance) // O(1)
                    candidates.Update(connection.To, newDistance); // O(log n)
            }
        }

        return distance;
    }
}