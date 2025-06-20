using Sample.Chunkers.Enums;
using Sample.Chunkers.Helpers;
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
        var cleaned = text.Trim()
                          .Replace('\u00A0', ' ')
                          .Replace(" \r\n", "\r\n")
                          .Replace(" \n", "\n")
                          .Replace("\r\n\r\n", "\n\n ")
                          .Replace("\r\n", "\n ")
                          .Replace("\u2014", "-");

        cleaned = CommonRegexHelper.GetMultipleSpacesRegex().Replace(cleaned, " ");

        return cleaned;
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
        int overlap = (int)(chunkWordsCount * overlapPercentage);

        while (currentStartIndex < words.Length)
        {
            var maxEndIndex = currentStartIndex + chunkWordsCount;

            var currentEndIndex = words.Length - currentStartIndex <= chunkWordsCount
                ? words.Length
                : semanticsIndexes.Where(x => x <= maxEndIndex).Max();

            var chunk = string.Join(" ", words[currentStartIndex..currentEndIndex].ToArray())
                              .Replace("\n ", "\n");
            chunks.Add(chunk);

            if (currentEndIndex == words.Length) break;

            currentStartIndex = CalculateChunkStartIndex(semanticsIndexes, chunkWordsCount, currentEndIndex, overlap, currentStartIndex);
        }

        return [.. chunks];
    }

    private static int CalculateChunkStartIndex(int[] semanticsIndexes, int maxChunkLength, int currentEndIndex, int overlap, int currentStartIndex)
    {
        if (overlap <= 0)
        {
            return currentEndIndex;
        }

        var veryEndIndex = currentStartIndex + maxChunkLength;
        var target = veryEndIndex - overlap;

        var bestIndex = currentEndIndex;
        var bestDistance = int.MaxValue;

        foreach (var index in semanticsIndexes)
        {
            if (index <= currentStartIndex || index > veryEndIndex) continue;

            int distance = Math.Abs(index - target);
            if (distance < bestDistance)
            {
                bestDistance = distance;
                bestIndex = index;
            }
        }

        return bestIndex;
    }
}
