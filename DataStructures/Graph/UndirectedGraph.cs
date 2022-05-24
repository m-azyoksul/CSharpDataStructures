using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Graph;

public class UndirectedGraph<TData> : Graph<TData>
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
                Vertices.Add(edge.Item1, Vertex<TData>.Empty());

            if (!Vertices.ContainsKey(edge.Item2))
                Vertices.Add(edge.Item2, Vertex<TData>.Empty());

            Vertices[edge.Item1].Connections.Add(edge.Item2);
            Vertices[edge.Item2].Connections.Add(edge.Item1);
        }
    }

    public UndirectedGraph(Dictionary<int, Vertex<TData>> vertices) : base(vertices)
    {
        // TODO
        // Check if every vertex is the neighbour its neighbours
        // This graph also allows for self-loops and multiple edges between two vertices
        foreach (var vertex in vertices)
        {
            foreach (var connection in vertex.Value.Connections)
            {
                if (!vertices[connection.To].Connections.Contains(vertex.Key))
                    throw new ArgumentException("Graph is not undirected");
            }
        }
    }

    #endregion

    #region Elementary Operations

    public override void AddEdge(int v1, int v2)
    {
        if (!Vertices.ContainsKey(v1))
            Vertices.Add(v1, Vertex<TData>.Empty());
        if (!Vertices.ContainsKey(v2))
            Vertices.Add(v2, Vertex<TData>.Empty());

        Vertices[v1].Connections.Add(v2);
        Vertices[v2].Connections.Add(v1);
    }

    public override void AddEdge(int v1, int v2, int weight)
    {
        if (!Vertices.ContainsKey(v1))
            Vertices.Add(v1, Vertex<TData>.Empty());
        if (!Vertices.ContainsKey(v2))
            Vertices.Add(v2, Vertex<TData>.Empty());

        Vertices[v1].Connections.Add(new Connection(v2, weight));
        Vertices[v2].Connections.Add(new Connection(v1, weight));
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

    public override void AddVertex(int v, List<Connection> connections)
    {
        if (!Vertices.ContainsKey(v))
            throw new ArgumentException("The vertex already exists");

        Vertices.Add(v, new Vertex<TData>(connections));

        // Add backwards connections
        foreach (var connection in connections)
        {
            if (!Vertices.ContainsKey(connection.To))
                Vertices.Add(connection.To, Vertex<TData>.Empty());

            Vertices[connection.To].Connections.Add(v);
        }
    }

    public override void RemoveVertex(int v)
    {
        if (!Vertices.ContainsKey(v))
            throw new ArgumentException("The vertex does not exist");

        foreach (var connection in Vertices[v].Connections)
            Vertices[connection.To].Connections.RemoveAll(c => c.To == v);

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

    #region Ungrouped

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
            foreach (var neighbor in UnaccountedConnections(v, visited))
            {
                queue.Enqueue(neighbor.To);
                visited.Add(neighbor.To);
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
                if (predecessor.ContainsKey(neighbor.To))
                {
                    if (predecessor[v] != neighbor.To)
                        return false;
                    continue;
                }

                queue.Enqueue(neighbor.To);
                predecessor[neighbor.To] = v;
            }
        }

        return VertexCount() == predecessor.Count;
    }

    #endregion

    #region Search

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

            backtrackStack.Pop();
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

    #endregion

    // Done

    #region Shortest Path

    #endregion

    // Done

    #region Strongly Connected Components

    /// <summary>
    /// Finds all strongly connected components using bfs.
    ///
    /// Time complexity: O(V+E)
    /// Space complexity: O(V)
    /// </summary>
    /// <returns>(The number of strongly connected components, for each vertex (id of vertex, index of scc))</returns>
    public override (int SccCount, Dictionary<int, int> SccDictionary) SccMap()
    {
        var vertexComponent = new Dictionary<int, int>();
        var componentIndex = 0;

        foreach (var vertex in Vertices.Keys)
        {
            if (vertexComponent.ContainsKey(vertex))
                continue;

            var queue = new Queue<int> {vertex};

            while (queue.Count > 0)
            {
                var v = queue.Dequeue();
                vertexComponent[v] = componentIndex;

                foreach (var connection in UnaccountedConnections(v, vertexComponent))
                {
                    queue.Enqueue(connection.To);
                }
            }

            componentIndex++;
        }

        return (componentIndex, vertexComponent);
    }

    /// <summary>
    /// Finds all strongly connected components using bfs.
    ///
    /// Time complexity: O(V+E)
    /// Space complexity: O(V)
    /// </summary>
    /// <returns>The list of strongly connected components</returns>
    public override List<List<int>> SccList()
    {
        var components = new List<List<int>>();
        var visited = new HashSet<int>();

        foreach (var vertex in Vertices.Keys)
        {
            if (visited.Contains(vertex))
                continue;

            var component = new HashSet<int>();

            var queue = new Queue<int> {vertex};

            while (queue.Count > 0)
            {
                var v = queue.Dequeue();
                visited.Add(v);
                component.Add(v);

                foreach (var connection in UnaccountedConnections(v, visited))
                    queue.Enqueue(connection.To);
            }

            components.Add(component.ToList());
        }

        return components;
    }

    #endregion

    #region Bridges and Articulation Points

    /// <summary>
    /// Finds all bridges (critical edges) in the graph.
    /// Uses recursive DFS.
    ///
    /// Time complexity: O(V+E)
    /// Space complexity: O(V)
    /// </summary>
    /// <returns>The list of bridges</returns>
    public override List<(int V1, int V2)> Bridges()
    {
        var bridges = new List<(int V1, int V2)>();

        var visited = new HashSet<int>();
        var low = new Dictionary<int, int>();
        var discoveryTime = new Dictionary<int, int>();
        var time = 0;

        foreach (var vertex in UnvisitedVertices(visited))
        {
            if (visited.Contains(vertex))
                continue;

            BridgesDfs(vertex, visited, low, discoveryTime, bridges, ref time, -1);
        }

        return bridges;
    }

    /// <summary>
    /// Recursive call for <see cref="Bridges"/> that finds all bridges.
    /// </summary>
    private void BridgesDfs(int v, HashSet<int> visited, Dictionary<int, int> low, Dictionary<int, int> discoveryTime, List<(int V1, int V2)> bridges, ref int time, int parent)
    {
        visited.Add(v);
        low[v] = discoveryTime[v] = time++;

        foreach (var to in AllConnections(v))
        {
            if (to.To == parent)
                continue;

            if (visited.Contains(to.To))
                low[v] = Math.Min(low[v], discoveryTime[to.To]);
            else
            {
                BridgesDfs(to.To, visited, low, discoveryTime, bridges, ref time, v);
                if (low[to.To] > discoveryTime[v])
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
    /// Time complexity: O(V+E)
    /// Space complexity: O(V)
    /// </summary>
    /// <returns>The list of bridges</returns>
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

    public override HashSet<int> ArticulationPoints()
    {
        var articulationPoints = new HashSet<int>();

        var visited = new HashSet<int>();
        var low = new Dictionary<int, int>();
        var discoveryTime = new Dictionary<int, int>();
        var time = 0;

        foreach (var vertex in UnvisitedVertices(visited))
        {
            if (visited.Contains(vertex))
                continue;

            ArticulationPointsDfs(vertex, visited, low, discoveryTime, articulationPoints, ref time, -1);
        }

        return articulationPoints;
    }

    /// <summary>
    /// Recursive call for <see cref="ArticulationPoints"/> that finds all articulation points.
    /// </summary>
    private void ArticulationPointsDfs(int v, HashSet<int> visited, Dictionary<int, int> low, Dictionary<int, int> discoveryTime, HashSet<int> articulationPoints, ref int time, int parent)
    {
        visited.Add(v);
        low[v] = discoveryTime[v] = time++;
        var components = 0;

        foreach (var to in AllConnections(v))
        {
            if (to.To == parent)
                continue;

            if (visited.Contains(to.To))
                low[v] = Math.Min(low[v], discoveryTime[to.To]);
            else
            {
                components++;
                ArticulationPointsDfs(to.To, visited, low, discoveryTime, articulationPoints, ref time, v);
                if (parent == -1 && components > 1 || parent != -1 && discoveryTime[v] <= low[to.To])
                    articulationPoints.Add(v);
                low[v] = Math.Min(low[v], low[to.To]);
            }
        }
    }

    #endregion

    #region Minimum Spanning Tree

    public override (double Weight, List<(int V1, int V2)> Edges) MinimumSpanningTree()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Excentricity

    #endregion
}