using BenchmarkDotNet.Attributes;

namespace BenchmarkDotNetFundamentals.Stages;

public class ArgumentsBenchmarks
{
    // Arguments are simple values passed to the benchmark method.
    [Benchmark]
    [Arguments(10)]
    [Arguments(1_000)]
    [Arguments(100_000)]
    public int SumRange(int n)
    { 
        int sum = 0;
        for (int i = 0; i < n; i++)
        {
            sum += i;
        }

        return sum;
    }

    // ArgumentsSource are more complex computed values passed via a submethod.
    [Benchmark]
    [ArgumentsSource(nameof(GetArrays))]
    public int SumArray(int[] array)
    {
        int sum = 0;
        for (int i = 0; i < array.Length; i++)
        {
            sum += array[i];
        }

        return sum;
    }

    public static IEnumerable<int[]> GetArrays()
    {
        yield return Enumerable.Range(0, 10).ToArray();
        yield return Enumerable.Range(0, 1_000).ToArray();
        yield return Enumerable.Range(0, 100_000).ToArray();
    }
}
