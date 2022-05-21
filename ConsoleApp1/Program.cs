using AlgorithmHelpers.DataStructures;
using BenchmarkDotNet.Running;
using ConsoleApp1;


var tree = new AvlTree<int>();

for (int i = 0; i < 1000; i++)
    tree.Add(i);

//BenchmarkRunner.Run<AvlAddBenchmark>();