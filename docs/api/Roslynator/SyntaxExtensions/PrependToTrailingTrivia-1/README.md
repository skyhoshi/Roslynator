# SyntaxExtensions\.PrependToTrailingTrivia Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| PrependToTrailingTrivia\(SyntaxToken, IEnumerable\<SyntaxTrivia>\) | Creates a new token from this token with the trailing trivia replaced with a new trivia where the specified trivia is inserted at the begining of the trailing trivia\. |
| PrependToTrailingTrivia\(SyntaxToken, SyntaxTrivia\) | Creates a new token from this token with the trailing trivia replaced with a new trivia where the specified trivia is inserted at the begining of the trailing trivia\. |
| PrependToTrailingTrivia\<TNode>\(TNode, IEnumerable\<SyntaxTrivia>\) | Creates a new node from this node with the trailing trivia replaced with a new trivia where the specified trivia is inserted at the begining of the trailing trivia\. |
| PrependToTrailingTrivia\<TNode>\(TNode, SyntaxTrivia\) | Creates a new node from this node with the trailing trivia replaced with a new trivia where the specified trivia is inserted at the begining of the trailing trivia\. |

## PrependToTrailingTrivia\<TNode>\(TNode, IEnumerable\<SyntaxTrivia>\)

### Summary

Creates a new node from this node with the trailing trivia replaced with a new trivia where the specified trivia is inserted at the begining of the trailing trivia\.

```csharp
public static TNode PrependToTrailingTrivia<TNode>(this TNode node, IEnumerable<SyntaxTrivia> trivia) 
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
| trivia | |

#### Returns

TNode




## PrependToTrailingTrivia\<TNode>\(TNode, SyntaxTrivia\)

### Summary

Creates a new node from this node with the trailing trivia replaced with a new trivia where the specified trivia is inserted at the begining of the trailing trivia\.

```csharp
public static TNode PrependToTrailingTrivia<TNode>(this TNode node, SyntaxTrivia trivia) 
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
| trivia | |

#### Returns

TNode




## PrependToTrailingTrivia\(SyntaxToken, IEnumerable\<SyntaxTrivia>\)

### Summary

Creates a new token from this token with the trailing trivia replaced with a new trivia where the specified trivia is inserted at the begining of the trailing trivia\.

```csharp
public static SyntaxToken PrependToTrailingTrivia(this SyntaxToken token, IEnumerable<SyntaxTrivia> trivia)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| token | |
| trivia | |

#### Returns

[SyntaxToken](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtoken)




## PrependToTrailingTrivia\(SyntaxToken, SyntaxTrivia\)

### Summary

Creates a new token from this token with the trailing trivia replaced with a new trivia where the specified trivia is inserted at the begining of the trailing trivia\.

```csharp
public static SyntaxToken PrependToTrailingTrivia(this SyntaxToken token, SyntaxTrivia trivia)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| token | |
| trivia | |

#### Returns

[SyntaxToken](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtoken)




