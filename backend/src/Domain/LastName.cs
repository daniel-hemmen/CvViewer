using CvViewer.Shared.Extensions;

namespace CvViewer.Domain;

public sealed record LastName
{
    public string? Interfix
    {
        get;
        set => field = value?.Capitalize();
    }

    public required string Surname
    {
        get;
        set => field = value.Capitalize();
    }

    public override string ToString()
        => Interfix is null
            ? Surname
            : $"{Interfix.Capitalize()} {Surname}";
}
