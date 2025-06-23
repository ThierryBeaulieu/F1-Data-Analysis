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
    public void Benchmark_FetchContent()
    {
        var lapTime = new LapTimes();
        _parser.FetchContent(lapTime);
    }

    [Benchmark]
    public void Benchmark_Stream_FetchContent()
    {
        var lapTime = new LapTimes();
        _parserStream.FetchContent(lapTime);
    }
}