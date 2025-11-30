namespace CvViewer.Shared.Extensions;

public static class DateOnlyExtensions
{
    extension(DateOnly dateOnly)
    {
        public DateTime DateTimeUtc => DateTime.SpecifyKind(dateOnly.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc);
        public DateTime DateTimeLocal => DateTime.SpecifyKind(dateOnly.ToDateTime(TimeOnly.MinValue), DateTimeKind.Local);
    }
}
