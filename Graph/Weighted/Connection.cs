namespace Graph;

public struct Connection
{
    public int To;
    public int Weight;

    public Connection(int to, int weight)
    {
        To = to;
        Weight = weight;
    }

    public Connection(int to)
    {
        To = to;
        Weight = 1;
    }

    public static implicit operator Connection(int to)
    {
        return new Connection(to, 1);
    }

    public static implicit operator Connection((int To, int Weight) edge)
    {
        return new Connection(edge.To, edge.Weight);
    }
}