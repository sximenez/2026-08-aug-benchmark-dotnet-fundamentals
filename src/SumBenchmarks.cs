using BenchmarkDotNet.Attributes;

namespace BenchmarkDotNetFundamentals;

public class SumBenchmarks
{
    private readonly double[] data = Enumerable.Range(0, 1000)
        .Select(i => (double)i)
        .ToArray();

    [Benchmark]
    public double SumReturned()
    {
        double sum = 0;
        for (int i = 0; i < data.Length; i++)
        {
            sum += data[i];
        }

        return sum;
    }

    [Benchmark]
    public void SumDiscarded()
    {
        double sum = 0;
        for (int i = 0; i < data.Length; i++)
        {
            sum += data[i];
        }

        // No return value on purpose.
    }
}
