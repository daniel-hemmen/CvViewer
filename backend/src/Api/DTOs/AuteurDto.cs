namespace CvViewer.Api.DTOs;

public sealed record AuteurDto(
    string Naam,
    string? Email,
    string? Telefoonnummer,
    string? LinkedInUrl
    );
