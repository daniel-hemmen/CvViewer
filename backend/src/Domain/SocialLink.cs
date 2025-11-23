namespace CvViewer.Domain
{
    public class SocialLink
    {
        public Guid Id { get; init; }
        public required string Platform { get; init; }
        public required Uri Url { get; init; }
    }
}
