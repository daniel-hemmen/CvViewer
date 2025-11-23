using System.Text.RegularExpressions;

namespace Shared.Helpers;

public partial class RegexHelpers
{
    [GeneratedRegex(@"^0.*$")]
    public static partial Regex HasLeadingZero();

    [GeneratedRegex(@"^([a-z])(.*)$")]
    public static partial Regex HasLeadingLowercaseLetter();
}
