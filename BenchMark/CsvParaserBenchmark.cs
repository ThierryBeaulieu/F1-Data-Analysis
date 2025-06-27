using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using F1_Data_Analysis.Models;
using Microsoft.Extensions.Configuration;
using Models;

[MemoryDiagnoser]
public class CsvParserBenchmark
{
    private IFileParser _parser = null!;
    private IFileParser _parserStream = null!;

    [GlobalSetup]
    public void Setup()
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "DataFiles:LapTimes", "lap_times.csv" }
            })

            .Build();

        _parser = new CsvParser(config);
        _parserStream = new CsvStreamParser(config);

    }

    [Benchmark(Baseline = true)]
    public async Task Benchmark_FetchContent()
    {
        LapTime[] lapTimes = [];
        await _parser.FetchContent();
    }

    [Benchmark]
    public async Task Benchmark_Stream_FetchContent()
    {
        LapTime[] lapTimes = [];
        await _parserStream.FetchContent();
    }
}