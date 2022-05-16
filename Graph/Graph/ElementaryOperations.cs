using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph;

public abstract partial class Graph<TData>
{
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

    public abstract void AddEdge(int v1, int v2, int weight);

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
        if (Vertices.ContainsKey(v))
            throw new ArgumentException("Vertex already exists");


        Vertices.Add(v, Vertex<TData>.Empty());
    }

    public void AddVertices(List<int> vertices)
    {
        foreach (var v in vertices)
            AddVertex(v);
    }

    public abstract void AddVertex(int v, List<Connection> connections);

    public abstract void RemoveVertex(int v);

    protected abstract bool HasEdge(int v1, int v2);

    public bool VerticesConnected((int v1, int v2) edge)
    {
        return HasEdge(edge.v1, edge.v2);
    }

    public bool ContainsVertex(int v)
    {
        return Vertices.ContainsKey(v);
    }

    public void Clear()
    {
        Vertices.Clear();
    }

    protected int VertexCount()
    {
        return Vertices.Count;
    }

    public abstract int EdgeCount();

    protected List<Connection> AllConnections(int v)
    {
        CheckVertex(v);
        return Vertices[v].Connections;
    }

    protected int ConnectionCount(int v)
    {
        CheckVertex(v);
        return Vertices[v].Connections.Count;
    }

    public List<int> GetVertices()
    {
        return Vertices.Keys.ToList();
    }

    public bool IsEmpty()
    {
        return Vertices.Count == 0;
    }

    protected void CheckVertex(int v)
    {
        if (!Vertices.ContainsKey(v))
            throw new ArgumentException($"The vertex does not exist. Parameter: {nameof(v)} = {v}");
    }

    protected void CheckVertices(int v1, int v2)
    {
        if (!Vertices.ContainsKey(v1))
            throw new ArgumentException($"The vertex does not exist. Parameter: {nameof(v1)} = {v1}");

        if (!Vertices.ContainsKey(v2))
            throw new ArgumentException($"The vertex does not exist. Parameter: {nameof(v2)} = {v2}");
    }
}

public partial class DirectedGraph<TVertexData>
{
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
        CheckVertices(v1, v2);
        if (!Vertices[v1].Connections.Contains(v2))
            throw new ArgumentException("The edge does not exist");

        Vertices[v1].Connections.Remove(v2);
    }

    public override void AddVertex(int v, List<Connection> connections)
    {
        if (!Vertices.ContainsKey(v))
            throw new ArgumentException("The vertex already exists");

        Vertices.Add(v, new Vertex<TVertexData>(connections));
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
}

public partial class UndirectedGraph<TVertexData>
{
    public override void AddEdge(int v1, int v2)
    {
        if (!Vertices.ContainsKey(v1))
            Vertices.Add(v1, Vertex<TVertexData>.Empty());
        if (!Vertices.ContainsKey(v2))
            Vertices.Add(v2, Vertex<TVertexData>.Empty());

        Vertices[v1].Connections.Add(v2);
        Vertices[v2].Connections.Add(v1);
    }

    public override void AddEdge(int v1, int v2, int weight)
    {
        if (!Vertices.ContainsKey(v1))
            Vertices.Add(v1, Vertex<TVertexData>.Empty());
        if (!Vertices.ContainsKey(v2))
            Vertices.Add(v2, Vertex<TVertexData>.Empty());

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

        Vertices.Add(v, new Vertex<TVertexData>(connections));

        // Add backwards connections
        foreach (var connection in connections)
        {
            if (!Vertices.ContainsKey(connection.To))
                Vertices.Add(connection.To, Vertex<TVertexData>.Empty());

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
}