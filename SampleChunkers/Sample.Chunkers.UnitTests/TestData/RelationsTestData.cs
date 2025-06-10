using Sample.Chunkers.Models;

namespace Sample.Chunkers.UnitTests.TestData;

internal static class RelationsTestData
{
    internal static readonly List<RelationshipModel> DevToRealWorldArticleChunksRelations =
    [
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.DevToRealWorldArticleTextChunks[0].Index,
            SecondChunkIndex = TextChunkTestData.DevToRealWorldArticleTextChunks[1].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.DevToRealWorldArticleTextChunks[1].Index,
            SecondChunkIndex = TextChunkTestData.DevToRealWorldArticleTextChunks[2].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.DevToRealWorldArticleTextChunks[2].Index,
            SecondChunkIndex = TextChunkTestData.DevToRealWorldArticleTextChunks[3].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.DevToRealWorldArticleTextChunks[3].Index,
            SecondChunkIndex = TextChunkTestData.DevToRealWorldArticleTextChunks[4].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.DevToRealWorldArticleTextChunks[4].Index,
            SecondChunkIndex = TextChunkTestData.DevToRealWorldArticleTextChunks[5].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.DevToRealWorldArticleTextChunks[5].Index,
            SecondChunkIndex = TextChunkTestData.DevToRealWorldArticleTextChunks[6].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.DevToRealWorldArticleTextChunks[6].Index,
            SecondChunkIndex = TextChunkTestData.DevToRealWorldArticleTextChunks[7].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.DevToRealWorldArticleTextChunks[7].Index,
            SecondChunkIndex = TextChunkTestData.DevToRealWorldArticleTextChunks[8].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.DevToRealWorldArticleTextChunks[1].Index,
            SecondChunkIndex = CodeBlocksTestData.DevToRealWorldArticleCodeBlocks[0].Index,
            RelationshipType = "RELATED_CODE_BLOCK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.DevToRealWorldArticleTextChunks[2].Index,
            SecondChunkIndex = CodeBlocksTestData.DevToRealWorldArticleCodeBlocks[0].Index,
            RelationshipType = "RELATED_CODE_BLOCK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.DevToRealWorldArticleTextChunks[6].Index,
            SecondChunkIndex = CodeBlocksTestData.DevToRealWorldArticleCodeBlocks[1].Index,
            RelationshipType = "RELATED_CODE_BLOCK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.DevToRealWorldArticleTextChunks[6].Index,
            SecondChunkIndex = CodeBlocksTestData.DevToRealWorldArticleCodeBlocks[2].Index,
            RelationshipType = "RELATED_CODE_BLOCK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.DevToRealWorldArticleTextChunks[6].Index,
            SecondChunkIndex = CodeBlocksTestData.DevToRealWorldArticleCodeBlocks[3].Index,
            RelationshipType = "RELATED_CODE_BLOCK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.DevToRealWorldArticleTextChunks[7].Index,
            SecondChunkIndex = LinksTestData.DevToRealWorldArticleLinks[0].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.DevToRealWorldArticleTextChunks[8].Index,
            SecondChunkIndex = LinksTestData.DevToRealWorldArticleLinks[0].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
    ];

    internal static readonly List<RelationshipModel> GeeksForGeeksAboutDataModelingChunksRelations =
    [
        // text chunks sequence
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[0].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[1].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        }, // 33 - 34
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[1].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[2].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        }, // 34 - 35
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[2].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[3].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        }, // 35 - 36
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[3].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[4].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        }, // 36 - 37
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[4].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[5].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        }, // 37 - 38
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[5].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[6].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        }, // 38 - 39
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[6].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[7].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        }, // 39 - 40
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[7].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[8].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        }, // 40 - 41
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[8].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[9].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        }, // 41 - 42
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[9].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[10].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        }, // 42 - 43
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[10].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[11].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        }, // 43 - 44
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[11].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[12].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        }, // 44 - 45
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[12].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[13].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        }, // 45 - 46
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[13].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[14].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        }, // 46 - 47
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[14].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[15].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        }, // 47 - 48
        // topics graph relations
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[0].Index,
            SecondChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[1].Index,
            RelationshipType = "HAS_NEXT_TOPIC",
        }, // 25 - 26
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[1].Index,
            SecondChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[2].Index,
            RelationshipType = "HAS_NEXT_TOPIC",
        }, // 26 - 27
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[2].Index,
            SecondChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[3].Index,
            RelationshipType = "HAS_NEXT_TOPIC",
        }, // 27 - 28
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[3].Index,
            SecondChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[4].Index,
            RelationshipType = "HAS_NEXT_TOPIC",
        }, // 28 - 29
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[4].Index,
            SecondChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[5].Index,
            RelationshipType = "HAS_NEXT_TOPIC",
        }, // 29 - 30
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[5].Index,
            SecondChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[6].Index,
            RelationshipType = "HAS_NEXT_TOPIC",
        }, // 30 - 31
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[6].Index,
            SecondChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[7].Index,
            RelationshipType = "HAS_NEXT_TOPIC",
        }, // 31 - 32
        // text chunk with index = 33 (1-th)
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[0].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[0].Index,
            RelationshipType = "STARTS_WITH",
        },
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[1].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[0].Index,
            RelationshipType = "STARTS_WITH",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[0].Index,
            SecondChunkIndex = ImageLinksTestData.GeeksForGeeksAboutDataModelingImageLinks[0].Index,
            RelationshipType = "RELATED_IMAGE",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[0].Index,
            SecondChunkIndex = ImageLinksTestData.GeeksForGeeksAboutDataModelingImageLinks[1].Index,
            RelationshipType = "RELATED_IMAGE",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[0].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[0].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        // text chunk with index = 34 (2-th)
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[2].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[1].Index,
            RelationshipType = "STARTS_WITH",
        },
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[3].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[1].Index,
            RelationshipType = "STARTS_WITH",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[1].Index,
            SecondChunkIndex = ImageLinksTestData.GeeksForGeeksAboutDataModelingImageLinks[2].Index,
            RelationshipType = "RELATED_IMAGE",
        },
        // text chunk with index = 35 (3-th)
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[3].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[2].Index,
            RelationshipType = "STARTS_WITH",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[2].Index,
            SecondChunkIndex = ImageLinksTestData.GeeksForGeeksAboutDataModelingImageLinks[2].Index,
            RelationshipType = "RELATED_IMAGE",
        },
        // text chunk with index = 36 (4-th)
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[3].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[1].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        // text chunk with index = 37 (5-th)
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[4].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[4].Index,
            RelationshipType = "STARTS_WITH",
        },
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[4].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[5].Index,
            RelationshipType = "STARTS_WITH",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[4].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[1].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        // text chunk with index = 38 (6-th)
        // text chunk with index = 39 (7-th)
        // text chunk with index = 40 (8-th)
        // text chunk with index = 41 (9-th)
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[8].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[2].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[8].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[3].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        // text chunk with index = 42 (10-th)
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[5].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[9].Index,
            RelationshipType = "STARTS_WITH",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[9].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[2].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[9].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[3].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[9].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[4].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        // text chunk with index = 43 (11-th)
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[5].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[10].Index,
            RelationshipType = "STARTS_WITH",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[10].Index,
            SecondChunkIndex = ImageLinksTestData.GeeksForGeeksAboutDataModelingImageLinks[3].Index,
            RelationshipType = "RELATED_IMAGE",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[10].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[4].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[10].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[5].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        // text chunk with index = 44 (12-th)
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[11].Index,
            SecondChunkIndex = ImageLinksTestData.GeeksForGeeksAboutDataModelingImageLinks[3].Index,
            RelationshipType = "RELATED_IMAGE",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[11].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[5].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[11].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[6].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[11].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[7].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        // text chunk with index = 45 (13-th)
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[6].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[12].Index,
            RelationshipType = "STARTS_WITH",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[12].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[6].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[12].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[7].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        // text chunk with index = 46 (14-th)
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[6].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[13].Index,
            RelationshipType = "STARTS_WITH",
        },
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[7].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[13].Index,
            RelationshipType = "STARTS_WITH",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[13].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[8].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[13].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[9].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[13].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[10].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        // text chunk with index = 47 (15-th)
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders[7].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[14].Index,
            RelationshipType = "STARTS_WITH",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[14].Index,
            SecondChunkIndex = ImageLinksTestData.GeeksForGeeksAboutDataModelingImageLinks[0].Index,
            RelationshipType = "RELATED_IMAGE",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[14].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[8].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[14].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[9].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[14].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[10].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[14].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[11].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[14].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[12].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[14].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[13].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[14].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[14].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[14].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[15].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[14].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[16].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[14].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[17].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[14].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[18].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        // text chunk with index = 48 (16-th)
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[15].Index,
            SecondChunkIndex = ImageLinksTestData.GeeksForGeeksAboutDataModelingImageLinks[0].Index,
            RelationshipType = "RELATED_IMAGE",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[15].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[11].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[15].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[12].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[15].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[13].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[15].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[14].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[15].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[15].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[15].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[16].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[15].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[17].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks[15].Index,
            SecondChunkIndex = LinksTestData.GeeksForGeeksAboutDataModelingLinks[18].Index,
            RelationshipType = "ADDITIONAL_LINK",
        },
    ];

    internal static readonly List<RelationshipModel> GeeksForGeeksAboutRelationalAlgebraChunksRelations =
    [
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutRelationalAlgebraTextChunks[0].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutRelationalAlgebraTextChunks[1].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutRelationalAlgebraTextChunks[1].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutRelationalAlgebraTextChunks[2].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutRelationalAlgebraTextChunks[2].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutRelationalAlgebraTextChunks[3].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutRelationalAlgebraTextChunks[3].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutRelationalAlgebraTextChunks[4].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutRelationalAlgebraTextChunks[4].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutRelationalAlgebraTextChunks[5].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = TextChunkTestData.GeeksForGeeksAboutRelationalAlgebraTextChunks[5].Index,
            SecondChunkIndex = TextChunkTestData.GeeksForGeeksAboutRelationalAlgebraTextChunks[6].Index,
            RelationshipType = "HAS_NEXT_CHUNK",
        },
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutRelationalAlgebraHeaders[0].Index,
            SecondChunkIndex = HeadersTestData.GeeksForGeeksAboutRelationalAlgebraHeaders[1].Index,
            RelationshipType = "HAS_NEXT_TOPIC",
        },
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutRelationalAlgebraHeaders[1].Index,
            SecondChunkIndex = HeadersTestData.GeeksForGeeksAboutRelationalAlgebraHeaders[2].Index,
            RelationshipType = "HAS_NEXT_TOPIC",
        },
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutRelationalAlgebraHeaders[2].Index,
            SecondChunkIndex = HeadersTestData.GeeksForGeeksAboutRelationalAlgebraHeaders[3].Index,
            RelationshipType = "HAS_NEXT_TOPIC",
        },
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutRelationalAlgebraHeaders[3].Index,
            SecondChunkIndex = HeadersTestData.GeeksForGeeksAboutRelationalAlgebraHeaders[4].Index,
            RelationshipType = "HAS_NEXT_TOPIC",
        },
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutRelationalAlgebraHeaders[4].Index,
            SecondChunkIndex = HeadersTestData.GeeksForGeeksAboutRelationalAlgebraHeaders[5].Index,
            RelationshipType = "HAS_NEXT_TOPIC",
        },
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutRelationalAlgebraHeaders[5].Index,
            SecondChunkIndex = HeadersTestData.GeeksForGeeksAboutRelationalAlgebraHeaders[6].Index,
            RelationshipType = "HAS_NEXT_TOPIC",
        },
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutRelationalAlgebraHeaders[6].Index,
            SecondChunkIndex = HeadersTestData.GeeksForGeeksAboutRelationalAlgebraHeaders[7].Index,
            RelationshipType = "HAS_NEXT_TOPIC",
        },
        new RelationshipModel
        {
            FirstChunkIndex = HeadersTestData.GeeksForGeeksAboutRelationalAlgebraHeaders[7].Index,
            SecondChunkIndex = HeadersTestData.GeeksForGeeksAboutRelationalAlgebraHeaders[8].Index,
            RelationshipType = "HAS_NEXT_TOPIC",
        },
    ];
}
