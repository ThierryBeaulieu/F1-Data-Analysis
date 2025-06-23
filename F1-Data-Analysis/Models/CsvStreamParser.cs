using Models;

namespace F1_Data_Analysis.Models;

public class CsvStreamParser(IConfiguration config) : IFileParser
{
    private readonly string? _lapTimesPath = config["DataFiles:LapTimes"];
    private readonly int N_LINE_FOR_HEADER = 1;
    public void FetchContent(LapTimes lapTime)
    {
        if (_lapTimesPath is null || !File.Exists(_lapTimesPath)) return;

        var nLines = File.ReadLines(_lapTimesPath!).Count();
        lapTime.Init(nLines - N_LINE_FOR_HEADER);

        using var reader = new StreamReader(_lapTimesPath);
        var lineCount = 0;
        var headerRead = false;

        while (!reader.EndOfStream)
        {
            string? line = reader.ReadLine();
            if (line == null) continue;
            if (!headerRead)
            {
                headerRead = true;
                continue;
            }

            lineCount++;
            if (lineCount <= N_LINE_FOR_HEADER) continue;

            ReadOnlySpan<char> lineSpan = line.AsSpan();
            lapTime.Add(lineCount, SplitByComma(lineSpan));
        }
    }

    public string[] SplitByComma(ReadOnlySpan<char> line)
    {
        var parts = new List<string>();
        int start = 0;

        while (true)
        {
            int index = line.Slice(start).IndexOf(',');

            if (index == -1)
            {
                // Last segment
                parts.Add(line.Slice(start).ToString());
                break;
            }

            parts.Add(line.Slice(start, index).ToString());
            start += index + 1; // move past the comma
        }

        return parts.ToArray();
    }
}
