namespace CvViewer.Shared.Extensions;

public static class DateTimeExtensions
{
    extension(DateTime dateTime)
    {
        public DateOnly DateOnlyUtc => DateOnly.FromDateTime(dateTime.ToUniversalTime());
        public DateOnly DateOnlyLocal => DateOnly.FromDateTime(dateTime.ToLocalTime());
    }
}
