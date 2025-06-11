using System.Globalization;
using System.Xml;

namespace Models
{
    public class LapTime
    {
        public string? raceId { get; set; } = "";
        public string? driverId { get; set; } = "";
        public string? lap { get; set; } = "";
        public string? position { get; set; } = "";
        public string? time { get; set; } = "";
        public string? milliseconds { get; set; } = "";
    }

    public class LapTimes
    {
        private const int ELEMENT_IN_LAP_TIME = 6;
        public string?[] RaceId { get; set; } = [];
        public string?[] DriverId { get; set; } = [];
        public string?[] Lap { get; set; } = [];
        public string?[] Position { get; set; } = [];
        public string?[] Time { get; set; } = [];
        public string?[] Milliseconds { get; set; } = [];
        public int Length { get; set; } = 0;

        public void Init(int nLapTime)
        {
            Length = nLapTime;
            RaceId = new string[nLapTime];
            DriverId = new string[nLapTime];
            Lap = new string[nLapTime];
            Position = new string[nLapTime];
            Time = new string?[nLapTime];
            Milliseconds = new string[nLapTime];
        }

        public void Add(int index, string[] lapTime)
        {
            if (lapTime.Length != ELEMENT_IN_LAP_TIME ||
                index >= Length ||
                index < 0
            )
            {
                return;
            }

            RaceId[index] = lapTime[0];
            DriverId[index] = lapTime[1];
            Lap[index] = lapTime[2];
            Position[index] = lapTime[3];
            Time[index] = lapTime[4];
            Milliseconds[index] = lapTime[5];
        }

        public LapTime? GetLapTimeByIndex(int index)
        {
            if (index < 0 || index >= Length) return null;

            return new LapTime()
            {
                raceId = RaceId[index],
                driverId = DriverId[index],
                lap = Lap[index],
                position = Position[index],
                time = Time[index],
                milliseconds = Milliseconds[index]
            };
        }
    }
}


