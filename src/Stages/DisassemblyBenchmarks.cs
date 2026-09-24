using BenchmarkDotNet.Attributes;

namespace BenchmarkDotNetFundamentals.Stages;

[MemoryDiagnoser]
[DisassemblyDiagnoser(printSource: true, maxDepth: 3)]
public class DisassemblyBenchmarks
{
    private const int N = 100_000;
    private int[] _array = null!;

    [GlobalSetup]
    public void Setup()
    {
        //_array = Enumerable.Range(0, N).ToArray();
        _array = new int[N];
        for (int i = 0; i < N; i++)
        {
            _array[i] = i;
        }
    }

    [Benchmark(Baseline = true)]
    public int SumRange()
    {
        int sum = 0;
        for (int i = 0; i < N; i++)
        {
            sum += i;
        }

        return sum;
    }

    [Benchmark]
    public int SumArray()
    {
        int sum = 0;
        for (int i = 0; i < _array.Length; i++)
        {
            sum += _array[i];
        }

        return sum;
    }
}
