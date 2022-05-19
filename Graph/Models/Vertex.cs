using System.Collections.Generic;

namespace Graph;

public struct Vertex<T>
{
    public T? Data;
    public readonly List<Connection> Connections;

    public Vertex()
    {
        Data = default;
        Connections = new List<Connection>();
    }

    public Vertex(List<Connection> connections, T? data = default)
    {
        Data = data;
        Connections = connections;
    }

    public Vertex(T? data)
    {
        Data = data;
        Connections = new List<Connection>();
    }

    public static Vertex<T> Empty() => new();
}