using System.Text.Json.Serialization;

namespace CvViewer.Types;

public readonly struct DateParts
{
    public int Year { get; }
    public int? Month { get; }
    public int? Day { get; }

    public DateOnly DateOnly => new(Year, Month ?? 1, Day ?? 1);

    [JsonConstructor]
    public DateParts(int year, int? month, int? day)
    {
        Year = year;
        Month = month;
        Day = day;
    }

    public override string ToString()
        => (Day, Month, Year) switch
        {
            (null, null, var year) => $"{year:D4}",
            (null, var month, var year) => $"{month:D2}-{year:D4}",
            (var day, var month, var year) => $"{day:D2}-{month:D2}-{year:D4}",
        };
}
