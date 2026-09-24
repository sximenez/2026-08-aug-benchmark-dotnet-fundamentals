using System.Text;
using BenchmarkDotNet.Attributes;

namespace BenchmarkDotNetFundamentals.Stages;

public class StringConcatBenchmarks
{
    private const int Iterations = 100;

    [Benchmark]
    public string Concat()
    {
        string result = string.Empty;
        for (int i = 0; i < Iterations; i++)
        {
            result += "x";
        }

        return result;
    }

    [Benchmark]
    public string StringBuilderAppend()
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < Iterations; i++)
        {
            builder.Append("x");
        }

        return builder.ToString();
    }
}
