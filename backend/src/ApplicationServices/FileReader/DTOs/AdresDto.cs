namespace CvViewer.ApplicationServices.FileReader.DTOs;

public sealed record AdresDto(
    string Straat,
    string Huisnummer,
    string Plaats,
    string Postcode,
    string Land);
