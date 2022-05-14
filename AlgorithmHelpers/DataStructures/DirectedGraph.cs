using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmHelpers.Algorithms.Graph;

namespace AlgorithmHelpers.DataStructures;

public class DirectedGraph<TVertexData> : Graph<TVertexData>
{
    #region Constructors

    public DirectedGraph() : base()
    {
    }

    public DirectedGraph(List<(int, int)> edges) : base(edges)
    {
        // Find all vertices
        foreach (var edge in edges)
        {
            if (!Vertices.ContainsKey(edge.Item1))
                Vertices.Add(edge.Item1, Vertex<TVertexData>.Empty());

            if (!Vertices.ContainsKey(edge.Item2))
                Vertices.Add(edge.Item2, Vertex<TVertexData>.Empty());

            Vertices[edge.Item1].Neighbors.Add(edge.Item2);
        }
    }

    public DirectedGraph(Dictionary<int, Vertex<TVertexData>> vertices) : base(vertices)
    {
        // Find all edges
        foreach (var vertex in vertices)
        {
            foreach (var neighbor in vertex.Value.Neighbors)
            {
                Edges.Add((vertex.Key, neighbor));
            }
        }
    }

    #endregion

    #region Elementary Operations

    public override void AddEdge(int v1, int v2)
    {
        if (!Vertices.ContainsKey(v1) && !Vertices.ContainsKey(v2))
            throw new ArgumentException("The vertex does not exist");

        Edges.Add((v1, v2));
        Vertices[v1].Neighbors.Add(v2);
    }

    public override void RemoveEdge(int v1, int v2)
    {
        if (!Vertices.ContainsKey(v1) && !Vertices.ContainsKey(v2))
            throw new ArgumentException("The vertex does not exist");
        if (!Edges.Contains((v1, v2)))
            throw new ArgumentException("The edge does not exist");

        Edges.Remove((v1, v2));
        Vertices[v1].Neighbors.Remove(v2);
    }

    public override void RemoveVertex(int v)
    {
        if (!Vertices.ContainsKey(v))
            throw new ArgumentException("The vertex does not exist");

        Vertices.Remove(v);
        Edges.RemoveAll(edge => edge.Item1 == v || edge.Item2 == v);
    }

    public override bool ContainsEdge(int v1, int v2)
    {
        return Edges.Contains((v1, v2));
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
            foreach (var neighbour in vertex.Value.Neighbors)
            {
                if (degree.ContainsKey(neighbour))
                    degree[neighbour]++;
                else
                    degree.Add(neighbour, 1);
            }
        }

        return degree.Values.All(v => v == Vertices[v].Neighbors.Count);
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
        foreach (var edge in Edges)
            undirectedGraph.AddEdge(edge.Item1, edge.Item2);

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

            foreach (var neighbour in UnvisitedNeighbours(current, successors))
            {
                queue.Enqueue(neighbour);
                successors.Add(neighbour);
            }
        }

        // Find predecessors
        queue.Enqueue(firstVertex);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var neighbour in AllBackwardsNeighbours(current))
            {
                if (predecessors.Contains(neighbour))
                    continue;

                queue.Enqueue(neighbour);
                predecessors.Add(neighbour);
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
        foreach (var neighbour in vertex.Value.Neighbors)
            incomingEdges.Remove(neighbour);

        // If there is more than one root vertex, it is not a tree
        if (incomingEdges.Count != 1)
            return false;

        // Do a breadth first search from the root
        var visited = new HashSet<int>();
        var queue = new Queue<int> {incomingEdges.Single()};

        while (queue.Count > 0)
        {
            var v = queue.Dequeue();

            // Enqueue neighbours
            foreach (var neighbour in AllNeighbours(v))
            {
                if (visited.Contains(neighbour))
                    return false;

                queue.Enqueue(neighbour);
                visited.Add(neighbour);
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
    /// <returns>(The number of sccs, for each vertex (id of vertex, index of scc))</returns>
    public (int SccCount, Dictionary<int, int> SccDictionary) TarjanSccMap()
    {
        var id = Vertices.ToDictionary(v => v.Key, v => -1);
        var lowLink = Vertices.ToDictionary(v => v.Key, v => 0);
        var sccDict = Vertices.ToDictionary(v => v.Key, v => 0);
        var inStack = Vertices.ToDictionary(v => v.Key, v => false);
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

        foreach (int nextV in Vertices[v].Neighbors)
        {
            // If node has not been visited yet, visit it
            if (id[nextV] == -1) // Unvisited
                SccDfs(nextV, id, lowLink, sccDict, inStack, stack, ref newId, ref sccCount);

            // If node is in stack, update lowest reachable value
            if (inStack[nextV])
                lowLink[v] = Math.Min(lowLink[v], lowLink[nextV]);
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