using BenchmarkDotNet.Attributes;
using StackExchange.Redis;

namespace BenchmarkDotNetFundamentals.Redis
{
    [JsonExporter]
    [MemoryDiagnoser]
    public class RedisBenchmarks
    {
        private const string WarmKey = "bench:warm";
        private const string ColdKey = "bench:missing";

        private ConnectionMultiplexer _connection = null!;
        private IDatabase _db = null!;
        private Dictionary<string, string> _data = null!;

        [GlobalSetup]
        public void Setup()
        {
            _connection = ConnectionMultiplexer.Connect("localhost:6379");
            _db = _connection.GetDatabase();
            _db.StringSet(WarmKey, "value");

            _data = new Dictionary<string, string>()
            {
                [WarmKey] = "value"
            };
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _connection.Dispose();
        }

        [Benchmark(Baseline = true)] // Control.
        public string? LocalLookup()
        {
            return _data.TryGetValue(WarmKey, out string? value) ? value : null;
        }

        [Benchmark]
        public string? RedisGetWarm()
        {
            return _db.StringGet(WarmKey);
        }

        [Benchmark]
        public string? RedisGetCold()
        {
            return _db.StringGet(ColdKey);
        }
    }
}
