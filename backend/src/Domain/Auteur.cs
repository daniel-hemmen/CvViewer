namespace CvViewer.Domain;

public sealed record Auteur
{
    public required string Voornaam { get; init; }
    public string? Tussenvoegsel { get; init; }
    public required string Achternaam { get; init; }
    public Adres? Adres { get; init; }
    public Contactgegevens? Contactgegevens { get; init; }
}
