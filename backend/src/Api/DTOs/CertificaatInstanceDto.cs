namespace CvViewer.Api.DTOs;

public sealed record CertificaatInstanceDto(
    string Naam,
    string Uitgever,
    string DatumAfgifte,
    string? Verloopdatum,
    string? Url);
