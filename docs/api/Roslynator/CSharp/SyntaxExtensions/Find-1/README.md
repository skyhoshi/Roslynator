# SyntaxExtensions\.Find Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| Find\(SyntaxTokenList, SyntaxKind\) | Searches for a token of the specified kind and returns the first occurrence within the entire [SyntaxTokenList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtokenlist)\. |
| Find\(SyntaxTriviaList, SyntaxKind\) | Searches for a trivia of the specified kind and returns the first occurrence within the entire [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\. |
| Find\<TNode>\(SeparatedSyntaxList\<TNode>, SyntaxKind\) | Searches for a node of the specified kind and returns the first occurrence within the entire [SeparatedSyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.separatedsyntaxlist-1)\. |
| Find\<TNode>\(SyntaxList\<TNode>, SyntaxKind\) | Searches for a node of the specified kind and returns the first occurrence within the entire [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\. |

## Find\<TNode>\(SeparatedSyntaxList\<TNode>, SyntaxKind\)

### Summary

Searches for a node of the specified kind and returns the first occurrence within the entire [SeparatedSyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.separatedsyntaxlist-1)\.

```csharp
public static TNode Find<TNode>(this SeparatedSyntaxList<TNode> list, SyntaxKind kind) where TNode : Microsoft.CodeAnalysis.SyntaxNode
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| list | |
| kind | |

#### Returns

TNode


## Find\<TNode>\(SyntaxList\<TNode>, SyntaxKind\)

### Summary

Searches for a node of the specified kind and returns the first occurrence within the entire [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\.

```csharp
public static TNode Find<TNode>(this SyntaxList<TNode> list, SyntaxKind kind) where TNode : Microsoft.CodeAnalysis.SyntaxNode
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| list | |
| kind | |

#### Returns

TNode


## Find\(SyntaxTokenList, SyntaxKind\)

### Summary

Searches for a token of the specified kind and returns the first occurrence within the entire [SyntaxTokenList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtokenlist)\.

```csharp
public static SyntaxToken Find(this SyntaxTokenList tokenList, SyntaxKind kind)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| tokenList | |
| kind | |

#### Returns

[SyntaxToken](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtoken)


## Find\(SyntaxTriviaList, SyntaxKind\)

### Summary

Searches for a trivia of the specified kind and returns the first occurrence within the entire [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\.

```csharp
public static SyntaxTrivia Find(this SyntaxTriviaList triviaList, SyntaxKind kind)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| triviaList | |
| kind | |

#### Returns

[SyntaxTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtrivia)


