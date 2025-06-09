using System;
using Models;

namespace F1_Data_Analysis.Models;

public interface IFileParser
{
    public void FetchContent(LapTimes lapTime);
}

public class CsvParser : IFileParser
{
    private readonly string LapTimesPath = "Kaggle-Data/lap_times.csv";
    private readonly int N_LINE_FOR_HEADER = 1;

    public void FetchContent(LapTimes lapTime)
    {
        var nLines = File.ReadLines(LapTimesPath).Count();
        lapTime.Init(nLines - N_LINE_FOR_HEADER);

        var lineIndex = 0;
        var isHeaderSkipped = false;
        foreach (var line in File.ReadLines(LapTimesPath))
        {
            if (!isHeaderSkipped)
            {
                isHeaderSkipped = true;
                continue;
            }
            lapTime.Add(lineIndex, line.Split(','));
            lineIndex++;
        }
    }
}
