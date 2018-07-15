# ModifierList\.RemoveAll Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| RemoveAll\<TNode>\(TNode\) | Creates a new node with all modifiers removed\. |
| RemoveAll\<TNode>\(TNode, Func\<SyntaxToken, Boolean>\) | Creates a new node with modifiers that matches the predicate removed\. |

## RemoveAll\<TNode>\(TNode, Func\<SyntaxToken, Boolean>\)

### Summary

Creates a new node with modifiers that matches the predicate removed\.

```csharp
public static TNode RemoveAll<TNode>(TNode node, Func<SyntaxToken, bool> predicate) where TNode : Microsoft.CodeAnalysis.SyntaxNode
```

#### Type Parameters

| Name | Summary |
| ---- | ------- |
| TNode | |

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| predicate | |

#### Returns

TNode

## RemoveAll\<TNode>\(TNode\)

### Summary

Creates a new node with all modifiers removed\.

```csharp
public static TNode RemoveAll<TNode>(TNode node) where TNode : Microsoft.CodeAnalysis.SyntaxNode
```

#### Type Parameters

| Name | Summary |
| ---- | ------- |
| TNode | |

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |

#### Returns

TNode

