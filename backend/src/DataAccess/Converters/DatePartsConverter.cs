using CvViewer.Types;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CvViewer.DataAccess.Converters;

public static class DatePartsConverter
{
    public static ValueConverter<DateParts, DateOnly> Converter
        => new(
            v => v.DateOnly,
            v => new DateParts(v.Year, v.Month, v.Day)
        );

    public static ValueConverter<DateParts?, DateOnly?> NullableConverter
        => new(
            v => v == null
                ? null
                : v.Value.DateOnly,
            v => v == null
                ? null
                : new DateParts(v.Value.Year, v.Value.Month, v.Value.Day)
        );

    public static ValueComparer<DateParts> ValueComparer
        => new(
            (l, r) =>
                l.Year == r.Year &&
                l.Month == r.Month &&
                l.Day == r.Day,
            v => HashCode.Combine(v.Year, v.Month, v.Day),
            v => new DateParts(v.Year, v.Month, v.Day)
        );

    public static ValueComparer<DateParts?> NullableValueComparer
        => new(
            (l, r) =>
                l == null && r == null ||
                l != null && r != null &&
                l.Value.Year == r.Value.Year &&
                l.Value.Month == r.Value.Month &&
                l.Value.Day == r.Value.Day,
            v => v == null
                ? 0
                : HashCode.Combine(v.Value.Year, v.Value.Month, v.Value.Day),
            v => v == null
                ? null
                : new DateParts(v.Value.Year, v.Value.Month, v.Value.Day)
        );
}
