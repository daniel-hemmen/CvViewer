using Shared.Helpers;

namespace CvViewer.Domain;

public sealed record PhoneNumber
{
    public byte CountryCode { get; init; }
    public byte Prefix { get; init; }
    public int Number { get; init; }

    public override string ToString()
        => $"+{CountryCode} {BracketLeadingZero(Prefix)} - {Number}";

    private static string BracketLeadingZero(byte prefix)
        => RegexHelpers.HasLeadingZero().Replace(prefix.ToString(), "(0)");
}
