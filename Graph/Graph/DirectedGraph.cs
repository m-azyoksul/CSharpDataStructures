using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph;

public partial class DirectedGraph<TVertexData> : Graph<TVertexData>
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