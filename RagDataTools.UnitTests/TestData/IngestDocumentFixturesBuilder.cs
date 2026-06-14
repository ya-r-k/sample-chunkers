using System.Globalization;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using RagDataTools.Ingest;

namespace RagDataTools.UnitTests.TestData;

public static class IngestDocumentFixturesBuilder
{
    public static MemoryStream CreateHtml(string html)
    {
        return new MemoryStream(Encoding.UTF8.GetBytes(html));
    }

    public static MemoryStream CreateWordDocument(params string[] pages)
    {
        var stream = new MemoryStream();

        using (var document = WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document, true))
        {
            var mainDocumentPart = document.AddMainDocumentPart();
            mainDocumentPart.Document = new Document(new Body());

            for (var i = 0; i < pages.Length; i++)
            {
                if (i > 0)
                {
                    mainDocumentPart.Document.Body!.Append(new Paragraph(new ParagraphProperties(new PageBreakBefore())));
                }

                mainDocumentPart.Document.Body!.Append(new Paragraph(new Run(new Text(pages[i]) { Space = SpaceProcessingModeValues.Preserve })));
            }

            mainDocumentPart.Document.Save();
        }

        stream.Position = 0;
        return stream;
    }

    public static MemoryStream CreatePdfDocument(params string[] pages)
    {
        var objects = new List<string>();
        var fontObjectNumber = 3 + (pages.Length * 2);

        objects.Add("1 0 obj\n<< /Type /Catalog /Pages 2 0 R >>\nendobj\n");
        objects.Add(BuildPagesObject(pages.Length));

        for (var i = 0; i < pages.Length; i++)
        {
            var pageObjectNumber = 3 + (i * 2);
            var contentObjectNumber = pageObjectNumber + 1;

            objects.Add(BuildPageObject(pageObjectNumber, contentObjectNumber, fontObjectNumber));
            objects.Add(BuildContentObject(contentObjectNumber, pages[i]));
        }

        objects.Add($"{fontObjectNumber} 0 obj\n<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>\nendobj\n");

        var builder = new StringBuilder();
        builder.Append("%PDF-1.4\n");

        var offsets = new List<int> { 0 };
        foreach (var pdfObject in objects)
        {
            offsets.Add(Encoding.ASCII.GetByteCount(builder.ToString()));
            builder.Append(pdfObject);
        }

        var xrefOffset = Encoding.ASCII.GetByteCount(builder.ToString());
        builder.Append($"xref\n0 {objects.Count + 1}\n");
        builder.Append("0000000000 65535 f \n");

        for (var i = 1; i < offsets.Count; i++)
        {
            builder.Append(offsets[i].ToString("D10", CultureInfo.InvariantCulture));
            builder.Append(" 00000 n \n");
        }

        builder.Append($"trailer\n<< /Size {objects.Count + 1} /Root 1 0 R >>\n");
        builder.Append("startxref\n");
        builder.Append(xrefOffset.ToString(CultureInfo.InvariantCulture));
        builder.Append("\n%%EOF\n");

        return new MemoryStream(Encoding.ASCII.GetBytes(builder.ToString()));
    }

    private static string BuildPagesObject(int pageCount)
    {
        var kids = new StringBuilder();
        for (var i = 0; i < pageCount; i++)
        {
            var pageObjectNumber = 3 + (i * 2);
            kids.Append(pageObjectNumber).Append(" 0 R ");
        }

        return $"2 0 obj\n<< /Type /Pages /Kids [{kids.ToString().Trim()}] /Count {pageCount} >>\nendobj\n";
    }

    private static string BuildPageObject(int pageObjectNumber, int contentObjectNumber, int fontObjectNumber)
    {
        return $"{pageObjectNumber} 0 obj\n<< /Type /Page /Parent 2 0 R /MediaBox [0 0 612 792] /Contents {contentObjectNumber} 0 R /Resources << /Font << /F1 {fontObjectNumber} 0 R >> >> >>\nendobj\n";
    }

    private static string BuildContentObject(int contentObjectNumber, string text)
    {
        var escapedText = text.Replace("\\", "\\\\", StringComparison.Ordinal).Replace("(", "\\(", StringComparison.Ordinal).Replace(")", "\\)", StringComparison.Ordinal);
        var contentStream = $"BT /F1 24 Tf 72 720 Td ({escapedText}) Tj ET";
        var length = Encoding.ASCII.GetByteCount(contentStream);

        return $"{contentObjectNumber} 0 obj\n<< /Length {length} >>\nstream\n{contentStream}\nendstream\nendobj\n";
    }
}
