namespace CvViewer.Domain;

public sealed record Contactgegevens
{
    public string? Email { get; init; }
    public string? Telefoonnummer { get; init; }
    public string? LinkedInUrl { get; init; }
}
