# SyntaxExtensions\.WithTriviaFrom Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| WithTriviaFrom\(SyntaxToken, SyntaxNode\) | Creates a new token from this token with both the leading and trailing trivia of the specified node\. |
| WithTriviaFrom\<TNode>\(SeparatedSyntaxList\<TNode>, SyntaxNode\) | Creates a new separated list with both leading and trailing trivia of the specified node\. If the list contains more than one item, first item is updated with leading trivia and last item is updated with trailing trivia\. |
| WithTriviaFrom\<TNode>\(SyntaxList\<TNode>, SyntaxNode\) | Creates a new list with both leading and trailing trivia of the specified node\. If the list contains more than one item, first item is updated with leading trivia and last item is updated with trailing trivia\. |
| WithTriviaFrom\<TNode>\(TNode, SyntaxToken\) | Creates a new node from this node with both the leading and trailing trivia of the specified token\. |

## WithTriviaFrom\<TNode>\(SeparatedSyntaxList\<TNode>, SyntaxNode\)

### Summary

Creates a new separated list with both leading and trailing trivia of the specified node\.
If the list contains more than one item, first item is updated with leading trivia and last item is updated with trailing trivia\.

```csharp
public static SeparatedSyntaxList<TNode> WithTriviaFrom<TNode>(this SeparatedSyntaxList<TNode> list, SyntaxNode node) 
    where TNode : SyntaxNode
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| list | |
| node | |

#### Returns

[SeparatedSyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.separatedsyntaxlist-1)




## WithTriviaFrom\<TNode>\(SyntaxList\<TNode>, SyntaxNode\)

### Summary

Creates a new list with both leading and trailing trivia of the specified node\.
If the list contains more than one item, first item is updated with leading trivia and last item is updated with trailing trivia\.

```csharp
public static SyntaxList<TNode> WithTriviaFrom<TNode>(this SyntaxList<TNode> list, SyntaxNode node) 
    where TNode : SyntaxNode
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| list | |
| node | |

#### Returns

[SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)




## WithTriviaFrom\<TNode>\(TNode, SyntaxToken\)

### Summary

Creates a new node from this node with both the leading and trailing trivia of the specified token\.

```csharp
public static TNode WithTriviaFrom<TNode>(this TNode node, SyntaxToken token) 
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
| token | |

#### Returns

TNode




## WithTriviaFrom\(SyntaxToken, SyntaxNode\)

### Summary

Creates a new token from this token with both the leading and trailing trivia of the specified node\.

```csharp
public static SyntaxToken WithTriviaFrom(this SyntaxToken token, SyntaxNode node)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| token | |
| node | |

#### Returns

[SyntaxToken](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtoken)




