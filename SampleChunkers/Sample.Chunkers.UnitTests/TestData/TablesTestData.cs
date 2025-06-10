using Sample.Chunkers.Enums;
using Sample.Chunkers.Models;

namespace Sample.Chunkers.UnitTests.TestData;

internal class TablesTestData
{
    // https://www.geeksforgeeks.org/basic-operators-in-relational-algebra-2/
    internal static readonly ChunkModel[] GeeksForGeeksAboutRelationalAlgebraTables =
    [
        new ChunkModel
        {
            Index = 14,
            ChunkType = ChunkType.Table,
            RelatedChunksIndexes = [],
            RawContent = @"<table>
<thead></thead>
<tbody>
<tr>
<td><b><strong>ROLL_NO</strong></b></td>
<td><b><strong>SPORTS</strong></b></td>
</tr>
<tr>
<td><span>1</span></td>
<td><span>Badminton</span></td>
</tr>
<tr>
<td><span>2</span></td>
<td><span>Cricket</span></td>
</tr>
<tr>
<td><span>2</span></td>
<td><span>Badminton</span></td>
</tr>
<tr>
<td><span>4</span></td>
<td><span>Badminton</span></td>
</tr>
</tbody>
</table>",
        },
        new ChunkModel
        {
            Index = 15,
            ChunkType = ChunkType.Table,
            RelatedChunksIndexes = [],
            RawContent = @"<table>
<thead></thead>
<tbody>
<tr>
<td><b><strong>EMP_NO</strong></b></td>
<td><b><strong>NAME</strong></b></td>
<td><b><strong>ADDRESS</strong></b></td>
<td><b><strong>PHONE</strong></b></td>
<td><b><strong>AGE</strong></b></td>
</tr>
<tr>
<td><span>1</span></td>
<td><span>RAM</span></td>
<td><span>DELHI</span></td>
<td><span>9455123451</span></td>
<td><span>18</span></td>
</tr>
<tr>
<td><span>5</span></td>
<td><span>NARESH</span></td>
<td><span>HISAR</span></td>
<td><span>9782918192</span></td>
<td><span>22</span></td>
</tr>
<tr>
<td><span>6</span></td>
<td><span>SWETA</span></td>
<td><span>RANCHI</span></td>
<td><span>9852617621</span></td>
<td><span>21</span></td>
</tr>
<tr>
<td><span>4</span></td>
<td><span>SURESH</span></td>
<td><span>DELHI</span></td>
<td><span>9156768971</span></td>
<td><span>18</span></td>
</tr>
</tbody>
</table>",
        },
        new ChunkModel
        {
            Index = 16,
            ChunkType = ChunkType.Table,
            RelatedChunksIndexes = [],
            RawContent = @"<table>
<thead></thead>
<tbody>
<tr>
<td><b><strong>ROLL_NO</strong></b></td>
<td><b><strong>NAME</strong></b></td>
<td><b><strong>ADDRESS</strong></b></td>
<td><b><strong>PHONE</strong></b></td>
<td><b><strong>AGE</strong></b></td>
</tr>
<tr>
<td><span>1</span></td>
<td><span>RAM</span></td>
<td><span>DELHI</span></td>
<td><span>9455123451</span></td>
<td><span>18</span></td>
</tr>
<tr>
<td><span>2</span></td>
<td><span>RAMESH</span></td>
<td><span>GURGAON</span></td>
<td><span>9652431543</span></td>
<td><span>18</span></td>
</tr>
<tr>
<td><span>3</span></td>
<td><span>SUJIT</span></td>
<td><span>ROHTAK</span></td>
<td><span>9156253131</span></td>
<td><span>20</span></td>
</tr>
<tr>
<td><span>4</span></td>
<td><span>SURESH</span></td>
<td><span>DELHI</span></td>
<td><span>9156768971</span></td>
<td><span>18</span></td>
</tr>
</tbody>
</table>",
        },
        new ChunkModel
        {
            Index = 17,
            ChunkType = ChunkType.Table,
            RelatedChunksIndexes = [],
            RawContent = @"<table>
<thead></thead>
<tbody>
<tr>
<td><b><strong>ROLL_NO</strong></b></td>
<td><b><strong>NAME</strong></b></td>
<td><b><strong>ADDRESS</strong></b></td>
<td><b><strong>PHONE</strong></b></td>
<td><b><strong>AGE</strong></b></td>
</tr>
<tr>
<td><span>3</span></td>
<td><span>SUJIT</span></td>
<td><span>ROHTAK</span></td>
<td><span>9156253131</span></td>
<td><span>20</span></td>
</tr>
</tbody>
</table>",
        },
        new ChunkModel
        {
            Index = 18,
            ChunkType = ChunkType.Table,
            RelatedChunksIndexes = [],
            RawContent = @"<table>
<thead></thead>
<tbody>
<tr>
<td><b><strong>ROLL_NO</strong></b></td>
<td><b><strong>NAME</strong></b></td>
</tr>
<tr>
<td><span>1</span></td>
<td><span>RAM</span></td>
</tr>
<tr>
<td><span>2</span></td>
<td><span>RAMESH</span></td>
</tr>
<tr>
<td><span>3</span></td>
<td><span>SUJIT</span></td>
</tr>
<tr>
<td><span>4</span></td>
<td><span>SURESH</span></td>
</tr>
</tbody>
</table>",
        },
        new ChunkModel
        {
            Index = 19,
            ChunkType = ChunkType.Table,
            RelatedChunksIndexes = [],
            RawContent = @"<table>
<thead></thead>
<tbody>
<tr>
<td><b><strong>ROLL_NO</strong></b></td>
<td><b><strong>NAME</strong></b></td>
<td><b><strong>ADDRESS</strong></b></td>
<td><b><strong>PHONE</strong></b></td>
<td><b><strong>AGE</strong></b></td>
<td><b><strong>ROLL_NO</strong></b></td>
<td><b><strong>SPORTS</strong></b></td>
</tr>
<tr>
<td><span>1</span></td>
<td><span>RAM</span></td>
<td><span>DELHI</span></td>
<td><span>9455123451</span></td>
<td><span>18</span></td>
<td><span>1</span></td>
<td><span>Badminton</span></td>
</tr>
<tr>
<td><span>1</span></td>
<td><span>RAM</span></td>
<td><span>DELHI</span></td>
<td><span>9455123451</span></td>
<td><span>18</span></td>
<td><span>2</span></td>
<td><span>Cricket</span></td>
</tr>
<tr>
<td><span>1</span></td>
<td><span>RAM</span></td>
<td><span>DELHI</span></td>
<td><span>9455123451</span></td>
<td><span>18</span></td>
<td><span>2</span></td>
<td><span>Badminton</span></td>
</tr>
<tr>
<td><span>1</span></td>
<td><span>RAM</span></td>
<td><span>DELHI</span></td>
<td><span>9455123451</span></td>
<td><span>18</span></td>
<td><span>4</span></td>
<td><span>Badminton</span></td>
</tr>
<tr>
<td><span>2</span></td>
<td><span>RAMESH</span></td>
<td><span>GURGAON</span></td>
<td><span>9652431543</span></td>
<td><span>18</span></td>
<td><span>1</span></td>
<td><span>Badminton</span></td>
</tr>
<tr>
<td><span>2</span></td>
<td><span>RAMESH</span></td>
<td><span>GURGAON</span></td>
<td><span>9652431543</span></td>
<td><span>18</span></td>
<td><span>2</span></td>
<td><span>Cricket</span></td>
</tr>
<tr>
<td><span>2</span></td>
<td><span>RAMESH</span></td>
<td><span>GURGAON</span></td>
<td><span>9652431543</span></td>
<td><span>18</span></td>
<td><span>2</span></td>
<td><span>Badminton</span></td>
</tr>
<tr>
<td><span>2</span></td>
<td><span>RAMESH</span></td>
<td><span>GURGAON</span></td>
<td><span>9652431543</span></td>
<td><span>18</span></td>
<td><span>4</span></td>
<td><span>Badminton</span></td>
</tr>
<tr>
<td><span>3</span></td>
<td><span>SUJIT</span></td>
<td><span>ROHTAK</span></td>
<td><span>9156253131</span></td>
<td><span>20</span></td>
<td><span>1</span></td>
<td><span>Badminton</span></td>
</tr>
<tr>
<td><span>3</span></td>
<td><span>SUJIT</span></td>
<td><span>ROHTAK</span></td>
<td><span>9156253131</span></td>
<td><span>20</span></td>
<td><span>2</span></td>
<td><span>Cricket</span></td>
</tr>
<tr>
<td><span>3</span></td>
<td><span>SUJIT</span></td>
<td><span>ROHTAK</span></td>
<td><span>9156253131</span></td>
<td><span>20</span></td>
<td><span>2</span></td>
<td><span>Badminton</span></td>
</tr>
<tr>
<td><span>3</span></td>
<td><span>SUJIT</span></td>
<td><span>ROHTAK</span></td>
<td><span>9156253131</span></td>
<td><span>20</span></td>
<td><span>4</span></td>
<td><span>Badminton</span></td>
</tr>
<tr>
<td><span>4</span></td>
<td><span>SURESH</span></td>
<td><span>DELHI</span></td>
<td><span>9156768971</span></td>
<td><span>18</span></td>
<td><span>1</span></td>
<td><span>Badminton</span></td>
</tr>
<tr>
<td><span>4</span></td>
<td><span>SURESH</span></td>
<td><span>DELHI</span></td>
<td><span>9156768971</span></td>
<td><span>18</span></td>
<td><span>2</span></td>
<td><span>Cricket</span></td>
</tr>
<tr>
<td><span>4</span></td>
<td><span>SURESH</span></td>
<td><span>DELHI</span></td>
<td><span>9156768971</span></td>
<td><span>18</span></td>
<td><span>2</span></td>
<td><span>Badminton</span></td>
</tr>
<tr>
<td><span>4</span></td>
<td><span>SURESH</span></td>
<td><span>DELHI</span></td>
<td><span>9156768971</span></td>
<td><span>18</span></td>
<td><span>4</span></td>
<td><span>Badminton</span></td>
</tr>
</tbody>
</table>",
        },
        new ChunkModel
        {
            Index = 20,
            ChunkType = ChunkType.Table,
            RelatedChunksIndexes = [],
            RawContent = @"<table>
<thead></thead>
<tbody>
<tr>
<td><b><strong>ROLL_NO</strong></b></td>
<td><b><strong>NAME</strong></b></td>
<td><b><strong>ADDRESS</strong></b></td>
<td><b><strong>PHONE</strong></b></td>
<td><b><strong>AGE</strong></b></td>
</tr>
<tr>
<td><span>1</span></td>
<td><span>RAM</span></td>
<td><span>DELHI</span></td>
<td><span>9455123451</span></td>
<td><span>18</span></td>
</tr>
<tr>
<td><span>2</span></td>
<td><span>RAMESH</span></td>
<td><span>GURGAON</span></td>
<td><span>9652431543</span></td>
<td><span>18</span></td>
</tr>
<tr>
<td><span>3</span></td>
<td><span>SUJIT</span></td>
<td><span>ROHTAK</span></td>
<td><span>9156253131</span></td>
<td><span>20</span></td>
</tr>
<tr>
<td><span>4</span></td>
<td><span>SURESH</span></td>
<td><span>DELHI</span></td>
<td><span>9156768971</span></td>
<td><span>18</span></td>
</tr>
<tr>
<td><span>5</span></td>
<td><span>NARESH</span></td>
<td><span>HISAR</span></td>
<td><span>9782918192</span></td>
<td><span>22</span></td>
</tr>
<tr>
<td><span>6</span></td>
<td><span>SWETA</span></td>
<td><span>RANCHI</span></td>
<td><span>9852617621</span></td>
<td><span>21</span></td>
</tr>
</tbody>
</table>",
        },
        new ChunkModel
        {
            Index = 21,
            ChunkType = ChunkType.Table,
            RelatedChunksIndexes = [],
            RawContent = @"<table>
<thead></thead>
<tbody>
<tr>
<td><b><strong>ROLL_NO</strong></b></td>
<td><b><strong>NAME</strong></b></td>
<td><b><strong>ADDRESS</strong></b></td>
<td><b><strong>PHONE</strong></b></td>
<td><b><strong>AGE</strong></b></td>
</tr>
<tr>
<td><span>2</span></td>
<td><span>RAMESH</span></td>
<td><span>GURGAON</span></td>
<td><span>9652431543</span></td>
<td><span>18</span></td>
</tr>
<tr>
<td><span>3</span></td>
<td><span>SUJIT</span></td>
<td><span>ROHTAK</span></td>
<td><span>9156253131</span></td>
<td><span>20</span></td>
</tr>
</tbody>
</table>",
        },
    ];
}
