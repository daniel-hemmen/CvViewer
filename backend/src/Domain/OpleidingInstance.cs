namespace CvViewer.Domain;

public sealed record OpleidingInstance
{
    public required string Naam { get; init; }
    public required string Instituut { get; init; }
    public required DateOnly? Startdatum { get; init; }
    public DateOnly Einddatum { get; init; }
    public string? Beschrijving { get; init; }

    public override string ToString() => $"{Instituut} - {Naam} | {FormatDates()}";

    private string FormatDates()
        => Startdatum.HasValue
            ? $"{Startdatum:dd-MM-yyyy} - {Einddatum:dd-MM-yyyy}"
            : $"{Einddatum:dd-MM-yyyy}";
}
