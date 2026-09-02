using BenchmarkDotNet.Running;
using BenchmarkDotNetFundamentals;

public class Program()
{
    public static void Main(string[] args)
    {
        //BenchmarkRunner.Run<StringConcatBenchmarks>();
        //BenchmarkRunner.Run<SumBenchmarks>();
        //BenchmarkRunner.Run<SetupIsolationBenchmarks>();
        //BenchmarkRunner.Run<DictionaryLookupBenchmarks>();
        //BenchmarkRunner.Run<SumGapBenchmarks>();
        BenchmarkRunner.Run<DisassemblyBenchmarks>();
    }
}