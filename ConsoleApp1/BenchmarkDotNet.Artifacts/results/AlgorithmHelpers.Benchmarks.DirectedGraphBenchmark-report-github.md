``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1645 (21H2)
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  DefaultJob : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT


```
|            Method |      Mean |    Error |   StdDev | Rank |  Gen 0 | Allocated |
|------------------ |----------:|---------:|---------:|-----:|-------:|----------:|
| NonGeneric_Vertex |  72.65 ns | 0.278 ns | 0.232 ns |    1 | 0.0248 |     208 B |
|    Generic_Vertex | 100.14 ns | 0.518 ns | 0.432 ns |    2 | 0.0248 |     208 B |
|     Struct_Vertex | 110.58 ns | 0.609 ns | 0.540 ns |    3 | 0.0248 |     208 B |
|   NonGeneric_Edge | 225.23 ns | 1.400 ns | 1.310 ns |    4 | 0.0889 |     744 B |
|      Generic_Edge | 269.74 ns | 4.845 ns | 4.758 ns |    5 | 0.0982 |     824 B |
|       Struct_Edge | 275.33 ns | 1.053 ns | 0.985 ns |    6 | 0.0982 |     824 B |
