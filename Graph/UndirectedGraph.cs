using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph;

public class UndirectedGraph<TVertexData> : Graph<TVertexData>
{
    #region Constructors

    public UndirectedGraph()
    {
    }

    public UndirectedGraph(List<(int, int)> edges) : base(edges)
    {
        // Find all vertices
        foreach (var edge in edges)
        {
            if (!Vertices.ContainsKey(edge.Item1))
                Vertices.Add(edge.Item1, Vertex<TVertexData>.Empty());

            if (!Vertices.ContainsKey(edge.Item2))
                Vertices.Add(edge.Item2, Vertex<TVertexData>.Empty());

            Vertices[edge.Item1].Connections.Add(edge.Item2);
            Vertices[edge.Item2].Connections.Add(edge.Item1);
        }
    }

    public UndirectedGraph(Dictionary<int, Vertex<TVertexData>> vertices) : base(vertices)
    {
        // TODO
        // Check if every vertex is the neighbour its neighbours
        // This graph also allows for self-loops and multiple edges between two vertices
        foreach (var vertex in vertices)
        {
            foreach (var neighbour in vertex.Value.Connections)
            {
                if (!vertices[neighbour].Connections.Contains(vertex.Key))
                    throw new ArgumentException("Graph is not undirected");
            }
        }
    }

    #endregion

    #region Elementary operations

    public override void AddEdge(int v1, int v2)
    {
        if (!Vertices.ContainsKey(v1))
            Vertices.Add(v1, Vertex<TVertexData>.Empty());
        if (!Vertices.ContainsKey(v2))
            Vertices.Add(v2, Vertex<TVertexData>.Empty());

        Vertices[v1].Connections.Add(v2);
        Vertices[v2].Connections.Add(v1);
    }

    public override void RemoveEdge(int v1, int v2)
    {
        if (!Vertices.ContainsKey(v1) || !Vertices.ContainsKey(v2))
            throw new ArgumentException("The vertex does not exist");
        if (!Vertices[v1].Connections.Contains(v2) || !Vertices[v2].Connections.Contains(v1))
            throw new ArgumentException("The edge does not exist");

        Vertices[v2].Connections.Remove(v1);
        Vertices[v1].Connections.Remove(v2);
    }

    public override void RemoveVertex(int v)
    {
        if (!Vertices.ContainsKey(v))
            throw new ArgumentException("The vertex does not exist");

        foreach (var vertex in Vertices[v].Connections)
            Vertices[vertex].Connections.RemoveAll(neighbor => neighbor == v);

        Vertices.Remove(v);
    }

    protected override bool HasEdge(int v1, int v2)
    {
        if (!Vertices.ContainsKey(v1) || !Vertices.ContainsKey(v2))
            throw new ArgumentException("The vertex does not exist");

        return Vertices[v1].Connections.Contains(v2) && Vertices[v2].Connections.Contains(v1);
    }

    public override int EdgeCount()
    {
        return Vertices.SelectMany(v => v.Value.Connections).Count() / 2;
    }

    #endregion

    #region Traversal

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
        var queue = new Queue<(int V, int P)> {(v, v)};
        var backtrackStack = new Stack<(int V, int P)>();

        while (queue.Count > 0)
        {
            var cur = queue.Dequeue();

            foreach (var neighbour in AllConnections(cur.V))
            {
                // If the edge was visited
                if (visitedEdges.Contains((neighbour, cur.V, true)))
                    continue;

                // If the vertex was visited
                if (visitedVertices.Contains(neighbour))
                {
                    visitedEdges.Add((cur.V, neighbour, true));
                    visitedEdges.Add((neighbour, cur.V, false));
                    continue;
                }

                visitedEdges.Add((cur.V, neighbour, true));
                visitedVertices.Add(neighbour);
                queue.Enqueue((neighbour, cur.V));
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
        foreach (var neighbour in AllConnections(v))
        {
            if (edgeList.Contains((neighbour, v, true)))
                continue;

            edgeList.Add((v, neighbour, true));

            if (visited.Contains(neighbour))
            {
                // Navigate and back
                edgeList.Add((neighbour, v, false));
                continue;
            }

            // Navigate
            visited.Add(neighbour);
            DfsEdgeTraversal(neighbour, visited, edgeList);

            // Backtrack
            edgeList.Add((neighbour, v, false));
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
                visitedEdges.Contains((Vertices[cur.V].Connections[cur.I], cur.V, true)))
            {
                // Leave
                visitedEdges.Add((cur.V, cur.P, false));
                continue;
            }

            var neighbor = Vertices[cur.V].Connections[cur.I];
            if (neighbor == cur.P)
                neighbor = Vertices[cur.V].Connections[cur.I + 1];

            // Push back
            stack.Push((cur.V, cur.P, cur.I + 1));

            // If the edge was visited
            if (visitedEdges.Contains((neighbor, cur.V, true)))
                continue;

            visitedEdges.Add((cur.V, neighbor, true));

            if (visitedVertices.Contains(neighbor))
            {
                // Navigate and back
                visitedEdges.Add((neighbor, cur.V, false));
            }
            else
            {
                // Navigate
                stack.Push((neighbor, cur.V, 0));
            }
        }

        var edgeList = visitedEdges.ToList();
        edgeList.RemoveAt(visitedEdges.Count - 1);
        return edgeList;
    }

    #endregion

    /// <summary>
    /// Time complexity: O(V)
    /// Space complexity: O(1)
    /// </summary>
    public override bool IsEulerian()
    {
        return Vertices.All(vertex => vertex.Value.Connections.Count % 2 == 0);
    }

    /// <summary>
    /// Search from a vertex.
    ///
    /// Time complexity: O(V + E)
    /// Space complexity: O(V)
    /// </summary>
    /// <returns></returns>
    public override bool IsConnected()
    {
        if (VertexCount() <= 1)
            return true;

        var firstVertex = Vertices.First().Key;

        var visited = new HashSet<int> {firstVertex};
        var queue = new Queue<int> {firstVertex};

        while (queue.Count > 0)
        {
            var v = queue.Dequeue();

            // Enqueue neighbors
            foreach (var neighbor in UnvisitedConnections(v, visited))
            {
                queue.Enqueue(neighbor);
                visited.Add(neighbor);
            }
        }

        return visited.Count == Vertices.Count;
    }

    /// <summary>
    /// Time complexity: O(V + E)
    /// Space complexity: O(V)
    /// </summary>
    public override bool IsTree()
    {
        if (VertexCount() <= 1)
            return true;

        var firstVertex = Vertices.First().Key;

        var predecessor = new Dictionary<int, int> {{firstVertex, -1}};
        var queue = new Queue<int> {firstVertex};

        while (queue.Count > 0)
        {
            var v = queue.Dequeue();

            // Enqueue neighbors
            foreach (var neighbor in AllConnections(v))
            {
                // If the neighbor is visited before and is not the predecessor, the graph is not a tree
                if (predecessor.ContainsKey(neighbor))
                {
                    if (predecessor[v] != neighbor)
                        return false;
                    continue;
                }

                queue.Enqueue(neighbor);
                predecessor[neighbor] = v;
            }
        }

        return VertexCount() == predecessor.Count;
    }

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

        foreach (var vertex in Vertices)
        {
            if (visited.Contains(vertex.Key))
                continue;

            BridgesDfs(vertex.Key, visited, low, timeOfVisit, bridges, ref time);
        }

        return bridges;
    }

    private void BridgesDfs(int v, HashSet<int> visited, Dictionary<int, int> low, Dictionary<int, int> timeOfVisit, List<(int V1, int V2)> bridges, ref int time, int parent = -1)
    {
        visited.Add(v);
        low[v] = timeOfVisit[v] = time;
        time++;

        foreach (var to in Vertices[v].Connections)
        {
            if (to == parent)
                continue;

            if (visited.Contains(to))
                low[v] = Math.Min(low[v], timeOfVisit[to]);
            else
            {
                BridgesDfs(to, visited, low, timeOfVisit, bridges, ref time, v);
                if (low[to] > timeOfVisit[v])
                    bridges.Add((v, to));
                low[v] = Math.Min(low[v], low[to]);
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
                    var neighbour = Vertices[current.vertex].Connections[current.visitedNCount - 1];
                    if (neighbour != current.parent)
                    {
                        if (low[neighbour] > timeOfVisit[current.vertex])
                            bridges.Add((current.vertex, neighbour));

                        low[current.vertex] = Math.Min(low[current.vertex], low[neighbour]);
                    }
                }

                // Push next unvisited neighbour to the stack
                if (current.visitedNCount < Vertices[current.vertex].Connections.Count)
                {
                    // Push the current vertex back to the stack
                    stack.Push((current.vertex, current.parent, current.visitedNCount + 1));

                    // If the neighbour is the parent, skip it
                    var neighbour = Vertices[current.vertex].Connections[current.visitedNCount];
                    if (neighbour == current.parent)
                        continue;

                    // If the neighbour is already visited, set the low value
                    if (visited.Contains(neighbour))
                        low[current.vertex] = Math.Min(low[current.vertex], timeOfVisit[neighbour]);

                    // If the neighbour is not visited, push it to the stack
                    else
                        stack.Push((neighbour, current.vertex, 0));
                }
            }
        }

        return bridges;
    }

    public (int, Dictionary<int, int>) ConnectedComponentMap()
    {
        var components = new Dictionary<int, int>();
        var componentIndex = 0;

        foreach (var vertex in Vertices.Keys)
        {
            if (components.ContainsKey(vertex))
                continue;

            var queue = new Queue<int> {vertex};

            while (queue.Count > 0)
            {
                var v = queue.Dequeue();

                foreach (var neighbor in UnvisitedConnections(v, components))
                {
                    queue.Enqueue(neighbor);
                    components[v] = componentIndex;
                }
            }

            componentIndex++;
        }

        return (componentIndex, components);
    }

    public List<List<int>> ConnectedComponentList()
    {
        var components = new List<List<int>>();

        foreach (var vertex in Vertices.Keys)
        {
            var visited = new HashSet<int>();

            if (visited.Contains(vertex))
                continue;

            var queue = new Queue<int> {vertex};

            while (queue.Count > 0)
            {
                var v = queue.Dequeue();

                foreach (var neighbor in UnvisitedConnections(v, visited))
                {
                    queue.Enqueue(neighbor);
                    visited.Add(v);
                }
            }

            components.Add(visited.ToList());
        }

        return components;
    }
}