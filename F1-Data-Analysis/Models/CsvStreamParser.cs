using System.Buffers;
using System.IO.Pipelines;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.CodeAnalysis;
using Microsoft.Diagnostics.Tracing.Parsers.AspNet;
using Models;

namespace F1_Data_Analysis.Models;

public class CsvStreamParser(IConfiguration config) : IFileParser
{
    public static ReadOnlySpan<byte> NewLine => "\r\n"u8;
    private readonly string? _lapTimesPath = config["DataFiles:LapTimes"];
    private readonly int N_LINE_FOR_HEADER = 1;
    public async Task<LapTime[]> FetchContent()
    {
        if (_lapTimesPath is null || !File.Exists(_lapTimesPath)) return [];

        var nLines = File.ReadLines(_lapTimesPath!).Count();
        var lapTimes = new LapTime[nLines - N_LINE_FOR_HEADER];
        var position = 0;

        using var stream = File.OpenRead(_lapTimesPath!);
        var pipeReader = PipeReader.Create(stream);
        var headerRead = false;
        while (true)
        {
            var result = await pipeReader.ReadAsync();
            var buffer = result.Buffer;
            var sequencePosition = ParseLines(lapTimes, ref buffer, ref position, ref headerRead);

            pipeReader.AdvanceTo(sequencePosition, buffer.End);

            if (result.IsCompleted)
            {
                break;
            }
        }

        pipeReader.Complete();

        return lapTimes;
    }

    private SequencePosition ParseLines(LapTime[] lapTimes, ref ReadOnlySequence<byte> buffer, ref int position, ref bool headerRead)
    {
        var reader = new SequenceReader<byte>(buffer);
        while (!reader.End)
        {
            if (!reader.TryReadToAny(out ReadOnlySpan<byte> line, NewLine, true))
            {
                break; // we don't have a new line in the current data;
            }
            if (!headerRead)
            {
                headerRead = true;
                break;
            }
            LapTime? parsedLine = LineParser.ParseLine(line); // we have a line to parse
            if (parsedLine is not null)
            {
                lapTimes[position++] = parsedLine;
            }
        }
        return reader.Position;
    }

    public static class LineParser
    {
        public static LapTime? ParseLine(ReadOnlySpan<byte> line)
        {
            string lineStr = System.Text.Encoding.UTF8.GetString(line);
            var lapTimeTokens = lineStr.Split(',');

            if (lapTimeTokens.Length < 6)
                return null;

            return new LapTime(
                lapTimeTokens[0],
                lapTimeTokens[1],
                lapTimeTokens[2],
                lapTimeTokens[3],
                lapTimeTokens[4],
                lapTimeTokens[5]
            );
        }
    }
}