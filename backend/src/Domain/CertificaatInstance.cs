namespace CvViewer.Domain;

public sealed record CertificaatInstance
{
    public required string Naam { get; init; }
    public required string Uitgever { get; init; }
    public required DateOnly DatumAfgifte { get; init; }
    public DateOnly? Verloopdatum { get; init; }
    public string? Url { get; init; }

    public override string ToString() => $"{Naam} {Uitgever} {DatumAfgifte}";
}
