using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmHelpers.DataStructures;

public class UndirectedGraph<TVertexData> : Graph<TVertexData>
{
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
            foreach (var neighbor in GetNeighbors(v))
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
}