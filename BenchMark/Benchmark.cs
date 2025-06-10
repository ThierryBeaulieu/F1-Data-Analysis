using BenchmarkDotNet.Attributes;
using F1_Data_Analysis.Models;
using Models;

public class MyBenchmark()
{
    private CsvParser _csvParser;

    [GlobalSetup]
    public void Setup()
    {
        _csvParser = new CsvParser();
    }

    [Benchmark]
    public void RunImportantMethod()
    {
        LapTimes lapTimes = new();
        _csvParser.FetchContent(lapTimes);
    }
}