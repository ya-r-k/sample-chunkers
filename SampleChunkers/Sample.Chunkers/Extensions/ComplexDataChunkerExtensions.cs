using Sample.Chunkers.Enums;
using Sample.Chunkers.Models;
using System.Text;
using System.Text.RegularExpressions;
using Sample.Chunkers.Helpers;

namespace Sample.Chunkers.Extensions;

public static class ComplexDataChunkerExtensions
{
    private static readonly ChunkType[] chunkTypes = 
    [
        ChunkType.CodeBlock,
        ChunkType.MathBlock,
        ChunkType.Table,
        ChunkType.ImageLink,
        ChunkType.ExternalLink,
        ChunkType.Title,
    ];

    private static readonly Dictionary<string, ChunkType> labelsChunkTypesPairs = new()
    {
        ["Title"] = ChunkType.Title,
        ["Table"] = ChunkType.Table,
        ["Math-Block"] = ChunkType.MathBlock,
        ["Code-Block"] = ChunkType.CodeBlock,
        ["Image-Link"] = ChunkType.ImageLink,
        ["External-Link"] = ChunkType.ExternalLink,
    };

    private static readonly Dictionary<ChunkType, string> labelsTemplatesChunkTypesPairs = new()
    {
        [ChunkType.Title] = ChunksConsts.HeaderTemplate,
        [ChunkType.Table] = ChunksConsts.TableTemplate,
        [ChunkType.MathBlock] = ChunksConsts.MathBlockTemplate,
        [ChunkType.CodeBlock] = ChunksConsts.CodeBlockTemplate,
        [ChunkType.ImageLink] = ChunksConsts.ImageLinkTemplate,
        [ChunkType.ExternalLink] = ChunksConsts.ExternalLinkTemplate,
    };

    public static Dictionary<ChunkType, List<ChunkModel>> ExtractSemanticChunksDeeply(this string[] texts, int chunkWordsCount, SemanticsType semanticsType, double overlapPercentage = 0.0, bool withTables = true, bool withCodeBlocks = true, bool withImages = true, bool withLinks = true)
    {
        var result = new Dictionary<ChunkType, List<ChunkModel>>();

        foreach (var text in texts)
        {
            var lastUsedIndex = result.Keys.Count > 0 ? result.SelectMany(x => x.Value).Select(x => x.Index).Max() : 0;
            var chunks = text.ExtractSemanticChunksDeeply(chunkWordsCount, semanticsType, overlapPercentage, withTables, withCodeBlocks, withImages, withLinks, lastUsedIndex);

            if (lastUsedIndex == 0)
            {
                result = chunks;
                continue;
            }

            foreach (var item in chunks)
            {
                result[item.Key].AddRange(item.Value);
            }
        }

        return result;
    }

    public static Dictionary<ChunkType, List<ChunkModel>> ExtractSemanticChunksDeeply(this string text, int chunkWordsCount, SemanticsType semanticsType, double overlapPercentage = 0.0, bool withTables = true, bool withCodeBlocks = true, bool withImages = true, bool withLinks = true, int lastUsedIndex = 0)
    {
        var dataChunks = text.RetrieveChunksFromText(withTables, withCodeBlocks, withImages, withLinks, lastUsedIndex);
        var processedText = text.ReplaceChunksWithLabels(dataChunks)
                                .PreprocessNaturalTextForChunking();

        var index = dataChunks.SelectMany(x => x.Value).Select(x => x.Index).Max();
        dataChunks.Add(ChunkType.Text, processedText.ExtractSemanticChunks(index, chunkWordsCount, semanticsType, overlapPercentage));

        return dataChunks;
    }

    public static Dictionary<ChunkType, List<ChunkModel>> RetrieveChunksFromText(this string text, bool withTables, bool withCodeBlocks, bool withImages, bool withLinks, int lastUsedIndex = 0)
    {
        var currentText = new StringBuilder(text);
        var result = new Dictionary<ChunkType, List<ChunkModel>>();
        var index = lastUsedIndex;

        if (withCodeBlocks)
        {
            var items = currentText.ExtractMarkdownCodeBlocks(index);

            result.Add(ChunkType.CodeBlock, items);
            index += items.Count;
        }

        if (withTables)
        {
            var items = currentText.ExtractHtmlTables(index);

            result.Add(ChunkType.Table, items);
            index += items.Count;
        }

        if (withImages)
        {
            var items = currentText.ExtractMarkdownImageLinks(index);

            result.Add(ChunkType.ImageLink, items);
            index += items.Count;
        }

        if (withLinks)
        {
            var items = ExtractMarkdownLinks(currentText, index);

            result.Add(ChunkType.ExternalLink, items);
            index += items.Count;
        }

        result.Add(ChunkType.Title, currentText.ExtractMarkdownHeaders(index));

        return result;
    }

    public static Dictionary<ChunkType, List<ChunkModel>>[] RetrieveChunksFromTexts(this string[] texts, bool withTables, bool withCodeBlocks, bool withImages, bool withLinks)
    {
        return [.. texts.Select(text => text.RetrieveChunksFromText(withTables, withCodeBlocks, withImages, withLinks))];
    }

    public static string ReplaceChunksWithLabels(this string text, Dictionary<ChunkType, List<ChunkModel>> chunks)
    {
        if (chunks.Keys.Count == 0)
        {
            return text;
        }

        var processedText = new StringBuilder(text);

        foreach (var type in chunkTypes)
        {
            if (chunks.TryGetValue(type, out var currentChunks) &&
                currentChunks.Count > 0 &&
                labelsTemplatesChunkTypesPairs.TryGetValue(type, out var labelTemplate))
            {
                if (type == ChunkType.ExternalLink)
                {
                    var linksData = currentChunks.Select(x => x.Data as LinkData).ToArray();

                    for (var i = 0; i < linksData.Length; i++)
                    {
                        processedText.Replace(currentChunks[i].RawContent, linksData[i]?.TextDescription + string.Format(labelTemplate, currentChunks[i].Index));
                    }
                }
                else
                {
                    foreach (var chunk in currentChunks)
                    {
                        processedText.Replace(chunk.RawContent, string.Format(labelTemplate, chunk.Index));
                    }
                }
            }
        }

        return SquashLabelsIntoWords(processedText);
    }

    public static string[] ReplaceChunksWithLabels(this string[] texts, Dictionary<ChunkType, List<ChunkModel>>[] chunks)
    {
        return [.. texts.Select((text, index) => text.ReplaceChunksWithLabels(chunks[index]))];
    }

    private static List<ChunkModel> ExtractSemanticChunks(this string text, int lastUsedIndex, int chunkWordsCount, SemanticsType semanticsType, double overlapPercentage = 0.0)
    {
        var result = new List<ChunkModel>();
        var textChunks = text.ExtractSemanticChunksFromText(chunkWordsCount, semanticsType, overlapPercentage);

        foreach (var item in textChunks)
        {
            var rawChunkData = new StringBuilder(item);
            var relatedIndexes = ExtractRelatedChunksIndexes(rawChunkData);

            result.Add(new ChunkModel
            {
                Index = ++lastUsedIndex,
                ChunkType = ChunkType.Text,
                RawContent = rawChunkData.ToString(),
                RelatedChunksIndexes = relatedIndexes,
            });
        }

        return [.. result];
    }

    private static string SquashLabelsIntoWords(this StringBuilder text)
    {
        var matches = ChunkTypesRegexHelper.GetChunkLabelWithWhitespacesRegex().Matches(text.ToString());

        foreach (Match match in matches)
        {
            text.Replace(match.Value, match.Groups[1].Value);
        }

        return text.ToString();
    }

    private static List<ChunkModel> ExtractMarkdownCodeBlocks(this StringBuilder text, int lastUsedIndex = 0)
    {
        var result = new List<ChunkModel>();
        var matches = ChunkTypesRegexHelper.GetCodeBlockRegex().Matches(text.ToString());

        foreach (Match match in matches)
        {
            var language = match.Groups[1].Value; // Название языка (если указано)
            language = string.IsNullOrEmpty(language) ? "unknown" : language.ToLower();

            result.Add(new ChunkModel
            {
                Index = ++lastUsedIndex,
                RawContent = match.Value,
                ChunkType = ChunkType.CodeBlock,
                Data = new CodeBlockData
                {
                    Language = language,
                },
                RelatedChunksIndexes = [],
            });

            text.Replace(match.Value, string.Empty);
        }

        return [.. result];
    }

    private static List<ChunkModel> ExtractHtmlTables(this StringBuilder text, int lastUsedIndex = 0)
    {
        var result = new List<ChunkModel>();
        var matches = ChunkTypesRegexHelper.GetHtmlTableRegex().Matches(text.ToString());
        
        foreach (Match match in matches)
        {
            var currentChunk = new ChunkModel
            {
                Index = ++lastUsedIndex,
                RawContent = match.Value,
                ChunkType = ChunkType.Table,
                RelatedChunksIndexes = []
            };
            result.Add(currentChunk);
            text.Replace(currentChunk.RawContent, string.Empty);
        }

        return [.. result];
    }

    private static List<ChunkModel> ExtractMarkdownImageLinks(this StringBuilder text, int lastUsedIndex = 0)
    {
        var result = new List<ChunkModel>();
        var matches = ChunkTypesRegexHelper.GetImageLinkRegex().Matches(text.ToString());

        foreach (Match match in matches)
        {
            result.Add(new ChunkModel
            {
                Index = ++lastUsedIndex,
                RawContent = match.Value,
                ChunkType = ChunkType.ImageLink,
                Data = new LinkData
                {
                    Url = match.Groups[2].Value,
                    TextDescription = match.Groups[1].Value,
                },
                RelatedChunksIndexes = []
            });

            text.Replace(match.Value, string.Format(ChunksConsts.ImageLinkTemplate, lastUsedIndex));
        }

        return [.. result];
    }

    private static List<ChunkModel> ExtractMarkdownHeaders(this StringBuilder text, int lastUsedIndex = 0)
    {
        var result = new List<ChunkModel>();
        var matches = ChunkTypesRegexHelper.GetHeadingRegex().Matches(text.ToString());

        foreach (Match match in matches)
        {
            var titleText = new StringBuilder(match.Groups[2].Value.TrimEnd());
            var relatedIndexes = titleText.ExtractRelatedChunksIndexes();

            result.Add(new ChunkModel
            {
                Index = ++lastUsedIndex,
                RawContent = match.Value.TrimEnd(),
                ChunkType = ChunkType.Title,
                Data = new HeaderData
                {
                    Level = match.Groups[1].Length,
                    Text = titleText.ToString(),
                },
                RelatedChunksIndexes = relatedIndexes,
            });

            text.Replace(match.Value, string.Empty);
        }

        return [.. result];
    }

    private static List<ChunkModel> ExtractMarkdownLinks(this StringBuilder text, int lastUsedIndex = 0)
    {
        var result = new List<ChunkModel>();

        // Регулярное выражение для поиска Markdown-ссылок ([Alt текст](URL))
        var matches = ChunkTypesRegexHelper.GetExternalLinkRegex().Matches(text.ToString());

        foreach (Match match in matches)
        {
            var textDescription = match.Groups[1].Value;
            result.Add(new ChunkModel
            {
                Index = ++lastUsedIndex,
                RawContent = match.Value,
                ChunkType = ChunkType.ExternalLink,
                Data = new LinkData
                {
                    Url = match.Groups[2].Value,
                    TextDescription = textDescription,
                },
                RelatedChunksIndexes = []
            });

            text.Replace(match.Value, textDescription + string.Format(ChunksConsts.ExternalLinkTemplate, lastUsedIndex));
        }

        return [.. result];
    }

    private static Dictionary<ChunkType, List<int>> ExtractRelatedChunksIndexes(this StringBuilder chunkValue)
    {
        var result = new Dictionary<ChunkType, List<int>>();
        var matches = ChunkTypesRegexHelper.GetChunkLabelRegex().Matches(chunkValue.ToString());

        foreach (Match match in matches) 
        {
            if (labelsChunkTypesPairs.TryGetValue(match.Groups[1].Value, out var type))
            {
                if (!result.TryGetValue(type, out var value))
                {
                    value = [];
                    result.Add(type, value);
                }

                value.Add(int.Parse(match.Groups[2].Value));
            }

            chunkValue.Replace(match.Value, string.Empty);
        }

        return result;
    }
}
