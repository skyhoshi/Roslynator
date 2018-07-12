# SyntaxExtensions\.AppendToLeadingTrivia Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| AppendToLeadingTrivia\(SyntaxToken, IEnumerable\<SyntaxTrivia>\) | Creates a new token from this token with the leading trivia replaced with a new trivia where the specified trivia is added at the end of the leading trivia\. |
| AppendToLeadingTrivia\(SyntaxToken, SyntaxTrivia\) | Creates a new token from this token with the leading trivia replaced with a new trivia where the specified trivia is added at the end of the leading trivia\. |
| AppendToLeadingTrivia\<TNode>\(TNode, IEnumerable\<SyntaxTrivia>\) | Creates a new node from this node with the leading trivia replaced with a new trivia where the specified trivia is added at the end of the leading trivia\. |
| AppendToLeadingTrivia\<TNode>\(TNode, SyntaxTrivia\) | Creates a new node from this node with the leading trivia replaced with a new trivia where the specified trivia is added at the end of the leading trivia\. |

## AppendToLeadingTrivia\<TNode>\(TNode, IEnumerable\<SyntaxTrivia>\)

### Summary

Creates a new node from this node with the leading trivia replaced with a new trivia where the specified trivia is added at the end of the leading trivia\.

```csharp
public static TNode AppendToLeadingTrivia<TNode>(this TNode node, IEnumerable<SyntaxTrivia> trivia) 
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




## AppendToLeadingTrivia\<TNode>\(TNode, SyntaxTrivia\)

### Summary

Creates a new node from this node with the leading trivia replaced with a new trivia where the specified trivia is added at the end of the leading trivia\.

```csharp
public static TNode AppendToLeadingTrivia<TNode>(this TNode node, SyntaxTrivia trivia) 
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




## AppendToLeadingTrivia\(SyntaxToken, IEnumerable\<SyntaxTrivia>\)

### Summary

Creates a new token from this token with the leading trivia replaced with a new trivia where the specified trivia is added at the end of the leading trivia\.

```csharp
public static SyntaxToken AppendToLeadingTrivia(this SyntaxToken token, IEnumerable<SyntaxTrivia> trivia)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| token | |
| trivia | |

#### Returns

[SyntaxToken](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtoken)




## AppendToLeadingTrivia\(SyntaxToken, SyntaxTrivia\)

### Summary

Creates a new token from this token with the leading trivia replaced with a new trivia where the specified trivia is added at the end of the leading trivia\.

```csharp
public static SyntaxToken AppendToLeadingTrivia(this SyntaxToken token, SyntaxTrivia trivia)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| token | |
| trivia | |

#### Returns

[SyntaxToken](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtoken)




