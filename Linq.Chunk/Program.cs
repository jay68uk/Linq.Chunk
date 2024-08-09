using BenchmarkDotNet.Running;
using Linq.Chunk;

var summary = BenchmarkRunner.Run<ChunkBenchmarks>();
Console.WriteLine(summary);