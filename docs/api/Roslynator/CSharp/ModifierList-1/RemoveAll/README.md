# ModifierList\.RemoveAll Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| RemoveAll\(TNode\) | Creates a new node with all modifiers removed\. |
| RemoveAll\(TNode, Func\<SyntaxToken, Boolean>\) | Creates a new node with modifiers that matches the predicate removed\. |

## RemoveAll\(TNode\)

### Summary

Creates a new node with all modifiers removed\.

```csharp
public TNode RemoveAll(TNode node)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| node | |

#### Returns

TNode


## RemoveAll\(TNode, Func\<SyntaxToken, Boolean>\)

### Summary

Creates a new node with modifiers that matches the predicate removed\.

```csharp
public TNode RemoveAll(TNode node, Func<SyntaxToken, bool> predicate)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| node | |
| predicate | |

#### Returns

TNode


