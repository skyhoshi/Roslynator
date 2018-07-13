# SyntaxExtensions\.Any Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| Any\(SyntaxTokenList, Func\<SyntaxToken, Boolean>\) | Returns true if any token in a [SyntaxTokenList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtokenlist) matches the predicate\. |
| Any\(SyntaxTriviaList, Func\<SyntaxTrivia, Boolean>\) | Returns true if any trivia in a [SyntaxTriviaList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtrivialist) matches the predicate\. |
| Any\<TNode>\(SeparatedSyntaxList\<TNode>, Func\<TNode, Boolean>\) | Returns true if any node in a list matches the predicate\. |
| Any\<TNode>\(SyntaxList\<TNode>, Func\<TNode, Boolean>\) | Returns true if any node in a list matches the predicate\. |

## Any\<TNode>\(SeparatedSyntaxList\<TNode>, Func\<TNode, Boolean>\)

### Summary

Returns true if any node in a list matches the predicate\.

```csharp
public static bool Any<TNode>(this SeparatedSyntaxList<TNode> list, Func<TNode, bool> predicate) where TNode : SyntaxNode
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| list | |
| predicate | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)


## Any\<TNode>\(SyntaxList\<TNode>, Func\<TNode, Boolean>\)

### Summary

Returns true if any node in a list matches the predicate\.

```csharp
public static bool Any<TNode>(this SyntaxList<TNode> list, Func<TNode, bool> predicate) where TNode : SyntaxNode
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| list | |
| predicate | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)


## Any\(SyntaxTokenList, Func\<SyntaxToken, Boolean>\)

### Summary

Returns true if any token in a [SyntaxTokenList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtokenlist) matches the predicate\.

```csharp
public static bool Any(this SyntaxTokenList list, Func<SyntaxToken, bool> predicate)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| list | |
| predicate | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)


## Any\(SyntaxTriviaList, Func\<SyntaxTrivia, Boolean>\)

### Summary

Returns true if any trivia in a [SyntaxTriviaList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtrivialist) matches the predicate\.

```csharp
public static bool Any(this SyntaxTriviaList list, Func<SyntaxTrivia, bool> predicate)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| list | |
| predicate | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)


