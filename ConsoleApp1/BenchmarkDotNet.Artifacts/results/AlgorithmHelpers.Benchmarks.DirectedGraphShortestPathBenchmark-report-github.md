``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1645 (21H2)
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  DefaultJob : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT


```
|          Method |     Mean |     Error |    StdDev | Rank |   Gen 0 |   Gen 1 |   Gen 2 | Allocated |
|---------------- |---------:|----------:|----------:|-----:|--------:|--------:|--------:|----------:|
|    Generic_Edge | 1.522 ms | 0.0088 ms | 0.0073 ms |    1 | 70.3125 | 35.1563 | 25.3906 |    786 KB |
|     Struct_Edge | 2.636 ms | 0.0165 ms | 0.0129 ms |    2 | 62.5000 | 31.2500 | 19.5313 |    786 KB |
| NonGeneric_Edge | 4.956 ms | 0.0863 ms | 0.0721 ms |    3 | 54.6875 | 23.4375 | 15.6250 |    786 KB |
