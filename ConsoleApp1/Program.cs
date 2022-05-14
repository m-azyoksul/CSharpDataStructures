using System;
using AlgorithmHelpers.DataStructures;
using BenchmarkDotNet.Running;

Console.WriteLine("Hello World!");
//BenchmarkRunner.Run<DirectedGraphGeneralBenchmark>();

var g = new UndirectedGraph<string>();
var list = g.DfsTraversal(0);