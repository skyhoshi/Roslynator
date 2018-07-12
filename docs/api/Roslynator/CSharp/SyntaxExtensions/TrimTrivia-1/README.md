# SyntaxExtensions\.TrimTrivia Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| TrimTrivia\(SyntaxToken\) | Removes all leading whitespace from the leading trivia and all trailing whitespace from the trailing trivia and returns a new token with the new trivia\. [WhitespaceTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.whitespacetrivia) and [EndOfLineTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.endoflinetrivia) is considered to be a whitespace\. Returns the same token if there is nothing to trim\. |
| TrimTrivia\<TNode>\(TNode\) | Removes all leading whitespace from the leading trivia and all trailing whitespace from the trailing trivia and returns a new node with the new trivia\. [WhitespaceTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.whitespacetrivia) and [EndOfLineTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.endoflinetrivia) is considered to be a whitespace\. Returns the same node if there is nothing to trim\. |

## TrimTrivia\<TNode>\(TNode\)

### Summary

Removes all leading whitespace from the leading trivia and all trailing whitespace from the trailing trivia and returns a new node with the new trivia\.
[WhitespaceTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.whitespacetrivia) and [EndOfLineTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.endoflinetrivia) is considered to be a whitespace\.
Returns the same node if there is nothing to trim\.

```csharp
public static TNode TrimTrivia<TNode>(this TNode node) 
    where TNode : SyntaxNode
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

TNode




## TrimTrivia\(SyntaxToken\)

### Summary

Removes all leading whitespace from the leading trivia and all trailing whitespace from the trailing trivia and returns a new token with the new trivia\.
[WhitespaceTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.whitespacetrivia) and [EndOfLineTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.endoflinetrivia) is considered to be a whitespace\.
Returns the same token if there is nothing to trim\.

```csharp
public static SyntaxToken TrimTrivia(this SyntaxToken token)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| token | |

#### Returns

[SyntaxToken](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtoken)




