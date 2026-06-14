using System.Text;
using FluentAssertions;
using RagDataTools.Chunkers.Models;
using RagDataTools.Ingest;
using RagDataTools.UnitTests.TestData;
using IngestDocumentFormat = RagDataTools.Ingest.DocumentFormat;

namespace RagDataTools.UnitTests.Ingest;

public class DocumentIngestServiceTests
{
    [Test]
    public async Task IngestAsync_EmptyHtmlDocument_ReturnsEmptyChunks()
    {
        var service = new DocumentIngestService();

        var chunks = await service.IngestAsync(
            IngestDocumentFixturesBuilder.CreateHtml("<html><body></body></html>"),
            IngestDocumentFormat.Html,
            new DocumentIngestOptions());

        chunks.Should().BeEmpty();
    }

    [Test]
    public async Task IngestAsync_UnsupportedFormat_ThrowsClearError()
    {
        var service = new DocumentIngestService();

        var act = async () => await service.IngestAsync(
            new MemoryStream(Array.Empty<byte>()),
            (IngestDocumentFormat)999,
            new DocumentIngestOptions());

        var exception = await act.Should().ThrowAsync<NotSupportedException>();
        exception.Which.Message.Should().Contain("Unsupported document format");
    }

    [Test]
    public async Task IngestAsync_MalformedPdf_ThrowsClearError()
    {
        var service = new DocumentIngestService();

        var malformedPdf = new MemoryStream(Encoding.ASCII.GetBytes("not-a-pdf"));

        var act = async () => await service.IngestAsync(
            malformedPdf,
            IngestDocumentFormat.Pdf,
            new DocumentIngestOptions());

        await act.Should().ThrowAsync<InvalidDataException>();
    }

    [Test]
    public async Task IngestAsync_PdfChunksCarryPageNumber()
    {
        var service = new DocumentIngestService();

        var chunks = await service.IngestAsync(
            IngestDocumentFixturesBuilder.CreatePdfDocument("PDF page one.", "PDF page two."),
            IngestDocumentFormat.Pdf,
            new DocumentIngestOptions
            {
                ChunkWordsCount = 100,
            });

        chunks.Should().HaveCount(2);
        chunks.Select(chunk => chunk.GetPageNumber()).Should().ContainInOrder(1, 2);
    }

    [Test]
    public async Task IngestAsync_WordChunksCarryPageNumber()
    {
        var service = new DocumentIngestService();

        var chunks = await service.IngestAsync(
            IngestDocumentFixturesBuilder.CreateWordDocument("Word page one.", "Word page two."),
            IngestDocumentFormat.Word,
            new DocumentIngestOptions
            {
                ChunkWordsCount = 100,
            });

        chunks.Should().HaveCount(2);
        chunks.Select(chunk => chunk.GetPageNumber()).Should().ContainInOrder(1, 2);
    }
}
