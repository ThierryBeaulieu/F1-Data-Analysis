using Models;

namespace F1_Data_Analysis.Models;

public class CsvStreamParser(IConfiguration config) : IFileParser
{
    private readonly string? _lapTimesPath = config["DataFiles:LapTimes"];
    private readonly int N_LINE_FOR_HEADER = 1;
    public void FetchContent(LapTimes lapTime)
    {
        if (_lapTimesPath is null || !File.Exists(_lapTimesPath)) return;

        using var reader = new StreamReader(_lapTimesPath);
        var lineCount = 0;

        while (!reader.EndOfStream)
        {
            string? line = reader.ReadLine();
            if (line == null) continue;

            lineCount++;
            if (lineCount <= N_LINE_FOR_HEADER) continue;

            ReadOnlySpan<char> lineSpan = line.AsSpan();
            var result = SplitByComma(lineSpan);
        }
    }

    public IEnumerable<ReadOnlySpan<char>> SplitByComma(ReadOnlySpan<char> line)
    {
        int start = 0;

        while (true)
        {
            int index = line.Slice(start).IndexOf(',');

            if (index == -1)
            {
                // Last segment
                yield return line.Slice(start);
                yield break;
            }

            yield return line.Slice(start, index);
            start += index + 1; // move past the comma
        }
    }
}
