using Sample.Chunkers.Enums;
using Sample.Chunkers.Models;

namespace Sample.Chunkers.UnitTests.TestData;

internal static class CodeBlocksTestData
{
    // https://dev.to/alex_ricciardi/recursion-in-programming-techniques-benefits-and-limitations-java-3o4p
    internal static readonly ChunkModel[] DevToRealWorldArticleCodeBlocks =
    [
        new ChunkModel
        {
            Index = 1,
            ChunkType = ChunkType.CodeBlock,
            RelatedChunksIndexes = [],
            RawContent = @"```
public class Factorial {
    public static int factorial(int n) {
    // --- Base case: factorial of 0 is 1 ----
    if (n == 0) { 
            return 1;  
    // --- Recursive case ---
    } else {
        return n * factorial(n - 1);  
    }
}
```",
            Data = new Dictionary<string, object>
            {
                ["content"] = @"```
public class Factorial {
    public static int factorial(int n) {
    // --- Base case: factorial of 0 is 1 ----
    if (n == 0) { 
            return 1;  
    // --- Recursive case ---
    } else {
        return n * factorial(n - 1);  
    }
}
```",
                ["language"] = "unknown",
            },
        },
        new ChunkModel
        {
            Index = 2,
            ChunkType = ChunkType.CodeBlock,
            RelatedChunksIndexes = [],
            RawContent = @"```
public class DFS {
    // Node class
    static class Node {
        int value;
        Node left, right;
        // Constructor
        Node(int value) {
            this.value = value;
            left = right = null; // Left and right children are null initially
        }
    }
    // Recursive Depth-First Search (DFS) method
    public static void depthFirstSearchRecursive(Node node) {
        // --- Base case ---
        if (node == null) {
            return; 
        }
        // Process the current node (visit)
        System.out.println(node.value);
        // Recursively traverse the left subtree
        depthFirstSearchRecursive(node.left);
        //--- Recursive case ---
        depthFirstSearchRecursive(node.right);
        /*
          Potential stack overflow issue: Each recursive call adds a new 
           frame to the call stack. If the tree is too deep (e.g., with 
           many levels), the recursion
          depth can exceed the system's maximum stack size, causing a
          StackOverflowError.
         */
    }
    public static void main(String[] args) {
        // Create a binary tree
        Node root = new Node(1);
        root.left = new Node(2);
        root.right = new Node(3);
        root.left.left = new Node(4);
        root.left.right = new Node(5);
        System.out.println(""DFS Traversal using Recursion:"");
        depthFirstSearchRecursive(root); 
    }
}
```",
            Data = new Dictionary<string, object>
            {
                ["content"] = @"```
public class DFS {
    // Node class
    static class Node {
        int value;
        Node left, right;
        // Constructor
        Node(int value) {
            this.value = value;
            left = right = null; // Left and right children are null initially
        }
    }
    // Recursive Depth-First Search (DFS) method
    public static void depthFirstSearchRecursive(Node node) {
        // --- Base case ---
        if (node == null) {
            return; 
        }
        // Process the current node (visit)
        System.out.println(node.value);
        // Recursively traverse the left subtree
        depthFirstSearchRecursive(node.left);
        //--- Recursive case ---
        depthFirstSearchRecursive(node.right);
        /*
          Potential stack overflow issue: Each recursive call adds a new 
           frame to the call stack. If the tree is too deep (e.g., with 
           many levels), the recursion
          depth can exceed the system's maximum stack size, causing a
          StackOverflowError.
         */
    }
    public static void main(String[] args) {
        // Create a binary tree
        Node root = new Node(1);
        root.left = new Node(2);
        root.right = new Node(3);
        root.left.left = new Node(4);
        root.left.right = new Node(5);
        System.out.println(""DFS Traversal using Recursion:"");
        depthFirstSearchRecursive(root); 
    }
}
```",
                ["language"] = "unknown",
            },
        },
        new ChunkModel
        {
            Index = 3,
            ChunkType = ChunkType.CodeBlock,
            RelatedChunksIndexes = [],
            RawContent = @"```
import java.util.ArrayDeque;
import java.util.Deque;
public class DFS {
    // Single node in the binary tree
    static class Node {
        int value;
        Node left, right;
        // Node Constructor 
        Node(int value) {
            this.value = value;
            left = right = null; // Left and right children are null initially
        }
    }
    // Depth-First Search (DFS) traversal method 
    public static void depthFirstSearch(Node root) {
        Deque stack = new ArrayDeque<>();
        stack.push(root);
        // traverse the stack until is empty
        while (!stack.isEmpty()) {
            // Pop the top node from the stack
            Node current = stack.pop();
            System.out.println(current.value); 

            if (current.right != null) {
                stack.push(current.right); // Add right child to stack
            }
            if (current.left != null) {
                stack.push(current.left); // Add left child to stack
            }
        }
    }
    public static void main(String[] args) {
        // Create a binary tree
        Node root = new Node(1); 
        root.left = new Node(2); 
        root.right = new Node(3); 
        root.left.left = new Node(4); 
        root.left.right = new Node(5); 

        System.out.println(""DFS Traversal using Deque:"");
        depthFirstSearch(root); 
    }
}
```",
            Data = new Dictionary<string, object>
            {
                ["content"] = @"```
import java.util.ArrayDeque;
import java.util.Deque;
public class DFS {
    // Single node in the binary tree
    static class Node {
        int value;
        Node left, right;
        // Node Constructor 
        Node(int value) {
            this.value = value;
            left = right = null; // Left and right children are null initially
        }
    }
    // Depth-First Search (DFS) traversal method 
    public static void depthFirstSearch(Node root) {
        Deque stack = new ArrayDeque<>();
        stack.push(root);
        // traverse the stack until is empty
        while (!stack.isEmpty()) {
            // Pop the top node from the stack
            Node current = stack.pop();
            System.out.println(current.value); 

            if (current.right != null) {
                stack.push(current.right); // Add right child to stack
            }
            if (current.left != null) {
                stack.push(current.left); // Add left child to stack
            }
        }
    }
    public static void main(String[] args) {
        // Create a binary tree
        Node root = new Node(1); 
        root.left = new Node(2); 
        root.right = new Node(3); 
        root.left.left = new Node(4); 
        root.left.right = new Node(5); 

        System.out.println(""DFS Traversal using Deque:"");
        depthFirstSearch(root); 
    }
}
```",
                ["language"] = "unknown",
            },
        },
        new ChunkModel
        {
            Index = 4,
            ChunkType = ChunkType.CodeBlock,
            RelatedChunksIndexes = [],
            RawContent = @"```
DFS Traversal using Deque:
1
2
4
5
3
```",
            Data = new Dictionary<string, object>
            {
                ["content"] = @"```
DFS Traversal using Deque:
1
2
4
5
3
```",
                ["language"] = "unknown",
            },
        },
    ];

    // https://www.geeksforgeeks.org/basic-operators-in-relational-algebra-2/
    internal static readonly ChunkModel[] GeeksForGeeksAboutRelationalAlgebraCodeBlocks = 
    [
        new ChunkModel
        {
            Index = 1,
            ChunkType = ChunkType.CodeBlock,
            RelatedChunksIndexes = [],
            RawContent = @"```
σ (Cond)(Relation Name)
```",
            Data = new Dictionary<string, object>
            {
                ["content"] = @"```
σ (Cond)(Relation Name)
```",
                ["language"] = "unknown",
            },
        },
        new ChunkModel
        {
            Index = 2,
            ChunkType = ChunkType.CodeBlock,
            RelatedChunksIndexes = [],
            RawContent = @"```
σ (AGE>18)(STUDENT)
```",
            Data = new Dictionary<string, object>
            {
                ["content"] = @"```
σ (AGE>18)(STUDENT)
```",
                ["language"] = "unknown",
            },
        },
        new ChunkModel
        {
            Index = 3,
            ChunkType = ChunkType.CodeBlock,
            RelatedChunksIndexes = [],
            RawContent = @"```
∏(Column 1,Column 2….Column n)(Relation Name)
```",
            Data = new Dictionary<string, object>
            {
                ["content"] = @"```
∏(Column 1,Column 2….Column n)(Relation Name)
```",
                ["language"] = "unknown",
            },
        },
        new ChunkModel
        {
            Index = 4,
            ChunkType = ChunkType.CodeBlock,
            RelatedChunksIndexes = [],
            RawContent = @"```
∏(ROLL_NO,NAME)(STUDENT)
```",
            Data = new Dictionary<string, object>
            {
                ["content"] = @"```
∏(ROLL_NO,NAME)(STUDENT)
```",
                ["language"] = "unknown",
            },
        },
        new ChunkModel
        {
            Index = 5,
            ChunkType = ChunkType.CodeBlock,
            RelatedChunksIndexes = [],
            RawContent = @"```
Relation1 X Relation2
```",
            Data = new Dictionary<string, object>
            {
                ["content"] = @"```
Relation1 X Relation2
```",
                ["language"] = "unknown",
            },
        },
        new ChunkModel
        {
            Index = 6,
            ChunkType = ChunkType.CodeBlock,
            RelatedChunksIndexes = [],
            RawContent = @"```
STUDENT X STUDENT_SPORTS
```",
            Data = new Dictionary<string, object>
            {
                ["content"] = @"```
STUDENT X STUDENT_SPORTS
```",
                ["language"] = "unknown",
            },
        },
        new ChunkModel
        {
            Index = 7,
            ChunkType = ChunkType.CodeBlock,
            RelatedChunksIndexes = [],
            RawContent = "```\r\n\u00A0Relation1\u00A0U Relation2\r\n```",
            Data = new Dictionary<string, object>
            {
                ["content"] = "```\r\n\u00A0Relation1\u00A0U Relation2\r\n```",
                ["language"] = "unknown",
            },
        },
        new ChunkModel
        {
            Index = 8,
            ChunkType = ChunkType.CodeBlock,
            RelatedChunksIndexes = [],
            RawContent = @"```
STUDENT U EMPLOYEE
```",
            Data = new Dictionary<string, object>
            {
                ["content"] = @"```
STUDENT U EMPLOYEE
```",
                ["language"] = "unknown",
            },
        },
        new ChunkModel
        {
            Index = 9,
            ChunkType = ChunkType.CodeBlock,
            RelatedChunksIndexes = [],
            RawContent = "```\r\n\u00A0Relation1 - Relation2\r\n```",
            Data = new Dictionary<string, object>
            {
                ["content"] = "```\r\n\u00A0Relation1 - Relation2\r\n```",
                ["language"] = "unknown",
            },
        },
        new ChunkModel
        {
            Index = 10,
            ChunkType = ChunkType.CodeBlock,
            RelatedChunksIndexes = [],
            RawContent = @"```
STUDENT - EMPLOYEE
```",
            Data = new Dictionary<string, object>
            {
                ["content"] = @"```
STUDENT - EMPLOYEE
```",
                ["language"] = "unknown",
            },
        },
        new ChunkModel
        {
            Index = 11,
            ChunkType = ChunkType.CodeBlock,
            RelatedChunksIndexes = [],
            RawContent = @"```
ρ(Relation2, Relation1)
```",
            Data = new Dictionary<string, object>
            {
                ["content"] = @"```
ρ(Relation2, Relation1)
```",
                ["language"] = "unknown",
            },
        },
        new ChunkModel
        {
            Index = 12,
            ChunkType = ChunkType.CodeBlock,
            RelatedChunksIndexes = [],
            RawContent = @"```
ρ(STUDENT1, STUDENT)
```",
            Data = new Dictionary<string, object>
            {
                ["content"] = @"```
ρ(STUDENT1, STUDENT)
```",
                ["language"] = "unknown",
            },
        },
        new ChunkModel
        {
            Index = 13,
            ChunkType = ChunkType.CodeBlock,
            RelatedChunksIndexes = [],
            RawContent = @"```
ρ(STUDENT_NAMES, ∏(ROLL_NO, NAME)(STUDENT))
```",
            Data = new Dictionary<string, object>
            {
                ["content"] = @"```
ρ(STUDENT_NAMES, ∏(ROLL_NO, NAME)(STUDENT))
```",
                ["language"] = "unknown",
            },
        },
    ];
}
