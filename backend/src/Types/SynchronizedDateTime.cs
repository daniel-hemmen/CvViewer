namespace CvViewer.Types;

public sealed class SynchronizedDateTime
{
    private DateTimeOffset _source;

    public DateTime Local
    {
        get => _source.LocalDateTime;
        set
        {
            // DateTimeKind.Unspecified is accepted and treated as Local for UI input-binding purposes
            if (value.Kind == DateTimeKind.Utc)
                throw new ArgumentException("Local time cannot be set with UTC DateTime");

            _source = DateTime.SpecifyKind(value, DateTimeKind.Local);
        }
    }

    public DateTime Utc
    {
        get => _source.UtcDateTime;
        set
        {
            if (value.Kind == DateTimeKind.Local || value.Kind == DateTimeKind.Unspecified)
                throw new ArgumentException("UTC time cannot be set with Local DateTime");

            _source = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
    }

    private SynchronizedDateTime(DateTime dateTime) => _source = dateTime;

    public static SynchronizedDateTime FromDateTime(DateTime dateTime)
    {
        if (dateTime.Kind == DateTimeKind.Unspecified)
            throw new NotSupportedException("DateTimeKind must be specified");

        return new SynchronizedDateTime(dateTime);
    }

    public static implicit operator DateTime(SynchronizedDateTime synchronizedDateTime) => synchronizedDateTime._source.DateTime;
    public static implicit operator SynchronizedDateTime(DateTime dateTime) => FromDateTime(dateTime);
}
