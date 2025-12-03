namespace CvViewer.ApplicationServices.FileReader.DTOs;

public sealed record WerkervaringDto(
    string Rol,
    string Organisatie,
    int? StartdatumDag,
    int? StartdatumMaand,
    int StartdatumJaar,
    int? EinddatumDag,
    int? EinddatumMaand,
    int? EinddatumJaar,
    string Beschrijving,
    string Plaats);
