using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmHelpers.DataStructures;

public class Vertex<T>
{
    public T? Data;
    public List<int> Neighbours;

    public Vertex(T? data, List<int> neighbours)
    {
        Data = data;
        Neighbours = neighbours;
    }

    public Vertex(T? data)
    {
        Data = data;
        Neighbours = new List<int>();
    }

    public static Vertex<T> Empty() => new(default, new List<int>());
}

public abstract class Graph<TData>
{
    public Dictionary<int, Vertex<TData>> VertexList { get; }
    public List<(int V1, int V2)> EdgeList { get; }


    protected Graph()
    {
        VertexList = new Dictionary<int, Vertex<TData>>();
        EdgeList = new List<(int, int)>();
    }

    protected Graph(List<(int, int)> edgeList)
    {
        VertexList = new Dictionary<int, Vertex<TData>>();
        EdgeList = edgeList;
    }

    protected Graph(Dictionary<int, Vertex<TData>> vertexList)
    {
        // Make sure the Neighbours lists only contain values that exist in the vertex list
        foreach (var vertex in vertexList)
        foreach (var neighbour in vertex.Value.Neighbours)
            if (!vertexList.ContainsKey(neighbour))
                throw new ArgumentException("The edge list contains a vertex that is not in the vertex list");

        VertexList = vertexList;
        EdgeList = new List<(int, int)>();
    }

    #region Elementary 
    
    public abstract void AddEdge(int v1, int v2);

    public void AddEdge((int v1, int v2) edge)
    {
        AddEdge(edge.v1, edge.v2);
    }

    public void AddEdges(List<(int v1, int v2)> edges)
    {
        foreach (var edge in edges)
            AddEdge(edge.v1, edge.v2);
    }

    public abstract void RemoveEdge(int v1, int v2);


    public void RemoveEdge((int v1, int v2) edge)
    {
        RemoveEdge(edge.v1, edge.v2);
    }

    public void RemoveEdges(List<(int v1, int v2)> edges)
    {
        foreach (var edge in edges)
            RemoveEdge(edge.v1, edge.v2);
    }

    public void AddVertex(int v)
    {
        if (VertexList.ContainsKey(v))
            throw new ArgumentException("The vertex already exists");

        VertexList.Add(v, Vertex<TData>.Empty());
    }

    public abstract void RemoveVertex(int v);

    public abstract bool ContainsEdge(int v1, int v2);

    public bool ContainsEdge((int v1, int v2) edge)
    {
        return ContainsEdge(edge.v1, edge.v2);
    }

    public bool ContainsVertex(int v)
    {
        return VertexList.ContainsKey(v);
    }

    public void Clear()
    {
        VertexList.Clear();
        EdgeList.Clear();
    }
    
    #endregion
    
    public int VertexCount()
    {
        return VertexList.Count;
    }

    public int EdgeCount()
    {
        return EdgeList.Count;
    }

    public List<int> GetNeighbors(int v)
    {
        if (!VertexList.ContainsKey(v))
            throw new ArgumentException("The vertex does not exist");

        return VertexList[v].Neighbours;
    }

    public List<int> GetVertices()
    {
        return VertexList.Keys.ToList();
    }

    public bool IsEmpty()
    {
        return VertexList.Count == 0;
    }

    protected int NonKeyValue()
    {
        var nonKeyValue = int.MinValue;
        while (VertexList.ContainsKey(nonKeyValue))
            nonKeyValue++;
        return nonKeyValue;
    }


    public Dictionary<int, TData?> TraverseBfs(int v)
    {
        if (!VertexList.ContainsKey(v))
            throw new ArgumentException("The vertex does not exist");

        var visited = new Dictionary<int, TData?> {{v, VertexList[v].Data}};
        var queue = new Queue<int> {v};

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var neighbour in VertexList[current].Neighbours)
            {
                if (visited.ContainsKey(neighbour))
                    continue;

                queue.Enqueue(neighbour);
                visited[neighbour] = VertexList[neighbour].Data;
            }
        }

        return visited;
    }

    public Dictionary<int, TData?> TraverseDfs(int v)
    {
        if (!VertexList.ContainsKey(v))
            throw new ArgumentException("The vertex does not exist");

        var visited = new Dictionary<int, TData?> {{v, VertexList[v].Data}};
        var stack = new Stack<int> {v};

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            visited[current] = VertexList[current].Data;

            foreach (var neighbour in VertexList[current].Neighbours)
            {
                if (visited.ContainsKey(neighbour))
                    continue;

                stack.Push(neighbour);
            }
        }

        return visited;
    }

    public bool IsSimple()
    {
        foreach (var vertex in VertexList)
        {
            if (vertex.Value.Neighbours.Contains(vertex.Key))
                return false;

            // Check if there is a duplicate in vertex.Value.Neighbours
            if (vertex.Value.Neighbours.HasDuplicate())
                return false;
        }

        return true;
    }

    public abstract bool IsEulerian();

    public abstract bool IsTree();
    
    public abstract List<(int V1, int V2)> Bridges();

    public bool IsConnected()
    {
        if (VertexCount() <= 1)
            return true;

        var visited = new HashSet<int>();
        var queue = new Queue<int>();
        queue.Enqueue(VertexList.Keys.First());

        while (queue.Count > 0)
        {
            var v = queue.Dequeue();

            // Enqueue neighbors
            foreach (var neighbor in GetNeighbors(v))
            {
                if (visited.Contains(neighbor))
                    continue;

                queue.Enqueue(neighbor);
                visited.Add(v);
            }
        }

        return visited.Count == VertexCount();
    }

    public bool IsTherePath(int fromVertex, int toVertex)
    {
        if (!VertexList.ContainsKey(fromVertex) || !VertexList.ContainsKey(toVertex))
            throw new ArgumentException("The vertex does not exist");

        var visited = new HashSet<int>();
        var queue = new Queue<int>();
        queue.Enqueue(fromVertex);

        while (queue.Count > 0)
        {
            var v = queue.Dequeue();

            if (v == toVertex)
                return true;

            // Enqueue neighbors
            foreach (var neighbor in GetNeighbors(v))
            {
                if (visited.Contains(neighbor))
                    continue;

                queue.Enqueue(neighbor);
                visited.Add(v);
            }
        }

        return false;
    }

    public List<int>? ShortestPath(int fromVertex, int toVertex)
    {
        if (!VertexList.ContainsKey(fromVertex) || !VertexList.ContainsKey(toVertex))
            throw new ArgumentException("The vertex does not exist");

        var predecessor = new Dictionary<int, int> {{fromVertex, -1}};
        var queue = new Queue<int>();
        queue.Enqueue(fromVertex);

        while (queue.Count > 0)
        {
            var v = queue.Dequeue();

            if (v == toVertex)
            {
                var path = new List<int>();
                for (int current = toVertex; current != -1; current = predecessor[current])
                    path.Add(current);

                path.Reverse();
                return path;
            }

            // Enqueue neighbors
            foreach (var neighbor in GetNeighbors(v))
            {
                if (predecessor.ContainsKey(neighbor))
                    continue;

                queue.Enqueue(neighbor);
                predecessor.Add(neighbor, v);
            }
        }

        return null;
    }

    // TODO: Correct implementation
    /// <returns>The shortest loop, null if there are no loops</returns>
    public List<int>? ShortestLoop()
    {
        var firstVertex = VertexList.Keys.First();

        var visited = new HashSet<int>();
        var queue = new Queue<int>();
        queue.Enqueue(firstVertex);
        var predecessor = new Dictionary<int, int>
        {
            {firstVertex, -1},
        };

        while (queue.Count > 0)
        {
            var v = queue.Dequeue();

            visited.Add(v);

            // Enqueue neighbors
            foreach (var neighbor in GetNeighbors(v))
            {
                if (visited.Contains(neighbor))
                {
                    // Loop found
                    var loop = new List<int> {neighbor};
                    for (int current = v; current != neighbor; current = predecessor[current])
                        loop.Add(current);

                    return loop;
                }

                queue.Enqueue(neighbor);
                predecessor.Add(neighbor, v);
            }
        }

        return null;
    }

    // TODO: Implement
    public List<int>? LongestLoop()
    {
        return null;
    }

    public int Excentricity(int vertex)
    {
        if (!VertexList.ContainsKey(vertex))
            throw new ArgumentException("The vertex does not exist");

        var visited = new HashSet<int>();
        var queue = new Queue<int>();
        queue.Enqueue(vertex);
        var distance = new Dictionary<int, int>
        {
            {vertex, 0},
        };

        while (queue.Count > 0)
        {
            var v = queue.Dequeue();

            visited.Add(v);

            // Enqueue neighbors
            foreach (var neighbor in GetNeighbors(v))
            {
                if (visited.Contains(neighbor))
                    continue;

                queue.Enqueue(neighbor);
                distance.Add(neighbor, distance[v] + 1);
            }
        }

        return distance.Values.Max();
    }

    // TODO: Maybe improvable
    public int Radius()
    {
        return VertexList.Keys.Min(Excentricity);
    }

    // TODO: Maybe improvable
    public int Diameter()
    {
        return VertexList.Keys.Max(Excentricity);
    }

    public bool IsCentralVertex(int vertex)
    {
        if (!VertexList.ContainsKey(vertex))
            throw new ArgumentException("The vertex does not exist");

        return Excentricity(vertex) == Radius();
    }

    public bool IsBorderVertex(int vertex)
    {
        if (!VertexList.ContainsKey(vertex))
            throw new ArgumentException("The vertex does not exist");

        return Excentricity(vertex) == Diameter();
    }

    // TODO: Maybe improvable
    public int[] GetCentralVertices()
    {
        var radius = Radius();

        return VertexList.Keys
            .Where(v => Excentricity(v) == radius)
            .ToArray();
    }
    
    
}