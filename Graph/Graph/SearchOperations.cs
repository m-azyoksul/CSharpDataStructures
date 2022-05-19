using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph;

public abstract partial class Graph<TData>
{
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
    /// Recursive call for recursive depth first search that traverses all vertices reachable from v.
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
}

public partial class DirectedGraph<TData>
{
    /// <summary>
    /// Iterative breath first search that traverses all vertices and all edges reachable from v.
    ///
    /// Time Complexity: O(E)
    /// Space Complexity: O(E)
    /// </summary>
    /// <param name="v">Start vertex</param>
    /// <returns>All visited vertices and edges</returns>
    public override List<(int From, int To, bool Forwards)> BfsEdgeTraversal(int v)
    {
        if (!Vertices.ContainsKey(v))
            throw new ArgumentException("The vertex does not exist");

        var edgeList = new List<(int From, int To, bool Forwards)>();

        var visited = new HashSet<int> {v};
        var backtrackStack = new Stack<(int V, int P)>();
        var queue = new Queue<(int V, int P)> {(v, v)};

        while (queue.Count > 0)
        {
            var cur = queue.Dequeue();

            foreach (var connection in AllConnections(cur.V))
            {
                if (visited.Contains(connection.To))
                {
                    edgeList.Add((cur.V, connection.To, true));
                    edgeList.Add((connection.To, cur.V, false));
                    continue;
                }

                edgeList.Add((cur.V, connection.To, true));
                visited.Add(connection.To);
                queue.Enqueue((connection.To, cur.V));
            }

            backtrackStack.Push(cur);
        }

        while (backtrackStack.Count > 1)
        {
            var cur = backtrackStack.Pop();
            edgeList.Add((cur.V, cur.P, false));
        }

        return edgeList;
    }

    /// <summary>
    /// Recursive depth first search that traverses all vertices and all edges reachable from v.
    ///
    /// Time Complexity: O(E)
    /// Space Complexity: O(E)
    /// </summary>
    /// <param name="v">Start vertex</param>
    /// <returns>All visited vertices and edges</returns>
    public override List<(int From, int To, bool Forward)> DfsEdgeTraversal(int v)
    {
        if (!Vertices.ContainsKey(v))
            throw new ArgumentException("The vertex does not exist");

        var edgeList = new List<(int, int, bool)>();

        var visited = new HashSet<int> {v};
        DfsEdgeTraversal(v, visited, edgeList, v);
        return edgeList;
    }

    /// <summary>
    /// Recursive call for recursive depth first search that traverses all vertices and all edges reachable from v.
    /// </summary>
    private void DfsEdgeTraversal(int v, HashSet<int> visited, List<(int, int, bool)> edgeList, int parent)
    {
        foreach (var connection in AllConnections(v))
        {
            edgeList.Add((v, connection.To, true));

            if (visited.Contains(connection.To))
            {
                // Navigate and back
                edgeList.Add((connection.To, v, false));
                continue;
            }

            // Navigate
            visited.Add(connection.To);
            DfsEdgeTraversal(connection.To, visited, edgeList, v);

            // Backtrack
            edgeList.Add((connection.To, v, false));
        }
    }

    /// <summary>
    /// Iterative depth first search that traverses all vertices and all edges reachable from v.
    /// Uses a stack frame data structure to store vertex data in stack.
    ///
    /// Time Complexity: O(E)
    /// Space Complexity: O(E)
    /// </summary>
    /// <param name="v">Start vertex</param>
    /// <returns>All visited vertices and edges</returns>
    public override List<(int From, int To, bool Forward)> DfsEdgeTraversalIterative(int v)
    {
        CheckVertex(v);

        var edgeList = new List<(int From, int To, bool Forward)>();

        var visited = new HashSet<int>();
        var stack = new Stack<(int V, int P, int I)> {(v, v, 0)};

        while (stack.Count > 0)
        {
            var cur = stack.Pop();

            if (cur.I == 0)
            {
                // Init
                visited.Add(cur.V);
            }
            else if (cur.I < ConnectionCount(cur.V))
            {
                // Backtrack
            }

            // If all edges have been visited
            if (cur.I >= ConnectionCount(cur.V))
            {
                // Leave
                edgeList.Add((cur.V, cur.P, false));
                continue;
            }

            var connection = Vertices[cur.V].Connections[cur.I];

            // Push back
            stack.Push((cur.V, cur.P, cur.I + 1));
            edgeList.Add((cur.V, connection.To, true));

            if (visited.Contains(connection.To))
            {
                // Navigate and back
                edgeList.Add((connection.To, cur.V, false));
            }
            else
            {
                // Navigate
                stack.Push((connection.To, cur.V, 0));
            }
        }

        edgeList.RemoveAt(edgeList.Count - 1);

        return edgeList;
    }


    /// <summary>
    /// Iterative breath first search that traverses all vertices and all edges.
    ///
    /// Time Complexity: O(V+E)
    /// Space Complexity: O(E)
    /// </summary>
    /// <returns>All vertices and edges</returns>
    public override List<(int From, int To, bool Forwards)> BfsEdgeTraversal()
    {
        var edgeList = new List<(int From, int To, bool Forwards)>();

        var visited = new HashSet<int>();
        var backtrackStack = new Stack<(int V, int P)>();

        foreach (var v in Vertices.Keys)
        {
            if (visited.Contains(v))
                continue;
            visited.Add(v);

            var queue = new Queue<(int V, int P)> {(v, v)};

            while (queue.Count > 0)
            {
                var cur = queue.Dequeue();

                foreach (var connection in AllConnections(cur.V))
                {
                    if (visited.Contains(connection.To))
                    {
                        edgeList.Add((cur.V, connection.To, true));
                        edgeList.Add((connection.To, cur.V, false));
                        continue;
                    }

                    edgeList.Add((cur.V, connection.To, true));
                    visited.Add(connection.To);
                    queue.Enqueue((connection.To, cur.V));
                }

                backtrackStack.Push(cur);
            }

            while (backtrackStack.Count > 1)
            {
                var cur = backtrackStack.Pop();
                edgeList.Add((cur.V, cur.P, false));
            }

            backtrackStack.Pop();
        }

        return edgeList;
    }

    /// <summary>
    /// Recursive depth first search that traverses all vertices and all edges.
    ///
    /// Time Complexity: O(V+E)
    /// Space Complexity: O(E)
    /// </summary>
    /// <returns>All vertices and edges</returns>
    public override List<(int From, int To, bool Forward)> DfsEdgeTraversal()
    {
        var edgeList = new List<(int, int, bool)>();

        var visited = new HashSet<int>();

        foreach (var v in Vertices.Keys)
        {
            if (visited.Contains(v))
                continue;
            visited.Add(v);

            DfsEdgeTraversal(v, visited, edgeList, v);
        }

        return edgeList;
    }

    /// <summary>
    /// Iterative depth first search that traverses all vertices and all edges.
    /// Uses a stack frame data structure to store vertex data in stack.
    ///
    /// Time Complexity: O(V+E)
    /// Space Complexity: O(E)
    /// </summary>
    /// <returns>All vertices and edges</returns>
    public override List<(int From, int To, bool Forward)> DfsEdgeTraversalIterative()
    {
        var edgeList = new List<(int From, int To, bool Forward)>();

        var visited = new HashSet<int>();
        foreach (var v in Vertices.Keys)
        {
            if (visited.Contains(v))
                continue;
            visited.Add(v);

            var stack = new Stack<(int V, int P, int I)> {(v, v, 0)};

            while (stack.Count > 0)
            {
                var cur = stack.Pop();

                if (cur.I == 0)
                {
                    // Init
                    visited.Add(cur.V);
                }
                else if (cur.I < ConnectionCount(cur.V))
                {
                    // Backtrack
                }

                // If all edges have been visited
                if (cur.I >= ConnectionCount(cur.V))
                {
                    // Leave
                    edgeList.Add((cur.V, cur.P, false));
                    continue;
                }

                var connection = Vertices[cur.V].Connections[cur.I];

                // Push back
                stack.Push((cur.V, cur.P, cur.I + 1));
                edgeList.Add((cur.V, connection.To, true));

                if (visited.Contains(connection.To))
                {
                    // Navigate and back
                    edgeList.Add((connection.To, cur.V, false));
                }
                else
                {
                    // Navigate
                    stack.Push((connection.To, cur.V, 0));
                }
            }

            edgeList.RemoveAt(edgeList.Count - 1);
        }

        return edgeList;
    }
}

public partial class UndirectedGraph<TData>
{
    /// <summary>
    /// Iterative breath first search that traverses all vertices and all edges reachable from v.
    ///
    /// Time Complexity: O(E)
    /// Space Complexity: O(E)
    /// </summary>
    /// <param name="v">Start vertex</param>
    /// <returns>All visited vertices and edges</returns>
    public override List<(int From, int To, bool Forwards)> BfsEdgeTraversal(int v)
    {
        if (!Vertices.ContainsKey(v))
            throw new ArgumentException("The vertex does not exist");

        var visitedEdges = new HashSet<(int From, int To, bool Forwards)>();

        var visitedVertices = new HashSet<int> {v};
        var backtrackStack = new Stack<(int V, int P)>();
        var queue = new Queue<(int V, int P)> {(v, v)};

        while (queue.Count > 0)
        {
            var cur = queue.Dequeue();

            foreach (var connection in AllConnections(cur.V))
            {
                // If the edge was visited
                if (visitedEdges.Contains((connection.To, cur.V, true)))
                    continue;

                // If the vertex was visited
                if (visitedVertices.Contains(connection.To))
                {
                    visitedEdges.Add((cur.V, connection.To, true));
                    visitedEdges.Add((connection.To, cur.V, false));
                    continue;
                }

                visitedEdges.Add((cur.V, connection.To, true));
                visitedVertices.Add(connection.To);
                queue.Enqueue((connection.To, cur.V));
            }

            backtrackStack.Push(cur);
        }

        while (backtrackStack.Count > 1)
        {
            var cur = backtrackStack.Pop();
            visitedEdges.Add((cur.V, cur.P, false));
        }

        return visitedEdges.ToList();
    }

    /// <summary>
    /// Recursive depth first search that traverses all vertices and all edges reachable from v.
    ///
    /// Time Complexity: O(E)
    /// Space Complexity: O(E)
    /// </summary>
    /// <param name="v">Start vertex</param>
    /// <returns>All visited vertices and edges</returns>
    public override List<(int From, int To, bool Forward)> DfsEdgeTraversal(int v)
    {
        if (!Vertices.ContainsKey(v))
            throw new ArgumentException("The vertex does not exist");

        var visitedEdges = new HashSet<(int From, int To, bool Forwards)>();

        var visited = new HashSet<int> {v};
        DfsEdgeTraversal(v, visited, visitedEdges);
        return visitedEdges.ToList();
    }

    /// <summary>
    /// Recursive call for recursive depth first search that traverses all vertices and all edges reachable from v.
    /// </summary>
    private void DfsEdgeTraversal(int v, HashSet<int> visited, HashSet<(int From, int To, bool Forwards)> edgeList)
    {
        foreach (var connection in AllConnections(v))
        {
            if (edgeList.Contains((connection.To, v, true)))
                continue;

            edgeList.Add((v, connection.To, true));

            if (visited.Contains(connection.To))
            {
                // Navigate and back
                edgeList.Add((connection.To, v, false));
                continue;
            }

            // Navigate
            visited.Add(connection.To);
            DfsEdgeTraversal(connection.To, visited, edgeList);

            // Backtrack
            edgeList.Add((connection.To, v, false));
        }
    }

    /// <summary>
    /// Iterative depth first search that traverses all vertices and all edges reachable from v.
    /// Uses a stack frame data structure to store vertex data in stack.
    ///
    /// Time Complexity: O(E)
    /// Space Complexity: O(E)
    /// </summary>
    /// <param name="v">Start vertex</param>
    /// <returns>All visited vertices and edges</returns>
    public override List<(int From, int To, bool Forward)> DfsEdgeTraversalIterative(int v)
    {
        if (!Vertices.ContainsKey(v))
            throw new ArgumentException("The vertex does not exist");

        var visitedEdges = new HashSet<(int From, int To, bool Forwards)>();

        var visitedVertices = new HashSet<int>();
        var stack = new Stack<(int V, int P, int I)> {(v, v, 0)};

        while (stack.Count > 0)
        {
            var cur = stack.Pop();

            if (cur.I == 0)
            {
                // Init
                visitedVertices.Add(cur.V);
            }
            else if (cur.I < ConnectionCount(cur.V))
            {
                // Backtrack
            }

            // If all edges have been visited
            if (cur.I >= ConnectionCount(cur.V) ||
                cur.I + 1 == ConnectionCount(cur.V) &&
                visitedEdges.Contains((Vertices[cur.V].Connections[cur.I].To, cur.V, true)))
            {
                // Leave
                visitedEdges.Add((cur.V, cur.P, false));
                continue;
            }

            var connection = Vertices[cur.V].Connections[cur.I];
            var newI = cur.I + 1;
            if (connection.To == cur.P)
            {
                connection = Vertices[cur.V].Connections[cur.I + 1];
                newI++;
            }

            // Push back
            stack.Push((cur.V, cur.P, newI));

            // If the edge was visited
            if (visitedEdges.Contains((connection.To, cur.V, true)))
                continue;

            visitedEdges.Add((cur.V, connection.To, true));

            if (visitedVertices.Contains(connection.To))
            {
                // Navigate and back
                visitedEdges.Add((connection.To, cur.V, false));
            }
            else
            {
                // Navigate
                stack.Push((connection.To, cur.V, 0));
            }
        }

        var edgeList = visitedEdges.ToList();
        edgeList.RemoveAt(visitedEdges.Count - 1);
        return edgeList;
    }


    /// <summary>
    /// Iterative breath first search that traverses all vertices and all edges.
    ///
    /// Time Complexity: O(V+E)
    /// Space Complexity: O(E)
    /// </summary>
    /// <returns>All vertices and edges</returns>
    public override List<(int From, int To, bool Forwards)> BfsEdgeTraversal()
    {
        var visitedEdges = new HashSet<(int From, int To, bool Forwards)>();

        var visitedVertices = new HashSet<int>();
        var backtrackStack = new Stack<(int V, int P)>();

        foreach (var v in Vertices.Keys)
        {
            if (visitedVertices.Contains(v))
                continue;
            visitedVertices.Add(v);

            var queue = new Queue<(int V, int P)> {(v, v)};

            while (queue.Count > 0)
            {
                var cur = queue.Dequeue();

                foreach (var connection in AllConnections(cur.V))
                {
                    // If the edge was visited
                    if (visitedEdges.Contains((connection.To, cur.V, true)))
                        continue;

                    // If the vertex was visited
                    if (visitedVertices.Contains(connection.To))
                    {
                        visitedEdges.Add((cur.V, connection.To, true));
                        visitedEdges.Add((connection.To, cur.V, false));
                        continue;
                    }

                    visitedEdges.Add((cur.V, connection.To, true));
                    visitedVertices.Add(connection.To);
                    queue.Enqueue((connection.To, cur.V));
                }

                backtrackStack.Push(cur);
            }

            while (backtrackStack.Count > 1)
            {
                var cur = backtrackStack.Pop();
                visitedEdges.Add((cur.V, cur.P, false));
            }
        }

        return visitedEdges.ToList();
    }

    /// <summary>
    /// Recursive depth first search that traverses all vertices and all edges.
    ///
    /// Time Complexity: O(V+E)
    /// Space Complexity: O(E)
    /// </summary>
    /// <returns>All vertices and edges</returns>
    public override List<(int From, int To, bool Forward)> DfsEdgeTraversal()
    {
        var visitedEdges = new HashSet<(int From, int To, bool Forwards)>();

        var visited = new HashSet<int>();

        foreach (var v in Vertices.Keys)
        {
            if (visited.Contains(v))
                continue;
            visited.Add(v);

            DfsEdgeTraversal(v, visited, visitedEdges);
        }

        return visitedEdges.ToList();
    }

    /// <summary>
    /// Iterative depth first search that traverses all vertices and all edges.
    /// Uses a stack frame data structure to store vertex data in stack.
    ///
    /// Time Complexity: O(V+E)
    /// Space Complexity: O(E)
    /// </summary>
    /// <returns>All vertices and edges</returns>
    public override List<(int From, int To, bool Forward)> DfsEdgeTraversalIterative()
    {
        var visitedEdges = new List<(int From, int To, bool Forwards)>();

        var visitedVertices = new HashSet<int>();

        foreach (var v in Vertices.Keys)
        {
            if (visitedVertices.Contains(v))
                continue;

            var stack = new Stack<(int V, int P, int I)> {(v, v, 0)};

            while (stack.Count > 0)
            {
                var cur = stack.Pop();

                if (cur.I == 0)
                {
                    // Init
                    visitedVertices.Add(cur.V);
                }
                else if (cur.I < ConnectionCount(cur.V))
                {
                    // Backtrack
                }

                // If all edges have been visited
                if (cur.I >= ConnectionCount(cur.V) ||
                    cur.I + 1 == ConnectionCount(cur.V) &&
                    visitedEdges.Contains((Vertices[cur.V].Connections[cur.I].To, cur.V, true)))
                {
                    // Leave
                    visitedEdges.Add((cur.V, cur.P, false));
                    continue;
                }

                var connection = Vertices[cur.V].Connections[cur.I];
                var newI = cur.I + 1;
                if (connection.To == cur.P)
                {
                    connection = Vertices[cur.V].Connections[cur.I + 1];
                    newI++;
                }

                // Push back
                stack.Push((cur.V, cur.P, newI));

                // If the edge was visited
                if (visitedEdges.Contains((connection.To, cur.V, true)))
                    continue;

                visitedEdges.Add((cur.V, connection.To, true));

                if (visitedVertices.Contains(connection.To))
                {
                    // Navigate and back
                    visitedEdges.Add((connection.To, cur.V, false));
                }
                else
                {
                    // Navigate
                    stack.Push((connection.To, cur.V, 0));
                }
            }

            // Since the parent of v is appointed as v, we need to remove (v, v) from visited edges
            visitedEdges.RemoveAt(visitedEdges.Count - 1);
        }

        return visitedEdges;
    }
}