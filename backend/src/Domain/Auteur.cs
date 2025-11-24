namespace CvViewer.Domain;

public sealed record Auteur
{
    public required string Voornaam { get; set; }
    public string? Tussenvoegsel { get; set; }
    public required string Achternaam { get; set; }
    public Adres? Adres { get; set; }
    public Contactgegevens? Contactgegevens { get; set; }
}
