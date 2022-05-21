using AlgorithmHelpers.DataStructures;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using DataStructures.BinarySearchTree;

namespace ConsoleApp1;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class AvlAddBenchmark
{
    private static readonly AvlTree<int> tree = new();

    [Benchmark]
    public void Add()
    {
        for (int i = 0; i < 4; i++)
            tree.Add(i);
    }
}