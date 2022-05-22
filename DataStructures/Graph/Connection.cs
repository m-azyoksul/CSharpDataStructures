namespace DataStructures.Graph;

public struct Connection
{
    public readonly int To;
    public readonly double Weight;

    public Connection(int to, double weight)
    {
        To = to;
        Weight = weight;
    }

    public static implicit operator Connection(int to)
    {
        return new Connection(to, 1);
    }
}