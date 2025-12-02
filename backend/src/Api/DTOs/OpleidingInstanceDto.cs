namespace CvViewer.Api.DTOs;

public sealed record OpleidingInstanceDto(
    string Naam,
    string Instituut,
    string? Startdatum,
    string Einddatum,
    string? Beschrijving);
