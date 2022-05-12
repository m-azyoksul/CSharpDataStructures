// using System.Collections.Generic;
// using AlgorithmHelpers.DataStructures;
// using BenchmarkDotNet.Attributes;
// using BenchmarkDotNet.Order;
//
// namespace AlgorithmHelpers.Benchmarks;
//
// [MemoryDiagnoser]
// [Orderer(SummaryOrderPolicy.FastestToSlowest)]
// [RankColumn]
// public class DirectedGraphInitializeBenchmark
// {
//     private static readonly List<(int, int)> Edges = new() {(0, 1), (0, 2), (1, 2), (1, 3), (2, 3)};
//
//     private static readonly Dictionary<int, List<int>> Vertexes = new Dictionary<int, List<int>>
//     {
//         {0, new List<int> {1, 2}},
//         {1, new List<int> {2, 3}},
//         {2, new List<int> {3}},
//         {3, new List<int>()}
//     };
//
//     private static readonly Dictionary<int, (string?, List<int>)> VertexesGeneric = new Dictionary<int, (string?, List<int>)>
//     {
//         {0, ("A", new List<int> {1, 2})},
//         {1, ("B", new List<int> {2, 3})},
//         {2, ("C", new List<int> {3})},
//         {3, ("D", new List<int>())}
//     };
//
//     private static readonly Dictionary<int, Vertex<string>> VertexesStruct = new Dictionary<int, Vertex<string>>
//     {
//         {0, new Vertex<string>("A", new List<int> {1, 2})},
//         {1, new Vertex<string>("B", new List<int> {2, 3})},
//         {2, new Vertex<string>("C", new List<int> {3})},
//         {3, new Vertex<string>("D", new List<int>())}
//     };
//
//     [Benchmark]
//     public void NonGeneric_Edge()
//     {
//         var graph = new DirectedGraph(Edges);
//     }
//
//     [Benchmark]
//     public void Generic_Edge()
//     {
//         var graph = new DirectedGraph<string>(Edges);
//     }
//
//     [Benchmark]
//     public void Struct_Edge()
//     {
//         var graph = new DirectedGraphStruct<string>(Edges);
//     }
//
//     [Benchmark]
//     public void NonGeneric_Vertex()
//     {
//         var graph = new DirectedGraph(Vertexes);
//     }
//
//     [Benchmark]
//     public void Generic_Vertex()
//     {
//         var graph = new DirectedGraph<string>(VertexesGeneric);
//     }
//
//     [Benchmark]
//     public void Struct_Vertex()
//     {
//         var graph = new DirectedGraphStruct<string>(VertexesStruct);
//     }
// }
//
// [MemoryDiagnoser]
// [Orderer(SummaryOrderPolicy.FastestToSlowest)]
// [RankColumn]
// public class DirectedGraphShortestPathBenchmark
// {
//     private static readonly List<(int, int)> Edges = new() {(0, 1), (0, 2), (1, 2), (1, 3), (2, 3)};
//
//     private static readonly Dictionary<int, List<int>> Vertexes = new Dictionary<int, List<int>>
//     {
//         {0, new List<int> {1, 2}},
//         {1, new List<int> {2, 3}},
//         {2, new List<int> {3}},
//         {3, new List<int>()}
//     };
//
//     private static readonly Dictionary<int, (string?, List<int>)> VertexesGeneric = new Dictionary<int, (string?, List<int>)>
//     {
//         {0, ("A", new List<int> {1, 2})},
//         {1, ("B", new List<int> {2, 3})},
//         {2, ("C", new List<int> {3})},
//         {3, ("D", new List<int>())}
//     };
//
//     private static readonly Dictionary<int, Vertex<string>> VertexesStruct = new Dictionary<int, Vertex<string>>
//     {
//         {0, new Vertex<string>("A", new List<int> {1, 2})},
//         {1, new Vertex<string>("B", new List<int> {2, 3})},
//         {2, new Vertex<string>("C", new List<int> {3})},
//         {3, new Vertex<string>("D", new List<int>())}
//     };
//
//     private static readonly DirectedGraph NonGenericFromEdges = new(Edges);
//     private static readonly DirectedGraph<string> GenericFromEdges = new(Edges);
//     private static readonly DirectedGraphStruct<string> StructFromEdges = new(Edges);
//     private static readonly DirectedGraph NonGenericFromVertices = new(Vertexes);
//     private static readonly DirectedGraph<string> GenericFromVertices = new(VertexesGeneric);
//     private static readonly DirectedGraphStruct<string> StructFromVertices = new(VertexesStruct);
//
//     [Benchmark]
//     public void NonGeneric_Edge()
//     {
//         NonGenericFromEdges.ShortestPath(0, 3);
//     }
//
//     [Benchmark]
//     public void Generic_Edge()
//     {
//         GenericFromEdges.ShortestPath(0, 3);
//     }
//
//     [Benchmark]
//     public void Struct_Edge()
//     {
//         StructFromEdges.ShortestPath(0, 3);
//     }
//
//     [Benchmark]
//     public void NonGeneric_Vertex()
//     {
//         NonGenericFromVertices.ShortestPath(0, 3);
//     }
//
//     [Benchmark]
//     public void Generic_Vertex()
//     {
//         GenericFromVertices.ShortestPath(0, 3);
//     }
//
//     [Benchmark]
//     public void Struct_Vertex()
//     {
//         StructFromVertices.ShortestPath(0, 3);
//     }
// }
//
// [MemoryDiagnoser]
// [Orderer(SummaryOrderPolicy.FastestToSlowest)]
// [RankColumn]
// public class DirectedGraphGeneralBenchmark
// {
//     [Benchmark]
//     public void NonGeneric_Edge()
//     {
//         var graph = new DirectedGraph();
//         for (var i = 0; i <= 100000; i++)
//             graph.AddVertex(i);
//         for (var i = 0; i < 100000; i++)
//             graph.AddEdge((i, i + 1));
//         for (int i = 0; i < 100; i++)
//             graph.ShortestPath(0, 100000);
//     }
//
//     [Benchmark]
//     public void Generic_Edge()
//     {
//         var graph = new DirectedGraph<string>();
//         for (var i = 0; i <= 100000; i++)
//             graph.AddVertex(i);
//         for (var i = 0; i < 100000; i++)
//             graph.AddEdge((i, i + 1));
//         for (int i = 0; i < 100; i++)
//             graph.ShortestPath(0, 100000);
//     }
//
//     [Benchmark]
//     public void Struct_Edge()
//     {
//         var graph = new DirectedGraphStruct<string>();
//         for (var i = 0; i <= 100000; i++)
//             graph.AddVertex(i);
//         for (var i = 0; i < 100000; i++)
//             graph.AddEdge((i, i + 1));
//         for (int i = 0; i < 100; i++)
//             graph.ShortestPath(0, 100000);
//     }
// }