```

BenchmarkDotNet v0.14.0, Debian GNU/Linux 12 (bookworm) (container)
11th Gen Intel Core i7-1165G7 2.80GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.303
  [Host]     : .NET 8.0.7 (8.0.724.31311), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 8.0.7 (8.0.724.31311), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method                        | Mean     | Error    | StdDev   | Median   | Gen0     | Gen1     | Allocated |
|------------------------------ |---------:|---------:|---------:|---------:|---------:|---------:|----------:|
| BenchmarkLinqChunk            | 19.53 ms | 0.614 ms | 1.790 ms | 19.59 ms | 562.5000 | 281.2500 |   3.33 MB |
| BenchmarkManualChunk          | 15.18 ms | 0.736 ms | 2.171 ms | 14.01 ms | 500.0000 | 250.0000 |   3.01 MB |
| BenchmarkLinqChunkSemaphore   |       NA |       NA |       NA |       NA |       NA |       NA |        NA |
| BenchmarkManualChunkSemaphore |       NA |       NA |       NA |       NA |       NA |       NA |        NA |

Benchmarks with issues:
  ChunkBenchmarks.BenchmarkLinqChunkSemaphore: DefaultJob
  ChunkBenchmarks.BenchmarkManualChunkSemaphore: DefaultJob
