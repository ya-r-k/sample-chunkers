using System.Text.RegularExpressions;

namespace Sample.Chunkers.Helpers;

internal static partial class CommonRegexHelper
{
    [GeneratedRegex(@"  {2,}")]
    internal static partial Regex GetMultipleSpacesRegex();
}
