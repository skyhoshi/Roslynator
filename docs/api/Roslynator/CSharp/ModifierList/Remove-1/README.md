# ModifierList\.Remove Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| Remove\<TNode>\(TNode, SyntaxKind\) | Creates a new node with a modifier of the specified kind removed\. |
| Remove\<TNode>\(TNode, SyntaxToken\) | Creates a new node with the specified modifier removed\. |

## Remove\<TNode>\(TNode, SyntaxKind\)

### Summary

Creates a new node with a modifier of the specified kind removed\.

```csharp
public static TNode Remove<TNode>(TNode node, SyntaxKind kind) where TNode : SyntaxNode
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| node | |
| kind | |

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Returns

[TNode](../TNode/README.md)




## Remove\<TNode>\(TNode, SyntaxToken\)

### Summary

Creates a new node with the specified modifier removed\.

```csharp
public static TNode Remove<TNode>(TNode node, SyntaxToken modifier) where TNode : SyntaxNode
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| node | |
| modifier | |

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Returns

[TNode](../TNode/README.md)




