namespace CvViewer.DataAccess.Entities;

public class AuteurEntity
{
    public long Id { get; set; }
    public required string Voornaam { get; set; }
    public string? Tussenvoegsel { get; set; }
    public required string Achternaam { get; set; }
    public required AdresEntity Adres { get; set; }
    public required ContactgegevensEntity Contactgegevens { get; set; }
    public List<CvEntity> Cvs { get; set; } = [];
}
