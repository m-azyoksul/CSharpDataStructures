``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1645 (21H2)
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  DefaultJob : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT


```
|          Method |     Mean |    Error |   StdDev | Rank |      Gen 0 |      Gen 1 |      Gen 2 | Allocated |
|---------------- |---------:|---------:|---------:|-----:|-----------:|-----------:|-----------:|----------:|
|    Generic_Edge | 734.8 ms | 14.69 ms | 33.16 ms |    1 | 51000.0000 | 48000.0000 | 46000.0000 |    695 MB |
| NonGeneric_Edge | 741.5 ms | 14.72 ms | 35.83 ms |    1 | 48000.0000 | 45000.0000 | 43000.0000 |    693 MB |
|     Struct_Edge | 743.8 ms | 14.85 ms | 35.58 ms |    1 | 45000.0000 | 42000.0000 | 40000.0000 |    695 MB |
