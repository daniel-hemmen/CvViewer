namespace CvViewer.ApplicationServices.FileReader.DTOs;

public sealed record OpleidingDto(
    string Naam,
    string Instituut,
    int? StartdatumDag,
    int? StartdatumMaand,
    int? StartdatumJaar,
    int? EinddatumDag,
    int? EinddatumMaand,
    int EinddatumJaar,
    string Beschrijving);
