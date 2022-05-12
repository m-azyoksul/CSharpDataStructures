using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmHelpers.Algorithms.Graph;

namespace AlgorithmHelpers.DataStructures;

public class DirectedGraph<TVertexData> : Graph<TVertexData>
{
    public DirectedGraph() : base()
    {
    }

    public DirectedGraph(List<(int, int)> edgeList) : base(edgeList)
    {
        // Find all vertices
        foreach (var edge in edgeList)
        {
            if (!VertexList.ContainsKey(edge.Item1))
                VertexList.Add(edge.Item1, Vertex<TVertexData>.Empty());

            if (!VertexList.ContainsKey(edge.Item2))
                VertexList.Add(edge.Item2, Vertex<TVertexData>.Empty());

            VertexList[edge.Item1].Neighbours.Add(edge.Item2);
        }
    }

    public DirectedGraph(Dictionary<int, Vertex<TVertexData>> vertexList) : base(vertexList)
    {
        // Find all edges
        foreach (var vertex in vertexList)
        {
            foreach (var neighbor in vertex.Value.Neighbours)
            {
                EdgeList.Add((vertex.Key, neighbor));
            }
        }
    }


    public override void AddEdge(int v1, int v2)
    {
        if (!VertexList.ContainsKey(v1) && !VertexList.ContainsKey(v2))
            throw new ArgumentException("The vertex does not exist");

        EdgeList.Add((v1, v2));
        VertexList[v1].Neighbours.Add(v2);
    }

    public override void RemoveEdge(int v1, int v2)
    {
        if (!VertexList.ContainsKey(v1) && !VertexList.ContainsKey(v2))
            throw new ArgumentException("The vertex does not exist");
        if (!EdgeList.Contains((v1, v2)))
            throw new ArgumentException("The edge does not exist");

        EdgeList.Remove((v1, v2));
        VertexList[v1].Neighbours.Remove(v2);
    }

    public override void RemoveVertex(int v)
    {
        if (!VertexList.ContainsKey(v))
            throw new ArgumentException("The vertex does not exist");

        VertexList.Remove(v);
        EdgeList.RemoveAll(edge => edge.Item1 == v || edge.Item2 == v);
    }

    public override bool ContainsEdge(int v1, int v2)
    {
        return EdgeList.Contains((v1, v2));
    }


    /// <summary>
    /// Time complexity: O(E)
    /// Space complexity: O(V) and O(E)
    /// </summary>
    public override bool IsEulerian()
    {
        if (VertexCount() <= 1)
            return true;

        var degree = new Dictionary<int, int>();

        foreach (var vertex in VertexList)
        {
            foreach (var neighbour in vertex.Value.Neighbours)
            {
                if (degree.ContainsKey(neighbour))
                    degree[neighbour]++;
                else
                    degree.Add(neighbour, 1);
            }
        }

        return degree.Values.All(v => v == VertexList[v].Neighbours.Count);
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
        var incomingEdges = VertexList.Keys.ToHashSet();

        foreach (var vertex in VertexList)
        foreach (var neighbour in vertex.Value.Neighbours)
            incomingEdges.Remove(neighbour);
        
        // If there is more than one root vertex, it is not a tree
        if (incomingEdges.Count != 1)
            return false;

        // Do a breadth first search from the root
        var visited = new HashSet<int>();
        var queue = new Queue<int>();
        queue.Enqueue(incomingEdges.Single());

        while (queue.Count > 0)
        {
            var v = queue.Dequeue();

            // Enqueue neighbours
            foreach (var neighbour in VertexList[v].Neighbours)
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

    /// <returns>(The number of sccs, for each vertex (id of vertex, index of scc))</returns>
    public (int, (int id, int sccIndex)[]) StronglyConnectedComponents()
    {
        var (sccCount, sccArray) = VertexList.Values.Select(v => v.Neighbours).ToList().TarjanSccSolve();

        var vertices = GetVertices();
        if (vertices.Count != sccArray.Length)
            throw new Exception("The number of vertices does not match the number of sccs");

        var idToScc = vertices.Select((v, i) => (v, sccArray[i])).ToArray();
        return (sccCount, idToScc);
    }

    public List<List<int>> StronglyConnectedComponentsVertexList()
    {
        var (sccCount, vertexSccArray) = StronglyConnectedComponents();

        // Initialize
        var sccList = new List<List<int>>(sccCount);
        for (int i = 0; i < sccCount; i++)
            sccList.Add(new List<int>());

        // Map the data
        for (int i = 0; i < vertexSccArray.Length; i++)
            sccList[vertexSccArray[i].sccIndex].Add(vertexSccArray[i].id);

        return sccList;
    }
}