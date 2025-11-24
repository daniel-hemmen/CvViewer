namespace CvViewer.Domain
{
    public sealed class WerkervaringInstance
    {
        public required string Bedrijfsnaam { get; init; }
        public required string Functietitel { get; init; }
        public DateTime StartDatum { get; init; }
        public DateTime? EindDatum { get; init; }
        public string? Locatie { get; init; }
        public string? Beschrijving { get; init; }
    }
}
