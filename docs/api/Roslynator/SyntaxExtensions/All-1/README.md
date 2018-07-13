# SyntaxExtensions\.All Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| All\(SyntaxTokenList, Func\<SyntaxToken, Boolean>\) | Returns true if all tokens in a [SyntaxTokenList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtokenlist) matches the predicate\. |
| All\(SyntaxTriviaList, Func\<SyntaxTrivia, Boolean>\) | Returns true if all trivia in a [SyntaxTriviaList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtrivialist) matches the predicate\. |
| All\<TNode>\(SeparatedSyntaxList\<TNode>, Func\<TNode, Boolean>\) | Returns true if all nodes in a list matches the predicate\. |
| All\<TNode>\(SyntaxList\<TNode>, Func\<TNode, Boolean>\) | Returns true if all nodes in a list matches the predicate\. |

## All\<TNode>\(SeparatedSyntaxList\<TNode>, Func\<TNode, Boolean>\)

### Summary

Returns true if all nodes in a list matches the predicate\.

```csharp
public static bool All<TNode>(this SeparatedSyntaxList<TNode> list, Func<TNode, bool> predicate) where TNode : SyntaxNode
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


## All\<TNode>\(SyntaxList\<TNode>, Func\<TNode, Boolean>\)

### Summary

Returns true if all nodes in a list matches the predicate\.

```csharp
public static bool All<TNode>(this SyntaxList<TNode> list, Func<TNode, bool> predicate) where TNode : SyntaxNode
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


## All\(SyntaxTokenList, Func\<SyntaxToken, Boolean>\)

### Summary

Returns true if all tokens in a [SyntaxTokenList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtokenlist) matches the predicate\.

```csharp
public static bool All(this SyntaxTokenList list, Func<SyntaxToken, bool> predicate)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| list | |
| predicate | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)


## All\(SyntaxTriviaList, Func\<SyntaxTrivia, Boolean>\)

### Summary

Returns true if all trivia in a [SyntaxTriviaList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtrivialist) matches the predicate\.

```csharp
public static bool All(this SyntaxTriviaList list, Func<SyntaxTrivia, bool> predicate)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| list | |
| predicate | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)


