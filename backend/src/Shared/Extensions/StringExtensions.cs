using Shared.Helpers;

namespace CvViewer.Shared.Extensions;

public static class StringExtensions
{
    public static string Capitalize(this string input)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);

        return RegexHelpers
            .HasLeadingLowercaseLetter()
            .Replace(
                input,
                m => m.Groups[1].Value.ToUpper() + m.Groups[2].Value);
    }
}
