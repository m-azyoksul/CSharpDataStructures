using System.Collections.Generic;

namespace Graph;

public class Vertex<T>
{
    public T? Data;
    public readonly List<int> Connections;

    public Vertex(T? data, List<int> connections)
    {
        Data = data;
        Connections = connections;
    }

    public Vertex(T? data)
    {
        Data = data;
        Connections = new List<int>();
    }

    public static Vertex<T> Empty() => new(default, new List<int>());
}