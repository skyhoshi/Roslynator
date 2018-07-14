# SyntaxExtensions\.ReplaceAt Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| ReplaceAt\(SyntaxTokenList, Int32, SyntaxToken\) | Creates a new [SyntaxTokenList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtokenlist) with a token at the specified index replaced with a new token\. |
| ReplaceAt\(SyntaxTriviaList, Int32, SyntaxTrivia\) | Creates a new [SyntaxTriviaList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtrivialist) with a trivia at the specified index replaced with new trivia\. |
| ReplaceAt\<TNode>\(SeparatedSyntaxList\<TNode>, Int32, TNode\) | Creates a new list with a node at the specified index replaced with a new node\. |
| ReplaceAt\<TNode>\(SyntaxList\<TNode>, Int32, TNode\) | Creates a new list with the node at the specified index replaced with a new node\. |

## ReplaceAt\<TNode>\(SeparatedSyntaxList\<TNode>, Int32, TNode\)

### Summary

Creates a new list with a node at the specified index replaced with a new node\.

```csharp
public static SeparatedSyntaxList<TNode> ReplaceAt<TNode>(this SeparatedSyntaxList<TNode> list, int index, TNode newNode) where TNode : Microsoft.CodeAnalysis.SyntaxNode
```

#### Type Parameters

| Name | Summary |
| ---- | ------- |
| TNode | |

#### Parameters

| Name | Summary |
| ---- | ------- |
| list | |
| index | |
| newNode | |

#### Returns

[SeparatedSyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.separatedsyntaxlist-1)


## ReplaceAt\<TNode>\(SyntaxList\<TNode>, Int32, TNode\)

### Summary

Creates a new list with the node at the specified index replaced with a new node\.

```csharp
public static SyntaxList<TNode> ReplaceAt<TNode>(this SyntaxList<TNode> list, int index, TNode newNode) where TNode : Microsoft.CodeAnalysis.SyntaxNode
```

#### Type Parameters

| Name | Summary |
| ---- | ------- |
| TNode | |

#### Parameters

| Name | Summary |
| ---- | ------- |
| list | |
| index | |
| newNode | |

#### Returns

[SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)


## ReplaceAt\(SyntaxTokenList, Int32, SyntaxToken\)

### Summary

Creates a new [SyntaxTokenList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtokenlist) with a token at the specified index replaced with a new token\.

```csharp
public static SyntaxTokenList ReplaceAt(this SyntaxTokenList tokenList, int index, SyntaxToken newToken)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| tokenList | |
| index | |
| newToken | |

#### Returns

[SyntaxTokenList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtokenlist)


## ReplaceAt\(SyntaxTriviaList, Int32, SyntaxTrivia\)

### Summary

Creates a new [SyntaxTriviaList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtrivialist) with a trivia at the specified index replaced with new trivia\.

```csharp
public static SyntaxTriviaList ReplaceAt(this SyntaxTriviaList triviaList, int index, SyntaxTrivia newTrivia)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| triviaList | |
| index | |
| newTrivia | |

#### Returns

[SyntaxTriviaList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtrivialist)


