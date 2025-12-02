using System.Text.Json;
using CvViewer.DataAccess.Snapshots;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CvViewer.DataAccess.Converters;

public static class VaardigheidInstanceConverter
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
        PropertyNameCaseInsensitive = true
    };

    public static ValueConverter<List<VaardigheidInstanceSnapshot>, string> Converter
        => new(
            v => JsonSerializer.Serialize(v, SerializerOptions),
            v => JsonSerializer.Deserialize<List<VaardigheidInstanceSnapshot>>(v, SerializerOptions) ?? new()
        );

    public static ValueComparer<List<VaardigheidInstanceSnapshot>> ValueComparer
        => new(
            equalsExpression: (l1, l2) =>
                l1 == null && l2 == null ||
                l1 != null && l2 != null &&
                JsonSerializer.Serialize(l1, SerializerOptions) == JsonSerializer.Serialize(l2, SerializerOptions),
            hashCodeExpression: l =>
                l == null
                    ? 0
                    : JsonSerializer.Serialize(l, SerializerOptions).GetHashCode(),
            snapshotExpression: l =>
                l == null
                    ? new()
                    : JsonSerializer.Deserialize<List<VaardigheidInstanceSnapshot>>(
                        JsonSerializer.Serialize(l, SerializerOptions), SerializerOptions) ?? new()
        );
}
