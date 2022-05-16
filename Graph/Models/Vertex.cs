using System.Collections.Generic;

namespace Graph;

public struct Vertex<T>
{
    public T? Data;
    public readonly List<Connection> Connections;

    public Vertex(T? data, List<Connection> connections)
    {
        Data = data;
        Connections = connections;
    }

    public Vertex(T? data)
    {
        Data = data;
        Connections = new List<Connection>();
    }

    public Vertex(List<Connection> connections)
    {
        Data = default;
        Connections = connections;
    }

    public static Vertex<T> Empty() => new(default, new List<Connection>());
}