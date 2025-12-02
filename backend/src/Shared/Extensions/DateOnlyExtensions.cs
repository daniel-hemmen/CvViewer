using CvViewer.Types;

namespace CvViewer.Shared.Extensions;

public static class DateOnlyExtensions
{
    public static DateParts ToDateParts(this DateOnly dateOnly)
        => new(dateOnly.Year, dateOnly.Month, dateOnly.Day);

    extension(DateOnly dateOnly)
    {
        public DateTime DateTimeUtc => DateTime.SpecifyKind(dateOnly.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc);
        public DateTime DateTimeLocal => DateTime.SpecifyKind(dateOnly.ToDateTime(TimeOnly.MinValue), DateTimeKind.Local);

        public DateParts DateParts => dateOnly.ToDateParts();
    }
}
