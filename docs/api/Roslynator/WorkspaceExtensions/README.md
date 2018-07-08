# WorkspaceExtensions Class

Namespace: [Roslynator](../README.md)

Assembly: Roslynator\.CSharp\.Workspaces\.dll

## Summary

A set of extension methods for the workspace layer\.

```csharp
class WorkspaceExtensions
```


## Methods

| Method | Summary |
| ------ | ------- |
| [InsertNodeAfterAsync(Document, SyntaxNode, SyntaxNode, CancellationToken)](InsertNodeAfterAsync/README.md) | Creates a new document with a new node inserted after the specified node\. |
| [InsertNodeBeforeAsync(Document, SyntaxNode, SyntaxNode, CancellationToken)](InsertNodeBeforeAsync/README.md) | Creates a new document with a new node inserted before the specified node\. |
| [InsertNodesAfterAsync(Document, SyntaxNode, IEnumerable\<SyntaxNode>, CancellationToken)](InsertNodesAfterAsync/README.md) | Creates a new document with new nodes inserted after the specified node\. |
| [InsertNodesBeforeAsync(Document, SyntaxNode, IEnumerable\<SyntaxNode>, CancellationToken)](InsertNodesBeforeAsync/README.md) | Creates a new document with new nodes inserted before the specified node\. |
| [RemoveNodeAsync(Document, SyntaxNode, SyntaxRemoveOptions, CancellationToken)](RemoveNodeAsync/README.md) | Creates a new document with the specified node removed\. |
| [RemoveNodesAsync(Document, IEnumerable\<SyntaxNode>, SyntaxRemoveOptions, CancellationToken)](RemoveNodesAsync/README.md) | Creates a new document with the specified nodes removed\. |
| [ReplaceNodeAsync(Document, SyntaxNode, IEnumerable\<SyntaxNode>, CancellationToken)](ReplaceNodeAsync/README.md) | Creates a new document with the specified old node replaced with new nodes\. |
| [ReplaceNodeAsync(Document, SyntaxNode, SyntaxNode, CancellationToken)](ReplaceNodeAsync/README.md) | Creates a new document with the specified old node replaced with a new node\. |
| [ReplaceNodeAsync\<TNode>(Solution, TNode, TNode, CancellationToken)](ReplaceNodeAsync-1/README.md) | Creates a new solution with the specified old node replaced with a new node\. |
| [ReplaceNodesAsync\<TNode>(Document, IEnumerable\<TNode>, Func\<TNode, TNode, SyntaxNode>, CancellationToken)](ReplaceNodesAsync-1/README.md) | Creates a new document with the specified old nodes replaced with new nodes\. |
| [ReplaceNodesAsync\<TNode>(Solution, IEnumerable\<TNode>, Func\<TNode, TNode, SyntaxNode>, CancellationToken)](ReplaceNodesAsync-1/README.md) | Creates a new solution with the specified old nodes replaced with new nodes\. |
| [ReplaceTokenAsync(Document, SyntaxToken, IEnumerable\<SyntaxToken>, CancellationToken)](ReplaceTokenAsync/README.md) | Creates a new document with the specified old token replaced with new tokens\. |
| [ReplaceTokenAsync(Document, SyntaxToken, SyntaxToken, CancellationToken)](ReplaceTokenAsync/README.md) | Creates a new document with the specified old token replaced with a new token\. |
| [ReplaceTriviaAsync(Document, SyntaxTrivia, IEnumerable\<SyntaxTrivia>, CancellationToken)](ReplaceTriviaAsync/README.md) | Creates a new document with the specified old trivia replaced with a new trivia\. |
| [ReplaceTriviaAsync(Document, SyntaxTrivia, SyntaxTrivia, CancellationToken)](ReplaceTriviaAsync/README.md) | Creates a new document with the specified old trivia replaced with a new trivia\. |
| [WithTextChangeAsync(Document, TextChange, CancellationToken)](WithTextChangeAsync/README.md) | Creates a new document updated with the specified text change\. |
| [WithTextChangesAsync(Document, IEnumerable\<TextChange>, CancellationToken)](WithTextChangesAsync/README.md) | Creates a new document updated with the specified text changes\. |
| [WithTextChangesAsync(Document, TextChange\[\], CancellationToken)](WithTextChangesAsync/README.md) | Creates a new document updated with the specified text changes\. |

