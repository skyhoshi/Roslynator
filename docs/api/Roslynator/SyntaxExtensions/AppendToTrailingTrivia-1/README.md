# SyntaxExtensions\.AppendToTrailingTrivia Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| AppendToTrailingTrivia\(SyntaxToken, IEnumerable\<SyntaxTrivia>\) | Creates a new token from this token with the trailing trivia replaced with a new trivia where the specified trivia is added at the end of the trailing trivia\. |
| AppendToTrailingTrivia\(SyntaxToken, SyntaxTrivia\) | Creates a new token from this token with the trailing trivia replaced with a new trivia where the specified trivia is added at the end of the trailing trivia\. |
| AppendToTrailingTrivia\<TNode>\(TNode, IEnumerable\<SyntaxTrivia>\) | Creates a new node from this node with the trailing trivia replaced with a new trivia where the specified trivia is added at the end of the trailing trivia\. |
| AppendToTrailingTrivia\<TNode>\(TNode, SyntaxTrivia\) | Creates a new node from this node with the trailing trivia replaced with a new trivia where the specified trivia is added at the end of the trailing trivia\. |

## AppendToTrailingTrivia\<TNode>\(TNode, IEnumerable\<SyntaxTrivia>\)

### Summary

Creates a new node from this node with the trailing trivia replaced with a new trivia where the specified trivia is added at the end of the trailing trivia\.

```csharp
public static TNode AppendToTrailingTrivia<TNode>(this TNode node, IEnumerable<SyntaxTrivia> trivia) where TNode : SyntaxNode
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| node | |
| trivia | |

#### Returns

TNode


## AppendToTrailingTrivia\<TNode>\(TNode, SyntaxTrivia\)

### Summary

Creates a new node from this node with the trailing trivia replaced with a new trivia where the specified trivia is added at the end of the trailing trivia\.

```csharp
public static TNode AppendToTrailingTrivia<TNode>(this TNode node, SyntaxTrivia trivia) where TNode : SyntaxNode
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| node | |
| trivia | |

#### Returns

TNode


## AppendToTrailingTrivia\(SyntaxToken, IEnumerable\<SyntaxTrivia>\)

### Summary

Creates a new token from this token with the trailing trivia replaced with a new trivia where the specified trivia is added at the end of the trailing trivia\.

```csharp
public static SyntaxToken AppendToTrailingTrivia(this SyntaxToken token, IEnumerable<SyntaxTrivia> trivia)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| token | |
| trivia | |

#### Returns

[SyntaxToken](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtoken)


## AppendToTrailingTrivia\(SyntaxToken, SyntaxTrivia\)

### Summary

Creates a new token from this token with the trailing trivia replaced with a new trivia where the specified trivia is added at the end of the trailing trivia\.

```csharp
public static SyntaxToken AppendToTrailingTrivia(this SyntaxToken token, SyntaxTrivia trivia)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| token | |
| trivia | |

#### Returns

[SyntaxToken](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtoken)


