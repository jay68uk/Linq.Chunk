using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;

namespace Linq.Chunk;

[MemoryDiagnoser]
public class ChunkBenchmarks
{
  private IEnumerable<TestData>? _data;
  private ParallelOptions _parallelOptions = new ParallelOptions
  {
    MaxDegreeOfParallelism = 4 
  };

  [GlobalSetup]
  public void Setup()
  {
    _data = GenerateLargeEnumerable();
  }

  // ChunkBenchmarks using LINQ Chunk
  [Benchmark]
  public void BenchmarkLinqChunk()
  {
    var exceptions = new ConcurrentBag<Exception>();
    
    var chunks = _data!.Chunk(200);
    
    Parallel.ForEach(chunks, _parallelOptions, async chunk =>
    {
      try
      {
        await ProcessChunkAsync(chunk);
      }
      catch (Exception ex)
      {
        exceptions.Add(ex);
      }
    });
    
    if (exceptions.Any())
    {
      // Process Exceptios
      // throw new AggregateException(exceptions);
    }

  }

  // ChunkBenchmarks using foreach loop to create chunks
  [Benchmark]
  public Task BenchmarkManualChunk()
  {
    var exceptions = new ConcurrentBag<Exception>();
    
    var chunks = CreateChunksManually(_data!, 200);
    
    Parallel.ForEach(chunks,_parallelOptions, async chunk =>
    {
      try
      {
        await ProcessChunkAsync(chunk);
      }
      catch (Exception ex)
      {
        exceptions.Add(ex);
      }
    });
    
    if (exceptions.Any())
    {
      // Process Exceptios
      // throw new AggregateException(exceptions);
    }

    return Task.CompletedTask;
  }

  [Benchmark]
  public async Task BenchmarkLinqChunkSemaphore()
  {
    var exceptions = new ConcurrentBag<Exception>();
    var chunks = _data.Chunk(200);

    // Limit parallelism
    var semaphore = new SemaphoreSlim(4);

    var tasks = chunks.Select(async chunk =>
    {
      await semaphore.WaitAsync();
      try
      {
        await ProcessChunkAsync(chunk);
      }
      catch (Exception ex)
      {
        exceptions.Add(ex);
      }
      finally
      {
        semaphore.Release();
      }
    });
    await Task.WhenAll(tasks);

    if (exceptions.Any())
    {
      //throw new AggregateException(exceptions);
    }
  }
  
  [Benchmark]
  public async Task BenchmarkManualChunkSemaphore()
  {
    var exceptions = new ConcurrentBag<Exception>();
    var chunks = CreateChunksManually(_data, 200);

    // Limit parallelism
    var semaphore = new SemaphoreSlim(4);

    var tasks = chunks.Select(async chunk =>
    {
      await semaphore.WaitAsync();
      try
      {
        await ProcessChunkAsync(chunk);
      }
      catch (Exception ex)
      {
        exceptions.Add(ex);
      }
      finally
      {
        semaphore.Release();
      }
    });
    await Task.WhenAll(tasks);

    if (exceptions.Any())
    {
      //throw new AggregateException(exceptions);
    }
  }
  
  // Method to create chunks manually using foreach loop
  private static IEnumerable<IEnumerable<T>> CreateChunksManually<T>(IEnumerable<T> source, int chunkSize)
  {
    var chunk = new List<T>(chunkSize);
    foreach (var item in source)
    {
      chunk.Add(item);
      if (chunk.Count == chunkSize)
      {
        yield return chunk.ToArray();
        chunk.Clear();
      }
    }

    if (chunk.Count > 0)
    {
      yield return chunk.ToArray();
    }
  }
  
  private static async Task ProcessChunkAsync(IEnumerable<TestData> chunk)
  {
    foreach (var item in chunk)
    {
      await Task.Delay(10);
      if (item.Id % 1000 == 0)
      {
        throw new Exception($"Error processing chunk starting with Id {item.Id}");
      }
    }
  }
  
  private static IEnumerable<TestData> GenerateLargeEnumerable()
  {
    for (int i = 1; i <= 10000; i++)
    {
      yield return new TestData
      {
        Id = i,
        Title = $"Title {i}",
        Description = $"Description {i}"
      };
    }
  }
}

public class TestData
{
  public int Id { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
}