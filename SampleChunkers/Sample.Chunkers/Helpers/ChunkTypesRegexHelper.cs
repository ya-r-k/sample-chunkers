using System.Text.RegularExpressions;

namespace Sample.Chunkers.Helpers;

internal static partial class ChunkTypesRegexHelper
{
    [GeneratedRegex(@"```\s*(\w*)\s*\n([\s\S]*?)```", RegexOptions.Multiline)]
    internal static partial Regex GetCodeBlockRegex();

    [GeneratedRegex(@"<table[^>]*>[\s\S]*?</table>", RegexOptions.Multiline)]
    internal static partial Regex GetHtmlTableRegex();

    [GeneratedRegex(@"\[([^\]]*|\[RELATEDCHUNK][^\]]*\[/RELATEDCHUNK][^\]]*)\]\(([^)]+)\)", RegexOptions.Singleline)]
    internal static partial Regex GetExternalLinkRegex();

    [GeneratedRegex(@"(#+)\s*(.+)", RegexOptions.Multiline)]
    internal static partial Regex GetHeadingRegex();

    [GeneratedRegex(@"!\\?\[([^\]]*)\]\(([^)]+)\)", RegexOptions.Singleline)]
    internal static partial Regex GetImageLinkRegex();

    [GeneratedRegex(@"\[RELATEDCHUNK\]([a-zA-Z\-]+)-(\d+)\[\/RELATEDCHUNK\]", RegexOptions.Multiline)]
    internal static partial Regex GetChunkLabelRegex();

    [GeneratedRegex(@"(\[RELATEDCHUNK\]\S+\[/RELATEDCHUNK\])\s*", RegexOptions.Multiline)]
    internal static partial Regex GetChunkLabelWithWhitespacesRegex();
}
