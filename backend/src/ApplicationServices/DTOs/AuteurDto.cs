namespace CvViewer.ApplicationServices.DTOs;

public sealed record AuteurDto()
{
    public required string Naam { get; init; }
    public string? Email { get; set; }
    public string? Telefoonnummer { get; set; }
    public string? LinkedInUrl { get; set; }
}
