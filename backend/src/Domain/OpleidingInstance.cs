namespace CvViewer.Domain
{
    public class OpleidingInstance
    {
        public required string Naam { get; init; }
        public required string Instituut { get; init; }
        public required DateOnly? Startdatum { get; init; }
        public DateOnly Einddatum { get; init; }
        public string? Beschrijving { get; init; }
    }
}
