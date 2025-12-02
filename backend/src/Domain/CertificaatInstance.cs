using CvViewer.Types;

namespace CvViewer.Domain;

public sealed record CertificaatInstance
{
    public required string Naam { get; init; }
    public required string Uitgever { get; init; }
    public required DateParts DatumAfgifte { get; init; }
    public DateParts? Verloopdatum { get; init; }
    public string? Url { get; init; }

    public override string ToString() => $"{Naam} {Uitgever} {DatumAfgifte}";
}
