# ModifierList\.Insert Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| Insert\(SyntaxTokenList, SyntaxKind, IComparer\<SyntaxKind>\) | Creates a new list of modifiers with the modifier of the specified kind inserted\. |
| Insert\(SyntaxTokenList, SyntaxToken, IComparer\<SyntaxToken>\) | Creates a new list of modifiers with a specified modifier inserted\. |
| Insert\<TNode>\(TNode, SyntaxKind, IComparer\<SyntaxKind>\) | Creates a new node with a modifier of the specified kind inserted\. |
| Insert\<TNode>\(TNode, SyntaxToken, IComparer\<SyntaxToken>\) | Creates a new node with the specified modifier inserted\. |

## Insert\<TNode>\(TNode, SyntaxKind, IComparer\<SyntaxKind>\)

### Summary

Creates a new node with a modifier of the specified kind inserted\.

```csharp
public static TNode Insert<TNode>(TNode node, SyntaxKind kind, IComparer<SyntaxKind> comparer = null) where TNode : SyntaxNode
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| node | |
| kind | |
| comparer | |

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Returns

[TNode](../TNode/README.md)




## Insert\<TNode>\(TNode, SyntaxToken, IComparer\<SyntaxToken>\)

### Summary

Creates a new node with the specified modifier inserted\.

```csharp
public static TNode Insert<TNode>(TNode node, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null) where TNode : SyntaxNode
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| node | |
| modifier | |
| comparer | |

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Returns

[TNode](../TNode/README.md)




## Insert\(SyntaxTokenList, SyntaxKind, IComparer\<SyntaxKind>\)

### Summary

Creates a new list of modifiers with the modifier of the specified kind inserted\.

```csharp
public static SyntaxTokenList Insert(SyntaxTokenList modifiers, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| modifiers | |
| kind | |
| comparer | |

#### Returns

[SyntaxTokenList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtokenlist)




## Insert\(SyntaxTokenList, SyntaxToken, IComparer\<SyntaxToken>\)

### Summary

Creates a new list of modifiers with a specified modifier inserted\.

```csharp
public static SyntaxTokenList Insert(SyntaxTokenList modifiers, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| modifiers | |
| modifier | |
| comparer | |

#### Returns

[SyntaxTokenList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtokenlist)




