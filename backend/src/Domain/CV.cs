namespace CvViewer.Domain;

public record CV
{
    public Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Blurb { get; init; }
    public required Author Author { get; init; }
    public List<JobExperience> JobExperiences { get; init; } = [];
}
