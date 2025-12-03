namespace CvViewer.ApplicationServices.FileReader.DTOs;

public sealed record CertificaatDto(
    string Naam,
    string Uitgever,
    int? DatumAfgifteDag,
    int? DatumAfgifteMaand,
    int DatumAfgifteJaar,
    int? VerloopdatumDag,
    int? VerloopdatumMaand,
    int? VerloopdatumJaar,
    string Url);
