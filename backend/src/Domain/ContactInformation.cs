namespace CvViewer.Domain;

public sealed record ContactInformation
{
    public (string Primary, string? Secondary) Email { get; set; }
    public (string Primary, string? Secondary) Phone { get; set; }
}
