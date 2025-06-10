using Sample.Chunkers.Enums;
using Sample.Chunkers.Models;

namespace Sample.Chunkers.UnitTests.TestData;

internal class LinksTestData
{
    // https://dev.to/alex_ricciardi/recursion-in-programming-techniques-benefits-and-limitations-java-3o4p
    internal static readonly ChunkModel[] DevToRealWorldArticleLinks =
    [
        new ChunkModel
        {
            Index = 5,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[Alex.omegapy - Medium](https://medium.com/@alex.omegapy)",
            Data = new LinkData
            {
                Url = "https://medium.com/@alex.omegapy",
                TextDescription = "Alex.omegapy - Medium",
            },
        },
    ];

    // https://www.geeksforgeeks.org/data-modeling-a-comprehensive-guide-for-analysts/
    internal static readonly ChunkModel[] GeeksForGeeksAboutDataModelingLinks =
    [
        new ChunkModel
        {
            Index = 6,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[[RELATEDCHUNK]Image-Link-15[/RELATEDCHUNK]\r\n                                                    Follow](https://news.google.com/publications/CAAqBwgKMLTrzwsw44bnAw?hl=en-IN&amp;gl=IN&amp;ceid=IN%3Aen)",
            Data = new LinkData
            {
                Url = "https://news.google.com/publications/CAAqBwgKMLTrzwsw44bnAw?hl=en-IN&amp;gl=IN&amp;ceid=IN%3Aen",
                TextDescription = "[RELATEDCHUNK]Image-Link-15[/RELATEDCHUNK]\r\n                                                    Follow",
            },
        },
        new ChunkModel
        {
            Index = 7,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[data structure.](https://www.geeksforgeeks.org/data-structures/)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/data-structures/",
                TextDescription = "data structure.",
            },
        },
        new ChunkModel
        {
            Index = 8,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[development](https://www.geeksforgeeks.org/class-10-social-science-economics-chapter-1-development/)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/class-10-social-science-economics-chapter-1-development/",
                TextDescription = "development",
            },
        },
        new ChunkModel
        {
            Index = 9,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[database schema.](https://www.geeksforgeeks.org/database-schemas/)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/database-schemas/",
                TextDescription = "database schema.",
            },
        },
        new ChunkModel
        {
            Index = 10,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[tree.](https://www.geeksforgeeks.org/introduction-to-tree-data-structure-and-algorithm-tutorials/)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/introduction-to-tree-data-structure-and-algorithm-tutorials/",
                TextDescription = "tree.",
            },
        },
        new ChunkModel
        {
            Index = 11,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[database design](https://www.geeksforgeeks.org/significance-of-database-design/)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/significance-of-database-design/",
                TextDescription = "database design",
            },
        },
        new ChunkModel
        {
            Index = 12,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[relational model](https://www.geeksforgeeks.org/relational-model-in-dbms/)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/relational-model-in-dbms/",
                TextDescription = "relational model",
            },
        },
        new ChunkModel
        {
            Index = 13,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[entity-relationship model (ER model)](https://www.geeksforgeeks.org/introduction-of-er-model/)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/introduction-of-er-model/",
                TextDescription = "entity-relationship model (ER model)",
            },
        },
        new ChunkModel
        {
            Index = 14,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[integrity](https://www.geeksforgeeks.org/integrity-importance-and-challenges/)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/integrity-importance-and-challenges/",
                TextDescription = "integrity",
            },
        },
        new ChunkModel
        {
            Index = 15,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[normalization.](https://www.geeksforgeeks.org/introduction-of-database-normalization/)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/introduction-of-database-normalization/",
                TextDescription = "normalization.",
            },
        },
        new ChunkModel
        {
            Index = 16,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[data analysis](https://www.geeksforgeeks.org/data-analysis-tutorial/)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/data-analysis-tutorial/",
                TextDescription = "data analysis",
            },
        },
        new ChunkModel
        {
            Index = 17,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[Advertise with us](https://www.geeksforgeeks.org/about/contact-us/?listicles)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/about/contact-us/?listicles",
                TextDescription = "Advertise with us",
            },
        },
        new ChunkModel
        {
            Index = 18,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[Next Article](https://www.geeksforgeeks.org/what-are-the-advantages-and-disadvantages-of-random-forest/?ref=next_article)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/what-are-the-advantages-and-disadvantages-of-random-forest/?ref=next_article",
                TextDescription = "Next Article",
            },
        },
        new ChunkModel
        {
            Index = 19,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[What are the Advantages and Disadvantages of Random Forest?](https://www.geeksforgeeks.org/what-are-the-advantages-and-disadvantages-of-random-forest/?ref=next_article)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/what-are-the-advantages-and-disadvantages-of-random-forest/?ref=next_article",
                TextDescription = "What are the Advantages and Disadvantages of Random Forest?",
            },
        },
        new ChunkModel
        {
            Index = 20,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[R](https://www.geeksforgeeks.org/user/rahulpawar_7/contributions/?itm_source=geeksforgeeks&amp;itm_medium=article_author&amp;itm_campaign=auth_user)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/user/rahulpawar_7/contributions/?itm_source=geeksforgeeks&amp;itm_medium=article_author&amp;itm_campaign=auth_user",
                TextDescription = "R",
            },
        },
        new ChunkModel
        {
            Index = 21,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[rahulpawar_7](https://www.geeksforgeeks.org/user/rahulpawar_7/contributions/?itm_source=geeksforgeeks&amp;itm_medium=article_author&amp;itm_campaign=auth_user)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/user/rahulpawar_7/contributions/?itm_source=geeksforgeeks&amp;itm_medium=article_author&amp;itm_campaign=auth_user",
                TextDescription = "rahulpawar_7",
            },
        },
        new ChunkModel
        {
            Index = 22,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[[RELATEDCHUNK]Image-Link-15[/RELATEDCHUNK]](https://news.google.com/publications/CAAqBwgKMLTrzwsw44bnAw?hl=en-IN&amp;gl=IN&amp;ceid=IN%3Aen)",
            Data = new LinkData
            {
                Url = "https://news.google.com/publications/CAAqBwgKMLTrzwsw44bnAw?hl=en-IN&amp;gl=IN&amp;ceid=IN%3Aen",
                TextDescription = "[RELATEDCHUNK]Image-Link-15[/RELATEDCHUNK]",
            },
        },
        new ChunkModel
        {
            Index = 23,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[Data Analysis](https://www.geeksforgeeks.org/category/ai-ml-ds/r-data-analysis/?ref=article_category)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/category/ai-ml-ds/r-data-analysis/?ref=article_category",
                TextDescription = "Data Analysis",
            },
        },
        new ChunkModel
        {
            Index = 24,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[AI-ML-DS](https://www.geeksforgeeks.org/category/ai-ml-ds/?ref=article_category)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/category/ai-ml-ds/?ref=article_category",
                TextDescription = "AI-ML-DS",
            },
        },
    ];

    // https://www.geeksforgeeks.org/basic-operators-in-relational-algebra-2/
    internal static readonly ChunkModel[] GeeksForGeeksAboutRelationalAlgebraLinks =
    [
        new ChunkModel
        {
            Index = 25,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[[RELATEDCHUNK]Image-Link-84[/RELATEDCHUNK]\r\n                                                    Follow](https://news.google.com/publications/CAAqBwgKMLTrzwsw44bnAw?hl=en-IN&amp;gl=IN&amp;ceid=IN%3Aen)",
            Data = new LinkData
            {
                Url = "https://news.google.com/publications/CAAqBwgKMLTrzwsw44bnAw?hl=en-IN&amp;gl=IN&amp;ceid=IN%3Aen",
                TextDescription = "[RELATEDCHUNK]Image-Link-84[/RELATEDCHUNK]\r\n                                                    Follow",
            },
        },
        new ChunkModel
        {
            Index = 26,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[Note:** [SELECT operation](https://www.geeksforgeeks.org/select-operation-in-relational-algebra/)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/select-operation-in-relational-algebra/",
                TextDescription = "Note:** [SELECT operation",
            },
        },
        new ChunkModel
        {
            Index = 27,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[Rename operator](https://www.geeksforgeeks.org/rename-operation-in-relational-algebra/)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/rename-operation-in-relational-algebra/",
                TextDescription = "Rename operator",
            },
        },
        new ChunkModel
        {
            Index = 28,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[Natural Join](https://www.geeksforgeeks.org/sql-natural-join/)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/sql-natural-join/",
                TextDescription = "Natural Join",
            },
        },
        new ChunkModel
        {
            Index = 29,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[Relational Algebra](https://www.geeksforgeeks.org/introduction-of-relational-algebra-in-dbms/)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/introduction-of-relational-algebra-in-dbms/",
                TextDescription = "Relational Algebra",
            },
        },
        new ChunkModel
        {
            Index = 30,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[database](https://www.geeksforgeeks.org/what-is-database/)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/what-is-database/",
                TextDescription = "database",
            },
        },
        new ChunkModel
        {
            Index = 31,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[SQL](https://www.geeksforgeeks.org/what-is-sql/)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/what-is-sql/",
                TextDescription = "SQL",
            },
        },
        new ChunkModel
        {
            Index = 32,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[Advertise with us](https://www.geeksforgeeks.org/about/contact-us/?listicles)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/about/contact-us/?listicles",
                TextDescription = "Advertise with us",
            },
        },
        new ChunkModel
        {
            Index = 33,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[Next Article](https://www.geeksforgeeks.org/extended-operators-in-relational-algebra/?ref=next_article)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/extended-operators-in-relational-algebra/?ref=next_article",
                TextDescription = "Next Article",
            },
        },
        new ChunkModel
        {
            Index = 34,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[Extended Operators in Relational Algebra](https://www.geeksforgeeks.org/extended-operators-in-relational-algebra/?ref=next_article)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/extended-operators-in-relational-algebra/?ref=next_article",
                TextDescription = "Extended Operators in Relational Algebra",
            },
        },
        new ChunkModel
        {
            Index = 35,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[[RELATEDCHUNK]Image-Link-84[/RELATEDCHUNK]](https://news.google.com/publications/CAAqBwgKMLTrzwsw44bnAw?hl=en-IN&amp;gl=IN&amp;ceid=IN%3Aen)",
            Data = new LinkData
            {
                Url = "https://news.google.com/publications/CAAqBwgKMLTrzwsw44bnAw?hl=en-IN&amp;gl=IN&amp;ceid=IN%3Aen",
                TextDescription = "[RELATEDCHUNK]Image-Link-84[/RELATEDCHUNK]",
            },
        },
        new ChunkModel
        {
            Index = 36,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[DBMS](https://www.geeksforgeeks.org/category/computer-subject/dbms/?ref=article_category)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/category/computer-subject/dbms/?ref=article_category",
                TextDescription = "DBMS",
            },
        },
        new ChunkModel
        {
            Index = 37,
            ChunkType = ChunkType.ExternalLink,
            RelatedChunksIndexes = [],
            RawContent = "[DBMS-Relational Algebra](https://www.geeksforgeeks.org/tag/dbms-relational-algebra/?ref=article_tag)",
            Data = new LinkData
            {
                Url = "https://www.geeksforgeeks.org/tag/dbms-relational-algebra/?ref=article_tag",
                TextDescription = "DBMS-Relational Algebra",
            },
        },
    ];
}
