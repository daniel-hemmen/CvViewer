namespace CvViewer.Api.DTOs;

public sealed record WerkervaringInstanceDto(
    string Rol,
    string Organisatie,
    string? Startdatum,
    string? Einddatum,
    string? Beschrijving,
    string? Plaats);
