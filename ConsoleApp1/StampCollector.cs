using System;

namespace ConsoleApp1;

public sealed class StampCollector
{
    private static readonly Lazy<StampCollector> InstanceField = new(() => new StampCollector());

    private StampCollector()
    {
    }

    public static StampCollector Instance => InstanceField.Value;
}