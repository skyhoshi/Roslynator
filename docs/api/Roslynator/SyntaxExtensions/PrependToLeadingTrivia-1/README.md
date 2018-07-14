# SyntaxExtensions\.PrependToLeadingTrivia Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| PrependToLeadingTrivia\(SyntaxToken, IEnumerable\<SyntaxTrivia>\) | Creates a new token from this token with the leading trivia replaced with a new trivia where the specified trivia is inserted at the begining of the leading trivia\. |
| PrependToLeadingTrivia\(SyntaxToken, SyntaxTrivia\) | Creates a new token from this token with the leading trivia replaced with a new trivia where the specified trivia is inserted at the begining of the leading trivia\. |
| PrependToLeadingTrivia\<TNode>\(TNode, IEnumerable\<SyntaxTrivia>\) | Creates a new node from this node with the leading trivia replaced with a new trivia where the specified trivia is inserted at the begining of the leading trivia\. |
| PrependToLeadingTrivia\<TNode>\(TNode, SyntaxTrivia\) | Creates a new node from this node with the leading trivia replaced with a new trivia where the specified trivia is inserted at the begining of the leading trivia\. |

## PrependToLeadingTrivia\<TNode>\(TNode, IEnumerable\<SyntaxTrivia>\)

### Summary

Creates a new node from this node with the leading trivia replaced with a new trivia where the specified trivia is inserted at the begining of the leading trivia\.

```csharp
public static TNode PrependToLeadingTrivia<TNode>(this TNode node, IEnumerable<SyntaxTrivia> trivia) where TNode : Microsoft.CodeAnalysis.SyntaxNode
```

#### Type Parameters

| Name | Summary |
| ---- | ------- |
| TNode | |

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| trivia | |

#### Returns

TNode


## PrependToLeadingTrivia\<TNode>\(TNode, SyntaxTrivia\)

### Summary

Creates a new node from this node with the leading trivia replaced with a new trivia where the specified trivia is inserted at the begining of the leading trivia\.

```csharp
public static TNode PrependToLeadingTrivia<TNode>(this TNode node, SyntaxTrivia trivia) where TNode : Microsoft.CodeAnalysis.SyntaxNode
```

#### Type Parameters

| Name | Summary |
| ---- | ------- |
| TNode | |

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| trivia | |

#### Returns

TNode


## PrependToLeadingTrivia\(SyntaxToken, IEnumerable\<SyntaxTrivia>\)

### Summary

Creates a new token from this token with the leading trivia replaced with a new trivia where the specified trivia is inserted at the begining of the leading trivia\.

```csharp
public static SyntaxToken PrependToLeadingTrivia(this SyntaxToken token, IEnumerable<SyntaxTrivia> trivia)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| token | |
| trivia | |

#### Returns

[SyntaxToken](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtoken)


## PrependToLeadingTrivia\(SyntaxToken, SyntaxTrivia\)

### Summary

Creates a new token from this token with the leading trivia replaced with a new trivia where the specified trivia is inserted at the begining of the leading trivia\.

```csharp
public static SyntaxToken PrependToLeadingTrivia(this SyntaxToken token, SyntaxTrivia trivia)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| token | |
| trivia | |

#### Returns

[SyntaxToken](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtoken)


