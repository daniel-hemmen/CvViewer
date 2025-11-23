namespace CvViewer.Domain;

public sealed record Author
{
    public required string FirstName { get; set; }
    public string? Interfix { get; set; }
    public required string LastName { get; set; }
    public Address? Address { get; set; }
    public ContactInformation? ContactInformation { get; set; }
}
