namespace CvViewer.Domain
{
    public sealed record CvContactInformation
    {
        public Guid Id { get; init; }
        public required string Email { get; init; }
        public string? PhoneNumber { get; init; }
        public string? WebsiteUrl { get; init; }
        public IEnumerable<SocialLink>? SocialLinks { get; init; }
    }
}
