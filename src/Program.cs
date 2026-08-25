using BenchmarkDotNet.Running;
using BenchmarkDotNetFundamentals;

public class Program()
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<StringConcatBenchmarks>();
    }
}