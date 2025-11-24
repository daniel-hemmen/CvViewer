namespace CvViewer.Domain
{
    public class CertificaatInstance
    {
        public required string Naam { get; init; }
        public required string Uitgever { get; init; }
        public required DateOnly? DatumAfgifte { get; init; }
        public DateOnly? Verloopdatum { get; init; }
        public string? Url { get; init; }
    }
}
