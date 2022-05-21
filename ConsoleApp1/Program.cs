using AlgorithmHelpers.DataStructures;

var tree = new AvlTree<int>();

for (int i = 0; i < 1000; i++)
    tree.Add(i);

tree.PrintInOrder();

//BenchmarkRunner.Run<AvlAddBenchmark>();