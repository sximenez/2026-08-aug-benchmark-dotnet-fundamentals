using BenchmarkDotNet.Attributes;

namespace BenchmarkDotNetFundamentals;

public class DictionaryLookupBenchmarks
{
    //[Params(10, 1_000, 100_000)]
    [Params(10, 100, 1_000, 10_000, 100_000, 1_000_000)]
    public int N;

    private Dictionary<int, int> _dictionary = null!;
    private List<int> _list = null!;

    [GlobalSetup]
    public void Setup()
    {
        _dictionary = new Dictionary<int, int>();
        _list = new List<int>();
        for (int i = 0; i < N; i++)
        {
            _dictionary[i] = i;
            _list.Add(i);
        }
    }

    [Benchmark(Baseline = true)]
    public bool DictionaryLookup()
    {
        return _dictionary.TryGetValue(N / 2 , out _);
    }

    [Benchmark]
    public bool ListLookup()
    {
        return _list.Contains(N / 2);
    }
}
