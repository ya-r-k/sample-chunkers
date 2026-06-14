using System.Text;
using RagDataTools.Ingest;
using UglyToad.PdfPig;

namespace RagDataTools.Ingest.Conversion;

/// <summary>
/// Converts PDF content into page-separated Markdown text.
/// </summary>
public sealed class PdfDocumentToMarkdownConverter : IDocumentToMarkdownConverter
{
    public DocumentFormat Format => DocumentFormat.Pdf;

    public Task<IReadOnlyList<MarkdownPageSegment>> ConvertAsync(Stream document, CancellationToken cancellationToken = default)
    {
        try
        {
            using var pdf = PdfDocument.Open(document);
            var segments = new List<MarkdownPageSegment>(pdf.NumberOfPages);

            for (var pageNumber = 1; pageNumber <= pdf.NumberOfPages; pageNumber++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var text = NormalizeMarkdown(pdf.GetPage(pageNumber).Text);
                if (!string.IsNullOrWhiteSpace(text))
                {
                    segments.Add(new MarkdownPageSegment(pageNumber, text));
                }
            }

            return Task.FromResult<IReadOnlyList<MarkdownPageSegment>>(segments);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidDataException("The PDF document is malformed and could not be converted to Markdown.", ex);
        }
    }

    private static string NormalizeMarkdown(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return string.Empty;
        }

        var normalized = text.Replace("\r\n", "\n", StringComparison.Ordinal)
            .Replace('\r', '\n');

        var lines = normalized
            .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(line => line.Length > 0)
            .ToArray();

        return lines.Length == 0
            ? string.Empty
            : string.Join(Environment.NewLine, lines);
    }
}
