using System.Globalization;
using System.Xml;

namespace Models
{
    public record LapTime(
        string? RaceId,
        string? DriverId,
        string? Lap,
        string? Position,
        string? Time,
        string? Milliseconds
    );
}


