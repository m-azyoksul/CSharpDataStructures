using System.Collections.Generic;

namespace DataStructures.Graph;

public struct Vertex<T>
{
    public readonly T? Data;
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

    public Vertex(T? data, params Connection[] connections)
    {
        Data = data;
        Connections = new List<Connection>(connections);
    }

    public static Vertex<T> Empty() => new();
}