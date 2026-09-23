using BenchmarkDotNet.Attributes;

namespace BenchmarkDotNetFundamentals;

[MemoryDiagnoser]
[ThreadingDiagnoser]
public class ThreadingBenchmarks
{
    private const int N = 1000;

    [Benchmark(Baseline = true)]
    public int SyncWork()
    {
        int sum = 0;
        for (int i = 0; i < N; i++)
        {
            sum += i;
        }

        return sum;
    }

    [Benchmark]
    public async Task<int> AsyncWork()
    {
        return await Task.Run(() =>
        {
            int sum = 0;
            for (int i = 0; i < N; i++)
            {
                sum += i;
            }

            return sum;
        });
    }
}
