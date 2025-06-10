using Sample.Chunkers.Enums;
using System.Text.RegularExpressions;

namespace Sample.Chunkers.Extensions;

public static class SimpleTextChunkerExtensions
{
    public static Span<string> GetWords(this string text)
    {
        return new Span<string>(text.Split([' '], StringSplitOptions.RemoveEmptyEntries));
    }

    public static int[] ExtractSentenceStartIndices(this string text)
    {
        var indexes = new List<int>();
        int wordIndex = 0;
        var sentences = Regex.Split(text, @"(?<=[.!?])\s+");

        foreach (var sentence in sentences)
        {
            indexes.Add(wordIndex);
            wordIndex += sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
        }

        return [.. indexes];
    }

    public static int[] ExtractParagraphStartIndexes(this string text)
    {
        var indexes = new List<int>();
        int wordIndex = 0;

        var paragraphs = text.Split("\n ", StringSplitOptions.RemoveEmptyEntries);

        foreach (var paragraph in paragraphs)
        {
            indexes.Add(wordIndex);
            wordIndex += paragraph.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
        }

        return [.. indexes];
    }

    public static string PreprocessNaturalTextForChunking(this string text)
    {
        return Regex.Replace(text.Trim()
                        .Replace("\u00A0", " ")
                        .Replace(" \r\n", "\r\n")
                        .Replace(" \n", "\n")
                        .Replace("\r\n\r\n", "\n\n ")
                        .Replace("\r\n", "\n "),
                        "( )+", " ")
                    .Replace("\u2014", "-");
    }

    public static string[] PreprocessNaturalTextsForChunking(this string[] texts)
    {
        return [.. texts.Select(PreprocessNaturalTextForChunking)];
    }

    public static string[] ExtractSemanticChunksFromText(this string text, int chunkWordsCount, SemanticsType semanticsType, double overlapPercentage = 0.0)
    {
        var preprocessedText = PreprocessNaturalTextForChunking(text);
        var words = GetWords(preprocessedText);
        var semanticsIndexes = semanticsType switch
        {
            SemanticsType.Sentence => ExtractSentenceStartIndices(preprocessedText),
            SemanticsType.Paragraph => ExtractParagraphStartIndexes(preprocessedText),
            _ => throw new InvalidOperationException(),
        };

        return GetChunks(words, semanticsIndexes, chunkWordsCount, overlapPercentage);
    }

    private static string[] GetChunks(this Span<string> words, int[] semanticsIndexes, int chunkWordsCount, double overlapPercentage = 0.0)
    {
        var chunks = new List<string>();
        var currentStartIndex = 0;
        int overlap = (int)(chunkWordsCount * overlapPercentage); // Количество слов для перекрытия

        while (currentStartIndex < words.Length)
        {
            var chunkLength = CalculateActualChunkSize(semanticsIndexes, words.Length, currentStartIndex, chunkWordsCount);

            var chunk = string.Join(" ", words.Slice(currentStartIndex, chunkLength).ToArray())
                              .Replace("\n ", "\n");

            chunks.Add(chunk);

            if (overlap > 0)
            {
                currentStartIndex = CalculateChunkStartIndex(semanticsIndexes, chunkWordsCount, chunkLength, overlap, currentStartIndex);
            }
            else
            {
                currentStartIndex += chunkLength;
            }
        }

        return [.. chunks];
    }

    private static int CalculateActualChunkSize(int[] semanticsIndexes, int wordsLength, int currentStartIndex, int targetChunkSize)
    {
        var maxEndIndex = currentStartIndex + targetChunkSize;

        return wordsLength - currentStartIndex <= targetChunkSize
            ? wordsLength - currentStartIndex
            : semanticsIndexes.LastOrDefault(x => x <= maxEndIndex) - currentStartIndex;
    }

    private static int CalculateChunkStartIndex(int[] semanticsIndexes, int maxChunkLength, int chunkLength, int overlap, int currentStartIndex)
    {
        var veryEndIndex = currentStartIndex + maxChunkLength;
        var validIndexes = semanticsIndexes.Where(x => x > currentStartIndex && x <= veryEndIndex).ToArray();

        if (validIndexes.Length == 0)
        {
            return currentStartIndex + chunkLength;
        }

        veryEndIndex -= overlap;

        return validIndexes.OrderBy(x => Math.Abs(x - veryEndIndex)).First();
    }
}
