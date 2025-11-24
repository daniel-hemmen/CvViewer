namespace CvViewer.Domain
{
    public sealed class CvMetadata
    {
        public required Guid Id { get; init; }
        public required string AuteurNaam { get; init; }
        public bool IsFavorite { get; init; }
        public DateTime? LastUpdated { get; set; }
    }
}
