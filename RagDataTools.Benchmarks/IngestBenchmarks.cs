using System.Text;
using BenchmarkDotNet.Attributes;
using RagDataTools.Ingest;
using IngestDocumentFormat = RagDataTools.Ingest.DocumentFormat;

namespace RagDataTools.Benchmarks;

[MemoryDiagnoser]
public class IngestBenchmarks
{
    private readonly DocumentIngestService service = new();
    private readonly DocumentIngestOptions options = new()
    {
        ChunkWordsCount = 120,
    };

    private readonly byte[] smallHtml = Encoding.UTF8.GetBytes("""
        <html>
          <body>
            <h1>Benchmark title</h1>
            <p>Alpha beta gamma delta epsilon zeta.</p>
          </body>
        </html>
        """);

    private readonly byte[] complexHtml = Encoding.UTF8.GetBytes("""
        <html>
          <body>
            <h1>Benchmark title</h1>
            <p>Alpha beta gamma delta epsilon zeta eta theta iota kappa lambda.</p>
            <table>
              <tr><th>Column A</th><th>Column B</th></tr>
              <tr><td>Value 1</td><td>Value 2</td></tr>
            </table>
            <pre><code>var answer = 42;</code></pre>
            <p>Additional text for chunking.</p>
          </body>
        </html>
        """);

    [Benchmark]
    public Task HtmlIngest_Small()
    {
        return service.IngestAsync(new MemoryStream(smallHtml, writable: false), IngestDocumentFormat.Html, options);
    }

    [Benchmark]
    public Task HtmlIngest_Complex()
    {
        return service.IngestAsync(new MemoryStream(complexHtml, writable: false), IngestDocumentFormat.Html, options);
    }
}
