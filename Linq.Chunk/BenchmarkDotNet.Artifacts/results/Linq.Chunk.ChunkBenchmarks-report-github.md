```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.3880/23H2/2023Update/SunValley3)
11th Gen Intel Core i7-1165G7 2.80GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.200
  [Host]     : .NET 8.0.2 (8.0.224.6711), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 8.0.2 (8.0.224.6711), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method                        | Mean     | Error    | StdDev   | Gen0     | Gen1     | Allocated |
|------------------------------ |---------:|---------:|---------:|---------:|---------:|----------:|
| BenchmarkLinqChunk            | 12.96 ms | 0.318 ms | 0.908 ms | 539.0625 | 273.4375 |    3.2 MB |
| BenchmarkManualChunk          | 12.64 ms | 0.346 ms | 1.004 ms | 500.0000 | 257.8125 |   2.99 MB |
| BenchmarkLinqChunkSemaphore   |       NA |       NA |       NA |       NA |       NA |        NA |
| BenchmarkManualChunkSemaphore |       NA |       NA |       NA |       NA |       NA |        NA |

Benchmarks with issues:
  ChunkBenchmarks.BenchmarkLinqChunkSemaphore: DefaultJob
  ChunkBenchmarks.BenchmarkManualChunkSemaphore: DefaultJob
