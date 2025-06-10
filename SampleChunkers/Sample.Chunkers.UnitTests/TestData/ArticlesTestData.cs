namespace Sample.Chunkers.UnitTests.TestData;

internal static class ArticlesTestData
{
    // https://dev.to/alex_ricciardi/recursion-in-programming-techniques-benefits-and-limitations-java-3o4p
    internal const string DevToRealWorldArticleText = @"This article explains the concept of recursion in programming, where a function calls itself to solve smaller instances of a problem, and its applications in tasks like tree traversal and divide-and-conquer algorithms. It also discusses the benefits of recursion for simplifying complex problems and improving code readability, while highlighting potential issues like stack overflow.

* * *

In computer science, recursion is an algorithmic technique in which a function calls itself to solve smaller instances of the same problem. In programming, recursion is an amazing technique with the help of which we can reduce the length of our code and make it easier to read and write” (GeeksforGeeks, 2024, p1). Recursion is used in programming to solve complex problems involving repetition and hierarchical structures such as tree traversals, graph algorithms, and divide-and-conquer problems like sorting and searching.

The basic components found in recursive functions are base cases and recursive cases. A base case is a condition that, when met, ends the recursion process (Ricciardi, 2024). A recursive case is a set of code lines that are executed until a base condition is met.

A classic example where recursion is well suited is in computing the factorial of a number. A factorial can be defined as a non-negative integer 𝑛, denoted 𝑛!, the product of all positive integers less than or equal to 𝑛:

𝑛! = 𝑛×(𝑛−1)!

In Java:

```
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
```

Enter fullscreen mode

    Exit fullscreen mode

Note that the factorial() method calls itself until it reaches the base case where 𝑛 = 0

There are various benefits to using recursion. One of the biggest benefits of using recursion is that it allows programmers to easily break down complex problems into simpler, more manageable subproblems. This approach is often referred to as the divide-and-conquer approach, it is implemented in algorithms like mergesort, where recursion divides a complex sort problem into smaller problems leading to a more efficient sort solution than the linear sort iterating solution. Additionally, recursion helps with code readability by simplifying and shortening code lines. When using recursion, programmers can write problems involving repetition or hierarchical structures (trees) without the need to implement complex loops. Recursion also simplifies, and it is efficient at handling dynamic and random data structures such as linked lists and tree structures. For instance, when traversing a binary tree, using recursion simplifies the implementation of the process without the need to implement a stack.

Although recursion has various advantages, in some scenarios using a stack is preferable over recursion. For example, recursion can generate a stack overflow error, ‘StackOverflowError ’, if the recursive depth (number of recursion calls) becomes too large. This can happen in cases like deep tree traversals or depth-first search algorithms, where the number of recursion calls may exceed the system’s call stack capacity. In Java, the limit of the call stack varies depending on the platform used and the Java Virtual Machine implemented. Java stack size can be configured using the JVM argument ‘-Xss’, for example ‘java -Xss1M MyProgram‘ where 1M is the size of the call back for MyProgram (Goodrich, Tamassia, & Goldwasser, 2023). It is best practice to use a stack or tail recursion, if possible, in this scenario. “A recursion is a tail recursion if any recursive call that is made from one context is the very last operation in that context, with the return value of the recursive call (if any) immediately returned by the enclosing recursion” (Goodrich, Tamassia, & Goldwasser, 2023, 5.5 Eliminating tail recursion). Note that while some programming languages optimize tail-recursive functions, Java does not. Thus, in Java, an optimized tail-recursive function needs to be implemented implicitly.

Below are examples of implementing a depth-first search (DFS) traversal of a tree, using recursion with a possibility of ‘StackOverflowError ’and a stack (Dequee) eliminating the possibility of a ‘StackOverflowError ’:

Using recursion possibility of ‘StackOverflowError’:

```
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
```

Enter fullscreen mode

    Exit fullscreen mode

Using the stack approach eliminating the possibility of a ‘StackOverflowError’:

```
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
```

Enter fullscreen mode

    Exit fullscreen mode

Output:

```
DFS Traversal using Deque:
1
2
4
5
3
```

Enter fullscreen mode

    Exit fullscreen mode

To summarize, recursion is a technique in which a function calls itself to solve smaller instances of the same problem, it is often used in problems like tree traversal, graph algorithms, and divide-and-conquer strategies. While recursion simplifies complex problems and code readability, excessive recursive calls can lead to stack overflow errors, particularly in deeply nested structures such as trees, making iterative approaches using explicit stacks preferable in certain cases.

* * *

**References:**

Arslan, Ş. (2023, February 25). *A Comprehensive tree traversal guide in Javascript — General and binary tree traversals.* Shinar Arslan Blog. https://www.sahinarslan.tech/posts/a-comprehensive-tree-traversal-guide-in-javascript-general-and-binary-tree-traversals

GeeksforGeeks (2024, August 18). *Introduction to recursion*. GeeksforGeeks. https://www.geeksforgeeks.org/introduction-to-recursion-2/Links to an external site

Goodrich T, M., Tamassia, R., & Goldwasser H. M. (2023, June). Chapter 5: Algorithms recursion. *Data structures and algorithms*. zyBook ISBN: 979–8–203–40813–6.

* * *

Originally published at [Alex.omegapy - Medium](https://medium.com/@alex.omegapy) on September 22, 2024.";

    // https://www.geeksforgeeks.org/data-modeling-a-comprehensive-guide-for-analysts/
    internal const string GeeksForGeeksTextAboutDataModeling = @"# Data Modeling: A Comprehensive Guide for Analysts

Last Updated : 
                                                    16 Jul, 2024

Comments

Improve

- 
- 
-

Suggest changes
                                                                                          
5 Likes

Like

Report

[!\[News\](https://media.geeksforgeeks.org/auth-dashboard-uploads/Google-news.svg)
                                                    Follow](https://news.google.com/publications/CAAqBwgKMLTrzwsw44bnAw?hl=en-IN&amp;gl=IN&amp;ceid=IN%3Aen)

Data modelling is a fundamental component that facilitates the organisation, structuring, and interpretation of complicated datasets by analysts. 

In this tutorial we'll dive into the field of data modelling, examining its importance, the procedures involved, and answering common queries. 

Table of Content

- What is Data Modeling?
- Types of Data Models
- Data Modeling Process
- Types of Data Modeling
- Benefits of Data Modeling
- Data Modeling - FAQs

![Data-Modeling-in-Analysis](https://media.geeksforgeeks.org/wp-content/uploads/20240215112547/Data-Modeling-in-Analysis.webp)
## **What is Data Modeling?**

Data modelling in analysis is  the process of creating a visual representation , abstraction of data structures, relationships, and rules within a system or organization. Determining and analysing the data requirements required to support business activities within the bounds of related information systems in organisations is another process known as data modelling.  

The main objective of data modelling is to provide a precise and well-organized framework for data organisation and representation, since it enables efficient analysis and decision-making. Analysts can discover trends, understand the connections between various data items, and make sure that data is efficiently and accurately stored by building models.

### **What is Data Model?**

Data models are visual representations of an enterprise’s data elements and the connections between them. Models assist to define and arrange data in the context of key business processes, hence facilitating the creation of successful information systems. They let business and technical personnel to collaborate on how data will be kept, accessed, shared, updated, and utilised within an organisation.

## Types of Data Models

There are three main types of data models:

![modeling](https://media.geeksforgeeks.org/wp-content/uploads/20240215113046/modeling.webp)

- **Conceptual Data Model**: Conceptual Data Model is a representations of data Examine and describe in depth your abstract, high-level business concepts and structures. They are most commonly employed when working through high-level concepts and preliminary needs at the start of a new project. They are typically developed as alternatives or preludes to the logical data models that come later.T he main purpose of this data model is to organize, define business problems , rules and concepts. For instance, it helps business people to view any data like market data, customer data, and purchase data.
- **Logical Data Model**: In the logical data model, By offering a thorough representation of the data at a logical level, the logical data model expands on the conceptual model. It outlines the tables, columns, connections, and constraints that make up the[data structure.](https://www.geeksforgeeks.org/data-structures/) Although logical data models are not dependant on any particular database management system (DBMS), they are more similar to how data would be implemented in a database. The physical design of databases is based on this idea.
- **Physical Data Model**: In Physical Data model ,The implementation is explained with reference to a particular database system. It outlines every part and service needed to construct a database. It is made with queries and the database language. Every table, column, and constraint—such as primary key, foreign key, NOT NULL, etc.—is represented in the physical data model. The creation of a database is the primary task of the physical data model. Developers and database administrators (DBAs) designed this model. This kind of data modelling aids in the creation of the schema and provides us with an abstraction of the databases. This model explains how the data model is specifically implemented. Constraints, RDBMS features, and database column keys are made possible by the physical data model.

## Data Modeling Process

The practice of conceptually representing data items and their connections to one another is known as data modelling. Data modellers collaborate with stakeholders at each stage of the process to define entities and attributes, establish relationships between data objects, and create models that accurately represent the data in a format that can be consumed by applications. These stakeholders may include developers, database administrators, and other interested parties. Lets discuss the data modelling steps:

1. **Identifying data sources**: The first stage is to identify and investigate the different sources of data both inside and outside the company. It's critical to comprehend the sources of the data and how various sources add to the information as a whole. Determining the sources of data is essential since it guarantees a thorough framework for data modelling. It assists in gathering all pertinent data, setting the stage for a precise and comprehensive depiction of the data landscape.
2. **Defining Entities and Attributes**: This stage is all on identifying the entities (items or ideas) and the characteristics that go along with them. Entities constitute the subject matter of the data, whereas attributes specify the particular qualities of each entity. The foundation of data modelling is the definition of entities and characteristics. It offers an orderly and transparent framework, which is necessary to comprehend the characteristics of the data and create a useful model.
3. **Mapping Relationships**: Relationships show the connections or associations between various things. Relationship mapping entails locating and characterising these linkages, indicating the nature and cardinality of every relationship. In order to capture the interdependencies within the data, it is essential to understand relationships. It improves the correctness of the model by capturing the relationships between various data pieces that exist in the real world.
4. **Choosing a model Type**: The right data model type is selected based on the project needs and data properties. Choosing between conceptual, logical, or physical models, or going with a particular model like relational or object-oriented, may be part of this decision.  The degree of abstraction and detail in the representation is determined by the model type that is selected. It guarantees adherence to project objectives and facilitates the[development](https://www.geeksforgeeks.org/class-10-social-science-economics-chapter-1-development/) of a model appropriate for the data type.
5. **Implementing and Maintaining**: The process of implementation converts a physical or logical data model into a[database schema.](https://www.geeksforgeeks.org/database-schemas/) This entails establishing constraints, generating tables, and adding database-specific information. Updating the model to account for shifting technological or commercial needs is called maintenance. Significance: The theoretical model becomes a useful database upon implementation. Frequent upkeep guarantees that the model stays current and accurate, allowing it to adjust to the changing requirements of the company.

## Types of Data Modeling

These are the 5 different types of data models:

**Hierarchical Model**: The structure of the hierarchical model resembles a[tree.](https://www.geeksforgeeks.org/introduction-to-tree-data-structure-and-algorithm-tutorials/) The remaining child nodes are arranged in a certain sequence, and there is only one root node—or, alternatively, one parent node. However, the hierarchical approach is no longer widely applied. approach connections in the actual world may be modelled using this approach.

For Example , For example, in a college there are many courses, many professors and students. So college became a parent and professors and students became its children. 

![daat](https://media.geeksforgeeks.org/wp-content/uploads/20240215113207/daat.webp)**Relational Model** :Relational Mode represent the links between tables by representing data as rows and columns in tables. It is frequently utilised in[database design](https://www.geeksforgeeks.org/significance-of-database-design/) and is strongly related to relational database management systems (RDBMS).

**Object-Oriented Data Model**: In this model, data is represented as objects, similar to those used in object-oriented programming ,Creating objects with stored values is the object-oriented method. In addition to allowing data abstraction, inheritance, and encapsulation, the object-oriented architecture facilitates communication.

**Network Model** :We have a versatile approach to represent objects and the relationships among these things thanks to the network model. One of its features is a schema, which is a graph representation of the data. An item is stored within a node, and the relationship between them is represented as an edge. This allows them to generalise the maintenance of many parent and child records.

**ER-Model**: A high-level[relational model](https://www.geeksforgeeks.org/relational-model-in-dbms/) called the [entity-relationship model (ER model)](https://www.geeksforgeeks.org/introduction-of-er-model/) is used to specify the data pieces and relationships between the entities in a system. This conceptual design gives us an easier-to-understand perspective on the facts. An entity-relationship diagram, which is made up of entities, attributes, and relationships, is used in this model to depict the whole database.

A relationship between entities is called an association. Mapping cardinality many associations like:

- one to one
- one to many
- many to one
- many to many

## Benefits of Data Modeling

In order to organise and structure data and provide database design clarity, data modelling is essential. It acts as a common language, promoting efficient stakeholder communication. It directs the best database architecture for effective data storage and retrieval through visual representation. 

- Visualizes complex data structures, providing a clear roadmap for understanding relationships.
- Acts as a universal language, fostering effective communication between business and technical stakeholders.
- Creates organized databases by defining entities, properties, and relationships.
- Enhances data quality and[integrity](https://www.geeksforgeeks.org/integrity-importance-and-challenges/) by reducing anomalies and redundancy through[normalization.](https://www.geeksforgeeks.org/introduction-of-database-normalization/)
- Minimizes errors in database and application development.
- Ensures consistency in documentation and system designs across the organization.
- Improves database and application performance.
- Facilitates quick correlation of data across the company.
- Strengthens communication between business intelligence and development teams.

## Conclusion

In conclusion,Data modelling is an essential component of[data analysis](https://www.geeksforgeeks.org/data-analysis-tutorial/) that offers a methodical way to arrange and comprehend intricate facts. Analysts may create reliable models that improve insights and decision-making by adhering to the process's specified phases.

Comment

More info

                               [Advertise with us](https://www.geeksforgeeks.org/about/contact-us/?listicles)

[Next Article](https://www.geeksforgeeks.org/what-are-the-advantages-and-disadvantages-of-random-forest/?ref=next_article)

[What are the Advantages and Disadvantages of Random Forest?](https://www.geeksforgeeks.org/what-are-the-advantages-and-disadvantages-of-random-forest/?ref=next_article)

[R](https://www.geeksforgeeks.org/user/rahulpawar_7/contributions/?itm_source=geeksforgeeks&amp;itm_medium=article_author&amp;itm_campaign=auth_user)

[rahulpawar_7](https://www.geeksforgeeks.org/user/rahulpawar_7/contributions/?itm_source=geeksforgeeks&amp;itm_medium=article_author&amp;itm_campaign=auth_user)

Follow

[!\[News\](https://media.geeksforgeeks.org/auth-dashboard-uploads/Google-news.svg)](https://news.google.com/publications/CAAqBwgKMLTrzwsw44bnAw?hl=en-IN&amp;gl=IN&amp;ceid=IN%3Aen)

5

Improve

Article Tags :

- [Data Analysis](https://www.geeksforgeeks.org/category/ai-ml-ds/r-data-analysis/?ref=article_category)
- [AI-ML-DS](https://www.geeksforgeeks.org/category/ai-ml-ds/?ref=article_category)
";

    // https://www.geeksforgeeks.org/basic-operators-in-relational-algebra-2/
    internal const string GeeksForGeeksTextAboutRelationalAlgebra = @"# Basic Operators in Relational Algebra

Last Updated : 
                                                    19 Sep, 2024

Comments

Improve

- 
- 
-

Suggest changes
                                                                                          
75 Likes

Like

Report

[!\[News\](https://media.geeksforgeeks.org/auth-dashboard-uploads/Google-news.svg)
                                                    Follow](https://news.google.com/publications/CAAqBwgKMLTrzwsw44bnAw?hl=en-IN&amp;gl=IN&amp;ceid=IN%3Aen)

The Relational Model is a way of structuring data using relations, which are a collection of tuples that have the same attributes. Relational Algebra is a procedural query language that takes relations as input and returns relations as output. It uses a set of operators to manipulate and retrieve data from these relations. Here, we’ll explore the basic operators of Relational Algebra using the STUDENT\_SPORTS, EMPLOYEE, and STUDENT relations from Table 1, Table 2, and Table 3, respectively.

**Table 1: STUDENT\_SPORTS**

<table>
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
</table>

 **Table 2: EMPLOYEE**

<table>
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
</table>

**Table 3: STUDENT** 

<table>
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
</table>

## ***1. Selection operator (*****σ*****)***

Selection operator is used to selecting tuples from a relation based on some condition. Syntax: 

```
σ (Cond)(Relation Name)
```

Extract students whose age is greater than 18 from STUDENT relation given in Table 3  

```
σ (AGE>18)(STUDENT)
```

**[Note:** [SELECT operation](https://www.geeksforgeeks.org/select-operation-in-relational-algebra/) does not show any result, the projection operator must be called before the selection operator to generate or project the result. So, the correct syntax to generate the result is**:** ∏(**σ** (AGE&gt;18)(STUDENT))]

**RESULT:** 

<table>
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
</table>

## ***2. Projection Operator (*****∏*****)***

Projection operator is used to project particular columns from a relation. Syntax:  

```
∏(Column 1,Column 2….Column n)(Relation Name)
```

Extract ROLL\_NO and NAME from STUDENT relation given in Table 3  

```
∏(ROLL_NO,NAME)(STUDENT)
```

**RESULT:**  

<table>
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
</table>

**Note:** If the resultant relation after projection has duplicate rows, it will be removed. For Example  **∏**(ADDRESS)(STUDENT) will remove one duplicate row with the value DELHI and return three rows. 

## ***3. Cross Product(X)***

Cross product is used to join two relations. For every row of Relation1, each row of Relation2 is concatenated. If Relation1 has m tuples and and Relation2 has n tuples, cross product of Relation1 and Relation2 will have m X n tuples. Syntax: 

```
Relation1 X Relation2
```

To apply Cross Product on STUDENT relation given in Table 1 and STUDENT\_SPORTS relation given in Table 2,  

```
STUDENT X STUDENT_SPORTS
```

**RESULT:** 

<table>
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
</table>

## ***4. Union (U)***

Union on two relations R1 and R2 can only be computed if R1 and R2 are **union compatible** (These two relations should have the same number of attributes and corresponding attributes in two relations have the same domain). Union operator when applied on two relations R1 and R2 will give a relation with tuples that are either in R1 or in R2. The tuples which are in both R1 and R2 will appear only once in the result relation. Syntax: 

```
 Relation1 U Relation2
```

Find the person who is either student or employees, we can use Union operators like: 

```
STUDENT U EMPLOYEE
```

**RESULT:**  

<table>
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
</table>

## ***5. Minus (-) or Set Difference***

Minus on two relations R1 and R2 can only be computed if R1 and R2 are **union compatible**. Minus operator when applied on two relations as R1-R2 will give a relation with tuples that are in R1 but not in R2. Syntax: 

```
 Relation1 - Relation2
```

Find the person who is a student but not an employee, we can use minus operator like:  

```
STUDENT - EMPLOYEE
```

**RESULT:**  

<table>
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
</table>

## ***6. Rename(*****ρ*****)***

[Rename operator](https://www.geeksforgeeks.org/rename-operation-in-relational-algebra/) is used to giving another name to a relation. Syntax:  

```
ρ(Relation2, Relation1)
```

To rename STUDENT relation to STUDENT1, we can use rename operator like:  

```
ρ(STUDENT1, STUDENT)
```

If you want to create a relation STUDENT\_NAMES with ROLL\_NO and NAME from STUDENT, it can be done using rename operator as:  

```
ρ(STUDENT_NAMES, ∏(ROLL_NO, NAME)(STUDENT))
```

### Extended Relational Algebra Operators

- Intersection (∩)
- Division (÷)
- Join Operations (⋈)
- [Natural Join](https://www.geeksforgeeks.org/sql-natural-join/)
- Theta Join
- Equi Join

These operators provide more functionality for complex queries in relational databases.

## Conclusion

[Relational Algebra](https://www.geeksforgeeks.org/introduction-of-relational-algebra-in-dbms/) provides a fundamental toolkit for querying and manipulating relations in a [database](https://www.geeksforgeeks.org/what-is-database/). It offers a set of operators that allow users to perform basic operations like selection, projection, union, and more. Understanding these operators is essential for working with relational databases and forms the basis for more advanced [SQL](https://www.geeksforgeeks.org/what-is-sql/) operations.

Comment

More info

                               [Advertise with us](https://www.geeksforgeeks.org/about/contact-us/?listicles)

[Next Article](https://www.geeksforgeeks.org/extended-operators-in-relational-algebra/?ref=next_article)

[Extended Operators in Relational Algebra](https://www.geeksforgeeks.org/extended-operators-in-relational-algebra/?ref=next_article)

![https://media.geeksforgeeks.org/auth/avatar.png](https://media.geeksforgeeks.org/wp-content/uploads/20200717172614/authPreLogo.png)

GeeksforGeeks

[!\[News\](https://media.geeksforgeeks.org/auth-dashboard-uploads/Google-news.svg)](https://news.google.com/publications/CAAqBwgKMLTrzwsw44bnAw?hl=en-IN&amp;gl=IN&amp;ceid=IN%3Aen)

75

Improve

Article Tags :

- [DBMS](https://www.geeksforgeeks.org/category/computer-subject/dbms/?ref=article_category)
- [DBMS-Relational Algebra](https://www.geeksforgeeks.org/tag/dbms-relational-algebra/?ref=article_tag)
";
}
