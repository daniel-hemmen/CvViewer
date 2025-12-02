namespace CvViewer.Types;

public sealed record DatePartsRange
{
    public DateParts? Start { get; }
    public DateParts? End { get; }

    public DatePartsRange(DateParts? start, DateParts? end)
    {
        if (EndDatePrecedesStartDate(start, end))
        {
            throw new ArgumentException("End date cannot precede start date");
        }

        Start = start;
        End = end;
    }

    public override string ToString()
        => (Start, End) switch
        {
            (null, null) => "",
            (var start, null) => $"{start} - heden",
            (null, var end) => $"{end}",
            (var start, var end) => $"{start} - {end}",
        };

    private static bool EndDatePrecedesStartDate(DateParts? start, DateParts? end)
        => start.HasValue &&
            end.HasValue &&
            start.Value.DateOnly > end.Value.DateOnly;
}
