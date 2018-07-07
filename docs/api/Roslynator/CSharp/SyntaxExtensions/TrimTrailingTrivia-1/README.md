# SyntaxExtensions\.TrimTrailingTrivia Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| TrimTrailingTrivia\(SyntaxToken\) | Removes all trailing whitespace from the trailing trivia and returns a new token with the new trailing trivia\. [WhitespaceTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.whitespacetrivia) and [EndOfLineTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.endoflinetrivia) is considered to be a whitespace\. Returns the same token if there is nothing to trim\. |
| TrimTrailingTrivia\<TNode>\(TNode\) | Removes all trailing whitespace from the trailing trivia and returns a new node with the new trailing trivia\. [WhitespaceTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.whitespacetrivia) and [EndOfLineTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.endoflinetrivia) is considered to be a whitespace\. Returns the same node if there is nothing to trim\. |

## TrimTrailingTrivia\<TNode>\(TNode\)

### Summary

Removes all trailing whitespace from the trailing trivia and returns a new node with the new trailing trivia\.
[WhitespaceTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.whitespacetrivia) and [EndOfLineTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.endoflinetrivia) is considered to be a whitespace\.
Returns the same node if there is nothing to trim\.

```csharp
public static TNode TrimTrailingTrivia<TNode>(this TNode node) where TNode : SyntaxNode
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| node | |

#### Returns

[TNode](../TNode/README.md)




## TrimTrailingTrivia\(SyntaxToken\)

### Summary

Removes all trailing whitespace from the trailing trivia and returns a new token with the new trailing trivia\.
[WhitespaceTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.whitespacetrivia) and [EndOfLineTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.endoflinetrivia) is considered to be a whitespace\.
Returns the same token if there is nothing to trim\.

```csharp
public static SyntaxToken TrimTrailingTrivia(this SyntaxToken token)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| token | |

#### Returns

[SyntaxToken](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtoken)




