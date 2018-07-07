# SyntaxExtensions\.ReplaceRange Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| ReplaceRange\(SyntaxTokenList, Int32, Int32, IEnumerable\<SyntaxToken>\) | Creates a new list with the tokens in the specified range replaced with new tokens\. |
| ReplaceRange\(SyntaxTriviaList, Int32, Int32, IEnumerable\<SyntaxTrivia>\) | Creates a new list with the trivia in the specified range replaced with new trivia\. |
| ReplaceRange\<TNode>\(SeparatedSyntaxList\<TNode>, Int32, Int32, IEnumerable\<TNode>\) | Creates a new list with the elements in the specified range replaced with new nodes\. |
| ReplaceRange\<TNode>\(SyntaxList\<TNode>, Int32, Int32, IEnumerable\<TNode>\) | Creates a new list with the elements in the specified range replaced with new nodes\. |

## ReplaceRange\<TNode>\(SeparatedSyntaxList\<TNode>, Int32, Int32, IEnumerable\<TNode>\)

### Summary

Creates a new list with the elements in the specified range replaced with new nodes\.

```csharp
public static SeparatedSyntaxList<TNode> ReplaceRange<TNode>(this SeparatedSyntaxList<TNode> list, int index, int count, IEnumerable<TNode> newNodes) where TNode : SyntaxNode
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| list | |
| index | |
| count | |
| newNodes | |

#### Returns

[SeparatedSyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.separatedsyntaxlist-1)




## ReplaceRange\<TNode>\(SyntaxList\<TNode>, Int32, Int32, IEnumerable\<TNode>\)

### Summary

Creates a new list with the elements in the specified range replaced with new nodes\.

```csharp
public static SyntaxList<TNode> ReplaceRange<TNode>(this SyntaxList<TNode> list, int index, int count, IEnumerable<TNode> newNodes) where TNode : SyntaxNode
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| list | |
| index | |
| count | |
| newNodes | |

#### Returns

[SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)




## ReplaceRange\(SyntaxTokenList, Int32, Int32, IEnumerable\<SyntaxToken>\)

### Summary

Creates a new list with the tokens in the specified range replaced with new tokens\.

```csharp
public static SyntaxTokenList ReplaceRange(this SyntaxTokenList list, int index, int count, IEnumerable<SyntaxToken> newTokens)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| list | |
| index | |
| count | |
| newTokens | |

#### Returns

[SyntaxTokenList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtokenlist)




## ReplaceRange\(SyntaxTriviaList, Int32, Int32, IEnumerable\<SyntaxTrivia>\)

### Summary

Creates a new list with the trivia in the specified range replaced with new trivia\.

```csharp
public static SyntaxTriviaList ReplaceRange(this SyntaxTriviaList list, int index, int count, IEnumerable<SyntaxTrivia> newTrivia)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| list | |
| index | |
| count | |
| newTrivia | |

#### Returns

[SyntaxTriviaList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtrivialist)




