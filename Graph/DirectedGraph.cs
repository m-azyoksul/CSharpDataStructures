using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph;

public class DirectedGraph<TVertexData> : Graph<TVertexData>
{
    #region Constructors

    public DirectedGraph()
    {
    }

    public DirectedGraph(List<(int From, int To)> edges) : base(edges)
    {
        // Find all vertices
        foreach (var edge in edges)
        {
            if (!Vertices.ContainsKey(edge.From))
                Vertices.Add(edge.From, Vertex<TVertexData>.Empty());

            if (!Vertices.ContainsKey(edge.To))
                Vertices.Add(edge.To, Vertex<TVertexData>.Empty());

            Vertices[edge.From].Connections.Add(edge.To);
        }
    }

    public DirectedGraph(Dictionary<int, Vertex<TVertexData>> vertices) : base(vertices)
    {
    }

    #endregion

    #region Elementary Operations

    public override void AddEdge(int v1, int v2)
    {
        if (!Vertices.ContainsKey(v1))
            Vertices.Add(v1, Vertex<TVertexData>.Empty());
        if (!Vertices.ContainsKey(v2))
            Vertices.Add(v2, Vertex<TVertexData>.Empty());

        Vertices[v1].Connections.Add(v2);
    }

    public override void AddEdge(int v1, int v2, int weight)
    {
        if (!Vertices.ContainsKey(v1))
            Vertices.Add(v1, Vertex<TVertexData>.Empty());
        if (!Vertices.ContainsKey(v2))
            Vertices.Add(v2, Vertex<TVertexData>.Empty());

        Vertices[v1].Connections.Add(new Connection(v2, weight));
    }

    public override void RemoveEdge(int v1, int v2)
    {
        if (!Vertices.ContainsKey(v1) || !Vertices.ContainsKey(v2))
            throw new ArgumentException("The vertex does not exist");
        if (!Vertices[v1].Connections.Contains(v2))
            throw new ArgumentException("The edge does not exist");

        Vertices[v1].Connections.Remove(v2);
    }

    public override void RemoveVertex(int v)
    {
        if (!Vertices.ContainsKey(v))
            throw new ArgumentException("The vertex does not exist");

        Vertices.Remove(v);
    }

    protected override bool HasEdge(int v1, int v2)
    {
        if (!Vertices.ContainsKey(v1) || !Vertices.ContainsKey(v2))
            throw new ArgumentException("The vertex does not exist");

        return Vertices[v1].Connections.Contains(v2);
    }

    public override int EdgeCount()
    {
        return Vertices.SelectMany(v => v.Value.Connections).Count();
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

        var edgeList = new List<(int From, int To, bool Forwards)>();

        var visited = new HashSet<int> {v};
        var queue = new Queue<(int V, int P)> {(v, v)};
        var backtrackStack = new Stack<(int V, int P)>();

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
        if (!Vertices.ContainsKey(v))
            throw new ArgumentException("The vertex does not exist");

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

    #endregion

    /// <summary>
    /// Time complexity: O(E)
    /// Space complexity: O(V) and O(E)
    /// </summary>
    public override bool IsEulerian()
    {
        if (VertexCount() <= 1)
            return true;

        var degree = new Dictionary<int, int>();

        foreach (var vertex in Vertices)
        {
            foreach (var connection in vertex.Value.Connections)
            {
                if (degree.ContainsKey(connection.To))
                    degree[connection.To]++;
                else
                    degree.Add(connection.To, 1);
            }
        }

        return degree.Values.All(v => v == Vertices[v].Connections.Count);
    }

    /// <summary>
    /// Creates an undirected graph from the directed graph and calls IsConnected on it.
    ///
    /// Time complexity: O(V + E)
    /// Space complexity: O(V + E)
    /// </summary>
    public override bool IsConnected()
    {
        // Create an undirected vertex list from the directed vertex list
        var undirectedVertexDict = Vertices.ToDictionary(
            vertex => vertex.Key,
            vertex => new Vertex<TVertexData>(vertex.Value.Data)
        );

        var undirectedGraph = new UndirectedGraph<TVertexData>(undirectedVertexDict);
        foreach (var vertex in Vertices)
        foreach (var connection in vertex.Value.Connections)
            undirectedGraph.AddEdge(vertex.Key, connection.To);

        return undirectedGraph.IsConnected();
    }

    /// <summary>
    /// Picks a vertex and runs search from it once forwards and once backwards.
    /// If all vertices are visited, then the graph is connected.
    /// TODO: This algorithm should have less complexity. Maybe investigate
    ///
    /// Time complexity: O((V + E)^2)
    /// Space complexity: O((V + E) * v)
    /// </summary>
    public bool IsConnected2()
    {
        var firstVertex = Vertices.First().Key;

        var successors = new HashSet<int> {firstVertex};
        var predecessors = new HashSet<int> {firstVertex};

        // Find successors
        var queue = new Queue<int> {firstVertex};

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var connection in UnvisitedConnections(current, successors))
            {
                queue.Enqueue(connection.To);
                successors.Add(connection.To);
            }
        }

        // Find predecessors
        queue.Enqueue(firstVertex);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var connection in AllBackwardsConnections(current))
            {
                if (predecessors.Contains(connection))
                    continue;

                queue.Enqueue(connection);
                predecessors.Add(connection);
            }
        }

        successors.UnionWith(predecessors);
        return successors.Overlaps(Vertices.Keys);
    }

    /// <summary>
    /// Time complexity: O(V + E)
    /// Space complexity: O(V)
    /// </summary>
    public override bool IsTree()
    {
        if (VertexCount() <= 1)
            return true;

        // Find the root vertex
        var incomingEdges = Vertices.Keys.ToHashSet();

        foreach (var vertex in Vertices)
        foreach (var connection in vertex.Value.Connections)
            incomingEdges.Remove(connection.To);

        // If there is more than one root vertex, it is not a tree
        if (incomingEdges.Count != 1)
            return false;

        // Do a breadth first search from the root
        var visited = new HashSet<int>();
        var queue = new Queue<int> {incomingEdges.Single()};

        while (queue.Count > 0)
        {
            var v = queue.Dequeue();

            // Enqueue connections
            foreach (var connection in AllConnections(v))
            {
                if (visited.Contains(connection.To))
                    return false;

                queue.Enqueue(connection.To);
                visited.Add(connection.To);
            }
        }

        return true;
    }

    public override List<(int V1, int V2)> Bridges()
    {
        // TODO: Academic paper for the algorithm: https://stackoverflow.com/a/17107586/7279624
        throw new NotImplementedException();
    }

    /// <summary>
    /// Find all strongly connected components using the Tarjan algorithm.
    /// Implementation inspired by https://github.com/williamfiset/Algorithms
    ///
    /// Time complexity: O(V + E)
    /// Space complexity: O(V)
    /// </summary>
    /// <returns>(The number of strongly connected components, for each vertex (id of vertex, index of scc))</returns>
    public (int SccCount, Dictionary<int, int> SccDictionary) TarjanSccMap()
    {
        var id = Vertices.ToDictionary(v => v.Key, _ => -1);
        var lowLink = Vertices.ToDictionary(v => v.Key, _ => 0);
        var sccDict = Vertices.ToDictionary(v => v.Key, _ => 0);
        var inStack = Vertices.ToDictionary(v => v.Key, _ => false);
        var stack = new Stack<int>();
        int newId = 0;
        int sccCount = 0;

        // Start DFS from each node
        foreach (var vertex in Vertices.Keys)
        {
            if (id[vertex] == -1) // Unvisited
            {
                SccDfs(vertex, id, lowLink, sccDict, inStack, stack, ref newId, ref sccCount);
            }
        }

        return (sccCount, sccDict);
    }

    private void SccDfs(int v,
        Dictionary<int, int> id,
        Dictionary<int, int> lowLink,
        Dictionary<int, int> sccDict,
        Dictionary<int, bool> inStack,
        Stack<int> stack,
        ref int newId,
        ref int sccCount)
    {
        lowLink[v] = id[v] = newId++;
        inStack[v] = true;
        stack.Push(v);

        foreach (var connection in AllConnections(v))
        {
            // If node has not been visited yet, visit it
            if (id[connection.To] == -1) // Unvisited
                SccDfs(connection.To, id, lowLink, sccDict, inStack, stack, ref newId, ref sccCount);

            // If node is in stack, update lowest reachable value
            if (inStack[connection.To])
                lowLink[v] = Math.Min(lowLink[v], lowLink[connection.To]);
        }

        // If lowest reachable value has not changed we're at the root node
        if (id[v] != lowLink[v])
            return;

        // Empty the seen stack until back to root node
        for (int nodeToPop = stack.Pop();; nodeToPop = stack.Pop())
        {
            inStack[nodeToPop] = false;
            sccDict[nodeToPop] = sccCount;
            if (nodeToPop == v)
                break;
        }

        sccCount++;
    }

    public List<List<int>> TarjanSccList()
    {
        // Call the SCC algorithm
        (int count, Dictionary<int, int> sccDict) = TarjanSccMap();

        // Initialize
        var sccList = new List<List<int>>(count);
        for (int i = 0; i < count; i++)
            sccList.Add(new List<int>());

        // Map the data
        foreach (var v in Vertices.Keys)
            sccList[sccDict[v]].Add(v);

        return sccList;
    }
}