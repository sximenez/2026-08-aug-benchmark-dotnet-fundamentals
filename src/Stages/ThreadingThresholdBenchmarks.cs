using BenchmarkDotNet.Attributes;

namespace BenchmarkDotNetFundamentals.Stages
{
    [MemoryDiagnoser]
    [ThreadingDiagnoser]
    public class ThreadingThresholdBenchmarks
    {
        //private const int ItemCount = 8;
        private const int ItemCount = 4;

        //[Params(1_000, 10_000, 50_000, 100_000, 500_000)]
        [Params(10, 100, 500, 1_000)]
        public int WorkPerItem;

        private double IncreaseWork(int seed)
        {
            double acc = 0;
            for (int i = 0; i < WorkPerItem; i++)
            {
                acc += Math.Sqrt(i + seed);
            }

            return acc;
        }

        [Benchmark(Baseline = true)]
        public double Sequential()
        {
            double total = 0;
            for (int i = 0; i < ItemCount; i++)
            {
                total += IncreaseWork(i);
            }

            return total;
        }

        [Benchmark]
        public async Task<double> Parallel()
        {
            Task<double>[] tasks = new Task<double>[ItemCount];
            for (int i = 0; i < ItemCount; i++)
            {
                int seed = i;
                tasks[i] = Task.Run(() => IncreaseWork(seed));
            }

            double[] results = await Task.WhenAll(tasks);

            double total = 0;
            for (int i = 0; i < ItemCount; i++)
            {
                total += results[i];
            }

            return total;
        }
    }
}
