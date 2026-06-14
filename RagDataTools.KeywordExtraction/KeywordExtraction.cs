using System.Text.RegularExpressions;
using RagDataTools.Chunkers.Models;

namespace RagDataTools.KeywordExtraction;

/// <summary>
/// Options that control keyword extraction.
/// </summary>
public sealed record KeywordExtractionOptions
{
    /// <summary>
    /// Gets or sets the maximum time allowed for one extraction call.
    /// </summary>
    public TimeSpan Timeout { get; init; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// Gets or sets the maximum number of keywords stored per chunk.
    /// </summary>
    public int MaxKeywordsPerChunk { get; init; } = 5;
}

/// <summary>
/// Contract for a pluggable keyword extraction backend.
/// </summary>
public interface IKeywordExtractionBackend
{
    /// <summary>
    /// Extracts keywords for a single chunk.
    /// </summary>
    Task<string[]> ExtractAsync(ChunkModel chunk, KeywordExtractionOptions options, CancellationToken cancellationToken = default);
}

/// <summary>
/// Contract for extracting keywords from one or more chunks.
/// </summary>
public interface IKeywordExtractor
{
    /// <summary>
    /// Extracts keywords from the provided chunks and stores them in chunk metadata.
    /// </summary>
    Task<ChunkModel[]> ExtractAsync(ChunkModel[] chunks, KeywordExtractionOptions? options = null, CancellationToken cancellationToken = default);
}

/// <summary>
/// Default keyword extraction service with timeout enforcement.
/// </summary>
public sealed class KeywordExtractionService : IKeywordExtractor
{
    private readonly IKeywordExtractionBackend backend;

    /// <summary>
    /// Gets the default service instance that uses the built-in heuristic backend.
    /// </summary>
    public static KeywordExtractionService Default { get; } = new();

    /// <summary>
    /// Creates a service using the built-in heuristic backend.
    /// </summary>
    public KeywordExtractionService()
        : this(new HeuristicKeywordExtractionBackend())
    {
    }

    /// <summary>
    /// Creates a service using the specified backend.
    /// </summary>
    public KeywordExtractionService(IKeywordExtractionBackend backend)
    {
        ArgumentNullException.ThrowIfNull(backend);
        this.backend = backend;
    }

    /// <inheritdoc />
    public async Task<ChunkModel[]> ExtractAsync(ChunkModel[] chunks, KeywordExtractionOptions? options = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(chunks);
        options ??= new KeywordExtractionOptions();

        ValidateOptions(options);

        using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        var extractionTask = ExtractInternalAsync(chunks, options, timeoutCts.Token);
        var timeoutTask = Task.Delay(options.Timeout, cancellationToken);
        var completed = await Task.WhenAny(extractionTask, timeoutTask).ConfigureAwait(false);

        if (completed == timeoutTask)
        {
            timeoutCts.Cancel();

            if (cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }

            throw new TimeoutException($"Keyword extraction timed out after {options.Timeout}.");
        }

        return await extractionTask.ConfigureAwait(false);
    }

    private async Task<ChunkModel[]> ExtractInternalAsync(ChunkModel[] chunks, KeywordExtractionOptions options, CancellationToken cancellationToken)
    {
        for (var i = 0; i < chunks.Length; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var keywords = await backend.ExtractAsync(chunks[i], options, cancellationToken).ConfigureAwait(false);
            chunks[i].SetKeywords((keywords ?? Array.Empty<string>()).Take(options.MaxKeywordsPerChunk).ToArray());
        }

        return chunks;
    }

    private static void ValidateOptions(KeywordExtractionOptions options)
    {
        if (options.Timeout <= TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(options.Timeout), "Timeout must be greater than zero.");
        }

        if (options.MaxKeywordsPerChunk <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(options.MaxKeywordsPerChunk), "MaxKeywordsPerChunk must be greater than zero.");
        }
    }
}

/// <summary>
/// Deterministic keyword backend used when no external service is configured.
/// </summary>
public sealed class HeuristicKeywordExtractionBackend : IKeywordExtractionBackend
{
    private static readonly Regex TokenRegex = new(@"[\p{L}\p{N}][\p{L}\p{N}\p{Mn}'-]*", RegexOptions.Compiled | RegexOptions.CultureInvariant);
    private static readonly HashSet<string> StopWords = new(StringComparer.OrdinalIgnoreCase)
    {
        "a", "an", "and", "are", "as", "at", "be", "been", "but", "by", "for", "from", "has",
        "have", "he", "her", "hers", "him", "his", "i", "if", "in", "into", "is", "it", "its",
        "me", "my", "of", "on", "or", "our", "ours", "she", "so", "than", "that", "the", "their",
        "them", "there", "these", "they", "this", "those", "to", "was", "we", "were", "with", "you",
        "your", "yours",
    };

    /// <inheritdoc />
    public Task<string[]> ExtractAsync(ChunkModel chunk, KeywordExtractionOptions options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(chunk);
        ArgumentNullException.ThrowIfNull(options);
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(chunk.RawContent))
        {
            return Task.FromResult(Array.Empty<string>());
        }

        var keywords = TokenRegex.Matches(chunk.RawContent)
            .Select(match => match.Value.Trim())
            .Where(token => token.Length > 1)
            .Where(token => !StopWords.Contains(token))
            .GroupBy(token => token, StringComparer.OrdinalIgnoreCase)
            .Select(group => new
            {
                Keyword = group.Key.ToLowerInvariant(),
                Count = group.Count(),
                FirstIndex = group.Min(token => chunk.RawContent.IndexOf(token, StringComparison.OrdinalIgnoreCase)),
            })
            .OrderByDescending(item => item.Count)
            .ThenBy(item => item.FirstIndex)
            .Select(item => item.Keyword)
            .Take(options.MaxKeywordsPerChunk)
            .ToArray();

        return Task.FromResult(keywords);
    }
}
