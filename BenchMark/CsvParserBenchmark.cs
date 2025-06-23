using BenchmarkDotNet.Attributes;
using F1_Data_Analysis.Models;
using Microsoft.Extensions.Configuration;
using Models;

[MemoryDiagnoser]
public class CsvParserBenchmark
{
    private IFileParser _parser = null!;
    private LapTimes _lapTimes = null!;

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

        _lapTimes = new LapTimes();
    }

    [Benchmark]
    public void Benchmark_FetchContent()
    {
        _parser.FetchContent(_lapTimes);
    }
}