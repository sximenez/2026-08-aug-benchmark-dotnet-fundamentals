using BenchmarkDotNet.Attributes;

namespace BenchmarkDotNetFundamentals.Stages;

public class SetupIsolationBenchmarks
{
    private double[] _data = null!;

    [GlobalSetup]
    public void Setup()
    {
        _data = Enumerable.Range(1, 1000).Select(i => (double)i).ToArray();
    }

    [Benchmark(Baseline = true)]
    public double SumWithGlobalSetup()
    {
        double sum = 0;
        for (int i = 0; i < _data.Length; i++)
        {
            sum += _data[i];
        }

        return sum;
    }

    [Benchmark]
    public double SumWithInlineSetup()
    {
        double sum = 0;
        
        double[] data = Enumerable.Range(1, 1000).Select(i => (double)i).ToArray();
        for (int i = 0; i < data.Length; i++)
        {
            sum += data[i];
        }
        
        return sum;
    }
}
