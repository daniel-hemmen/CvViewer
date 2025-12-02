using System.Text.Json;
using CvViewer.Types;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CvViewer.DataAccess.Converters;

public static class DatePartsConverter
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
        PropertyNameCaseInsensitive = true
    };

    public static ValueConverter<DateParts, string> Converter
        => new(
            v => JsonSerializer.Serialize(v, SerializerOptions),
            v => JsonSerializer.Deserialize<DateParts>(v, SerializerOptions)
        );

    public static ValueConverter<DateParts?, string?> NullableConverter
        => new(
            v => v == null
                ? null
                : JsonSerializer.Serialize(v.Value, SerializerOptions),
            v => v == null
                ? null
                : JsonSerializer.Deserialize<DateParts>(v, SerializerOptions)
        );

    public static ValueComparer<DateParts> ValueComparer
        => new(
            (l, r) => l.Year == r.Year && l.Month == r.Month && l.Day == r.Day,
            v => HashCode.Combine(v.Year, v.Month, v.Day),
            v => v
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
            v => v
        );
}
