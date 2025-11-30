namespace CvViewer.DataAccess.Entities;


public class AdresEntity
{
    public required string Straat { get; set; }
    public required string Huisnummer { get; set; }
    public required string Plaats { get; set; }
    public required string Postcode { get; set; }
    public required string Land { get; set; }
}
