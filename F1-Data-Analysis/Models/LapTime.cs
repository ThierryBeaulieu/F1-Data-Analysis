using System.Globalization;
using System.Xml;


public class LapTime
{
    public string? raceId;
    public string? driverId;
    public string? lap;
    public string? position;
    public TimeSpan? time;
    public string? milliseconds;
}

public class LapTimes
{
    private const int ELEMENT_IN_LAP_TIME = 6;
    private const string TIME_FORMAT = @"m\:ss\.fff";
    public string?[] raceId = [];
    public string?[] driverId = [];
    public string?[] lap = [];
    public string?[] position = [];
    public TimeSpan?[] time = [];
    public string?[] milliseconds = [];
    public int Length = 0;

    public void Init(int nLapTime)
    {
        Length = nLapTime;
        raceId = new string[nLapTime];
        driverId = new string[nLapTime];
        lap = new string[nLapTime];
        position = new string[nLapTime];
        time = new TimeSpan?[nLapTime];
        milliseconds = new string[nLapTime];
    }

    public void Add(int index, string[] lapTime)
    {
        if (lapTime.Length != ELEMENT_IN_LAP_TIME ||
            Length >= index ||
            index < 0
        )
        {
            return;
        }

        raceId[index] = lapTime[0];
        driverId[index] = lapTime[1];
        lap[index] = lapTime[2];
        position[index] = lapTime[3];
        time[index] = TimeSpan.TryParseExact(lapTime[4], TIME_FORMAT, CultureInfo.InvariantCulture, out TimeSpan result) ? result : null;
        milliseconds[index] = lapTime[5];
    }

    public LapTime? GetLapTimeByIndex(int index)
    {
        if (index < 0 || index >= Length) return null;

        return new LapTime()
        {
            raceId = raceId[index],
            driverId = driverId[index],
            lap = lap[index],
            position = position[index],
            time = time[index],
            milliseconds = milliseconds[index]
        };
    }
}