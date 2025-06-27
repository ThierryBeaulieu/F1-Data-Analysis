using System;
using System.Management;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Models;

namespace F1_Data_Analysis.Models;

public interface IFileParser
{
    public Task<LapTime[]> FetchContent();
}

public class CsvParser(IConfiguration config) : IFileParser
{
    private readonly string? LapTimesPath = config["DataFiles:LapTimes"];
    private readonly int N_LINE_FOR_HEADER = 1;
    public Task<LapTime[]> FetchContent()
    {
        // verify if LapTimesPath exists
        if (LapTimesPath == null) Task.FromResult(Array.Empty<LapTime>());

        var nLines = File.ReadLines(LapTimesPath!).Count();
        var lapTimes = new LapTime[nLines - N_LINE_FOR_HEADER];

        var lineIndex = 0;
        var isHeaderSkipped = false;
        foreach (var line in File.ReadLines(LapTimesPath!))
        {
            if (!isHeaderSkipped)
            {
                isHeaderSkipped = true;
                continue;
            }
            lapTimes[lineIndex] = LineParser.ParseLine(line);
            lineIndex++;
        }
        return Task.FromResult(lapTimes);
    }

    public static class LineParser
    {
        public static LapTime ParseLine(string line)
        {
            string[] lapTimeTokens = line.Split(',');
            var lapTime = new LapTime(
                lapTimeTokens[0],
                lapTimeTokens[1],
                lapTimeTokens[2],
                lapTimeTokens[3],
                lapTimeTokens[4],
                lapTimeTokens[5]
            );
            return lapTime;
        }
    }

}
