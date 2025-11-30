namespace CvViewer.Domain;

public sealed record WerkervaringInstance
{
    public required string Bedrijfsnaam { get; init; }
    public required string Functietitel { get; init; }
    public DateOnly Startdatum { get; init; }
    public DateOnly? Einddatum { get; init; }
    public string? Locatie { get; init; }
    public string? Beschrijving { get; init; }

    public override string ToString() => $"{Bedrijfsnaam} - {Functietitel} | {FormatDates()}";

    private string FormatDates()
        => Einddatum.HasValue
            ? $"{Startdatum:dd-MM-yyyy} - {Einddatum:dd-MM-yyyy}"
            : $"{Startdatum:dd-MM-yyyy} - heden";
}
