using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmHelpers.DataStructures;

public class UndirectedGraph<TVertexData> : Graph<TVertexData>
{
    #region Constructors
    
    public UndirectedGraph() : base()
    {
    }

    public UndirectedGraph(List<(int, int)> edgeList) : base(edgeList)
    {
        // Find all vertices
        foreach (var edge in edgeList)
        {
            if (!VertexList.ContainsKey(edge.Item1))
                VertexList.Add(edge.Item1, Vertex<TVertexData>.Empty());

            if (!VertexList.ContainsKey(edge.Item2))
                VertexList.Add(edge.Item2, Vertex<TVertexData>.Empty());

            VertexList[edge.Item1].Neighbours.Add(edge.Item2);
            VertexList[edge.Item2].Neighbours.Add(edge.Item1);
        }
    }

    public UndirectedGraph(Dictionary<int, Vertex<TVertexData>> vertexList) : base(vertexList)
    {
        // TODO
        // Check if every vertex is the neighbour its neighbours
        // This graph also allows for self-loops and multiple edges between two vertices
        foreach (var vertex in vertexList)
        {
            foreach (var neighbour in vertex.Value.Neighbours)
            {
                if (!vertexList[neighbour].Neighbours.Contains(vertex.Key))
                    throw new ArgumentException("Graph is not undirected");
            }
        }
    }

    #endregion

    #region Elementary operations

    public override void AddEdge(int v1, int v2)
    {
        if (!VertexList.ContainsKey(v1) && !VertexList.ContainsKey(v2))
            throw new ArgumentException("The vertex does not exist");

        EdgeList.Add((v1, v2));
        VertexList[v1].Neighbours.Add(v2);
        VertexList[v2].Neighbours.Add(v1);
    }

    public override void RemoveEdge(int v1, int v2)
    {
        if (!VertexList.ContainsKey(v1) && !VertexList.ContainsKey(v2))
            throw new ArgumentException("The vertex does not exist");

        if (EdgeList.Contains((v1, v2)))
        {
            EdgeList.Remove((v1, v2));
            VertexList[v1].Neighbours.Remove(v2);
            VertexList[v2].Neighbours.Remove(v1);
        }
        else if (EdgeList.Contains((v2, v1)))
        {
            EdgeList.Remove((v2, v1));
            VertexList[v2].Neighbours.Remove(v1);
            VertexList[v1].Neighbours.Remove(v2);
        }
        else
            throw new ArgumentException("The edge does not exist");
    }

    public override void RemoveVertex(int v)
    {
        if (!VertexList.ContainsKey(v))
            throw new ArgumentException("The vertex does not exist");

        foreach (var vertex in VertexList[v].Neighbours)
            VertexList[vertex].Neighbours.Remove(v);

        VertexList.Remove(v);
        EdgeList.RemoveAll(edge => edge.Item1 == v || edge.Item2 == v);
    }

    public override bool ContainsEdge(int v1, int v2)
    {
        return EdgeList.Contains((v1, v2)) || EdgeList.Contains((v2, v1));
    }
    
    #endregion

    /// <summary>
    /// Time complexity: O(V)
    /// Space complexity: O(1)
    /// </summary>
    public override bool IsEulerian()
    {
        return VertexList.All(vertex => vertex.Value.Neighbours.Count % 2 == 0);
    }

    /// <summary>
    /// Time complexity: O(V)
    /// Space complexity: O(V)
    /// </summary>
    public override bool IsTree()
    {
        if (VertexCount() <= 1)
            return true;

        var firstVertex = VertexList.First().Key;

        var predecessor = new Dictionary<int, int> {{firstVertex, -1}};
        var queue = new Queue<int>();
        queue.Enqueue(firstVertex);

        while (queue.Count > 0)
        {
            var v = queue.Dequeue();

            // Enqueue neighbors
            foreach (var neighbor in AllNeighbours(v))
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

        foreach (var vertex in VertexList)
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

        foreach (int to in VertexList[v].Neighbours)
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

        foreach (var vertex in VertexList.Keys)
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
                else if (current.visitedNCount > 0)
                {
                    int neighbour = VertexList[current.vertex].Neighbours[current.visitedNCount - 1];
                    if (neighbour != current.parent)
                    {
                        if (low[neighbour] > timeOfVisit[current.vertex])
                            bridges.Add((current.vertex, neighbour));

                        low[current.vertex] = Math.Min(low[current.vertex], low[neighbour]);
                    }
                }

                // Push next unvisited neighbour to the stack
                if (current.visitedNCount < VertexList[current.vertex].Neighbours.Count)
                {
                    // Push the current vertex back to the stack
                    stack.Push((current.vertex, current.parent, current.visitedNCount + 1));

                    // If the neighbour is the parent, skip it
                    int neighbour = VertexList[current.vertex].Neighbours[current.visitedNCount];
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

    public (int, Dictionary<int, int>) ConnectedComponents()
    {
        var components = new Dictionary<int, int>();
        var componentIndex = 0;

        foreach (var vertex in VertexList.Keys)
        {
            if (components.ContainsKey(vertex))
                continue;

            var queue = new Queue<int>();
            queue.Enqueue(vertex);

            while (queue.Count > 0)
            {
                var v = queue.Dequeue();

                foreach (var neighbor in UnvisitedNeighbours(v, components))
                {
                    queue.Enqueue(neighbor);
                    components[v] = componentIndex;
                }
            }

            componentIndex++;
        }

        return (componentIndex, components);
    }

    public List<int[]> ConnectedComponentList()
    {
        var components = new List<int[]>();

        foreach (var vertex in VertexList.Keys)
        {
            var visited = new HashSet<int>();

            if (visited.Contains(vertex))
                continue;

            var queue = new Queue<int>();
            queue.Enqueue(vertex);

            while (queue.Count > 0)
            {
                var v = queue.Dequeue();

                foreach (var neighbor in UnvisitedNeighbours(v, visited))
                {
                    queue.Enqueue(neighbor);
                    visited.Add(v);
                }
            }

            components.Add(visited.ToArray());
        }

        return components;
    }
}